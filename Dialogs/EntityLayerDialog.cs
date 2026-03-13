namespace csharp_editor.Dialogs {
    /// <summary>
    /// Dialog for creating or editing an Entity Layer.
    /// Pass a non-null <paramref name="existingName"/> to enter edit mode.
    /// </summary>
    public partial class EntityLayerDialog : Form {

        public string LayerName => textBoxName.Text.Trim();

        private readonly bool _isEditMode;

        public EntityLayerDialog(string? existingName = null) {
            InitializeComponent();
            _isEditMode = existingName != null;

            Text = _isEditMode ? "Edit Entity Layer" : "Add Entity Layer";
            buttonConfirm.Text = _isEditMode ? "Save" : "Add";

            if (_isEditMode)
                textBoxName.Text = existingName;
        }

        private void buttonConfirm_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(textBoxName.Text)) {
                MessageBox.Show("Please enter a layer name.", "Validation Error",
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
