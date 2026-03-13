using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class PropertyDefinitionDialog : Form {

        // Output properties (read after ShowDialog == OK)
        public string       PropertyName  { get; private set; } = "";
        public EntityEditor.PropertyType PropertyType { get; private set; } = EntityEditor.PropertyType.String;
        public string       DefaultValue  { get; private set; } = "";

        public PropertyDefinitionDialog() {
            InitializeComponent();
        }

        private void ComboBoxType_SelectedIndexChanged(object? sender, EventArgs e) {
            // Update placeholder hint based on selected type
            textBoxDefault.PlaceholderText = comboBoxType.SelectedItem?.ToString() switch {
                "Int"   => "e.g. 0",
                "Float" => "e.g. 0.0",
                "Bool"  => "true / false",
                "Color" => "#RRGGBB",
                _       => "optional"
            };
        }

        private void ButtonOK_Click(object? sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(textBoxName.Text)) {
                MessageBox.Show("Property name cannot be empty.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            PropertyName = textBoxName.Text.Trim();
            PropertyType = comboBoxType.SelectedIndex switch {
                0 => EntityEditor.PropertyType.Int,
                1 => EntityEditor.PropertyType.Float,
                3 => EntityEditor.PropertyType.Bool,
                4 => EntityEditor.PropertyType.Color,
                _ => EntityEditor.PropertyType.String
            };
            DefaultValue = textBoxDefault.Text;
        }
    }
}
