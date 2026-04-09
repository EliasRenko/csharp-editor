# Refactor Plan

> **Date:** April 9, 2026  
> **Scope:** Full codebase review of `csharp-editor` — a WinForms-based tile/entity map editor that wraps a native Haxe runtime via P/Invoke.

---

## Current State Summary

| File / Area | Lines | Problem |
|---|---|---|
| `Editor.cs` | ~1 018 | God class — owns UI wiring, I/O, viewport, dialogs, events |
| `CExternsEditor.cs` | ~432 | Global namespace; mixes raw P/Invoke structs with wrapper logic |
| `HierarchyTree.cs` | ~960 | UserControl directly calls the backend; inner model class |
| `Models/` | — | `struct` types with mutable public fields; inconsistent naming |
| `Program.cs` | — | Manual `DoEvents` game-loop; high CPU spin |
| `Dialogs/` | — | Two dialogs built inline inside `Editor.cs` in code |

---

## Phase 1 — Quick Wins (Low Risk, No Architecture Change)

These changes are safe, self-contained, and can be done in isolation.

### 1.1 — Put `CExternsEditor` in the correct namespace

**Problem:** `CExternsEditor.cs` declares the class at the global namespace (no `namespace` block), while everything else lives in `csharp_editor`.

**Fix:** Wrap the entire class in `namespace csharp_editor { }`.  
All existing call sites (`CExternsEditor.X(...)`) already compile from within `csharp_editor`, so no `using` directives change.

---

### 1.2 — Promote inline dialogs to real Dialog classes

**Problem:** `Editor.cs` builds two fully functional `Form` objects in code (`ShowProjectLoadConflictDialog` and `ShowTilesetSelectionDialog`), bypassing the Designer and making them hard to style or extend.

**Fix:**
- Create `Dialogs/ProjectLoadConflictDialog.cs` + `.Designer.cs`
- Create `Dialogs/TilesetSelectionDialog.cs` + `.Designer.cs`
- Replace the inline Form construction in `Editor.cs` with `new ProjectLoadConflictDialog(...)` and `new TilesetSelectionDialog(...)`.

---

### 1.3 — Rename Models for clarity

