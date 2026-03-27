using System.Runtime.InteropServices;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    /// <summary>
    /// Dialog for creating or editing a Tile Layer.
    /// Pass a non-null <paramref name="existingName"/> to enter edit mode.
    /// </summary>
    public partial class TileLayerDialog : Form {

        public string LayerName     => textBoxName.Text.Trim();
        public string SelectedTileset => comboBoxTileset.SelectedItem?.ToString() ?? "";
        public int    TileSize      => (int)numericUpDownTileSize.Value;

        private readonly ExternView _externView;
        private readonly bool _isEditMode;

        public TileLayerDialog(ExternView externView, string? existingName = null,
                               string? existingTileset = null, int existingTileSize = 32) {
            InitializeComponent();
            _externView = externView;
            _isEditMode = existingName != null;

            Text = _isEditMode ? "Edit Tile Layer" : "Add Tile Layer";
            buttonConfirm.Text = _isEditMode ? "Save" : "Add";

            LoadTilesets();

            if (_isEditMode) {
                textBoxName.Text = existingName;

                if (!string.IsNullOrEmpty(existingTileset)) {
                    int idx = comboBoxTileset.FindStringExact(existingTileset);
                    if (idx >= 0) comboBoxTileset.SelectedIndex = idx;
                }

                if (existingTileSize > 0)
                    numericUpDownTileSize.Value = Math.Clamp(existingTileSize,
                        (int)numericUpDownTileSize.Minimum,
                        (int)numericUpDownTileSize.Maximum);
            }
        }

        private void LoadTilesets() {
            comboBoxTileset.Items.Clear();
            int count = CExternsEditor.GetTilesetCount();
            for (int i = 0; i < count; i++) {
                CExternsEditor.TilesetInfoStruct info = new CExternsEditor.TilesetInfoStruct();
                if (CExternsEditor.GetTilesetAt(i, out info)) {
                    string name = Marshal.PtrToStringAnsi(info.name) ?? "";
                    if (!string.IsNullOrEmpty(name))
                        comboBoxTileset.Items.Add(name);
                }
            }
            if (comboBoxTileset.Items.Count > 0)
                comboBoxTileset.SelectedIndex = 0;
        }

        private void buttonConfirm_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(textBoxName.Text)) {
                MessageBox.Show("Please enter a layer name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBoxTileset.SelectedItem == null) {
                MessageBox.Show("Please select a tileset.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
