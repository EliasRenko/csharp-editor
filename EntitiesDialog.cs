using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace csharp_editor {
    public partial class EntitiesDialog : Form {
        
        public class EntityEntry {
            public string Name { get; set; } = "";
            public int Width { get; set; } = 32;
            public int Height { get; set; } = 32;
            public string TilemapName { get; set; } = "";
            
            // Tileset region (in tile coordinates)
            public int TileX { get; set; } = 0;
            public int TileY { get; set; } = 0;
            public int TileWidth { get; set; } = 1;
            public int TileHeight { get; set; } = 1;
            
            public override string ToString() {
                return $"{Name} ({Width}x{Height}) - Tilemap: {TilemapName} - Region: ({TileX},{TileY}) {TileWidth}×{TileHeight}";
            }
        }

        public string SelectedEntityName { get; private set; } = "";
        
        private List<EntityEntry> _entities = new List<EntityEntry>();
        private ExternView _externView;
        private Action<string>? _onEntitySelected;
        private Rectangle _currentRegion = new Rectangle(0, 0, 1, 1); // Default region in tiles

        public EntitiesDialog(ExternView externView, Action<string>? onEntitySelected = null) {
            InitializeComponent();
            _externView = externView;
            _onEntitySelected = onEntitySelected;
            
            buttonSelectRegion.Click += ButtonSelectRegion_Click;
            
            LoadExistingEntities();
            LoadAvailableTilemaps();
            UpdateRegionLabel();
        }
        
        private void UpdateRegionLabel() {
            labelRegionInfo.Text = $"Region: ({_currentRegion.X},{_currentRegion.Y}) {_currentRegion.Width}×{_currentRegion.Height} tiles";
        }
        
        private void ButtonSelectRegion_Click(object? sender, EventArgs e) {
            if (comboBoxTilemap.SelectedItem == null) {
                MessageBox.Show("Please select a tilemap first.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string tilesetName = comboBoxTilemap.SelectedItem.ToString() ?? "";
            int entityWidth = (int)numericUpDownWidth.Value;
            int entityHeight = (int)numericUpDownHeight.Value;
            
            using (TilesetRegionDialog dialog = new TilesetRegionDialog(
                _externView, 
                tilesetName, 
                entityWidth, 
                entityHeight,
                _currentRegion.X,
                _currentRegion.Y,
                _currentRegion.Width,
                _currentRegion.Height)) {
                
                if (dialog.ShowDialog() == DialogResult.OK) {
                    _currentRegion = dialog.SelectedRegion;
                    UpdateRegionLabel();
                }
            }
        }

        private void LoadExistingEntities() {
            listBoxEntities.Items.Clear();
            _entities.Clear();
            
            // TODO: Get entities from C++ when the API is ready
            // For now, we'll start with an empty list
        }

        private void LoadAvailableTilemaps() {
            comboBoxTilemap.Items.Clear();
            
            // Get tilesets from C++
            int count = _externView.GetTilesetCount();
            
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
                int result = _externView.GetTilesetAt(i, out tilesetInfo);
                
                if (result != 0) {
                    string tilesetName = Marshal.PtrToStringAnsi(tilesetInfo.name) ?? "";
                    
                    if (!string.IsNullOrEmpty(tilesetName)) {
                        comboBoxTilemap.Items.Add(tilesetName);
                    }
                }
            }
            
            if (comboBoxTilemap.Items.Count > 0) {
                comboBoxTilemap.SelectedIndex = 0;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e) {
            // Validate input
            if (string.IsNullOrWhiteSpace(textBoxName.Text)) {
                MessageBox.Show("Please enter an entity name.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxTilemap.SelectedItem == null) {
                MessageBox.Show("Please select a tilemap.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int width = (int)numericUpDownWidth.Value;
            int height = (int)numericUpDownHeight.Value;
            
            if (width <= 0 || height <= 0) {
                MessageBox.Show("Width and height must be greater than 0.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create entity entry
            EntityEntry newEntity = new EntityEntry {
                Name = textBoxName.Text.Trim(),
                Width = width,
                Height = height,
                TilemapName = comboBoxTilemap.SelectedItem.ToString() ?? "",
                TileX = _currentRegion.X,
                TileY = _currentRegion.Y,
                TileWidth = _currentRegion.Width,
                TileHeight = _currentRegion.Height
            };

            // TODO: Send to C++ when the API is ready
            try {
                // For now, just add to local list
                _entities.Add(newEntity);
                listBoxEntities.Items.Add(newEntity);
                
                // Clear input fields
                textBoxName.Clear();
                numericUpDownWidth.Value = 32;
                numericUpDownHeight.Value = 32;
                if (comboBoxTilemap.Items.Count > 0) {
                    comboBoxTilemap.SelectedIndex = 0;
                }
                _currentRegion = new Rectangle(0, 0, 1, 1);
                UpdateRegionLabel();
                
                MessageBox.Show($"Entity '{newEntity.Name}' has been created successfully.", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) {
                MessageBox.Show($"Error creating entity: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e) {
            if (listBoxEntities.SelectedIndex >= 0) {
                EntityEntry entity = _entities[listBoxEntities.SelectedIndex];
                
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete entity '{entity.Name}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes) {
                    int index = listBoxEntities.SelectedIndex;
                    
                    // TODO: Remove from C++ when the API is ready
                    
                    _entities.RemoveAt(index);
                    listBoxEntities.Items.RemoveAt(index);
                }
            } else {
                MessageBox.Show("Please select an entity to delete.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonUse_Click(object sender, EventArgs e) {
            if (listBoxEntities.SelectedIndex >= 0) {
                EntityEntry selectedEntity = _entities[listBoxEntities.SelectedIndex];
                
                try {
                    // TODO: Set current entity in C++ when the API is ready
                    
                    SelectedEntityName = selectedEntity.Name;
                    _onEntitySelected?.Invoke(selectedEntity.Name);
                    
                    MessageBox.Show($"Entity '{selectedEntity.Name}' is now selected.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) {
                    MessageBox.Show($"Error selecting entity: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("Please select an entity from the list.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void listBoxEntities_SelectedIndexChanged(object sender, EventArgs e) {
            // Enable/disable buttons based on selection
            bool hasSelection = listBoxEntities.SelectedIndex >= 0;
            buttonDelete.Enabled = hasSelection;
            buttonUse.Enabled = hasSelection;
        }
    }
}
