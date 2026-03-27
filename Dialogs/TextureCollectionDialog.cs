using System.Runtime.InteropServices;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class TextureCollectionDialog : Form {
        
        public string SelectedTilesetName { get; private set; } = "";
        
        public class TilesetEntry {
            public string Name { get; set; } = "";
            public string ImagePath { get; set; } = "";
            
            public override string ToString() {
                return $"{Name} - {Path.GetFileName(ImagePath)}";
            }
        }

        private List<TilesetEntry> _tilesets = new List<TilesetEntry>();
        private ExternView _externView;
        private Action<string>? _onTilesetSelected;
        private Action? _onTilesetDeleted;

        public TextureCollectionDialog(ExternView externView, Action<string>? onTilesetSelected = null, Action? onTilesetDeleted = null) {
            InitializeComponent();
            _externView = externView;
            _onTilesetSelected = onTilesetSelected;
            _onTilesetDeleted = onTilesetDeleted;
            LoadExistingTilesets();
        }

        private void LoadExistingTilesets() {
            listBoxTilesets.Items.Clear();
            _tilesets.Clear();
            labelTilesetMeta.Text = "";
            labelTilesetPath.Text = "";
            
            // Get count of tilesets from C++
            int count = CExternsEditor.GetTilesetCount();
            
            // Loop through and get each tileset info
            for (int i = 0; i < count; i++) {
                CExternsEditor.TilesetInfoStruct tilesetInfo = new CExternsEditor.TilesetInfoStruct();
                bool result = CExternsEditor.GetTilesetAt(i, out tilesetInfo);
                
                if (result) {
                    string tilesetName = Marshal.PtrToStringAnsi(tilesetInfo.name) ?? "";
                    
                    if (!string.IsNullOrEmpty(tilesetName)) {
                    
                        string texturePath = Marshal.PtrToStringAnsi(tilesetInfo.texturePath) ?? "";
                        
                        TilesetEntry entry = new TilesetEntry {
                            Name = tilesetName,
                            ImagePath = texturePath
                        };
                        
                        _tilesets.Add(entry);
                        listBoxTilesets.Items.Add(entry);
                    }
                }
            }
        }

        private void listBoxTilesets_SelectedIndexChanged(object sender, EventArgs e) {
            if (listBoxTilesets.SelectedItem is TilesetEntry entry) {
                CExternsEditor.TilesetInfoStruct tilesetInfo = new CExternsEditor.TilesetInfoStruct();
                bool result = CExternsEditor.GetTileset(entry.Name, out tilesetInfo);

                if (result && !string.IsNullOrEmpty(entry.ImagePath)) {
                    CExternsEditor.TextureDataStruct textureData = new CExternsEditor.TextureDataStruct();
                    CExternsEditor.GetTextureData(entry.ImagePath, out textureData);
                    textureViewer.SetTextureData(textureData, 0);

                    labelTilesetMeta.Text =
                        $"Texture: {textureData.Width} × {textureData.Height} px";
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

        private void buttonImport_Click(object sender, EventArgs e) {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Filter = "Image Files (*.png;*.tga;*.jpg;*.bmp)|*.png;*.tga;*.jpg;*.bmp|All Files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.Title = "Select Texture Image";

                if (dialog.ShowDialog() != DialogResult.OK) return;

                string imagePath = dialog.FileName;
                string name = Path.GetFileNameWithoutExtension(imagePath);

                bool success = CExternsEditor.CreateTileset(imagePath, name);
                if (!success) {
                    string error = _externView.GetLastErrorMessage();
                    MessageBox.Show($"Failed to import tileset '{name}':\n{error}", "Import Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoadExistingTilesets();

                // Select the newly imported entry
                for (int i = 0; i < listBoxTilesets.Items.Count; i++) {
                    if (listBoxTilesets.Items[i] is TilesetEntry e2 && e2.Name == name) {
                        listBoxTilesets.SelectedIndex = i;
                        break;
                    }
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
            bool success = CExternsEditor.CreateTileset(old.ImagePath, newName);
            if (!success) {
                string error = _externView.GetLastErrorMessage();
                MessageBox.Show($"Rename failed: {error}", "Error",
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
            if (listBoxTilesets.SelectedItem is not TilesetEntry entry) {
                MessageBox.Show("Please select a tileset to delete.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to delete tileset '{entry.Name}'?\nAll layers using this tileset will also be removed.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            bool success = CExternsEditor.DeleteTileset(entry.Name);
            if (!success) {
                string error = _externView.GetLastErrorMessage();
                MessageBox.Show($"Failed to delete tileset '{entry.Name}':\n{error}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _tilesets.Remove(entry);
            listBoxTilesets.Items.Remove(entry);
            textureViewer.Clear();
            labelTilesetMeta.Text = "";
            labelTilesetPath.Text = "";
            _onTilesetDeleted?.Invoke();
        }

        private void buttonUse_Click(object sender, EventArgs e) {
            if (listBoxTilesets.SelectedItem is TilesetEntry selectedTileset) {
                
                try {
                    bool result = CExternsEditor.SetActiveTileset(selectedTileset.Name);
                    
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
