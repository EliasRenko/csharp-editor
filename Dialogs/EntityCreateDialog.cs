using System.Runtime.InteropServices;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class EntityCreateDialog : Form {

        private ExternView _externView;
        private Rectangle _currentRegion = new Rectangle(0, 0, 1, 1);
        private int _currentTileSize = 32;

        public EntityCreateDialog(ExternView externView) {
            InitializeComponent();
            _externView = externView;

            LoadAvailableTilemaps();
            UpdateRegionLabel();

            comboBoxTilemap.SelectedIndexChanged += ComboBoxTilemap_SelectedIndexChanged;
            buttonSelectRegion.Click += ButtonSelectRegion_Click;
        }

        private void LoadAvailableTilemaps() {
            comboBoxTilemap.Items.Clear();

            int count = _externView.GetTilesetCount();
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
                int result = _externView.GetTilesetAt(i, out tilesetInfo);
                if (result != 0) {
                    string name = Marshal.PtrToStringAnsi(tilesetInfo.name) ?? "";
                    if (!string.IsNullOrEmpty(name))
                        comboBoxTilemap.Items.Add(name);
                }
            }

            if (comboBoxTilemap.Items.Count > 0)
                comboBoxTilemap.SelectedIndex = 0;

            UpdateCurrentTileSize();
        }

        private void UpdateCurrentTileSize() {
            if (comboBoxTilemap.SelectedItem == null) return;
            string selectedName = comboBoxTilemap.SelectedItem.ToString() ?? "";
            int count = _externView.GetTilesetCount();
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct info = new Externs.TilesetInfoStruct();
                if (_externView.GetTilesetAt(i, out info) != 0) {
                    string name = Marshal.PtrToStringAnsi(info.name) ?? "";
                    if (name == selectedName) {
                        _currentTileSize = info.tileSize > 0 ? info.tileSize : 32;
                        return;
                    }
                }
            }
        }

        private void UpdateRegionLabel() {
            int ts = _currentTileSize > 0 ? _currentTileSize : 1;
            labelRegionInfo.Text = $"Region: tile ({_currentRegion.X},{_currentRegion.Y})  {_currentRegion.Width}×{_currentRegion.Height} tiles  =  pixel ({_currentRegion.X * ts},{_currentRegion.Y * ts}) {_currentRegion.Width * ts}×{_currentRegion.Height * ts}px";
        }

        private void ComboBoxTilemap_SelectedIndexChanged(object? sender, EventArgs e) {
            // Reset region when tilemap changes
            _currentRegion = new Rectangle(0, 0, 1, 1);
            UpdateRegionLabel();
            UpdateCurrentTileSize();
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

                if (dialog.ShowDialog(this) == DialogResult.OK) {
                    _currentRegion = dialog.SelectedRegion;
                    UpdateRegionLabel();
                }
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e) {
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

            try {
                var error = _externView.CreateEntity(
                    textBoxName.Text.Trim(), width, height,
                    comboBoxTilemap.SelectedItem.ToString() ?? "");

                if (error != null) {
                    MessageBox.Show(error, "Entity Creation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _externView.SetEntityRegion(
                    textBoxName.Text.Trim(),
                    _currentRegion.X * _currentTileSize,
                    _currentRegion.Y * _currentTileSize,
                    _currentRegion.Width  * _currentTileSize,
                    _currentRegion.Height * _currentTileSize);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) {
                MessageBox.Show($"Error creating entity: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
