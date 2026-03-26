using System.Runtime.InteropServices;
using csharp_editor.Dialogs;
using csharp_editor.Models;

namespace csharp_editor.UserControls {
    public partial class EntityEditor : UserControl {

        public enum PropertyType { Int, Float, String, Bool, Color }

        public class CustomProperty {
            public string       Name    { get; set; } = "";
            public PropertyType Type    { get; set; } = PropertyType.String;
            public string       Default { get; set; } = "";
        }

        /// <summary>Raised after a successful Create or Save operation.</summary>
        public event EventHandler? SaveCompleted;

        private ExternView? _externView;
        private Rectangle   _currentRegion  = Rectangle.Empty;
        private int         _currentTileSize = 32;
        private string      _selectedPivot   = "BottomCenter";
        private bool        _isEditMode      = false;
        private float       _pivotXActual    = 0.5f;
        private float       _pivotYActual    = 1.0f;
        private bool        _suppressPivotSync = false;

        public string[] Tags => textBoxTags.Text
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        public EntityEditor() {
            InitializeComponent();
        }

        /// <summary>Must be called once before the control is used.</summary>
        public void Initialize(ExternView externView) {
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
            numPivotX.ValueChanged += NumPivot_ValueChanged;
            numPivotY.ValueChanged += NumPivot_ValueChanged;
            SyncPivotNumerics(0.5f, 1.0f);

            entityPreviewPanel.Clear();
        }

        /// <summary>Clears all fields and puts the editor in "New Entity" mode.</summary>
        public void ResetForNew() {
            textBoxName.Text      = "";
            textBoxName.ReadOnly  = false;
            textBoxTags.Text      = "";
            numericUpDownWidth.Value  = 32;
            numericUpDownHeight.Value = 32;
            checkBoxFlipX.Checked = false;
            checkBoxFlipY.Checked = false;
            listViewProperties.Items.Clear();
            _currentRegion   = Rectangle.Empty;
            _currentTileSize = 32;
            _selectedPivot   = "BottomCenter";
            _isEditMode      = false;
            UpdateRegionLabel();
            entityPreviewPanel.Clear();
            HighlightPivotButton(_selectedPivot);
            _pivotXActual = 0.5f;
            _pivotYActual = 1.0f;
            SyncPivotNumerics(0.5f, 1.0f);
            buttonSave.Text = "Create";
            comboBoxTilemap.SelectedIndex = 0; // always valid – (No Texture) is index 0
            if (comboBoxClass.Items.Count > 0)   comboBoxClass.SelectedIndex   = 0;
            buttonRemoveProperty.Enabled = false;
            checkBoxHitbox.Checked       = false;
            panelHitbox.Enabled          = false;
        }

        /// <summary>Populates the editor with an existing entity for editing.</summary>
        public void Populate(EntityEntry entry) {
            textBoxName.Text     = entry.Name;
            textBoxName.ReadOnly = true;

            numericUpDownWidth.Value  = Math.Clamp(entry.Width,  (int)numericUpDownWidth.Minimum,  (int)numericUpDownWidth.Maximum);
            numericUpDownHeight.Value = Math.Clamp(entry.Height, (int)numericUpDownHeight.Minimum, (int)numericUpDownHeight.Maximum);

            int tilemapIndex = 0; // default to (No Texture)
            if (!string.IsNullOrEmpty(entry.TilemapName)) {
                for (int i = 0; i < comboBoxTilemap.Items.Count; i++) {
                    if ((comboBoxTilemap.Items[i]?.ToString() ?? "") == entry.TilemapName) {
                        tilemapIndex = i;
                        break;
                    }
                }
            }
            comboBoxTilemap.SelectedIndex = tilemapIndex;

            _currentRegion = new Rectangle(
                entry.TileX, entry.TileY,
                Math.Max(1, entry.TileWidth),
                Math.Max(1, entry.TileHeight)
            );
            UpdateRegionLabel();

            _selectedPivot = string.IsNullOrEmpty(entry.PivotName)
                ? FloatsToPivot(entry.PivotX, entry.PivotY)
                : entry.PivotName;
            HighlightPivotButton(_selectedPivot);
            _pivotXActual = entry.PivotX;
            _pivotYActual = entry.PivotY;
            SyncPivotNumerics(entry.PivotX, entry.PivotY);
            UpdateRegionPreview();

            _isEditMode     = true;
            buttonSave.Text = "Save";
        }

        // ── Private helpers ──────────────────────────────────────────────────────────

        private const string NoTextureOption = "(No Texture)";

