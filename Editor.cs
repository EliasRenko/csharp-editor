using System.Runtime.InteropServices;

namespace csharp_editor {
    public partial class Editor : Form {

        public bool active = false;
        private string _currentTilesetName = "";
        private string _currentEntityName = "";
        
        public Editor() {
            InitializeComponent();

            active = true;

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
            hierarchyTree.LayersChanged += HierarchyTree_LayersChanged;
            
            // Initialize TilesetViewer
            tilesetViewer.SelectionChanged += TilesetViewer_SelectionChanged;
            
            // Enable keyboard events at form level
            KeyPreview = true;

            // Editor Events
            FormClosing += Editor_FormClosing;
            KeyDown += Editor_KeyDown;
            KeyUp += Editor_KeyUp;

            // ExternView Events
            view_extern.MouseDown += view_extern_MouseDown;
            view_extern.MouseUp += view_extern_MouseUp;

            // Debug 

            ToolStripMenuItem_textureViewer.MouseDown += ButtonTextureViewOnMouseDown;
            toolStripButton_tilesets.MouseDown += ButtonTilesetsOnMouseDown;
            toolStripButton_entities.MouseDown += ButtonEntitiesOnMouseDown;

            void ButtonTextureViewOnMouseDown(object? sender, MouseEventArgs e) {
                
                // Check if a layer is currently selected
                var selectedLayer = hierarchyTree.GetSelectedLayer();
                if (selectedLayer == null) {
                    MessageBox.Show("No layer is currently selected. Please select a layer from the Hierarchy first.",
                        "No Layer Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Check if it's a TileLayer
                if (selectedLayer.Type != LayerType.TileLayer) {
                    MessageBox.Show("The selected layer is not a Tile Layer. Please select a Tile Layer to view its tileset.",
                        "Invalid Layer Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                
                // Create and show TextureViewer dialog
                using (Form dialog = new Form()) {
                    string tilesetName = Marshal.PtrToStringAnsi(tilesetInfo.name) ?? "Unknown";
                    dialog.Text = $"Texture Viewer - {selectedLayer.Name} ({tilesetName})";
                    dialog.Size = new Size(620, 560);
                    dialog.StartPosition = FormStartPosition.CenterParent;
                    dialog.FormBorderStyle = FormBorderStyle.Sizable;
                    dialog.MinimumSize = new Size(400, 300);
                    
                    UserControls.TextureViewer viewer = new UserControls.TextureViewer();
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
            
            void ButtonTilesetsOnMouseDown(object? sender, MouseEventArgs e) {
                ShowTilesetImportDialog();
            }
        }

        private void ButtonEntitiesOnMouseDown(object? sender, MouseEventArgs e) {
            ShowEntitiesDialog();
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

        private void Editor_FormClosing(object sender, FormClosingEventArgs e) {
            active = false;
            Application.DoEvents(); // Process remaining messages
            System.Threading.Thread.Sleep(50); // Give loop time to exit
            view_extern.Release();
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e) {
            // Toggle console with tilde key (~) or F1
            if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.F1) {
                console.Visible = !console.Visible;
                e.Handled = true;
                return; // Don't pass console toggle to SDL
            }

            // Convert C# KeyCode to SDL Scancode and pass to SDL
            view_extern.OnKeyboardDown(KeyMapper.ToSDLScancode(e.KeyCode));
        }

        private void Editor_KeyUp(object sender, KeyEventArgs e) {
            // Toggle console with tilde key (~) or F1
            if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.F1) {
                e.Handled = true;
                return; // Don't pass console toggle to SDL
            }
            
            // Convert C# KeyCode to SDL Scancode and pass to SDL
            view_extern.OnKeyboardUp(KeyMapper.ToSDLScancode(e.KeyCode));
        }

        #endregion

        private void view_extern_MouseDown(object sender, MouseEventArgs e) {
            int button = MouseButtonMapper.ToSDLMouseButton(e.Button);
            view_extern.OnMouseButtonDown(e.X, e.Y, button);
        }

        private void view_extern_MouseUp(object sender, MouseEventArgs e) {
            int button = MouseButtonMapper.ToSDLMouseButton(e.Button);
            view_extern.OnMouseButtonUp(e.X, e.Y, button);
        }

        private void toolStripButton_openFile(object sender, MouseEventArgs e) {
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

        private void toolStripButton_export(object sender, MouseEventArgs e) {
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

        public void ShowTilesetImportDialog() {
            using (TilesetImportDialog dialog = new TilesetImportDialog(view_extern, (tilesetName) => {
                _currentTilesetName = tilesetName;
                Log($"Current tileset set to: {_currentTilesetName}");
            })) {
                dialog.ShowDialog(this);
            }
        }

        public void ShowEntitiesDialog() {
            using (EntitiesDialog dialog = new EntitiesDialog(view_extern, (entityName) => {
                _currentEntityName = entityName;
                Log($"Current entity set to: {_currentEntityName}");
            })) {
                dialog.ShowDialog(this);
            }
        }

        private void HierarchyTree_LayerSelected(object sender, HierarchyTree.LayerNode layer) {
            Log($"Layer selected: {layer.Name} ({layer.Type})");
            // TODO: Notify backend when selected layer changes
            
            // Update the embedded texture viewer when a tile layer is selected
            UpdateTextureViewer(layer);
        }

        private void HierarchyTree_LayersChanged(object sender, EventArgs e) {
            // TODO: Sync with backend when layers change
        }

        private void UpdateTextureViewer(HierarchyTree.LayerNode layer) {
            // Only update if it's a tile layer with a tileset
            if (layer.Type != LayerType.TileLayer || string.IsNullOrEmpty(layer.TilesetName)) {
                // Clear the tileset viewer if no valid tileset
                tilesetViewer.Clear();
                return;
            }
            
            Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
            
            // Get tileset info from C++ using the layer's tileset
            int result = view_extern.GetTileset(layer.TilesetName, out tilesetInfo);
            
            if (result == 0) {
                Log($"Failed to load tileset '{layer.TilesetName}' for tileset viewer");
                tilesetViewer.Clear();
                return;
            }
            
            // Get texture path from tileset info
            string texturePath = Marshal.PtrToStringAnsi(tilesetInfo.texturePath) ?? "";
            
            if (string.IsNullOrEmpty(texturePath)) {
                Log("Invalid texture path in tileset");
                tilesetViewer.Clear();
                return;
            }
            
            // Get texture data
            Externs.TextureDataStruct textureData;
            view_extern.GetTextureData(texturePath, out textureData);
            
            // Update tileset viewer
            tilesetViewer.SetTextureData(textureData, tilesetInfo);
            
            Log($"Tileset viewer updated with tileset: {layer.TilesetName}");
        }

        private void TilesetViewer_SelectionChanged(object? sender, int regionId) {
            // Update the selected tile in the backend
            view_extern.SetActiveTile(regionId);
            
            var selectedLayer = hierarchyTree.GetSelectedLayer();
            if (selectedLayer != null) {
                Log($"Selected tile from '{selectedLayer.Name}': RegionId={regionId}");
            }
        }
    }
}