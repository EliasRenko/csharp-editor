namespace csharp_editor.Dialogs {
    partial class PropertyDefinitionDialog {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            labelName         = new System.Windows.Forms.Label();
            textBoxName       = new System.Windows.Forms.TextBox();
            labelType         = new System.Windows.Forms.Label();
            comboBoxType      = new System.Windows.Forms.ComboBox();
            labelDefault      = new System.Windows.Forms.Label();
            textBoxDefault    = new System.Windows.Forms.TextBox();
            buttonOK          = new System.Windows.Forms.Button();
            buttonCancel      = new System.Windows.Forms.Button();
            SuspendLayout();

            // labelName
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(12, 15);
            labelName.Size     = new System.Drawing.Size(39, 15);
            labelName.Text     = "Name:";

            // textBoxName
            textBoxName.Location = new System.Drawing.Point(80, 12);
            textBoxName.Size     = new System.Drawing.Size(290, 23);
            textBoxName.TabIndex = 0;

            // labelType
            labelType.AutoSize = true;
            labelType.Location = new System.Drawing.Point(12, 47);
            labelType.Size     = new System.Drawing.Size(34, 15);
            labelType.Text     = "Type:";

            // comboBoxType
            comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxType.Location      = new System.Drawing.Point(80, 44);
            comboBoxType.Size          = new System.Drawing.Size(140, 23);
            comboBoxType.TabIndex      = 1;
            comboBoxType.Items.AddRange(new object[] { "Int", "Float", "String", "Bool", "Color" });
            comboBoxType.SelectedIndex = 2; // String
            comboBoxType.SelectedIndexChanged += ComboBoxType_SelectedIndexChanged;

            // labelDefault
            labelDefault.AutoSize = true;
            labelDefault.Location = new System.Drawing.Point(12, 79);
            labelDefault.Size     = new System.Drawing.Size(48, 15);
            labelDefault.Text     = "Default:";

            // textBoxDefault
            textBoxDefault.Location    = new System.Drawing.Point(80, 76);
            textBoxDefault.Size        = new System.Drawing.Size(290, 23);
            textBoxDefault.TabIndex    = 2;
            textBoxDefault.PlaceholderText = "optional";

            // buttonOK
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOK.Location     = new System.Drawing.Point(214, 112);
            buttonOK.Size         = new System.Drawing.Size(75, 26);
            buttonOK.TabIndex     = 3;
            buttonOK.Text         = "OK";
            buttonOK.Click       += ButtonOK_Click;

            // buttonCancel
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Location     = new System.Drawing.Point(295, 112);
            buttonCancel.Size         = new System.Drawing.Size(75, 26);
            buttonCancel.TabIndex     = 4;
            buttonCancel.Text         = "Cancel";

            // Form
            AcceptButton      = buttonOK;
            CancelButton      = buttonCancel;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode     = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize        = new System.Drawing.Size(384, 150);
            FormBorderStyle   = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox       = false;
            MinimizeBox       = false;
            StartPosition     = System.Windows.Forms.FormStartPosition.CenterParent;
            Text              = "Add Property";
            Controls.AddRange(new System.Windows.Forms.Control[] {
                labelName, textBoxName,
                labelType, comboBoxType,
                labelDefault, textBoxDefault,
                buttonOK, buttonCancel });
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label      labelName;
        private System.Windows.Forms.TextBox    textBoxName;
        private System.Windows.Forms.Label      labelType;
        private System.Windows.Forms.ComboBox   comboBoxType;
        private System.Windows.Forms.Label      labelDefault;
        private System.Windows.Forms.TextBox    textBoxDefault;
        private System.Windows.Forms.Button     buttonOK;
        private System.Windows.Forms.Button     buttonCancel;
    }
}
