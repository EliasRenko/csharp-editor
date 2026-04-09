using System.Runtime.InteropServices;
using csharp_editor.UserControls;
using csharp_editor.Models;
using csharp_editor.Dialogs;
using csharp_editor.Helpers;
using NativeHaxeRuntime;
using WeifenLuo.WinFormsUI.Docking;
using ToolStripRenderer = csharp_editor.Styles.ToolStripRenderer;

namespace csharp_editor {
    public partial class Editor : Runtime {
        
        private CExternsEditor.EntitySelectionChangedCallback? _entitySelectionChangedCallback;
        public event EventHandler? EntitySelectionChanged;
        
        private string _currentTilesetName = "";
        private string _currentEntityName = "";
        private bool _isEntityLayerActive = false;
        
        //private ExternError lastError;
        private WelcomePanel _welcomePanel = null!;
        private DebugConsoleDockContent _consoleDock = null!;
        private PropertyGridDockContent _propertyGridDock = null!;
        private HierarchyTreeDockContent _hierarchyDock = null!;
        private ViewportDockContent _viewportDock = null!;  // off-screen holder for ExternView
        private WelcomeDockContent _welcomeDock = null!;
        private readonly List<MapDocContent> _openMaps = new();

        public Editor() {
            InitializeComponent();

            // DockPanel Suite requires a theme before any DockContent is shown
            dockPanel.Theme = new CustomDockTheme();
            dockPanel.DockLeftPortion = 256;
            dockPanel.DockRightPortion = 256;
            _propertyGridDock = new PropertyGridDockContent();
            _propertyGridDock.Show(dockPanel, DockState.DockLeft);
            _hierarchyDock = new HierarchyTreeDockContent(hierarchyTree, textureViewer, entitySelector);
            _hierarchyDock.Show(dockPanel, DockState.DockRight);
            _consoleDock = new DebugConsoleDockContent();
            _consoleDock.Show(dockPanel, DockState.DockBottom);
            // ViewportDockContent is an off-screen holder; CreateControl gives ExternView a valid HWND.
            _viewportDock = new ViewportDockContent(view_extern);
            _viewportDock.Controls.Add(button_brush);
            _viewportDock.Controls.Add(button_entity);
            _viewportDock.Controls.Add(button_cursor);
            _viewportDock.CreateControl();

            active = true;
            KeyPreview = true;
            
            _entitySelectionChangedCallback = () => {
                // Marshal back to the UI thread
                BeginInvoke(() => EntitySelectionChanged?.Invoke(this, EventArgs.Empty));
            };

            // Init externs
            view_extern.Init(logHandler);
            
            toolStrip1.Renderer = new ToolStripRenderer();
            
            // Toolstrip Events
            toolStripMenuItem_open.MouseUp += toolStripButton_openFile;
            toolStripMenuItem_export.MouseUp += toolStripButton_export;
            saveProjectToolStripMenuItem.Click += SaveProject_Click;
            saveAsProjectToolStripMenuItem.Click += SaveAsProject_Click;
            editToolStripMenuItem_editProject.Click += EditProject_Click;
            toolStripButton_newMap.MouseDown += ToolStripButton_newMap_Click;

            // Initialize HierarchyTree
            hierarchyTree.SetExternView(view_extern);
            hierarchyTree.LayerSelected += HierarchyTree_LayerSelected;
            hierarchyTree.StateSelected += HierarchyTree_StateSelected;
            hierarchyTree.BatchSelected += HierarchyTree_BatchSelected;
            hierarchyTree.LayersChanged += HierarchyTree_LayersChanged;

            // Initialize TextureViewer
            textureViewer.SelectionChanged += TextureViewer_SelectionChanged;

            // Initialize EntitySelector
            entitySelector.SetExternView(view_extern);
            entitySelector.SelectionChanged += EntitySelector_SelectionChanged;

            // Editor Events
            FormClosing += Editor_FormClosing;
            KeyDown += Editor_KeyDown;
            KeyUp += Editor_KeyUp;

            // ExternView Events
            view_extern.MouseDown += view_extern_MouseDown;
            view_extern.MouseUp += view_extern_MouseUp;
            view_extern.MouseWheel += view_extern_MouseWheel;
            EntitySelectionChanged += ExternView_EntitySelectionChanged;

            // Debug 

            ToolStripMenuItem_timeline.MouseDown += ShowTimelineDialog;
            toolStripMenuItem_theme.Click        += EditTheme_Click;
            toolStripButton_tilesets.MouseDown += ShowTilesetDefDialog;
            toolStripButton_entitiesDefs.MouseDown += ShowEntitiesDefDialog;

            // View menu — re-show dock panels
            viewMenuItem_properties.Click += (_, _) => ShowDockPanel(_propertyGridDock, DockState.DockLeft);
            viewMenuItem_hierarchy.Click  += (_, _) => ShowDockPanel(_hierarchyDock,    DockState.DockRight);
            viewMenuItem_console.Click    += (_, _) => ShowDockPanel(_consoleDock,       DockState.DockBottom);



            // Tools

            button_brush.MouseDown += SelectTileDraw;
            //toolStripButton_tileErase.MouseDown += SelectTileErase;
            button_entity.MouseDown += SelectEntityAdd;
            button_cursor.MouseDown += SelectEntitySelect;
            toolStripButton_toggleLabels.MouseDown += ToolStripButton_toggleLabels_Click;
            // Welcome panel
            _welcomePanel = new WelcomePanel();
            _welcomePanel.NewProjectRequested  += WelcomePanel_NewProjectRequested;
            _welcomePanel.OpenProjectRequested += WelcomePanel_OpenProjectRequested;
            _welcomePanel.OpenMapRequested     += WelcomePanel_OpenMapRequested;
            _welcomeDock = new WelcomeDockContent(_welcomePanel);
            _welcomeDock.Show(dockPanel, DockState.Document);
            dockPanel.ActiveDocumentChanged += DockPanel_ActiveDocumentChanged;

            // Apply persisted theme and subscribe to live updates
            AppThemeManager.ThemeUpdated += OnThemeUpdated;
            ThemeApplier.Apply(this, AppThemeManager.Current);
        }

