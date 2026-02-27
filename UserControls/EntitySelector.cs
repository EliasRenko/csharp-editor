using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace csharp_editor.UserControls {
    public partial class EntitySelector : UserControl {
        private ExternView? _externView;
        private string _selectedEntityName = "";
        
        public string SelectedEntityName => _selectedEntityName;
        public bool HasSelection => !string.IsNullOrEmpty(_selectedEntityName);
        
        // Event for when entity selection changes
        public event EventHandler<string>? SelectionChanged;
        
        public EntitySelector() {
            InitializeComponent();
            listBoxEntities.SelectedIndexChanged += ListBoxEntities_SelectedIndexChanged;
            buttonRefresh.Click += ButtonRefresh_Click;
            buttonManage.Click += ButtonManage_Click;
        }
        
        public void SetExternView(ExternView externView) {
            _externView = externView;
            LoadEntities();
        }
        
        /// <summary>
        /// Loads entities into the list.  If <paramref name="tilesetFilter"/> is
        /// supplied, only entities whose TilemapName equals that string are shown.
        /// </summary>
        public void LoadEntities(string? tilesetFilter = null) {
            listBoxEntities.Items.Clear();
            _selectedEntityName = "";
            
            if (_externView == null) return;
            
            int count = _externView.GetEntityCount();
            int visibleCount = 0;
            
            for (int i = 0; i < count; i++) {
                Externs.EntityDataStruct entityData = new Externs.EntityDataStruct();
                _externView.GetEntityAt(i, out entityData);
                
                string name = Marshal.PtrToStringAnsi(entityData.name) ?? "";
                string tilesetName = Marshal.PtrToStringAnsi(entityData.tilesetName) ?? "";

                if (string.IsNullOrEmpty(name))
                    continue;

                // apply filter if provided
                if (!string.IsNullOrEmpty(tilesetFilter) && tilesetName != tilesetFilter)
                    continue;

                string displayText = string.IsNullOrEmpty(tilesetName)
                    ? $"{name} ({entityData.width}×{entityData.height})"
                    : $"{name} ({entityData.width}×{entityData.height}) - {tilesetName}";

                listBoxEntities.Items.Add(new EntityListItem {
                    Name = name,
                    DisplayText = displayText,
                    Width = entityData.width,
                    Height = entityData.height,
                    TilesetName = tilesetName,
                    RegionX = entityData.regionX,
                    RegionY = entityData.regionY,
                    RegionWidth = entityData.regionWidth,
                    RegionHeight = entityData.regionHeight
                });
                visibleCount++;
            }
            
            if (listBoxEntities.Items.Count > 0) {
                listBoxEntities.SelectedIndex = 0;
            }
            
            labelCount.Text = $"Entities: {visibleCount}";
        }
        
        private void ListBoxEntities_SelectedIndexChanged(object? sender, EventArgs e) {
            if (listBoxEntities.SelectedItem is EntityListItem item) {
                _selectedEntityName = item.Name;
                labelSelected.Text = $"Selected: {item.Name}";
                SelectionChanged?.Invoke(this, _selectedEntityName);
            } else {
                _selectedEntityName = "";
                labelSelected.Text = "Selected: None";
            }
        }
        
        private void ButtonRefresh_Click(object? sender, EventArgs e) {
            LoadEntities();
        }
        
        private void ButtonManage_Click(object? sender, EventArgs e) {
            if (_externView != null) {
                using (EntitiesDialog dialog = new EntitiesDialog(_externView)) {
                    if (dialog.ShowDialog() == DialogResult.OK) {
                        LoadEntities();
                    }
                }
            }
        }
        
        private class EntityListItem {
            public string Name { get; set; } = "";
            public string DisplayText { get; set; } = "";
            public int Width { get; set; }
            public int Height { get; set; }
            public string TilesetName { get; set; } = "";
            public int RegionX { get; set; }
            public int RegionY { get; set; }
            public int RegionWidth { get; set; }
            public int RegionHeight { get; set; }
            
            public override string ToString() {
                return DisplayText;
            }
        }
    }
}
