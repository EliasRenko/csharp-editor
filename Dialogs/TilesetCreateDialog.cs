using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class TilesetCreateDialog : Form {

        private ExternView _externView;

        public TilesetCreateDialog(ExternView externView) {
            InitializeComponent();
            _externView = externView;
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

        private void buttonCreate_Click(object sender, EventArgs e) {
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

            try {
                var error = _externView.CreateTileset(textBoxImagePath.Text.Trim(), textBoxName.Text.Trim());
                if (error != null) {
                    MessageBox.Show(error, "Tileset Creation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) {
                MessageBox.Show($"Error creating tileset: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
