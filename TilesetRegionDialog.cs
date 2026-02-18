using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using csharp_editor.UserControls;

namespace csharp_editor {
    public partial class TilesetRegionDialog : Form {
        private ExternView _externView;
        private string _tilesetName;
        private int _entityWidth;
        private int _entityHeight;
        
        public Rectangle SelectedRegion { get; private set; }
        
        public TilesetRegionDialog(ExternView externView, string tilesetName, int entityWidth, int entityHeight, 
                                   int initialTileX = 0, int initialTileY = 0, int initialTileWidth = 1, int initialTileHeight = 1) {
            InitializeComponent();
            
            _externView = externView;
            _tilesetName = tilesetName;
            _entityWidth = entityWidth;
            _entityHeight = entityHeight;
            
            this.Text = $"Select Region - {tilesetName}";
            
            // Enable region selection mode
            tilesetViewer.RegionSelectionMode = true;
            tilesetViewer.SnapToGrid = true;
            
            LoadTilesetTexture();
            
            // Set initial region
            tilesetViewer.SetInitialRegion(initialTileX, initialTileY, initialTileWidth, initialTileHeight);
            UpdateRegionLabel();
        }
        
        private void LoadTilesetTexture() {
            // Find tileset index
            int count = _externView.GetTilesetCount();
            int tilesetIndex = -1;
            
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
                int result = _externView.GetTilesetAt(i, out tilesetInfo);
                
                if (result != 0) {
                    string name = Marshal.PtrToStringAnsi(tilesetInfo.name) ?? "";
                    if (name == _tilesetName) {
                        tilesetIndex = i;
                        break;
                    }
                }
            }
            
            if (tilesetIndex < 0) {
                MessageBox.Show($"Tileset '{_tilesetName}' not found.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            
            // Load texture data
            Externs.TextureDataStruct textureData = new Externs.TextureDataStruct();
            Externs.TilesetInfoStruct tileset = new Externs.TilesetInfoStruct();
            
            int loadResult = _externView.GetTilesetAt(tilesetIndex, out tileset);
            if (loadResult != 0) {
                string texturePath = Marshal.PtrToStringAnsi(tileset.texturePath) ?? "";
                _externView.GetTextureData(texturePath, out textureData);
                
                tilesetViewer.SetTextureData(textureData, tileset);
                
                // Calculate suggested region based on entity size
                if (_entityWidth > 0 && _entityHeight > 0 && tileset.tileSize > 0) {
                    int suggestedTileWidth = (_entityWidth + tileset.tileSize - 1) / tileset.tileSize;
                    int suggestedTileHeight = (_entityHeight + tileset.tileSize - 1) / tileset.tileSize;
                    
                    labelSuggestion.Text = $"Suggested region for {_entityWidth}×{_entityHeight}px entity: {suggestedTileWidth}×{suggestedTileHeight} tiles";
                }
            }
        }
        
        private void UpdateRegionLabel() {
            Rectangle region = tilesetViewer.SelectedRegionInTiles;
            if (region != Rectangle.Empty) {
                labelRegion.Text = $"Selected: ({region.X}, {region.Y}) {region.Width}×{region.Height} tiles";
            } else {
                labelRegion.Text = "Selected: None";
            }
        }
        
        private void buttonOK_Click(object sender, EventArgs e) {
            SelectedRegion = tilesetViewer.SelectedRegionInTiles;
            
            if (SelectedRegion == Rectangle.Empty) {
                MessageBox.Show("Please select a region.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void buttonCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void timerUpdateLabel_Tick(object sender, EventArgs e) {
            UpdateRegionLabel();
        }
    }
}