        protected override void Log(string priority, string category, string message) {
            _consoleDock.Log(priority, category, message);
        }

        private void Log(string message) => Log("INFO", "", message);

        private void ShowTimelineDialog(object? sender, MouseEventArgs e) {
            using var dialog = new Dialogs.TimelineDialog();
            dialog.ShowDialog(this);
        }

        private void EditTheme_Click(object? sender, EventArgs e) {
            using var dialog = new Dialogs.ThemeDialog();
            dialog.ShowDialog(this);
        }

        private void OnThemeUpdated(AppTheme theme) {
            ThemeApplier.Apply(this, theme);
        }


        private void SelectTileDraw(object? sender, MouseEventArgs e) {
            CExternsEditor.SetToolType(ToolType.TileDraw);
        }

        private void SelectTileErase(object? sender, MouseEventArgs e) {
            CExternsEditor.SetToolType(ToolType.TileErase);
        }

        private void SelectEntityAdd(object? sender, MouseEventArgs e) {
            CExternsEditor.SetToolType(ToolType.EntityAdd);
        }

        private void SelectEntitySelect(object? sender, MouseEventArgs e) {
            CExternsEditor.SetToolType(ToolType.EntitySelect);
        }

        private void ToolStripButton_toggleLabels_Click(object? sender, MouseEventArgs e) {
            toolStripButton_toggleLabels.Checked = !toolStripButton_toggleLabels.Checked;
            CExternsEditor.ToggleLabels(toolStripButton_toggleLabels.Checked);
        }

        private void ShowEntitiesDefDialog(object? sender, MouseEventArgs e) {
            using (EntitiesDialog dialog = new EntitiesDialog(
                view_extern,
                onEntitySelected: (entityName) => {
                    _currentEntityName = entityName;
                    Log($"Current entity set to: {_currentEntityName}");
                },
                onEntityDefDeleted: () => {
                    hierarchyTree.RefreshAllEntityBatches();
                    entitySelector.LoadInstances();
                })) {
                dialog.ShowDialog(this);
            }
            entitySelector.LoadEntities();
            AutoSaveProject();
        }

        private void AutoSaveProject() {
            if (dockPanel.ActiveDocument is MapDocContent mapDoc && !string.IsNullOrEmpty(mapDoc.FilePath)) {
                string projectName = Path.GetFileNameWithoutExtension(mapDoc.FilePath);
                bool result = CExternsEditor.ExportProject(mapDoc.FilePath, projectName);
                if (result == false)
                    Log($"Warning: AutoSave — ExportProject returned 0 for '{mapDoc.FilePath}'");
                else
                    Log($"Project auto-saved: {mapDoc.FilePath}");
            }
        }

        private void SaveProject_Click(object? sender, EventArgs e) {
            if (dockPanel.ActiveDocument is MapDocContent mapDoc && !string.IsNullOrEmpty(mapDoc.FilePath)) {
                string projectName = Path.GetFileNameWithoutExtension(mapDoc.FilePath);
                bool result = CExternsEditor.ExportProject(mapDoc.FilePath, projectName);
                if (result == false)
                    Log($"Warning: ExportProject returned 0 for '{mapDoc.FilePath}'");
                else
                    Log($"Project saved: {mapDoc.FilePath}");
            } else {
                Log("Save: no project path available for the current tab.");
            }
        }

