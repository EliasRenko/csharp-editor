using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using csharp_editor.Dialogs;

namespace csharp_editor.UserControls {
    public partial class EntitySelector : UserControl {
        private ExternView? _externView;
        private string _selectedEntityName = "";
        private string _currentLayerName = "";
        private string? _tilesetFilter = null;
        private int _currentBatchIndex = -1; // -1 = all batches

        public string SelectedEntityName => _selectedEntityName;
        public bool HasSelection => !string.IsNullOrEmpty(_selectedEntityName);

        public event EventHandler<string>? SelectionChanged;

        public EntitySelector() {
            InitializeComponent();
            listBoxEntities.SelectedIndexChanged += ListBoxEntities_SelectedIndexChanged;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        public void SetExternView(ExternView externView) {
            _externView = externView;
            LoadEntities();
        }

        /// <summary>
        /// Sets the active entity layer and reloads both tabs.
        /// </summary>
        public void SetLayer(string layerName) {
            _currentLayerName = layerName;
            _tilesetFilter = null;
            _currentBatchIndex = -1;
            LoadEntities();
            LoadInstances();
        }

        /// <summary>
        /// Filters both tabs to a specific tileset batch. Expects SetLayer to have been called first.
        /// </summary>
        public void SetBatchFilter(string? tilesetFilter, int batchIndex) {
            _tilesetFilter = tilesetFilter;
            _currentBatchIndex = batchIndex;
            LoadEntities(_tilesetFilter);
            LoadInstances();
        }

        /// <summary>
        /// Reloads entity definitions, optionally filtered by tileset.
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

                if (string.IsNullOrEmpty(name)) continue;
                if (!string.IsNullOrEmpty(tilesetFilter) && tilesetName != tilesetFilter) continue;

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

            if (listBoxEntities.Items.Count > 0) listBoxEntities.SelectedIndex = 0;

            labelCount.Text = $"Entities: {visibleCount}";
        }

        /// <summary>
        /// Reloads placed instances for the current layer, filtered by _tilesetFilter when set.
        /// </summary>
        public void LoadInstances() {
            listBoxInstances.Items.Clear();

            if (_externView == null || string.IsNullOrEmpty(_currentLayerName)) {
                labelInstanceCount.Text = "Instances: —";
                return;
            }

            int count = _externView.GetEntityLayerInstanceCount(_currentLayerName);
            int visible = 0;

            for (int i = 0; i < count; i++) {
                Externs.EntityStruct data = new Externs.EntityStruct();
                if (_externView.GetEntityLayerInstanceAt(_currentLayerName, _currentBatchIndex, i, out data) == 0) continue;

                string defName = Marshal.PtrToStringAnsi(data.defName) ?? "";

                // Tileset filter: cross-reference the entity def's tileset
                if (!string.IsNullOrEmpty(_tilesetFilter)) {
                    Externs.EntityDataStruct defData = new Externs.EntityDataStruct();
                    _externView.GetEntity(defName, out defData);
                    string defTileset = Marshal.PtrToStringAnsi(defData.tilesetName) ?? "";
                    if (defTileset != _tilesetFilter) continue;
                }

                listBoxInstances.Items.Add($"{defName} @ ({data.x}, {data.y})");
                visible++;
            }

            labelInstanceCount.Text = $"Instances: {visible}";
        }

        private void TabControl_SelectedIndexChanged(object? sender, EventArgs e) {
            if (tabControl.SelectedTab == tabPageInstances) {
                LoadInstances();
            }
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

            public override string ToString() => DisplayText;
        }
    }
}
