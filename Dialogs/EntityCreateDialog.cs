using System.Runtime.InteropServices;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class EntityCreateDialog : Form {

        public enum PropertyType { Int, Float, String, Bool, Color }

        public class CustomProperty {
            public string Name    { get; set; } = "";
            public PropertyType Type { get; set; } = PropertyType.String;
            public string Default { get; set; } = "";
        }

        public string[] Tags => textBoxTags.Text
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        private readonly ExternView _externView;
        private Rectangle _currentRegion  = Rectangle.Empty;  // stored in pixels
        private int       _currentTileSize = 32;
        private string    _selectedPivot   = "BottomCenter";
        private bool      _isEditMode      = false;

        public EntityCreateDialog(ExternView externView) {
            InitializeComponent();
            _externView = externView;

            LoadAvailableTilemaps();
            LoadAvailableClasses();
            UpdateRegionLabel();
            HighlightPivotButton(_selectedPivot);

            comboBoxTilemap.SelectedIndexChanged += ComboBoxTilemap_SelectedIndexChanged;
            buttonSelectRegion.Click             += ButtonSelectRegion_Click;
            checkBoxHitbox.CheckedChanged        += (s, e) => panelHitbox.Enabled = checkBoxHitbox.Checked;
            listViewProperties.SelectedIndexChanged += (s, e) =>
                buttonRemoveProperty.Enabled = listViewProperties.SelectedItems.Count > 0;
        }

        private void LoadAvailableTilemaps() {
            comboBoxTilemap.Items.Clear();
            int count = _externView.GetTilesetCount();
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct info = new Externs.TilesetInfoStruct();
                if (_externView.GetTilesetAt(i, out info) != 0) {
                    string name = Marshal.PtrToStringAnsi(info.name) ?? "";
                    if (!string.IsNullOrEmpty(name)) comboBoxTilemap.Items.Add(name);
                }
            }
            if (comboBoxTilemap.Items.Count > 0) comboBoxTilemap.SelectedIndex = 0;
            UpdateCurrentTileSize();
        }

        private void UpdateCurrentTileSize() {
            // Tile size is no longer stored on TilesetInfoStruct; _currentTileSize keeps its default (32).
        }

        private void ComboBoxTilemap_SelectedIndexChanged(object? sender, EventArgs e) {
            _currentRegion = Rectangle.Empty;
            UpdateRegionLabel();
            UpdateCurrentTileSize();
        }

        private void LoadAvailableClasses() {
            comboBoxClass.Items.Clear();
            comboBoxClass.Items.Add("(none)");
            foreach (var cls in new[] { "Actor", "NPC", "Enemy", "Player", "Trigger", "Item", "Projectile", "Decoration" })
                comboBoxClass.Items.Add(cls);
            comboBoxClass.SelectedIndex = 0;
        }

        private void UpdateRegionLabel() {
            if (_currentRegion == Rectangle.Empty) {
                labelRegionInfo.Text = "No region selected";
                return;
            }
            labelRegionInfo.Text =
                $"({_currentRegion.X}, {_currentRegion.Y})  " +
                $"{_currentRegion.Width}×{_currentRegion.Height} px";
        }

        private void ButtonSelectRegion_Click(object? sender, EventArgs e) {
            if (comboBoxTilemap.SelectedItem == null) {
                MessageBox.Show("Please select a tilemap first.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var dialog = new TilesetRegionDialog(
                _externView,
                comboBoxTilemap.SelectedItem.ToString() ?? "",
                (int)numericUpDownWidth.Value,
                (int)numericUpDownHeight.Value,
                _currentRegion.X, _currentRegion.Y,
                _currentRegion.Width, _currentRegion.Height,
                _currentTileSize);

            if (dialog.ShowDialog(this) == DialogResult.OK) {
                _currentRegion = dialog.SelectedRegion;  // already in pixels
                UpdateRegionLabel();
                // Auto-fill width/height directly from pixel dimensions
                numericUpDownWidth.Value  = Math.Max(numericUpDownWidth.Minimum,
                    Math.Min(numericUpDownWidth.Maximum,  _currentRegion.Width));
                numericUpDownHeight.Value = Math.Max(numericUpDownHeight.Minimum,
                    Math.Min(numericUpDownHeight.Maximum, _currentRegion.Height));
            }
        }

        private void PivotButton_Click(object? sender, EventArgs e) {
            if (sender is Button btn && btn.Tag is string pivot) {
                _selectedPivot = pivot;
                HighlightPivotButton(pivot);
            }
        }

        private void HighlightPivotButton(string pivot) {
            var all = new[] {
                btnPivotTL, btnPivotTC, btnPivotTR,
                btnPivotML, btnPivotMC, btnPivotMR,
                btnPivotBL, btnPivotBC, btnPivotBR };
            foreach (var b in all) {
                b.BackColor = b.Tag?.ToString() == pivot
                    ? System.Drawing.Color.FromArgb(0, 120, 215)
                    : System.Drawing.SystemColors.Control;
            }
        }

        private void buttonAddProperty_Click(object? sender, EventArgs e) {
            using var dlg = new PropertyDefinitionDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK) {
                var item = new ListViewItem(dlg.PropertyName);
                item.SubItems.Add(dlg.PropertyType.ToString());
                item.SubItems.Add(dlg.DefaultValue);
                item.Tag = new CustomProperty {
                    Name    = dlg.PropertyName,
                    Type    = dlg.PropertyType,
                    Default = dlg.DefaultValue
                };
                listViewProperties.Items.Add(item);
            }
        }

        private void buttonRemoveProperty_Click(object? sender, EventArgs e) {
            foreach (ListViewItem item in listViewProperties.SelectedItems)
                listViewProperties.Items.Remove(item);
            buttonRemoveProperty.Enabled = false;
        }

        private void buttonCreate_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(textBoxName.Text)) {
                MessageBox.Show("Please enter an entity name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (comboBoxTilemap.SelectedItem == null) {
                MessageBox.Show("Please select a tilemap.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            int width  = (int)numericUpDownWidth.Value;
            int height = (int)numericUpDownHeight.Value;

            try {
                var data = new Externs.EntityDataStruct {
                    width       = width,
                    height      = height,
                    tilesetName = Marshal.StringToHGlobalAnsi(comboBoxTilemap.SelectedItem.ToString() ?? ""),
                    regionX      = _currentRegion.X,
                    regionY      = _currentRegion.Y,
                    regionWidth  = _currentRegion.Width,
                    regionHeight = _currentRegion.Height,
                    pivotX      = PivotToFloats(_selectedPivot).X,
                    pivotY      = PivotToFloats(_selectedPivot).Y
                };

                string? error = _isEditMode
                    ? _externView.EditEntity(textBoxName.Text.Trim(), ref data)
                    : _externView.CreateEntity(textBoxName.Text.Trim(), ref data);
                if (error != null) {
                    MessageBox.Show(error, _isEditMode ? "Entity Edit Error" : "Entity Creation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) {
                MessageBox.Show($"Error creating entity: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static PointF PivotToFloats(string pivot) => pivot switch {
            "TopLeft"      => new PointF(0.0f, 0.0f),
            "TopCenter"    => new PointF(0.5f, 0.0f),
            "TopRight"     => new PointF(1.0f, 0.0f),
            "MiddleLeft"   => new PointF(0.0f, 0.5f),
            "MiddleCenter" => new PointF(0.5f, 0.5f),
            "MiddleRight"  => new PointF(1.0f, 0.5f),
            "BottomLeft"   => new PointF(0.0f, 1.0f),
            "BottomCenter" => new PointF(0.5f, 1.0f),
            "BottomRight"  => new PointF(1.0f, 1.0f),
            _              => new PointF(0.5f, 1.0f)
        };

        public static string FloatsToPivot(float x, float y) {
            // Snap to the nearest of {0, 0.5, 1} to handle floating-point drift from C++
            float sx = x < 0.25f ? 0f : x < 0.75f ? 0.5f : 1f;
            float sy = y < 0.25f ? 0f : y < 0.75f ? 0.5f : 1f;
            string col = sx == 0f ? "Left"   : sx == 0.5f ? "Center" : "Right";
            string row = sy == 0f ? "Top"    : sy == 0.5f ? "Middle" : "Bottom";
            return row + col;
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Pre-populate the dialog with data from an existing entity for editing.
        /// </summary>
        public void Populate(EntitiesDialog.EntityEntry entry) {
            textBoxName.Text = entry.Name;
            textBoxName.ReadOnly = true;

            numericUpDownWidth.Value  = Math.Clamp(entry.Width,  (int)numericUpDownWidth.Minimum,  (int)numericUpDownWidth.Maximum);
            numericUpDownHeight.Value = Math.Clamp(entry.Height, (int)numericUpDownHeight.Minimum, (int)numericUpDownHeight.Maximum);

            for (int i = 0; i < comboBoxTilemap.Items.Count; i++) {
                if ((comboBoxTilemap.Items[i]?.ToString() ?? "") == entry.TilemapName) {
                    comboBoxTilemap.SelectedIndex = i;
                    break;
                }
            }

            // entry.TileX/Y/Width/Height are pixel coords from C++
            _currentRegion = new Rectangle(
                entry.TileX,
                entry.TileY,
                Math.Max(1, entry.TileWidth),
                Math.Max(1, entry.TileHeight)
            );
            UpdateRegionLabel();

            _selectedPivot = string.IsNullOrEmpty(entry.PivotName)
                ? FloatsToPivot(entry.PivotX, entry.PivotY)
                : entry.PivotName;
            HighlightPivotButton(_selectedPivot);

            _isEditMode       = true;
            this.Text         = "Edit Entity";
            buttonCreate.Text = "Save";
        }
    }
}