        private void LoadAvailableTilemaps() {
            comboBoxTilemap.Items.Clear();
            comboBoxTilemap.Items.Add(NoTextureOption);
            if (_externView == null) { comboBoxTilemap.SelectedIndex = 0; return; }
            int count = _externView.GetTilesetCount();
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct info = new Externs.TilesetInfoStruct();
                if (_externView.GetTilesetAt(i, out info)) {
                    string name = Marshal.PtrToStringAnsi(info.name) ?? "";
                    if (!string.IsNullOrEmpty(name)) comboBoxTilemap.Items.Add(name);
                }
            }
            comboBoxTilemap.SelectedIndex = 0;
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
        private void UpdateRegionPreview() {
            string selectedTilemap = comboBoxTilemap.SelectedItem?.ToString() ?? "";
            if (_externView == null || _currentRegion == Rectangle.Empty ||
                comboBoxTilemap.SelectedItem == null || selectedTilemap == NoTextureOption) {
                entityPreviewPanel.Clear();
                return;
            }

            int count = _externView.GetTilesetCount();
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct info = new Externs.TilesetInfoStruct();
                if (!_externView.GetTilesetAt(i, out info)) continue;
                string name = Marshal.PtrToStringAnsi(info.name) ?? "";
                if (name != selectedTilemap) continue;

                string texturePath = Marshal.PtrToStringAnsi(info.texturePath) ?? "";
                if (string.IsNullOrEmpty(texturePath)) break;

                _externView.GetTextureData(texturePath, out Externs.TextureDataStruct textureData);
                entityPreviewPanel.SetPreview(textureData, _currentRegion, _pivotXActual, _pivotYActual);
                return;
            }
            entityPreviewPanel.Clear();
        }
        private void ComboBoxTilemap_SelectedIndexChanged(object? sender, EventArgs e) {
            _currentRegion = Rectangle.Empty;
            UpdateRegionLabel();
            entityPreviewPanel.Clear();
            bool hasTexture = comboBoxTilemap.SelectedItem?.ToString() != NoTextureOption;
            buttonSelectRegion.Enabled = hasTexture;
        }

        private void ButtonSelectRegion_Click(object? sender, EventArgs e) {
            if (_externView == null) return;
            if (comboBoxTilemap.SelectedItem == null) {
                MessageBox.Show("Please select a tilemap first.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool hasExistingRegion = _currentRegion != Rectangle.Empty;
            using var dialog = new TilesetRegionDialog(
                _externView,
                comboBoxTilemap.SelectedItem.ToString() ?? "",
                (int)numericUpDownWidth.Value,
                (int)numericUpDownHeight.Value,
                _currentRegion.X, _currentRegion.Y,
                _currentRegion.Width, _currentRegion.Height,
                _currentTileSize,
                snapToGrid: !hasExistingRegion,
                showGrid:   false);

            if (dialog.ShowDialog(this) == DialogResult.OK) {
                _currentRegion = dialog.SelectedRegion;
                UpdateRegionLabel();
                UpdateRegionPreview();
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
                var pf = PivotToFloats(pivot);
                _pivotXActual = pf.X;
                _pivotYActual = pf.Y;
                SyncPivotNumerics(pf.X, pf.Y);
                entityPreviewPanel.UpdatePivot(pf.X, pf.Y);
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

        private void SyncPivotNumerics(float x, float y) {
            _suppressPivotSync = true;
            numPivotX.Value = (decimal)Math.Clamp(x, 0f, 1f);
            numPivotY.Value = (decimal)Math.Clamp(y, 0f, 1f);
            _suppressPivotSync = false;
        }

        private void NumPivot_ValueChanged(object? sender, EventArgs e) {
            if (_suppressPivotSync) return;
            _pivotXActual  = (float)numPivotX.Value;
            _pivotYActual  = (float)numPivotY.Value;
            _selectedPivot = FloatsToPivot(_pivotXActual, _pivotYActual);
            HighlightPivotButton(_selectedPivot);
            entityPreviewPanel.UpdatePivot(_pivotXActual, _pivotYActual);
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

        private void buttonSave_Click(object? sender, EventArgs e) {
            if (_externView == null) return;
            if (string.IsNullOrWhiteSpace(textBoxName.Text)) {
                MessageBox.Show("Please enter an entity name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            int width  = (int)numericUpDownWidth.Value;
            int height = (int)numericUpDownHeight.Value;
            bool noTexture = comboBoxTilemap.SelectedItem?.ToString() == NoTextureOption;
            IntPtr tilesetPtr = noTexture
                ? IntPtr.Zero
                : Marshal.StringToHGlobalAnsi(comboBoxTilemap.SelectedItem?.ToString() ?? "");

            try {
                var data = new Externs.EntityDataStruct {
                    width        = width,
                    height       = height,
                    tilesetName  = tilesetPtr,
                    regionX      = noTexture ? 0 : _currentRegion.X,
                    regionY      = noTexture ? 0 : _currentRegion.Y,
                    regionWidth  = noTexture ? 0 : _currentRegion.Width,
                    regionHeight = noTexture ? 0 : _currentRegion.Height,
                    pivotX       = _pivotXActual,
                    pivotY       = _pivotYActual
                };

                bool success = _isEditMode
                    ? _externView.EditEntity(textBoxName.Text.Trim(), ref data)
                    : _externView.CreateEntity(textBoxName.Text.Trim(), ref data);

                if (!success) {
                    string error = _externView.GetLastErrorMessage();
                    MessageBox.Show(error, _isEditMode ? "Entity Edit Error" : "Entity Creation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }

                SaveCompleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex) {
                MessageBox.Show($"Error saving entity: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                if (tilesetPtr != IntPtr.Zero) Marshal.FreeHGlobal(tilesetPtr);
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
            float sx = x < 0.25f ? 0f : x < 0.75f ? 0.5f : 1f;
            float sy = y < 0.25f ? 0f : y < 0.75f ? 0.5f : 1f;
            string col = sx == 0f ? "Left"   : sx == 0.5f ? "Center" : "Right";
            string row = sy == 0f ? "Top"    : sy == 0.5f ? "Middle" : "Bottom";
            return row + col;
        }
    }
}
