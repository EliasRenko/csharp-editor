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
            labelTilesetMeta.Text = "";
            labelTilesetPath.Text = "";
            
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
            if (listBoxTilesets.SelectedItem is TilesetEntry entry) {
                Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
                int result = _externView.GetTileset(entry.Name, out tilesetInfo);

                if (result != 0 && !string.IsNullOrEmpty(entry.ImagePath)) {
                    Externs.TextureDataStruct textureData = new Externs.TextureDataStruct();
                    _externView.GetTextureData(entry.ImagePath, out textureData);
                    textureViewer.SetTextureData(textureData, tilesetInfo);

                    int totalTiles = tilesetInfo.tilesPerRow * tilesetInfo.tilesPerCol;
                    labelTilesetMeta.Text =
                        $"Tile size: {tilesetInfo.tileSize} px   ·   " +
                        $"Texture: {textureData.Width} × {textureData.Height} px   ·   " +
                        $"Grid: {tilesetInfo.tilesPerRow} × {tilesetInfo.tilesPerCol}   ·   " +
                        $"Total tiles: {totalTiles}";
                    labelTilesetPath.Text = $"Path: {entry.ImagePath}";
                } else {
                    textureViewer.Clear();
                    labelTilesetMeta.Text = "";
                    labelTilesetPath.Text = "";
                }
            } else {
                textureViewer.Clear();
                labelTilesetMeta.Text = "";
                labelTilesetPath.Text = "";
            }
        }

        private void buttonNew_Click(object sender, EventArgs e) {
            using (var createDialog = new TilesetCreateDialog(_externView)) {
                if (createDialog.ShowDialog(this) == DialogResult.OK) {
                    LoadExistingTilesets();
                }
            }
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e) {
            string filter = textBoxFilter.Text.Trim();
            listBoxTilesets.BeginUpdate();
            listBoxTilesets.Items.Clear();
            foreach (var t in _tilesets) {
                if (string.IsNullOrEmpty(filter) ||
                    t.Name.Contains(filter, StringComparison.OrdinalIgnoreCase))
                    listBoxTilesets.Items.Add(t);
            }
            listBoxTilesets.EndUpdate();
        }

        // ── Inline rename ────────────────────────────────────────────────────────
        private TextBox? _renameBox;

        private void listBoxTilesets_DoubleClick(object sender, EventArgs e) {
            int idx = listBoxTilesets.SelectedIndex;
            if (idx < 0 || listBoxTilesets.SelectedItem is not TilesetEntry) return;
            TilesetEntry entry = (TilesetEntry)listBoxTilesets.SelectedItem;

            Rectangle itemRect = listBoxTilesets.GetItemRectangle(idx);
            // Translate to form coordinates
            Point loc = listBoxTilesets.PointToScreen(itemRect.Location);
            loc = PointToClient(loc);

            _renameBox = new TextBox {
                Text     = entry.Name,
                Location = loc,
                Size     = new Size(itemRect.Width, itemRect.Height + 2),
                Font     = listBoxTilesets.Font,
                BorderStyle = BorderStyle.FixedSingle,
            };
            _renameBox.SelectAll();
            _renameBox.KeyDown    += RenameBox_KeyDown;
            _renameBox.LostFocus  += RenameBox_LostFocus;
            Controls.Add(_renameBox);
            _renameBox.BringToFront();
            _renameBox.Focus();
        }

        private void CommitRename() {
            if (_renameBox == null) return;

            int idx = listBoxTilesets.SelectedIndex;
            string newName = _renameBox.Text.Trim();

            DestroyRenameBox();

            if (idx < 0 || string.IsNullOrEmpty(newName)) return;
            if (listBoxTilesets.Items[idx] is not TilesetEntry old) return;
            if (newName == old.Name) return;

            // Check for duplicate name
            if (_tilesets.Any(t => t.Name == newName)) {
                MessageBox.Show($"A tileset named '{newName}' already exists.",
                    "Rename Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // C++ has no rename API — recreate under the new name then update local entry
            string? err = _externView.CreateTileset(old.ImagePath, newName, old.TileSize);
            if (err != null) {
                MessageBox.Show($"Rename failed: {err}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            old.Name = newName;
            listBoxTilesets.Items[idx] = old;
            listBoxTilesets.SelectedIndex = idx;
        }

        private void DestroyRenameBox() {
            if (_renameBox == null) return;
            Controls.Remove(_renameBox);
            _renameBox.Dispose();
            _renameBox = null;
        }

        private void RenameBox_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)  { e.SuppressKeyPress = true; CommitRename(); }
            if (e.KeyCode == Keys.Escape) { DestroyRenameBox(); }
        }

        private void RenameBox_LostFocus(object? sender, EventArgs e) {
            CommitRename();
        }

        private void buttonRemove_Click(object sender, EventArgs e) {
            if (listBoxTilesets.SelectedItem is TilesetEntry entry) {
                _tilesets.Remove(entry);
                listBoxTilesets.Items.Remove(entry);
            }
        }

        private void buttonUse_Click(object sender, EventArgs e) {
            if (listBoxTilesets.SelectedItem is TilesetEntry selectedTileset) {
                
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