**Problem:** Model names are inconsistent. `MapInfoStruct` / `ProjectInfoStruct` sound like raw interop types (they are managed C# classes). `LayerInfoStruct` lives inside `CExternsEditor` as an actual unmanaged struct — the naming collision creates confusion.

**Fix:**

| Old name | New name | Rationale |
|---|---|---|
| `Models/MapInfoStruct` | `Models/MapInfo` | Managed DTO — no "Struct" suffix needed |
| `Models/ProjectInfoStruct` | `Models/ProjectInfo` | Same |
| `CExternsEditor.MapProps` | stays | True unmanaged struct; keep name |
| `CExternsEditor.ProjectProps` | stays | Same |

Update all references in `CExternsEditor.cs`, `Editor.cs`, and any dialog that uses them.

---

### 1.4 — Move `HierarchyTree.LayerNode` to `Models`

**Problem:** `LayerNode` is a public data class but it lives as an inner class of `HierarchyTree`. Other parts of the editor already receive `LayerNode` through events (`LayerSelected`, etc.), so it effectively belongs to the shared model layer.

**Fix:** Move `LayerNode` to `Models/LayerNode.cs` inside the `csharp_editor.Models` namespace. Add a `using csharp_editor.Models;` in `HierarchyTree.cs`.

---

### 1.5 — Fix `SetToolType` type mismatch

**Problem:** `CExternsEditor.SetToolType` is declared as `extern void SetToolType(int toolType)` but is called with a `ToolType` enum value.

**Fix:** Change the P/Invoke signature to accept `ToolType` directly:
```csharp
[DllImport(CExterns.DLL, EntryPoint = "setToolType")]
public static extern void SetToolType(ToolType toolType);
```
Remove the explicit cast at the call site in `Editor.cs`.

---

## Phase 2 — Extract from `Editor.cs` (Medium Risk)

`Editor.cs` is a textbook God Class. The goal here is to pull out cohesive clusters of responsibility into dedicated classes, leaving `Editor` as a thin coordinator.

### 2.1 — Extract `MapSessionManager`

**Responsibility:** Managing the list of open maps, opening, closing, and switching tabs.

**Moves out of `Editor.cs`:**
- `_openMaps` list
- `LoadMap(string path)`
- `ToolStripButton_newMap_Click`
- `DockPanel_ActiveDocumentChanged`
- `MapDoc_FormClosing`
- `CloseAllTabs(bool saveFirst)`
- `AttachViewportToContent` / `RescueViewport` (viewport lifecycle tied to tab lifecycle)

**New file:** `Services/MapSessionManager.cs`

```csharp
// Sketch — not final
public class MapSessionManager {
    public IReadOnlyList<MapDocContent> OpenMaps { get; }
    public void LoadMap(string path) { … }
    public void NewMap() { … }
    public void CloseAll(bool saveFirst) { … }
    public event EventHandler<MapDocContent>? ActiveMapChanged;
}
```

`Editor.cs` keeps a `MapSessionManager _session` field and delegates to it.

---

### 2.2 — Extract `ProjectManager`

**Responsibility:** Project-level save/load/edit and status display.

**Moves out of `Editor.cs`:**
- `SaveProject_Click`
- `SaveAsProject_Click`
- `EditProject_Click`
- `AutoSaveProject`
- `UpdateProjectStatus`
- `WelcomePanel_NewProjectRequested`
- `WelcomePanel_OpenProjectRequested`
- `ShowProjectLoadConflictDialog` (after Phase 1.2 promotes it to a real dialog)

**New file:** `Services/ProjectManager.cs`

---

### 2.3 — Extract `ViewportController`

**Responsibility:** Physical reparenting of the shared `ExternView` control and tool buttons between `MapDocContent` tabs.

**Moves out of `Editor.cs`:**
- `AttachViewportToContent`
- `RescueViewport`
- Button location constants

**New file:** `Services/ViewportController.cs`

---

### 2.4 — Use partial classes to split `Editor.cs` by concern (interim step)

Before Phase 2.1–2.3 are complete, using C# `partial class` lets us break the single 1 018-line file into focused files without changing any logic.

Suggested split:

| File | Contents |
|---|---|
| `Editor.cs` | Constructor, dock panel setup, lifecycle |
| `Editor.Maps.cs` | Map open/close/tab events |
| `Editor.Project.cs` | Project save/load/edit |
| `Editor.Layers.cs` | `HierarchyTree_*` event handlers |
| `Editor.Entities.cs` | `EntitySelector_*`, `ExternView_EntitySelectionChanged` |
| `Editor.Viewport.cs` | `AttachViewportToContent`, `RescueViewport` |
| `Editor.Input.cs` | `Editor_KeyDown`, `Editor_KeyUp`, mouse handlers |

This is a pure file reorganisation — zero logic change — and makes Phase 2.1–2.3 safer to execute.

---

## Phase 3 — Decouple UserControls from the Backend (Higher Risk)

### 3.1 — Introduce a backend abstraction layer

**Problem:** `HierarchyTree.cs` (a UserControl) directly calls `CExternsEditor` static methods. This creates an invisible dependency between the UI layer and the native interop layer, making the tree hard to test or reuse.

**Fix:** Define an interface `IEditorBackend` (or split into `ILayerBackend`, `IEntityBackend`, etc.) that `HierarchyTree` accepts via constructor injection. `Editor` passes a thin adapter that delegates to `CExternsEditor`.

```csharp
public interface ILayerBackend {
    int GetLayerCount();
    bool GetLayerInfoAt(int index, out LayerInfoStruct info);
    bool CreateTilemapLayer(string name, string tileset, int tileSize, int index);
    bool RemoveLayer(string name);
    // …
}
```

`HierarchyTree` changes from:
```csharp
CExternsEditor.CreateTilemapLayer(name, tilesetName, tileSize, insertIndex);
```
to:
```csharp
_backend.CreateTilemapLayer(name, tilesetName, tileSize, insertIndex);
```

---

### 3.2 — Split `CExternsEditor.cs` into domain-specific classes

**Problem:** `CExternsEditor` is a single 432-line static class covering Project, Map, Textures, Tilesets, Entity Definitions, Entity Instances, Layers, Batches, and View. Every consumer imports the entire surface.

**Fix:** Split into focused static (or singleton) classes:

| New class | Covers |
|---|---|
| `Interop/ProjectInterop.cs` | `ExportProject`, `ImportProject`, `GetProjectProps`, `EditProject` |
| `Interop/MapInterop.cs` | `ExportMap`, `ImportMap`, `GetMapProps`, `SetMapProps` |
| `Interop/TilesetInterop.cs` | `CreateTileset`, `DeleteTileset`, `GetTileset*`, `SetActiveTileset`, tiles |
| `Interop/LayerInterop.cs` | All `*Layer*` and `SetToolType` methods |
| `Interop/EntityInterop.cs` | Entity definitions and instance management |
| `Interop/TextureInterop.cs` | `GetTextureData` |

Keep all unmanaged `struct` definitions in a shared `Interop/NativeStructs.cs`.

---

## Phase 4 — Architecture Improvements (Lower Priority)

### 4.1 — Replace the manual `DoEvents` game loop

**Problem:** `Program.cs` spins a `while (editor.Active)` loop calling `Application.DoEvents()` to keep the message pump alive alongside the render loop. This pegs one CPU core and is fragile.

**Options (in order of preference):**
1. **`System.Windows.Forms.Timer`** — fire `UpdateFrame` + `Render` at a fixed interval from within the normal message pump. Simple, safe for a WinForms app.
2. **`Application.Idle` event** — render in the `Idle` handler, which fires whenever the message queue is empty. Better throughput than a timer for games.
3. **Dedicated render thread** — full thread separation, most complex, only warranted if render performance is a bottleneck.

---

### 4.2 — Replace mutable public fields on models with properties

**Problem:** `MapInfoStruct`, `ProjectInfoStruct` use public fields (`public string? idd;`). Fields bypass change notifications and do not appear properly in some reflection-based tools.

**Fix:** Convert all model types to use auto-properties:
```csharp
// Before
public struct MapInfoStruct {
    public string? idd;
}

// After
public class MapInfo {
    public string? Id { get; set; }   // also rename: idd → Id
}
```
Note the naming fix: `idd` → `Id`, `worldx` → `WorldX`, etc. (follow C# conventions).

---

### 4.3 — Centralise error handling

**Problem:** Error handling is duplicated across the codebase:
```csharp
string error = view_extern.GetLastErrorMessage();
MessageBox.Show($"Failed to …:\n{error}", "…", …);
```
This pattern appears at least a dozen times in `Editor.cs` alone.

**Fix:** Create a static `EditorErrors` helper:
```csharp
public static class EditorErrors {
    public static void Show(IWin32Window owner, string action, string detail)
        => MessageBox.Show(owner, $"Failed to {action}:\n{detail}", action, …);
}
```
And a single `GetNativeError()` extension or helper method on `ExternView`.

---

## Suggested Execution Order

```
Phase 1.1  Namespace fix for CExternsEditor        (30 min)
Phase 1.5  SetToolType enum fix                    (10 min)
Phase 1.4  Move LayerNode to Models                (30 min)
Phase 1.3  Rename model types                      (1–2 h, search/replace)
Phase 1.2  Promote inline dialogs                  (2 h)
Phase 2.4  Split Editor.cs into partial files      (1 h, no logic change)
Phase 2.3  Extract ViewportController              (2 h)
Phase 2.1  Extract MapSessionManager               (3–4 h)
Phase 2.2  Extract ProjectManager                  (2–3 h)
Phase 4.2  Properties on models                    (1–2 h)
Phase 4.3  Centralise error handling               (1–2 h)
Phase 3.2  Split CExternsEditor into Interop/*     (3–4 h)
Phase 3.1  Backend abstraction interface           (4–6 h)
Phase 4.1  Replace DoEvents game loop              (2–4 h)
```

---

## Non-Goals

- No change to the native Haxe runtime or DLL interface.
- No change to the WinForms DockPanel Suite integration pattern.
- Feature additions are out of scope for this refactor.
