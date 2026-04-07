using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class TilesetRegionDialog : Form {
        private ExternView _externView;
        private string _tilesetName;
        private int _entityWidth;
        private int _entityHeight;
        private int _tileSize;
        
        public Rectangle SelectedRegion { get; private set; }
        
        public TilesetRegionDialog(ExternView externView, string tilesetName, int entityWidth, int entityHeight,
                                   int initialPixelX = 0, int initialPixelY = 0, int initialPixelW = 0, int initialPixelH = 0,
                                   int tileSize = 32, bool snapToGrid = true, bool showGrid = false) {
            InitializeComponent();

            _externView  = externView;
            _tilesetName = tilesetName;
            _entityWidth = entityWidth;
            _entityHeight = entityHeight;
            _tileSize = tileSize;

            this.Text = $"Select Region - {tilesetName}";

            // Enable region selection mode
            textureViewer.RegionSelectionMode = true;
            textureViewer.SnapToGrid = snapToGrid;
            textureViewer.ShowGrid   = showGrid;

            // Initialise snap controls
            numericUpDownGridSize.Value = Math.Max(1, Math.Min(512, tileSize));
            checkBoxSnapToGrid.Checked  = snapToGrid;
            checkBoxShowGrid.Checked    = showGrid;

            checkBoxSnapToGrid.CheckedChanged += (s, e) => {
                textureViewer.SnapToGrid      = checkBoxSnapToGrid.Checked;
                numericUpDownGridSize.Enabled  = checkBoxSnapToGrid.Checked;
                UpdateSuggestion();
            };
            numericUpDownGridSize.ValueChanged += (s, e) => {
                _tileSize = (int)numericUpDownGridSize.Value;
                textureViewer.TileSize = _tileSize;
                UpdateSuggestion();
            };
            checkBoxShowGrid.CheckedChanged += (s, e) => {
                textureViewer.ShowGrid = checkBoxShowGrid.Checked;
            };

            LoadTilesetTexture();

            // Restore previous selection (if any)
            if (initialPixelW > 0 && initialPixelH > 0)
                textureViewer.SetInitialRegion(initialPixelX, initialPixelY, initialPixelW, initialPixelH);

            UpdateRegionLabel();
        }
        
        private void LoadTilesetTexture() {
            // Find tileset index
            int count = CExternsEditor.GetTilesetCount();
            int tilesetIndex = -1;
            
            for (int i = 0; i < count; i++) {
                CExternsEditor.TilesetInfoStruct tilesetInfo = new CExternsEditor.TilesetInfoStruct();
                bool result = CExternsEditor.GetTilesetAt(i, out tilesetInfo);
                
                if (result) {
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
                this.Load += (s, e) => this.Close();
                return;
            }
            
            // Load texture data
            CExternsEditor.TextureDataStruct textureData = new CExternsEditor.TextureDataStruct();
            CExternsEditor.TilesetInfoStruct tileset = new CExternsEditor.TilesetInfoStruct();
            
            bool loadResult = CExternsEditor.GetTilesetAt(tilesetIndex, out tileset);
            if (loadResult) {
                string texturePath = Marshal.PtrToStringAnsi(tileset.texturePath) ?? "";
                CExternsEditor.GetTextureData(texturePath, out textureData);
                
                textureViewer.SetTextureData(textureData, _tileSize);
                UpdateSuggestion();
            }
        }
        
        private void UpdateRegionLabel() {
            Rectangle region = textureViewer.SelectedRegionInPixels;
            if (region != Rectangle.Empty) {
                labelRegion.Text = $"Selected: ({region.X}, {region.Y})  {region.Width}×{region.Height} px";
            } else {
                labelRegion.Text = "Selected: None";
            }
        }

        private void UpdateSuggestion() {
            if (_entityWidth > 0 && _entityHeight > 0) {
                if (checkBoxSnapToGrid.Checked && _tileSize > 0) {
                    int sugW = ((_entityWidth  + _tileSize - 1) / _tileSize) * _tileSize;
                    int sugH = ((_entityHeight + _tileSize - 1) / _tileSize) * _tileSize;
                    labelSuggestion.Text = $"Suggested: {sugW}×{sugH} px  ({sugW / _tileSize}×{sugH / _tileSize} tiles of {_tileSize}px)";
                } else {
                    labelSuggestion.Text = $"Entity size: {_entityWidth}×{_entityHeight} px";
                }
            } else {
                labelSuggestion.Text = "";
            }
        }
        
        private void buttonOK_Click(object sender, EventArgs e) {
            SelectedRegion = textureViewer.SelectedRegionInPixels;

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
