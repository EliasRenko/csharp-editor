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
        private bool _suppressInstanceSync = false;

        public string SelectedEntityName => _selectedEntityName;
        public bool HasSelection => !string.IsNullOrEmpty(_selectedEntityName);

        public event EventHandler<string>? SelectionChanged;

        public EntitySelector() {
            InitializeComponent();
            listBoxEntities.SelectedIndexChanged  += ListBoxEntities_SelectedIndexChanged;
            listBoxInstances.SelectedIndexChanged += ListBoxInstances_SelectedIndexChanged;
            tabControl.SelectedIndexChanged       += TabControl_SelectedIndexChanged;
        }

        public void SetExternView(ExternView externView) {
            _externView = externView;
            LoadEntities();
        }

        /// <summary>
        /// Sets the active entity layer and reloads both tabs.
        /// </summary>
        public void SetLayer(string layerName) {
            _externView?.DeselectEntity();
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

                string defName = Marshal.PtrToStringAnsi(data.name) ?? "";

                // Tileset filter: cross-reference the entity def's tileset
                if (!string.IsNullOrEmpty(_tilesetFilter)) {
                    Externs.EntityDataStruct defData = new Externs.EntityDataStruct();
                    _externView.GetEntity(defName, out defData);
                    string defTileset = Marshal.PtrToStringAnsi(defData.tilesetName) ?? "";
                    if (defTileset != _tilesetFilter) continue;
                }

                listBoxInstances.Items.Add(new InstanceListItem {
                    Uid     = Marshal.PtrToStringAnsi(data.uid)  ?? "",
                    DefName = defName,
                    X       = data.x,
                    Y       = data.y
                });
                visible++;
            }

            labelInstanceCount.Text = $"Instances: {visible}";
        }

        private void TabControl_SelectedIndexChanged(object? sender, EventArgs e) {
            if (tabControl.SelectedTab == tabPageInstances) {
                LoadInstances();
            }
        }

        /// <summary>
        /// Reloads placed instances while preserving the currently selected UID.
        /// </summary>
        public void ReloadInstancesKeepSelection() {
            string? currentUid = (listBoxInstances.SelectedItem as InstanceListItem)?.Uid;
            LoadInstances();
            if (!string.IsNullOrEmpty(currentUid)) {
                _suppressInstanceSync = true;
                try {
                    for (int i = 0; i < listBoxInstances.Items.Count; i++) {
                        if (listBoxInstances.Items[i] is InstanceListItem item && item.Uid == currentUid) {
                            listBoxInstances.SelectedIndex = i;
                            break;
                        }
                    }
                } finally {
                    _suppressInstanceSync = false;
                }
            }
        }

        /// <summary>
        /// Switches to the Instances tab and highlights the item matching <paramref name="uid"/>.
        /// Does not trigger a backend selection call.
        /// </summary>
        public void SelectInstanceByUid(string uid) {
            if (string.IsNullOrEmpty(uid)) return;

            // Switch to instances tab (LoadInstances is called by the tab-change handler)
            if (tabControl.SelectedTab != tabPageInstances)
                tabControl.SelectedTab = tabPageInstances;

            _suppressInstanceSync = true;
            try {
                for (int i = 0; i < listBoxInstances.Items.Count; i++) {
                    if (listBoxInstances.Items[i] is InstanceListItem item && item.Uid == uid) {
                        listBoxInstances.SelectedIndex = i;
                        return;
                    }
                }
                // No match — clear selection without triggering a deselect call
                listBoxInstances.ClearSelected();
            } finally {
                _suppressInstanceSync = false;
            }
        }

        private void ListBoxInstances_SelectedIndexChanged(object? sender, EventArgs e) {
            if (_suppressInstanceSync) return;
            if (_externView == null) return;
            if (listBoxInstances.SelectedItem is not InstanceListItem item) return;
            if (!string.IsNullOrEmpty(_currentLayerName))
                _externView.SelectEntityInLayerByUID(_currentLayerName, item.Uid);
            else
                _externView.SelectEntityByUID(item.Uid);
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

        private class InstanceListItem {
            public string Uid     { get; set; } = "";
            public string DefName { get; set; } = "";
            public int    X       { get; set; }
            public int    Y       { get; set; }

            public override string ToString() => $"{DefName} @ ({X}, {Y})";
        }
    }
}
