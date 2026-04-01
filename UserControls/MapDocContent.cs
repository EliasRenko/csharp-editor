using WeifenLuo.WinFormsUI.Docking;

namespace csharp_editor.UserControls;

/// <summary>
/// A document-area tab that represents one open map / editor state.
/// The shared ExternView and tool buttons are physically reparented into
/// the currently active instance so the native SDL render window always
/// has the correct parent HWND.
/// </summary>
public sealed class MapDocContent : DockContent
{
    public int    StateId  { get; }
    public string FilePath { get; private set; }

    public MapDocContent(int stateId, string label, string filePath)
    {
        StateId  = stateId;
        FilePath = filePath;
        TabText  = label;
        Text     = label;

        HideOnClose        = false;          // real close, not hide
        CloseButtonVisible = true;
        DockAreas          = DockAreas.Document; // document-only → no float, no drag-out
    }

    /// <summary>Updates the stored file path and renames the tab.</summary>
    public void UpdateFilePath(string newPath)
    {
        FilePath = newPath;
        string name = Path.GetFileNameWithoutExtension(newPath);
        TabText = name;
        Text    = name;
    }
}
