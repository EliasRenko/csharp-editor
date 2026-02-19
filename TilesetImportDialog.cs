using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace csharp_editor {
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

        private void buttonBrowse_Click(object sender, EventArgs e) {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Filter = "Image Files (*.png;*.tga;*.jpg;*.bmp)|*.png;*.tga;*.jpg;*.bmp|All Files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.Title = "Select Tileset Image";
                
                if (dialog.ShowDialog() == DialogResult.OK) {
                    textBoxImagePath.Text = dialog.FileName;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e) {
            // Validate input
            if (string.IsNullOrWhiteSpace(textBoxName.Text)) {
                MessageBox.Show("Please enter a tileset name.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxImagePath.Text)) {
                MessageBox.Show("Please select an image file.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(textBoxImagePath.Text)) {
                MessageBox.Show("The specified image file does not exist.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int tileSize = (int)numericUpDownTileSize.Value;
            if (tileSize <= 0) {
                MessageBox.Show("Tile size must be greater than 0.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create tileset entry
            TilesetEntry newTileset = new TilesetEntry {
                Name = textBoxName.Text.Trim(),
                ImagePath = textBoxImagePath.Text.Trim(),
                TileSize = tileSize
            };

            // Send to C++
            try {
                var error = _externView.CreateTileset(newTileset.ImagePath, newTileset.Name, newTileset.TileSize);
                if (error != null) {
                    MessageBox.Show(error, "Tileset Creation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                
                // Add to list
                _tilesets.Add(newTileset);
                listBoxTilesets.Items.Add(newTileset);
                
                // Clear input fields
                textBoxName.Clear();
                textBoxImagePath.Clear();
                numericUpDownTileSize.Value = 16;
                
                MessageBox.Show($"Tileset '{newTileset.Name}' has been imported successfully.", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) {
                MessageBox.Show($"Error importing tileset: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
