using System.Runtime.InteropServices;
using csharp_editor.UserControls;
using csharp_editor.Models;
using csharp_editor.Dialogs;
using csharp_editor.Helpers;

namespace csharp_editor {
    public partial class Editor : Form {

        public bool active = false;
        
        private string _currentTilesetName = "";
        private string _currentEntityName = "";
        private bool _suppressStateSwitch = false;
        private bool _isEntityLayerActive = false;
        
        private ExternError lastError;
        private int _hoveredTabIndex = -1;
        private WelcomePanel _welcomePanel = null!;

        public Editor() {
            InitializeComponent();

            active = true;
            KeyPreview = true;

            Externs.CallbackDelegate callback = (priority, category, message) => {
                lastError.SetError(priority, category, message);
                Log(priority + " - " + category + " - " + message);
            };

            view_extern.Init(callback);

            // Toolstrip Events
            toolStripMenuItem_open.MouseUp += toolStripButton_openFile;
            toolStripMenuItem_export.MouseUp += toolStripButton_export;
            saveProjectToolStripMenuItem.Click += SaveProject_Click;
            saveAsProjectToolStripMenuItem.Click += SaveAsProject_Click;
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
            view_extern.EntitySelectionChanged += ExternView_EntitySelectionChanged;

            // Debug 

            ToolStripMenuItem_timeline.MouseDown += ShowTimelineDialog;
            toolStripButton_tilesets.MouseDown += ShowTilesetDefDialog;
            toolStripButton_entitiesDefs.MouseDown += ShowEntitiesDefDialog;

            // Tab / state switching
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            tabControl1.DrawItem += TabControl1_DrawItem;
            tabControl1.MouseClick += TabControl1_MouseClick;
            tabControl1.MouseMove += TabControl1_MouseMove;
            tabControl1.MouseLeave += TabControl1_MouseLeave;

            // Tools

            button_brush.MouseDown += SelectTileDraw;
            //toolStripButton_tileErase.MouseDown += SelectTileErase;
            button_entity.MouseDown += SelectEntityAdd;
            button_cursor.MouseDown += SelectEntitySelect;
            // Welcome panel
            _welcomePanel = new WelcomePanel();
            _welcomePanel.NewProjectRequested  += WelcomePanel_NewProjectRequested;
            _welcomePanel.OpenProjectRequested += WelcomePanel_OpenProjectRequested;
            Controls.Add(_welcomePanel);
        }

        private void ShowTimelineDialog(object? sender, MouseEventArgs e) {
            using var dialog = new Dialogs.TimelineDialog();
            dialog.ShowDialog(this);
        }

        private void SelectTileDraw(object? sender, MouseEventArgs e) {
            view_extern.SetToolType(ToolType.TileDraw);
        }

        private void SelectTileErase(object? sender, MouseEventArgs e) {
            view_extern.SetToolType(ToolType.TileErase);
        }

        private void SelectEntityAdd(object? sender, MouseEventArgs e) {
            view_extern.SetToolType(ToolType.EntityAdd);
        }

        private void SelectEntitySelect(object? sender, MouseEventArgs e) {
            view_extern.SetToolType(ToolType.EntitySelect);
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
        }

        public void UpdateFrame(float deltaTime) {
            view_extern.UpdateFrame(deltaTime);
        }

        public void PreRender() {
            //view_extern.PreRender();
        }

        public void Render() {
            view_extern.Render();
        }

        public void SwapBuffers() {
            view_extern.SwapBuffers();
        }

        #region Core

        private void SaveProject_Click(object? sender, EventArgs e) {
            if (tabControl1.SelectedTab?.Tag is TabState ts && !string.IsNullOrEmpty(ts.FilePath)) {
                string projectName = Path.GetFileNameWithoutExtension(ts.FilePath);
                bool result = view_extern.ExportProject(ts.FilePath, projectName);
                if (result == false)
                    Log($"Warning: ExportProject returned 0 for '{ts.FilePath}'");
                else
                    Log($"Project saved: {ts.FilePath}");
            } else {
                Log("Save: no project path available for the current tab.");
            }
        }