        private void SaveAsProject_Click(object? sender, EventArgs e) {
            if (dockPanel.ActiveDocument is not MapDocContent mapDoc) return;
            using SaveFileDialog dlg = new SaveFileDialog {
                Title = "Save Project As",
                Filter = "Project files (*.proj)|*.proj|All files (*.*)|*.*",
                FileName = string.IsNullOrEmpty(mapDoc.FilePath)
                    ? ""
                    : Path.GetFileName(mapDoc.FilePath)
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            string newPath = Path.GetFullPath(dlg.FileName);
            string projectName = Path.GetFileNameWithoutExtension(newPath);
            bool result = CExternsEditor.ExportProject(newPath, projectName);
            if (result == false) {
                Log($"Warning: ExportProject returned 0 for '{newPath}'");
            } else {
                mapDoc.UpdateFilePath(newPath);
                Log($"Project saved as: {newPath}");
            }
        }

        private void LoadMap(string path) {
            // Prevent loading the same file twice
            string normalizedPath = Path.GetFullPath(path);
            foreach (var existing in _openMaps) {
                if (string.Equals(existing.FilePath, normalizedPath, StringComparison.OrdinalIgnoreCase)) {
                    existing.Activate();
                    Log($"Map already open: {normalizedPath}");
                    return;
                }
            }

            int stateId = CExternsEditor.ImportMap(path);
            System.Diagnostics.Debug.WriteLine($"[LoadMap] ImportMap('{path}') returned state={stateId}");
            Log($"[DEBUG] ImportMap returned state={stateId}");

            if (stateId < 0) {
                MessageBox.Show($"Failed to import map:\n{GetLastErrorMessage()}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tabLabel = Path.GetFileNameWithoutExtension(path);
            var mapDoc = new MapDocContent(stateId, tabLabel, normalizedPath);
            mapDoc.FormClosing += MapDoc_FormClosing;
            _openMaps.Add(mapDoc);
            mapDoc.Show(dockPanel, DockState.Document);
            // ActiveDocumentChanged fires synchronously and handles: AttachViewport, SetActiveState, hierarchy, entities

            RecentMapsManager.Add(normalizedPath);
            _welcomePanel.RefreshRecentMaps();
            UpdateProjectStatus();
            Log($"Map loaded: {tabLabel} (state {stateId})");
        }

        private void ToolStripButton_newMap_Click(object? sender, MouseEventArgs e) {
            int stateId = CExterns.NewEditorState();
            var mapDoc = new MapDocContent(stateId, $"New Map {stateId}", "");
            mapDoc.FormClosing += MapDoc_FormClosing;
            _openMaps.Add(mapDoc);
            mapDoc.Show(dockPanel, DockState.Document);
            // ActiveDocumentChanged handles: AttachViewport, SetActiveState, hierarchy, entities
            Log($"New state created (id {stateId})");
        }

        // ── DockPanel document-tab management ─────────────────────────────────────

        /// <summary>
        /// Called whenever the active document tab changes.  Attaches the shared
        /// ExternView + tool buttons to the newly active MapDocContent and switches
        /// the native engine to its state.  When Welcome becomes active the recent
        /// lists are refreshed.
        /// </summary>
        private void DockPanel_ActiveDocumentChanged(object? sender, EventArgs e) {
            if (dockPanel.ActiveDocument is MapDocContent mapDoc) {
                if (view_extern.Parent != mapDoc)
                    AttachViewportToContent(mapDoc);
                CExterns.SetActiveState(mapDoc.StateId);
                hierarchyTree.LoadLayersFromBackend();
                entitySelector.LoadEntities();
                entitySelector.LoadInstances();
                Log($"Switched to state {mapDoc.StateId}");
            } else if (dockPanel.ActiveDocument is WelcomeDockContent) {
                _welcomePanel.RefreshRecent();
                _welcomePanel.RefreshRecentMaps();
            }
        }

        /// <summary>
        /// Physically moves view_extern and the three tool buttons from their current
        /// parent into <paramref name="target"/> so the native SDL window always has
        /// the correct parent HWND.
        /// </summary>
        private void AttachViewportToContent(MapDocContent target) {
            view_extern.Parent?.Controls.Remove(view_extern);
            button_brush.Parent?.Controls.Remove(button_brush);
            button_entity.Parent?.Controls.Remove(button_entity);
            button_cursor.Parent?.Controls.Remove(button_cursor);

            button_brush.Location  = new Point(6, 40);
            button_entity.Location = new Point(6, 88);
            button_cursor.Location = new Point(6, 136);
            target.Controls.Add(button_brush);
            target.Controls.Add(button_entity);
            target.Controls.Add(button_cursor);

            view_extern.Dock = DockStyle.Fill;
            target.Controls.Add(view_extern);
        }

        /// <summary>
        /// Rescues view_extern (and buttons) back into the off-screen holder before
        /// a MapDocContent is disposed, so they are never owned by a dead form.
        /// </summary>
        private void RescueViewport() {
            if (view_extern.Parent is MapDocContent) {
                view_extern.Parent.Controls.Remove(view_extern);
                button_brush.Parent?.Controls.Remove(button_brush);
                button_entity.Parent?.Controls.Remove(button_entity);
                button_cursor.Parent?.Controls.Remove(button_cursor);
                _viewportDock.Controls.Add(view_extern);
            }
        }

        /// <summary>Fired when the user closes a map tab.</summary>
        private void MapDoc_FormClosing(object? sender, FormClosingEventArgs e) {
            if (sender is not MapDocContent mapDoc) return;

            // Move ExternView to the safe holder BEFORE the form is disposed
            if (view_extern.Parent == mapDoc)
                RescueViewport();

            CExterns.ReleaseState(mapDoc.StateId);
            _openMaps.Remove(mapDoc);

            // If this was the last map, show Welcome on the next message-pump tick
            // (cannot activate another DockContent while one is in the middle of closing)
            if (_openMaps.Count == 0)
                BeginInvoke(() => {
                    _welcomeDock.Show(dockPanel, DockState.Document);
                    _welcomePanel.RefreshRecent();
                    _welcomePanel.RefreshRecentMaps();
                });

            UpdateProjectStatus();
        }

        private void UpdateProjectStatus() {
            if (CExternsEditor.GetProjectProps(out ProjectInfo info))
                statusLabel_project.Text = $"Project: {info.ProjectName}";
            else
                statusLabel_project.Text = "No project loaded";
        }

        /// <summary>
        /// Shows a dock panel, restoring it to <paramref name="defaultState"/> if it
        /// has been fully closed rather than merely hidden.
        /// </summary>
        private void ShowDockPanel(DockContent panel, DockState defaultState) {
            if (panel.IsDisposed) return;
            if (panel.IsHidden || panel.DockState == DockState.Unknown ||
                panel.DockState == DockState.Hidden)
                panel.Show(dockPanel, defaultState);
            else
                panel.Activate();
        }

        private void EditProject_Click(object? sender, EventArgs e) {
            if (!CExternsEditor.GetProjectProps(out ProjectInfo existingProps)) {
                MessageBox.Show(this,
                    "No project is loaded. Open or create a project first.",
                    "Edit Project",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new Dialogs.ProjectSettingsDialog(existingProps);
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            ProjectInfo updated = dialog.UpdatedProjectInfo;
            bool edited = CExternsEditor.EditProject(updated);
            if (!edited) {
                MessageBox.Show(this,
                    "Failed to apply project settings to engine.",
                    "Edit Project",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            UpdateProjectStatus();
            Log($"Project settings updated: {updated.ProjectName} ({updated.DefaultTileSizeX}x{updated.DefaultTileSizeY})");
        }

        #region Events

        private void Editor_FormClosing(object? sender, FormClosingEventArgs e) {
            active = false;
            Application.DoEvents(); // Process remaining messages
            System.Threading.Thread.Sleep(50); // Give loop time to exit
            view_extern.Release();
        }

        private void Editor_KeyDown(object? sender, KeyEventArgs e) {
            // Toggle console with tilde key (~) or F1
            if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.F1) {
                if (_consoleDock.IsHidden)
                    _consoleDock.Show(dockPanel, DockState.DockBottom);
                else
                    _consoleDock.Hide();
                e.Handled = true;
                return; // Don't pass console toggle to SDL
            }

            // TODO: OPTIMIZE FURTHER
            if (_propertyGridDock.PropertyGrid.ContainsFocus) {

                if (e.KeyCode == Keys.Escape) {
                    view_extern.Focus();
                }

                e.Handled = true;
                return;
            }

            // Convert C# KeyCode to SDL Scancode and pass to SDL
            CExterns.OnKeyboardDown(KeyMapper.ToSDLScancode(e.KeyCode));
        }

        private void Editor_KeyUp(object? sender, KeyEventArgs e) {
            // Toggle console with tilde key (~) or F1
            if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.F1) {
                e.Handled = true;
                return; // Don't pass console toggle to SDL
            }

            // Convert C# KeyCode to SDL Scancode and pass to SDL
            CExterns.OnKeyboardUp(KeyMapper.ToSDLScancode(e.KeyCode));
        }

        #endregion

        private void view_extern_MouseDown(object? sender, MouseEventArgs e) {
            int button = MouseButtonMapper.ToSDLMouseButton(e.Button);
            CExterns.OnMouseButtonDown(e.X, e.Y, button);
        }

        private void view_extern_MouseWheel(object? sender, MouseEventArgs e) {
            CExterns.OnMouseWheel(e.X, e.Y, e.Delta / 120.0f);
        }

        private void view_extern_MouseUp(object? sender, MouseEventArgs e) {
            int button = MouseButtonMapper.ToSDLMouseButton(e.Button);
            CExterns.OnMouseButtonUp(e.X, e.Y, button);

            // clicking in the extern view may have placed an entity – if the active layer
            // is an entity layer, refresh its batch groups and instance list so everything stays current.
            hierarchyTree.RefreshSelectedEntityBatches();
            entitySelector.ReloadInstancesKeepSelection();
        }

        private void ExternView_EntitySelectionChanged(object? sender, EventArgs e) {
            if (!_isEntityLayerActive) return;

            int count = CExternsEditor.GetEntitySelectionCount();

            if (count <= 0) {
                // Only wipe entity-specific info; preserve layer/state info if no entity was shown
                if (_propertyGridDock.PropertyGrid.SelectedObject is EntityInstanceDisplay)
                    _propertyGridDock.PropertyGrid.SelectedObject = null;
                return;
            }

            // Single selection – show details directly
            if (count == 1) {
                CExternsEditor.EntityStruct data = new CExternsEditor.EntityStruct();
                if (!CExternsEditor.GetEntitySelectionInfo(0, out data)) {
                    string error = view_extern.GetLastErrorMessage();
                    MessageBox.Show($"Failed to retrieve selected entity info:\n{error}",
                        "Entity Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var display = new EntityInstanceDisplay {
                    Uid     = Marshal.PtrToStringAnsi(data.uid)    ?? "",
                    DefName = Marshal.PtrToStringAnsi(data.name)   ?? "",
                    X       = data.x,
                    Y       = data.y,
                    Width   = data.width,
                    Height  = data.height
                };
                _propertyGridDock.PropertyGrid.SelectedObject = display;
                Log($"Entity selected: {display.DefName} at ({display.X}, {display.Y})");
                entitySelector.SelectInstanceByUid(display.Uid);
                return;
            }

            // Multi-selection – show an array so all entries are visible
            var items = new List<EntityInstanceDisplay>(count);
            for (int i = 0; i < count; i++) {
                CExternsEditor.EntityStruct data = new CExternsEditor.EntityStruct();
                if (!CExternsEditor.GetEntitySelectionInfo(i, out data)) {
                    string error = view_extern.GetLastErrorMessage();
                    Log($"Failed to retrieve entity selection info at index {i}: {error}");
                    continue;
                }
                items.Add(new EntityInstanceDisplay {
                    Uid     = Marshal.PtrToStringAnsi(data.uid)    ?? "",
                    DefName = Marshal.PtrToStringAnsi(data.name)   ?? "",
                    X       = data.x,
                    Y       = data.y,
                    Width   = data.width,
                    Height  = data.height
                });
            }
            _propertyGridDock.PropertyGrid.SelectedObjects = items.Cast<object>().ToArray();
            Log($"{items.Count} entities selected");
        }

        private void toolStripButton_openFile(object? sender, MouseEventArgs e) {
            string path = Utils.OpenFile("");

            // User cancelled or invalid path
            if (string.IsNullOrEmpty(path)) {
                return;
            }

            string ext = Path.GetExtension(path).ToLowerInvariant();

            switch (ext) {
                case ".json":
                    LoadMap(path);
                    break;

                default:
                    MessageBox.Show($"Unsupported file type: {ext}\nSupported types: .json, .ttf",
                        "Invalid File Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void toolStripButton_export(object? sender, MouseEventArgs e) {
            string startingPath = AppContext.BaseDirectory;
            string name = "default";
            string exten = "json";

            try {
                using (var dialog = new SaveFileDialog()) {
                    dialog.Filter = $"{exten.ToUpper()} Files (*.{exten})|*.{exten}|All Files (*.*)|*.*";
                    dialog.FilterIndex = 1;
                    dialog.InitialDirectory = startingPath;
                    dialog.FileName = name;
                    dialog.DefaultExt = exten;
                    dialog.AddExtension = true;

                    if (dialog.ShowDialog() == DialogResult.OK) {
                        bool ok = CExternsEditor.ExportMap(dialog.FileName);
                        if (!ok) {
                            MessageBox.Show($"Failed to export map:\n{GetLastErrorMessage()}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error saving file: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HierarchyTree_LayerSelected(object? sender, LayerNode layer) {
            Log($"Layer selected: {layer.Name} ({layer.Type})");

            // Retrieve layer info from backend
            CExternsEditor.LayerInfoStruct layerInfo = new CExternsEditor.LayerInfoStruct();
            bool infoResult = CExternsEditor.GetLayerInfo(layer.Name, out layerInfo);
            if (!infoResult) {
                string error = GetLastErrorMessage();
                Log($"Failed to retrieve layer info for '{layer.Name}': {error}");
                MessageBox.Show($"Failed to retrieve layer info for '{layer.Name}':\n{error}", "Layer Info Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _propertyGridDock.PropertyGrid.SelectedObject = null;
            }
            else {
                // Use editable LayerInfoDisplay class
                var layerInfoDisplay = new LayerInfoDisplay {
                    Name = Marshal.PtrToStringAnsi(layerInfo.name) ?? "",
                    Type = layerInfo.type,
                    TilesetName = Marshal.PtrToStringAnsi(layerInfo.tilesetName) ?? "",
                    Visible = layerInfo.visible == 1,
                    Silhouette = layerInfo.silhouette,
                    SilhouetteColor = Utils.ConvertFromRGBA(layerInfo.silhouetteColor)
                };
                layerInfoDisplay.SetOriginalName(layerInfoDisplay.Name);

                layerInfoDisplay.PropertyChanged += (s, e) => {
                    if (s is not LayerInfoDisplay display) return;

                    // Push updated properties to the backend using the original name as ID
                    bool propsOk = CExternsEditor.SetLayerProperties(display.OriginalName, display.Name, display.Visible, display.TilesetName, display.Type, display.Silhouette, display.SilhouetteColor);
                    if (!propsOk) {
                        string error = view_extern.GetLastErrorMessage();
                        MessageBox.Show($"Failed to set layer properties for '{display.OriginalName}':\n{error}", "Layer Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // If name changed, update the hierarchy tree and refresh the original name
                    if (e.PropertyName == nameof(LayerInfoDisplay.Name) && display.OriginalName != display.Name) {
                        hierarchyTree.RenameLayer(display.OriginalName, display.Name);
                        display.SetOriginalName(display.Name);
                    }
                };

                _propertyGridDock.PropertyGrid.SelectedObject = layerInfoDisplay;
            }

            // Switch between TextureViewer and EntitySelector based on layer type
            if (layer.Type == LayerType.TileLayer) {
                _isEntityLayerActive = false;
                textureViewer.Visible = true;
                entitySelector.Visible = false;
                CExternsEditor.DeselectEntity();
                UpdateTextureInfo(layer);
            }
            else if (layer.Type == LayerType.EntityLayer) {
                _isEntityLayerActive = true;
                textureViewer.Visible = false;
                entitySelector.Visible = true;
                entitySelector.SetLayer(layer.Name);
            }
            else {
                // Default or unknown layer type
                _isEntityLayerActive = false;
                textureViewer.Visible = false;
                entitySelector.Visible = false;
                CExternsEditor.DeselectEntity();
            }
        }

        private void HierarchyTree_StateSelected(object? sender, EventArgs e) {
            Log("State row selected");
            _isEntityLayerActive = false;
            CExternsEditor.DeselectEntity();
            try {
                if (view_extern != null) {
                    bool gotInfo = CExternsEditor.GetMapProps(out MapInfo info);

                    if (!gotInfo) {
                        MessageBox.Show($"Failed to retrieve map info:\n{GetLastErrorMessage()}", "Map Info Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _propertyGridDock.PropertyGrid.SelectedObject = null;
                    }
                    else {
                        // success: populate display object
                        var display = new MapInfoDisplay {
                            ID = info.idd ?? string.Empty,
                            Name = info.name ?? string.Empty,
                            WorldX = info.worldx,
                            WorldY = info.worldy,
                            Width = info.width,
                            Height = info.height,
                            TileSizeX = info.tileSizeX,
                            TileSizeY = info.tileSizeY,
                            BackgroundColor = Utils.ConvertFromRGBA(info.bgColor),
                            GridColor = Utils.ConvertFromRGBA(info.gridColor),
                            ProjectFilePath = info.projectFilePath,
                            ProjectName = info.projectName
                        };
                        display.PropertyChanged += (s, args) => {
                            if (s is MapInfoDisplay m && view_extern != null) {
                                // push back to engine
                                MapInfo native = new MapInfo {
                                    idd = m.ID,
                                    name = m.Name,
                                    worldx = m.WorldX,
                                    worldy = m.WorldY,
                                    width = m.Width,
                                    height = m.Height,
                                    tileSizeX = m.TileSizeX,
                                    tileSizeY = m.TileSizeY,
                                    bgColor = Utils.ConvertToRGBA(m.BackgroundColor),
                                    gridColor = Utils.ConvertToRGBA(m.GridColor),
                                    projectFilePath = m.ProjectFilePath,
                                    projectName = m.ProjectName
                                };
                                bool setOk = CExternsEditor.SetMapProps(native);
                                if (!setOk) {
                                    MessageBox.Show($"Failed to set map info:\n{GetLastErrorMessage()}", "Set Map Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        };
                        _propertyGridDock.PropertyGrid.SelectedObject = display;
                    }
                }
                else {
                    Log("Failed to retrieve map info");
                    _propertyGridDock.PropertyGrid.SelectedObject = null;
                }
            }
            catch (Exception ex) {
                Log("Error displaying map info: " + ex.Message);
            }
        }


        private void HierarchyTree_LayersChanged(object? sender, EventArgs e) {
            // TODO: Sync with backend when layers change
        }

        private void HierarchyTree_BatchSelected(object? sender, (string TilesetName, int BatchIndex) args) {
            Log($"Batch selected for tileset: {args.TilesetName} (index {args.BatchIndex})");
            textureViewer.Visible = false;
            entitySelector.Visible = true;
            entitySelector.SetBatchFilter(args.TilesetName, args.BatchIndex);
        }

        private void UpdateTextureInfo(LayerNode layer) {
            // Only update if it's a tile layer with a tileset
            if (layer.Type != LayerType.TileLayer || string.IsNullOrEmpty(layer.TilesetName)) {
                // Clear the texture viewer if no valid tileset
                textureViewer.Clear();
                _propertyGridDock.PropertyGrid.SelectedObject = null;
                return;
            }

            CExternsEditor.TilesetInfoStruct tilesetInfo = new CExternsEditor.TilesetInfoStruct();

            // Get tileset info from C++ using the layer's tileset
            bool result = CExternsEditor.GetTileset(layer.TilesetName, out tilesetInfo);

            if (!result) {
                Log($"Failed to load tileset '{layer.TilesetName}' for texture viewer");
                textureViewer.Clear();
                return;
            }

            // Get texture path from tileset info
            string texturePath = Marshal.PtrToStringAnsi(tilesetInfo.texturePath) ?? "";

            if (string.IsNullOrEmpty(texturePath)) {
                Log("Invalid texture path in tileset");
                textureViewer.Clear();
                return;
            }

            // Get texture data
            CExternsEditor.TextureDataStruct textureData;
            CExternsEditor.GetTextureData(texturePath, out textureData);

            // Update tileset viewer
            textureViewer.SetTextureData(textureData, layer.TileSize);

            // Get and select the active tile from backend
            int activeTile = CExternsEditor.GetActiveTile();
            textureViewer.SetSelectedTile(activeTile);

            Log($"Texture viewer updated with tileset: {layer.TilesetName}");
        }

        private void TextureViewer_SelectionChanged(object? sender, int regionId) {
            // Update the selected tile in the backend
            CExternsEditor.SetActiveTile(regionId);

            var selectedLayer = hierarchyTree.GetSelectedLayer();
            if (selectedLayer != null) {
                Log($"Selected tile from '{selectedLayer.Name}': RegionId={regionId}");
            }
        }

        private void EntitySelector_SelectionChanged(object? sender, string entityName) {
            _currentEntityName = entityName;

            // Set active entity in backend
            bool activeEntityOk = CExternsEditor.SetActiveEntity(entityName);
            if (!activeEntityOk) {
                string error = view_extern.GetLastErrorMessage();
                MessageBox.Show($"Failed to activate entity '{entityName}':\n{error}",
                    "Entity Activation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedLayer = hierarchyTree.GetSelectedLayer();
            if (selectedLayer != null) {
                Log($"Selected entity from '{selectedLayer.Name}': {entityName}");
            }
        }

        private void WelcomePanel_NewProjectRequested(object? sender, string path) {
            int stateId = CExterns.NewEditorState();
            string normalizedPath = Path.GetFullPath(path);
            string tabLabel = Path.GetFileNameWithoutExtension(path);
            var mapDoc = new MapDocContent(stateId, tabLabel, normalizedPath);
            mapDoc.FormClosing += MapDoc_FormClosing;
            _openMaps.Add(mapDoc);
            mapDoc.Show(dockPanel, DockState.Document);
            // ActiveDocumentChanged handles: AttachViewport, SetActiveState, hierarchy, entities
            RecentProjectsManager.Add(normalizedPath);
            _welcomePanel.RefreshRecent();
            // Save/initialise the project file via the backend
            string projectName = Path.GetFileNameWithoutExtension(normalizedPath);
            bool saveResult = CExternsEditor.ExportProject(normalizedPath, projectName);
            if (saveResult == false)
                Log($"Warning: ExportProject returned 0 for '{normalizedPath}'");
            Log($"New project created: {tabLabel} (state {stateId})");
            UpdateProjectStatus();
        }

        private void WelcomePanel_OpenMapRequested(object? sender, string path) {
            LoadMap(path);
        }

        private void WelcomePanel_OpenProjectRequested(object? sender, string path) {
            // Check if a project is already loaded — warn the user before overwriting
            if (CExternsEditor.GetProjectProps(out ProjectInfo existing)) {
                var action = ShowProjectLoadConflictDialog(existing.ProjectName ?? "Unknown");
                switch (action) {
                    case ProjectLoadAction.Abort:
                        return;
                    case ProjectLoadAction.SaveAll:
                        CloseAllTabs(saveFirst: true);
                        break;
                    case ProjectLoadAction.Close:
                        CloseAllTabs(saveFirst: false);
                        break;
                    case ProjectLoadAction.Add:
                        // Keep existing tabs open; just replace the project in the engine
                        break;
                }
            }

            // Try to load as a project first; fall back to plain map import
            string normalizedPath = Path.GetFullPath(path);
            bool result = CExternsEditor.ImportProject(normalizedPath);
            if (result != false) {
                // Project imported successfully — resolve name from backend
                CExternsEditor.GetProjectProps(out ProjectInfo importedProps);
                string tabLabel = importedProps.ProjectName ?? Path.GetFileNameWithoutExtension(path);
                int stateId = CExterns.NewEditorState();
                var mapDoc = new MapDocContent(stateId, tabLabel, normalizedPath);
                mapDoc.FormClosing += MapDoc_FormClosing;
                _openMaps.Add(mapDoc);
                mapDoc.Show(dockPanel, DockState.Document);
                // ActiveDocumentChanged handles: AttachViewport, SetActiveState, hierarchy, entities
                RecentProjectsManager.Add(normalizedPath);
                _welcomePanel.RefreshRecent();
                Log($"Project opened: {tabLabel}");
                UpdateProjectStatus();
            } else {
                // Fall back to plain map load
                LoadMap(path);
            }
        }

        private ProjectLoadAction ShowProjectLoadConflictDialog(string projectName) {
            using var dlg = new ProjectLoadConflictDialog(projectName);
            dlg.ShowDialog(this);
            return dlg.SelectedAction;
        }

        /// <summary>
        /// Releases and removes all open map tabs, optionally saving maps first.
        /// </summary>
        private void CloseAllTabs(bool saveFirst) {
            // Temporarily stop listening so cascading events don’t interfere
            dockPanel.ActiveDocumentChanged -= DockPanel_ActiveDocumentChanged;

            // Rescue ExternView before any MapDocContent is disposed
            RescueViewport();

            var maps = _openMaps.ToList();
            foreach (var mapDoc in maps) {
                if (saveFirst && !string.IsNullOrEmpty(mapDoc.FilePath) &&
                    mapDoc.FilePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) {
                    CExterns.SetActiveState(mapDoc.StateId);
                    bool ok = CExternsEditor.ExportMap(mapDoc.FilePath);
                    if (!ok)
                        MessageBox.Show($"Failed to save map '{mapDoc.FilePath}':\n{GetLastErrorMessage()}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        Log($"Saved map: {mapDoc.FilePath}");
                }
                mapDoc.FormClosing -= MapDoc_FormClosing;
                CExterns.ReleaseState(mapDoc.StateId);
                mapDoc.Close();
            }
            _openMaps.Clear();

            dockPanel.ActiveDocumentChanged += DockPanel_ActiveDocumentChanged;

            _welcomeDock.Show(dockPanel, DockState.Document);
            _welcomePanel.RefreshRecent();
            _welcomePanel.RefreshRecentMaps();
            UpdateProjectStatus();
        }

        private void ShowTilesetDefDialog(object? sender, MouseEventArgs e) {
             using (TextureCollectionDialog dialog = new TextureCollectionDialog(
                view_extern,
                onTilesetSelected: (tilesetName) => {
                    _currentTilesetName = tilesetName;
                    Log($"Current tileset set to: {_currentTilesetName}");
                },
                onTilesetDeleted: () => {
                    hierarchyTree.LoadLayersFromBackend();
                    Log("Tileset deleted — layers refreshed.");
                })) {
                dialog.ShowDialog(this);
            }
            AutoSaveProject();
        }

        /// <summary>
        /// Display a dialog listing all available tilesets and return the selected name (or null).
        /// </summary>
        public string? ShowTilesetSelectionDialog() {
            using var dlg = new TilesetSelectionDialog();
            return dlg.ShowDialog(this) == DialogResult.OK ? dlg.SelectedTileset : null;
        }
    }
}