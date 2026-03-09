using System.Runtime.InteropServices;
using csharp_editor.UserControls;
using csharp_editor.Models;
using csharp_editor.Dialogs;

namespace csharp_editor {
    public partial class Editor : Form {

        public bool active = false;
        private string _currentTilesetName = "";
        private string _currentEntityName = "";

        public Editor() {
            InitializeComponent();

            active = true;
            KeyPreview = true;

            Externs.CallbackDelegate callback = (value) => {
                Log(value);
            };

            view_extern.Init(callback);

            // Toolstrip Events
            toolStripMenuItem_open.MouseUp += toolStripButton_openFile;
            toolStripMenuItem_export.MouseUp += toolStripButton_export;

            // Initialize HierarchyTree
            hierarchyTree.SetExternView(view_extern);
            hierarchyTree.LayerSelected += HierarchyTree_LayerSelected;
            hierarchyTree.StateSelected += HierarchyTree_StateSelected;
            hierarchyTree.BatchSelected += HierarchyTree_BatchSelected;
            hierarchyTree.LayersChanged += HierarchyTree_LayersChanged;
            hierarchyTree.ReplaceTilesetClicked += ReplaceTilesetButton_Click;

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
            view_extern.EntitySelectionChanged += ExternView_EntitySelectionChanged;

            // Debug 

            ToolStripMenuItem_textureInfo.MouseDown += ButtonTextureViewOnMouseDown;
            toolStripButton_tilesets.MouseDown += ShowTilesetDefDialog;
            toolStripButton_entitiesDefs.MouseDown += ShowEntitiesDefDialog;

            // Tools

            toolStripButton_tileDraw.MouseDown += SelectTileDraw;
            toolStripButton_tileErase.MouseDown += SelectTileErase;
            toolStripButton_entityAdd.MouseDown += SelectEntityAdd;
            toolStripButton_entitySelect.MouseDown += SelectEntitySelect;
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

        private void ReplaceTilesetButton_Click(object? sender, EventArgs e) {
            var layer = hierarchyTree.GetSelectedLayer();

            if (layer is not { Type: LayerType.TileLayer } selectedLayer) {
                MessageBox.Show("The selected layer is not a Tile Layer. Please select a Tile Layer to view its tileset.", "No Tile Layer Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? selectedTileset = ShowTilesetSelectionDialog();
            if (!string.IsNullOrEmpty(selectedTileset)) {
                view_extern.ReplaceLayerTileset(selectedLayer.Name, selectedTileset);

                layer.TilesetName = selectedTileset; // Update local layer info
                UpdateTextureInfo(selectedLayer);
            }
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
                })) {
                dialog.ShowDialog(this);
            }
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

        private void LoadMap(string path) {
            view_extern.ImportMap(path);

            // Refresh the hierarchy tree to show loaded layers
            hierarchyTree.LoadLayersFromBackend();

            // Reload entity definitions so selector is up-to-date (useful after import)
            entitySelector.LoadEntities();

            Log($"Map loaded from: {path}");
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

        private void view_extern_MouseUp(object? sender, MouseEventArgs e) {
            int button = MouseButtonMapper.ToSDLMouseButton(e.Button);
            view_extern.OnMouseButtonUp(e.X, e.Y, button);

            // clicking in the extern view may have placed an entity – if the active layer
            // is an entity layer, refresh its batch groups so the hierarchy tree stays current.
            hierarchyTree.RefreshSelectedEntityBatches();
        }

        private void ExternView_EntitySelectionChanged(object? sender, EventArgs e) {
            int count = view_extern.GetEntitySelectionCount();

            if (count <= 0) {
                propertyGridPanel1.PropertyGrid.SelectedObject = null;
                return;
            }

            // Single selection – show details directly
            if (count == 1) {
                Externs.EntityStruct data = new Externs.EntityStruct();
                if (view_extern.GetEntitySelectionInfo(0, out data) != 0) {
                    var display = new EntityInstanceDisplay {
                        DefName = Marshal.PtrToStringAnsi(data.defName) ?? "",
                        X       = data.x,
                        Y       = data.y,
                        Width   = data.width,
                        Height  = data.height
                    };
                    propertyGridPanel1.PropertyGrid.SelectedObject = display;
                    Log($"Entity selected: {display.DefName} at ({display.X}, {display.Y})");
                }
                return;
            }

            // Multi-selection – show an array so all entries are visible
            var items = new List<EntityInstanceDisplay>(count);
            for (int i = 0; i < count; i++) {
                Externs.EntityStruct data = new Externs.EntityStruct();
                if (view_extern.GetEntitySelectionInfo(i, out data) != 0) {
                    items.Add(new EntityInstanceDisplay {
                        DefName = Marshal.PtrToStringAnsi(data.defName) ?? "",
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
                textureViewer.Visible = true;
                entitySelector.Visible = false;
                UpdateTextureInfo(layer);
            }
            else if (layer.Type == LayerType.EntityLayer) {
                textureViewer.Visible = false;
                entitySelector.Visible = true;
                // show all entities when switching to a new layer
                entitySelector.LoadEntities();
            }
            else {
                // Default or unknown layer type
                textureViewer.Visible = false;
                entitySelector.Visible = false;
            }
        }

        private void HierarchyTree_StateSelected(object? sender, EventArgs e) {
            Log("State row selected");
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
                            GridColor = Utils.ConvertFromRGBA(info.gridColor)
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

        private void HierarchyTree_BatchSelected(object? sender, string tilesetName) {
            // user picked a batch group: filter the entity selector
            Log($"Batch selected for tileset: {tilesetName}");
            entitySelector.LoadEntities(tilesetName);
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
            textureViewer.SetTextureData(textureData, tilesetInfo);

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

        // Externalized event handlers ------------------------------------------------

        private void ButtonTextureViewOnMouseDown(object? sender, MouseEventArgs e) {
            // Check if a layer is currently selected
            var selectedLayer = hierarchyTree.GetSelectedLayer();
            if (selectedLayer == null) {
                MessageBox.Show("No layer is currently selected. Please select a layer from the Hierarchy first.",
                    "No Layer Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the layer has a tileset assigned
            if (string.IsNullOrEmpty(selectedLayer.TilesetName)) {
                MessageBox.Show("The selected layer does not have a tileset assigned.",
                    "No Tileset", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();

            // Get tileset info from C++ using the layer's tileset
            int result = view_extern.GetTileset(selectedLayer.TilesetName, out tilesetInfo);

            if (result == 0) {
                MessageBox.Show($"Failed to get tileset '{selectedLayer.TilesetName}'. Please try reloading the tileset.",
                    "Tileset Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get texture path from tileset info
            string texturePath = Marshal.PtrToStringAnsi(tilesetInfo.texturePath) ?? "";

            if (string.IsNullOrEmpty(texturePath)) {
                MessageBox.Show("Invalid texture path in tileset", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get texture data
            Externs.TextureDataStruct textureData;
            view_extern.GetTextureData(texturePath, out textureData);

            // Create and show TextureInfo dialog
            using (Form dialog = new Form()) {
                string tilesetName = Marshal.PtrToStringAnsi(tilesetInfo.name) ?? "Unknown";
                dialog.Text = $"Texture Info - {selectedLayer.Name} ({tilesetName})";
                dialog.Size = new Size(620, 560);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.Sizable;
                dialog.MinimumSize = new Size(400, 300);

                UserControls.TextureInfo viewer = new UserControls.TextureInfo();
                viewer.Dock = DockStyle.Fill;
                viewer.SetTextureData(textureData, tilesetInfo);

                dialog.Controls.Add(viewer);
                dialog.ShowDialog(this);

                // Get selection after dialog closes
                if (viewer.HasSelection) {
                    Point selectedTile = viewer.SelectedTile;
                    int regionId = viewer.SelectedRegionId;
                    Log($"Selected tile from layer '{selectedLayer.Name}': X={selectedTile.X}, Y={selectedTile.Y}, RegionId={regionId}");

                    view_extern.SetActiveTile(regionId);
                }
            }
        }

        private void ShowTilesetDefDialog(object? sender, MouseEventArgs e) {
             using (TilesetImportDialog dialog = new TilesetImportDialog(view_extern, (tilesetName) => {
                _currentTilesetName = tilesetName;
                Log($"Current tileset set to: {_currentTilesetName}");
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