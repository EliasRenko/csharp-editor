using System.Runtime.InteropServices;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class AddTileLayerDialog : Form {

        public string LayerName => textBoxName.Text.Trim();
        public string SelectedTileset => comboBoxTileset.SelectedItem?.ToString() ?? "";
        public int TileSize => (int)numericUpDownTileSize.Value;

        private readonly ExternView _externView;

        public AddTileLayerDialog(ExternView externView) {
            InitializeComponent();
            _externView = externView;
            LoadTilesets();
        }

        private void LoadTilesets() {
            comboBoxTileset.Items.Clear();
            int count = _externView.GetTilesetCount();
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct info = new Externs.TilesetInfoStruct();
                if (_externView.GetTilesetAt(i, out info)) {
                    string name = Marshal.PtrToStringAnsi(info.name) ?? "";
                    if (!string.IsNullOrEmpty(name))
                        comboBoxTileset.Items.Add(name);
                }
            }
            if (comboBoxTileset.Items.Count > 0)
                comboBoxTileset.SelectedIndex = 0;
        }

        private void buttonAdd_Click(object sender, EventArgs e) {
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
