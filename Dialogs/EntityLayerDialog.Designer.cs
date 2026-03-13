namespace csharp_editor.Dialogs {
    partial class EntityLayerDialog {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent() {
            labelName     = new System.Windows.Forms.Label();
            textBoxName   = new System.Windows.Forms.TextBox();
            buttonConfirm = new System.Windows.Forms.Button();
            buttonCancel  = new System.Windows.Forms.Button();
            SuspendLayout();

            // labelName
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(12, 18);
            labelName.Name     = "labelName";
            labelName.Text     = "Name:";

            // textBoxName
            textBoxName.Anchor   = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxName.Location = new System.Drawing.Point(90, 15);
            textBoxName.Name     = "textBoxName";
            textBoxName.Size     = new System.Drawing.Size(240, 23);
            textBoxName.TabIndex = 0;

            // buttonConfirm
            buttonConfirm.Anchor   = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonConfirm.Location = new System.Drawing.Point(174, 60);
            buttonConfirm.Name     = "buttonConfirm";
            buttonConfirm.Size     = new System.Drawing.Size(75, 28);
            buttonConfirm.TabIndex = 1;
            buttonConfirm.Text     = "Add";
            buttonConfirm.Click   += buttonConfirm_Click;

            // buttonCancel
            buttonCancel.Anchor   = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCancel.Location = new System.Drawing.Point(255, 60);
            buttonCancel.Name     = "buttonCancel";
            buttonCancel.Size     = new System.Drawing.Size(75, 28);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text     = "Cancel";
            buttonCancel.Click   += buttonCancel_Click;

            // EntityLayerDialog
            AcceptButton        = buttonConfirm;
            CancelButton        = buttonCancel;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(346, 103);
            Controls.AddRange(new System.Windows.Forms.Control[] {
                labelName, textBoxName,
                buttonConfirm, buttonCancel
            });
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox     = false;
            MinimizeBox     = false;
            Name            = "EntityLayerDialog";
            StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            Text            = "Add Entity Layer";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label  labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button  buttonConfirm;
        private System.Windows.Forms.Button  buttonCancel;
    }
}