        private void SaveAsProject_Click(object? sender, EventArgs e) {
            if (tabControl1.SelectedTab?.Tag is not TabState ts) return;
            using SaveFileDialog dlg = new SaveFileDialog {
                Title = "Save Project As",
                Filter = "Project files (*.proj)|*.proj|All files (*.*)|*.*",
                FileName = string.IsNullOrEmpty(ts.FilePath)
                    ? ""
                    : Path.GetFileName(ts.FilePath)
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            string newPath = Path.GetFullPath(dlg.FileName);
            string projectName = Path.GetFileNameWithoutExtension(newPath);
            bool result = view_extern.ExportProject(newPath, projectName);
            if (result == false) {
                Log($"Warning: ExportProject returned 0 for '{newPath}'");
            } else {
                tabControl1.SelectedTab!.Tag = new TabState(ts.StateId, newPath);
                tabControl1.SelectedTab.Text = projectName;
                UpdateTabItemSize();
                Log($"Project saved as: {newPath}");
            }
        }

        private record TabState(int StateId, string FilePath);

        /// <summary>
        /// Measures the widest tab label and updates ItemSize so no text is ever clipped.
        /// Also shows/hides the tab strip depending on whether any tabs exist.
        /// </summary>
        private void UpdateTabItemSize() {
            if (tabControl1.TabPages.Count == 0) {
                tabControl1.Visible = false;
                return;
            }
            tabControl1.Visible = true;

            // Leave room for the close × button + horizontal padding
            const int extraPadding = 50;
            const int minWidth = 120;
            const int maxWidth = 300;

            int widest = minWidth;
            foreach (TabPage page in tabControl1.TabPages) {
                int w = TextRenderer.MeasureText(page.Text, tabControl1.Font).Width + extraPadding;
                if (w > widest) widest = w;
            }
            widest = Math.Min(widest, maxWidth);

            if (tabControl1.ItemSize.Width != widest)
                tabControl1.ItemSize = new Size(widest, tabControl1.ItemSize.Height);
        }

        private void LoadMap(string path) {
            _welcomePanel.Visible = false;

            // Prevent loading the same file twice
            string normalizedPath = Path.GetFullPath(path);
            foreach (TabPage existing in tabControl1.TabPages) {
                if (existing.Tag is TabState ts && string.Equals(ts.FilePath, normalizedPath, StringComparison.OrdinalIgnoreCase)) {
                    tabControl1.SelectedTab = existing;
                    Log($"Map already open: {normalizedPath}");
                    return;
                }
            }

            _suppressStateSwitch = true;
            int stateId = view_extern.ImportMap(path);
            System.Diagnostics.Debug.WriteLine($"[LoadMap] ImportMap('{path}') returned stateId={stateId}");
            Log($"[DEBUG] ImportMap returned stateId={stateId}");

            // Create a tab for this state
            string tabLabel = Path.GetFileNameWithoutExtension(path);
            TabPage tab = new TabPage(tabLabel) { Tag = new TabState(stateId, normalizedPath) };
            tabControl1.TabPages.Add(tab);
            UpdateTabItemSize();
            tabControl1.SelectedTab = tab;
            _suppressStateSwitch = false;

            // Explicitly activate the imported state (suppressed above so SelectedIndexChanged didn't do it)
            int setStateResult = view_extern.SetActiveState(stateId);
            System.Diagnostics.Debug.WriteLine($"[LoadMap] SetActiveState({stateId}) returned {setStateResult}");
            Log($"[DEBUG] SetActiveState({stateId}) returned {setStateResult}");

            // Show the editor panel if it was hidden
            panelMain.Visible = true;

            // Track in recent projects
            RecentProjectsManager.Add(normalizedPath);
            _welcomePanel.RefreshRecent();

            // Refresh the hierarchy tree to show loaded layers
            hierarchyTree.LoadLayersFromBackend();

            // Reload entity definitions so selector is up-to-date (useful after import)
            entitySelector.LoadEntities();

            Log($"Map loaded: {tabLabel} (state {stateId})");
        }

        private void ToolStripButton_newMap_Click(object? sender, MouseEventArgs e) {
            int stateId = view_extern.NewEditorState();
            TabPage tab = new TabPage($"New Map {stateId}") { Tag = new TabState(stateId, "") };
            _suppressStateSwitch = true;
            tabControl1.TabPages.Add(tab);
            UpdateTabItemSize();
            tabControl1.SelectedTab = tab;
            _suppressStateSwitch = false;
            view_extern.SetActiveState(stateId);
            panelMain.Visible = true;
            _welcomePanel.Visible = false;
            hierarchyTree.LoadLayersFromBackend();
            entitySelector.LoadEntities();
            Log($"New state created (id {stateId})");
        }

        private void TabControl1_SelectedIndexChanged(object? sender, EventArgs e) {
            if (_suppressStateSwitch) return;
            if (tabControl1.SelectedTab?.Tag is TabState ts) {
                view_extern.SetActiveState(ts.StateId);
                hierarchyTree.LoadLayersFromBackend();
                entitySelector.LoadEntities();
                entitySelector.LoadInstances();
                Log($"Switched to state {ts.StateId}");
            }
        }

        private Rectangle GetTabCloseRect(Rectangle tabRect) {
            const int size = 14;
            return new Rectangle(tabRect.Right - size - 5, tabRect.Top + (tabRect.Height - size) / 2, size, size);
        }

        private void TabControl1_MouseMove(object? sender, MouseEventArgs e) {
            int hovered = -1;
            for (int i = 0; i < tabControl1.TabPages.Count; i++) {
                if (tabControl1.GetTabRect(i).Contains(e.Location)) {
                    hovered = i;
                    break;
                }
            }
            if (hovered != _hoveredTabIndex) {
                _hoveredTabIndex = hovered;
                tabControl1.Invalidate();
            }
        }

        private void TabControl1_MouseLeave(object? sender, EventArgs e) {
            if (_hoveredTabIndex != -1) {
                _hoveredTabIndex = -1;
                tabControl1.Invalidate();
            }
        }

        private void TabControl1_DrawItem(object? sender, DrawItemEventArgs e) {
            TabPage tab = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);
            bool isSelected = tabControl1.SelectedIndex == e.Index;
            bool isHovered = !isSelected && _hoveredTabIndex == e.Index;

            // Background
            Color bgColor = isSelected ? SystemColors.Window
                          : isHovered  ? SystemColors.ControlLightLight
                                       : SystemColors.ControlLight;
            using Brush bgBrush = new SolidBrush(bgColor);
            e.Graphics.FillRectangle(bgBrush, tabRect);

            // Blue accent line along the bottom of the selected tab
            if (isSelected) {
                using Pen accent = new Pen(Color.FromArgb(0, 120, 215), 2);
                e.Graphics.DrawLine(accent, tabRect.Left + 1, tabRect.Bottom - 1,
                                            tabRect.Right - 1, tabRect.Bottom - 1);
            }

            // Subtle right-edge separator between inactive tabs
            if (!isSelected && e.Index < tabControl1.TabPages.Count - 1) {
                using Pen sep = new Pen(SystemColors.ControlDark, 1);
                e.Graphics.DrawLine(sep, tabRect.Right - 1, tabRect.Top + 5,
                                        tabRect.Right - 1, tabRect.Bottom - 5);
            }

            // Tab text (leave room for ×)
            Rectangle closeRect = GetTabCloseRect(tabRect);
            Rectangle textRect = new Rectangle(tabRect.Left + 8, tabRect.Top,
                                               tabRect.Width - closeRect.Width - 16, tabRect.Height);
            Color textColor = isSelected ? SystemColors.ControlText : Color.FromArgb(80, 80, 80);
            TextRenderer.DrawText(e.Graphics, tab.Text, tabControl1.Font, textRect, textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            // × button — more visible on active/hovered tabs
            Color closeColor = isSelected || isHovered ? SystemColors.ControlDarkDark : SystemColors.ControlDark;
            using Font closeFont = new Font(tabControl1.Font.FontFamily, 8f, FontStyle.Bold);
            TextRenderer.DrawText(e.Graphics, "×", closeFont, closeRect, closeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void TabControl1_MouseClick(object? sender, MouseEventArgs e) {
            for (int i = 0; i < tabControl1.TabPages.Count; i++) {
                if (GetTabCloseRect(tabControl1.GetTabRect(i)).Contains(e.Location)) {
                    CloseStateTab(i);
                    return;
                }
            }
        }

        private void CloseStateTab(int index) {
            if (tabControl1.TabPages[index].Tag is TabState ts) {
                view_extern.ReleaseState(ts.StateId);
            }
            tabControl1.TabPages.RemoveAt(index);
            UpdateTabItemSize();
            if (tabControl1.TabPages.Count == 0) {
                panelMain.Visible = false;
                _welcomePanel.RefreshRecent();
                _welcomePanel.Visible = true;
            }
        }

        #endregion

        #region Log
        public void Log(string text) {
            // Check if form and console are not disposed
            if (!IsDisposed && console != null && !console.IsDisposed) {
                console.Log(text);
            }
        }

        #endregion

        private void UpdateProjectStatus() {
            if (view_extern.GetProjectProps(out ProjectInfoStruct info))
                statusLabel_project.Text = $"Project: {info.ProjectName}";
            else
                statusLabel_project.Text = "No project loaded";
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
                console.Visible = !console.Visible;
                e.Handled = true;
                return; // Don't pass console toggle to SDL
            }

            // TODO: OPTIMIZE FURTHER
            if (propertyGridPanel1.PropertyGrid.ContainsFocus) {

                if (e.KeyCode == Keys.Escape) {
                    view_extern.Focus();
                }

                e.Handled = true;
                return;
            }

            // Convert C# KeyCode to SDL Scancode and pass to SDL
            view_extern.OnKeyboardDown(KeyMapper.ToSDLScancode(e.KeyCode));
        }

        private void Editor_KeyUp(object? sender, KeyEventArgs e) {
            // Toggle console with tilde key (~) or F1
            if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.F1) {
                e.Handled = true;
                return; // Don't pass console toggle to SDL
            }

            // Convert C# KeyCode to SDL Scancode and pass to SDL
            view_extern.OnKeyboardUp(KeyMapper.ToSDLScancode(e.KeyCode));
        }

        #endregion

        private void view_extern_MouseDown(object? sender, MouseEventArgs e) {
            int button = MouseButtonMapper.ToSDLMouseButton(e.Button);
            view_extern.OnMouseButtonDown(e.X, e.Y, button);
        }

        private void view_extern_MouseWheel(object? sender, MouseEventArgs e) {
            view_extern.OnMouseWheel(e.X, e.Y, e.Delta / 120.0f);
        }

        private void view_extern_MouseUp(object? sender, MouseEventArgs e) {
            int button = MouseButtonMapper.ToSDLMouseButton(e.Button);
            view_extern.OnMouseButtonUp(e.X, e.Y, button);

            // clicking in the extern view may have placed an entity – if the active layer
            // is an entity layer, refresh its batch groups and instance list so everything stays current.
            hierarchyTree.RefreshSelectedEntityBatches();
            entitySelector.ReloadInstancesKeepSelection();
        }

        private void ExternView_EntitySelectionChanged(object? sender, EventArgs e) {
            if (!_isEntityLayerActive) return;

            int count = view_extern.GetEntitySelectionCount();

            if (count <= 0) {
                // Only wipe entity-specific info; preserve layer/state info if no entity was shown
                if (propertyGridPanel1.PropertyGrid.SelectedObject is EntityInstanceDisplay)
                    propertyGridPanel1.PropertyGrid.SelectedObject = null;
                return;
            }

            // Single selection – show details directly
            if (count == 1) {
                Externs.EntityStruct data = new Externs.EntityStruct();
                if (view_extern.GetEntitySelectionInfo(0, out data) != 0) {
                    var display = new EntityInstanceDisplay {
                        Uid     = Marshal.PtrToStringAnsi(data.uid)    ?? "",
                        DefName = Marshal.PtrToStringAnsi(data.name)   ?? "",
                        X       = data.x,
                        Y       = data.y,
                        Width   = data.width,
                        Height  = data.height
                    };
                    propertyGridPanel1.PropertyGrid.SelectedObject = display;
                    Log($"Entity selected: {display.DefName} at ({display.X}, {display.Y})");
                    entitySelector.SelectInstanceByUid(display.Uid);
                }
                return;
            }

            // Multi-selection – show an array so all entries are visible
            var items = new List<EntityInstanceDisplay>(count);
            for (int i = 0; i < count; i++) {
                Externs.EntityStruct data = new Externs.EntityStruct();
                if (view_extern.GetEntitySelectionInfo(i, out data) != 0) {
                    items.Add(new EntityInstanceDisplay {
                        Uid     = Marshal.PtrToStringAnsi(data.uid)    ?? "",
                        DefName = Marshal.PtrToStringAnsi(data.name)   ?? "",
                        X       = data.x,
                        Y       = data.y,
                        Width   = data.width,
                        Height  = data.height
                    });
                }
            }
            propertyGridPanel1.PropertyGrid.SelectedObjects = items.Cast<object>().ToArray();
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
                        view_extern.ExportMap(dialog.FileName);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error saving file: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HierarchyTree_LayerSelected(object? sender, HierarchyTree.LayerNode layer) {
            Log($"Layer selected: {layer.Name} ({layer.Type})");

            // Retrieve layer info from backend
            Externs.LayerInfoStruct layerInfo = new Externs.LayerInfoStruct();
            int infoResult = view_extern.GetLayerInfo(layer.Name, out layerInfo);
            if (infoResult == 0) {
                Log($"Failed to retrieve layer info for '{layer.Name}'");
                propertyGridPanel1.PropertyGrid.SelectedObject = null;
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
                    view_extern.SetLayerProperties(display.OriginalName, display.Name, display.Visible, display.TilesetName, display.Type, display.Silhouette, display.SilhouetteColor);

                    // If name changed, update the hierarchy tree and refresh the original name
                    if (e.PropertyName == nameof(LayerInfoDisplay.Name) && display.OriginalName != display.Name) {
                        hierarchyTree.RenameLayer(display.OriginalName, display.Name);
                        display.SetOriginalName(display.Name);
                    }
                };

                propertyGridPanel1.PropertyGrid.SelectedObject = layerInfoDisplay;
            }

            // Switch between TextureViewer and EntitySelector based on layer type
            if (layer.Type == LayerType.TileLayer) {
                _isEntityLayerActive = false;
                textureViewer.Visible = true;
                entitySelector.Visible = false;
                view_extern?.DeselectEntity();
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
                view_extern?.DeselectEntity();
            }
        }

        private void HierarchyTree_StateSelected(object? sender, EventArgs e) {
            Log("State row selected");
            _isEntityLayerActive = false;
            view_extern?.DeselectEntity();
            try {
                if (view_extern != null) {
                    // call backend and capture possible error message
                    string? error = view_extern.GetMapProps(out MapInfoStruct info);

                    if (!string.IsNullOrEmpty(error)) {
                        // backend returned an error string – show to user and abort
                        MessageBox.Show(error, "Map Info Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (propertyGridPanel1?.PropertyGrid != null)
                            propertyGridPanel1.PropertyGrid.SelectedObject = null;
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
                                MapInfoStruct native = new MapInfoStruct {
                                    idd = m.ID,
                                    name = m.Name,
                                    worldx = m.WorldX,
                                    worldy = m.WorldY,
                                    width = m.Width,
                                    height = m.Height,
                                    tileSizeX = m.TileSizeX,
                                    tileSizeY = m.TileSizeY,
                                    bgColor = Utils.ConvertToRGBA(m.BackgroundColor),
                                    gridColor = Utils.ConvertToRGBA(m.GridColor)
                                };
                                view_extern.SetMapProps(native);
                            }
                        };
                        propertyGridPanel1.PropertyGrid.SelectedObject = display;
                    }
                }
                else {
                    Log("Failed to retrieve map info");
                    if (propertyGridPanel1?.PropertyGrid != null)
                        propertyGridPanel1.PropertyGrid.SelectedObject = null;
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

        private void UpdateTextureInfo(HierarchyTree.LayerNode layer) {
            // Only update if it's a tile layer with a tileset
            if (layer.Type != LayerType.TileLayer || string.IsNullOrEmpty(layer.TilesetName)) {
                // Clear the texture viewer if no valid tileset
                textureViewer.Clear();
                propertyGridPanel1.PropertyGrid.SelectedObject = null;
                return;
            }

            Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();

            // Get tileset info from C++ using the layer's tileset
            int result = view_extern.GetTileset(layer.TilesetName, out tilesetInfo);

            if (result == 0) {
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
            Externs.TextureDataStruct textureData;
            view_extern.GetTextureData(texturePath, out textureData);

            // Update tileset viewer
            textureViewer.SetTextureData(textureData, layer.TileSize);

            // Get and select the active tile from backend
            int activeTile = view_extern.GetActiveTile();
            textureViewer.SetSelectedTile(activeTile);

            Log($"Texture viewer updated with tileset: {layer.TilesetName}");
        }

        private void TextureViewer_SelectionChanged(object? sender, int regionId) {
            // Update the selected tile in the backend
            view_extern.SetActiveTile(regionId);

            var selectedLayer = hierarchyTree.GetSelectedLayer();
            if (selectedLayer != null) {
                Log($"Selected tile from '{selectedLayer.Name}': RegionId={regionId}");
            }
        }

        private void EntitySelector_SelectionChanged(object? sender, string entityName) {
            _currentEntityName = entityName;

            // Set active entity in backend
            view_extern.SetActiveEntity(entityName);

            var selectedLayer = hierarchyTree.GetSelectedLayer();
            if (selectedLayer != null) {
                Log($"Selected entity from '{selectedLayer.Name}': {entityName}");
            }
        }

        private void WelcomePanel_NewProjectRequested(object? sender, string path) {
            int stateId = view_extern.NewEditorState();
            string normalizedPath = Path.GetFullPath(path);
            string tabLabel = Path.GetFileNameWithoutExtension(path);
            TabPage tab = new TabPage(tabLabel) { Tag = new TabState(stateId, normalizedPath) };
            _suppressStateSwitch = true;
            tabControl1.TabPages.Add(tab);
            UpdateTabItemSize();
            tabControl1.SelectedTab = tab;
            _suppressStateSwitch = false;
            view_extern.SetActiveState(stateId);
            panelMain.Visible = true;
            _welcomePanel.Visible = false;
            hierarchyTree.LoadLayersFromBackend();
            entitySelector.LoadEntities();
            RecentProjectsManager.Add(normalizedPath);
            _welcomePanel.RefreshRecent();
            // Save/initialise the project file via the backend
            string projectName = Path.GetFileNameWithoutExtension(normalizedPath);
            bool saveResult = view_extern.ExportProject(normalizedPath, projectName);
            if (saveResult == false)
                Log($"Warning: ExportProject returned 0 for '{normalizedPath}'");
            Log($"New project created: {tabLabel} (state {stateId})");
            UpdateProjectStatus();
        }

        private void WelcomePanel_OpenProjectRequested(object? sender, string path) {
            // Check if a project is already loaded — warn the user before overwriting
            if (view_extern.GetProjectProps(out ProjectInfoStruct existing)) {
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
            bool result = view_extern.ImportProject(normalizedPath);
            if (result != false) {
                // Project imported successfully — resolve name from backend
                view_extern.GetProjectProps(out ProjectInfoStruct importedProps);
                string tabLabel = importedProps.ProjectName ?? Path.GetFileNameWithoutExtension(path);
                _welcomePanel.Visible = false;
                int stateId = view_extern.NewEditorState();
                TabPage tab = new TabPage(tabLabel) { Tag = new TabState(stateId, normalizedPath) };
                _suppressStateSwitch = true;
                tabControl1.TabPages.Add(tab);
                UpdateTabItemSize();
                tabControl1.SelectedTab = tab;
                _suppressStateSwitch = false;
                view_extern.SetActiveState(stateId);
                panelMain.Visible = true;
                _welcomePanel.Visible = false;
                hierarchyTree.LoadLayersFromBackend();
                entitySelector.LoadEntities();
                RecentProjectsManager.Add(normalizedPath);
                _welcomePanel.RefreshRecent();
                Log($"Project opened: {tabLabel}");
                UpdateProjectStatus();
            } else {
                // Fall back to plain map load
                LoadMap(path);
            }
        }

        private enum ProjectLoadAction { SaveAll, Close, Add, Abort }

        private ProjectLoadAction ShowProjectLoadConflictDialog(string projectName) {
            var result = ProjectLoadAction.Abort;

            using var dlg = new Form {
                Text = "Project Already Loaded",
                Size = new Size(500, 190),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var label = new Label {
                Text = $"A project is already loaded: \"{projectName}\"\n\n" +
                       "Loading a new project will overwrite it. What would you like to do with the currently open maps?",
                Dock = DockStyle.Top,
                Height = 65,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 10, 12, 0)
            };

            var btnPanel = new FlowLayoutPanel {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Height = 45,
                Padding = new Padding(8, 6, 8, 0)
            };

            var btnAbort   = new Button { Text = "Abort",            Width = 80,  Height = 30 };
            var btnAdd     = new Button { Text = "Add",              Width = 80,  Height = 30 };
            var btnClose   = new Button { Text = "Close All",        Width = 90,  Height = 30 };
            var btnSaveAll = new Button { Text = "Save All & Close", Width = 120, Height = 30 };

            btnAbort.Click   += (s, e) => { result = ProjectLoadAction.Abort;   dlg.Close(); };
            btnAdd.Click     += (s, e) => { result = ProjectLoadAction.Add;     dlg.Close(); };
            btnClose.Click   += (s, e) => { result = ProjectLoadAction.Close;   dlg.Close(); };
            btnSaveAll.Click += (s, e) => { result = ProjectLoadAction.SaveAll; dlg.Close(); };

            btnPanel.Controls.AddRange(new Control[] { btnAbort, btnAdd, btnClose, btnSaveAll });
            dlg.Controls.AddRange(new Control[] { label, btnPanel });
            dlg.ShowDialog(this);
            return result;
        }

        /// <summary>
        /// Releases and removes all open tabs, optionally saving maps with a known file path first.
        /// Does not show the welcome panel — caller handles that.
        /// </summary>
        private void CloseAllTabs(bool saveFirst) {
            _suppressStateSwitch = true;
            var tabs = tabControl1.TabPages.Cast<TabPage>().ToList();
            foreach (var tab in tabs) {
                if (tab.Tag is TabState ts) {
                    if (saveFirst && !string.IsNullOrEmpty(ts.FilePath) &&
                        ts.FilePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) {
                        view_extern.SetActiveState(ts.StateId);
                        view_extern.ExportMap(ts.FilePath);
                        Log($"Saved map: {ts.FilePath}");
                    }
                    view_extern.ReleaseState(ts.StateId);
                }
            }
            tabControl1.TabPages.Clear();
            _suppressStateSwitch = false;
            UpdateTabItemSize();
            panelMain.Visible = false;
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
        }

        /// <summary>
        /// Display a simple dialog listing all available tilesets and return the selected name (or null).
        /// </summary>
        public string? ShowTilesetSelectionDialog() {
            using (var dialog = new Form()) {
                dialog.Text = "Select Tileset";
                dialog.Size = new Size(350, 240);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;

                Label label = new Label {
                    Text = "Available Tilesets:",
                    Location = new Point(10, 10),
                    Size = new Size(320, 20)
                };

                ListBox listBox = new ListBox {
                    Location = new Point(10, 35),
                    Size = new Size(320, 120)
                };

                int count = view_extern?.GetTilesetCount() ?? 0;
                for (int i = 0; i < count; i++) {
                    Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
                    int result = view_extern?.GetTilesetAt(i, out tilesetInfo) ?? 0;
                    if (result != 0) {
                        string tilesetName = Marshal.PtrToStringAnsi(tilesetInfo.name) ?? "";
                        if (!string.IsNullOrEmpty(tilesetName))
                            listBox.Items.Add(tilesetName);
                    }
                }
                if (listBox.Items.Count > 0)
                    listBox.SelectedIndex = 0;

                Button buttonOk = new Button {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(175, 170),
                    Size = new Size(75, 30)
                };
                Button buttonCancel = new Button {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(255, 170),
                    Size = new Size(75, 30)
                };

                dialog.Controls.AddRange(new Control[] { label, listBox, buttonOk, buttonCancel });
                dialog.AcceptButton = buttonOk;
                dialog.CancelButton = buttonCancel;

                if (dialog.ShowDialog(this) == DialogResult.OK && listBox.SelectedItem != null)
                    return listBox.SelectedItem.ToString();
                return null;
            }
        }
    }
}