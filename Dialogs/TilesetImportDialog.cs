using System.Runtime.InteropServices;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class TilesetImportDialog : Form {
        
        public string SelectedTilesetName { get; private set; } = "";
        
        public class TilesetEntry {
            public string Name { get; set; } = "";
            public string ImagePath { get; set; } = "";
            public int TileSize { get; set; } = 16;
            
            public override string ToString() {
                return $"{Name} ({TileSize}px) - {Path.GetFileName(ImagePath)}";
            }
        }

        private List<TilesetEntry> _tilesets = new List<TilesetEntry>();
        private ExternView _externView;
        private Action<string>? _onTilesetSelected;

        public TilesetImportDialog(ExternView externView, Action<string>? onTilesetSelected = null) {
            InitializeComponent();
            _externView = externView;
            _onTilesetSelected = onTilesetSelected;
            LoadExistingTilesets();
        }

        private void LoadExistingTilesets() {
            listBoxTilesets.Items.Clear();
            _tilesets.Clear();
            
            // Get count of tilesets from C++
            int count = _externView.GetTilesetCount();
            
            // Loop through and get each tileset info
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
                int result = _externView.GetTilesetAt(i, out tilesetInfo);
                
                if (result != 0) {
                    string tilesetName = Marshal.PtrToStringAnsi(tilesetInfo.name) ?? "";
                    
                    if (!string.IsNullOrEmpty(tilesetName)) {
                    
                        string texturePath = Marshal.PtrToStringAnsi(tilesetInfo.texturePath) ?? "";
                        
                        TilesetEntry entry = new TilesetEntry {
                            Name = tilesetName,
                            ImagePath = texturePath,
                            TileSize = tilesetInfo.tileSize
                        };
                        
                        _tilesets.Add(entry);
                        listBoxTilesets.Items.Add(entry);
                    }
                }
            }
        }

        private void listBoxTilesets_SelectedIndexChanged(object sender, EventArgs e) {
            if (listBoxTilesets.SelectedIndex >= 0) {
                TilesetEntry entry = _tilesets[listBoxTilesets.SelectedIndex];

                Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
                int result = _externView.GetTileset(entry.Name, out tilesetInfo);

                if (result != 0 && !string.IsNullOrEmpty(entry.ImagePath)) {
                    Externs.TextureDataStruct textureData = new Externs.TextureDataStruct();
                    _externView.GetTextureData(entry.ImagePath, out textureData);
                    textureViewer.SetTextureData(textureData, tilesetInfo);
                } else {
                    textureViewer.Clear();
                }
            } else {
                textureViewer.Clear();
            }
        }

        private void buttonNew_Click(object sender, EventArgs e) {
            using (var createDialog = new TilesetCreateDialog(_externView)) {
                if (createDialog.ShowDialog(this) == DialogResult.OK) {
                    LoadExistingTilesets();
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e) {
            if (listBoxTilesets.SelectedIndex >= 0) {
                int index = listBoxTilesets.SelectedIndex;
                _tilesets.RemoveAt(index);
                listBoxTilesets.Items.RemoveAt(index);
            }
        }

        private void buttonUse_Click(object sender, EventArgs e) {
            if (listBoxTilesets.SelectedIndex >= 0) {
                TilesetEntry selectedTileset = _tilesets[listBoxTilesets.SelectedIndex];
                
                try {
                    bool result = _externView.SetActiveTileset(selectedTileset.Name);
                    
                    if (result) {
                        SelectedTilesetName = selectedTileset.Name;
                        _onTilesetSelected?.Invoke(selectedTileset.Name);
                        MessageBox.Show($"Tileset '{selectedTileset.Name}' is now active for drawing.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } else {
                        MessageBox.Show($"Failed to set tileset '{selectedTileset.Name}' as current.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"Error setting current tileset: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("Please select a tileset from the list.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
