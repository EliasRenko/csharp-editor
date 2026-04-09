namespace csharp_editor.Dialogs {
    partial class TilesetSelectionDialog {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent() {
            labelTitle = new System.Windows.Forms.Label();
            listBox    = new System.Windows.Forms.ListBox();
            btnOk      = new System.Windows.Forms.Button();
            btnCancel  = new System.Windows.Forms.Button();
            SuspendLayout();

            // labelTitle
            labelTitle.Location = new System.Drawing.Point(10, 10);
            labelTitle.Size     = new System.Drawing.Size(320, 20);
            labelTitle.Text     = "Available Tilesets:";
            labelTitle.Name     = "labelTitle";

            // listBox
            listBox.Location = new System.Drawing.Point(10, 35);
            listBox.Size     = new System.Drawing.Size(320, 120);
            listBox.Name     = "listBox";

            // btnOk
            btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOk.Location     = new System.Drawing.Point(175, 168);
            btnOk.Size         = new System.Drawing.Size(75, 30);
            btnOk.Text         = "OK";
            btnOk.Name         = "btnOk";

            // btnCancel
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location     = new System.Drawing.Point(255, 168);
            btnCancel.Size         = new System.Drawing.Size(75, 30);
            btnCancel.Text         = "Cancel";
            btnCancel.Name         = "btnCancel";

            // TilesetSelectionDialog
            AcceptButton        = btnOk;
            CancelButton        = btnCancel;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(344, 210);
            FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox         = false;
            MinimizeBox         = false;
            Name                = "TilesetSelectionDialog";
            ShowInTaskbar       = false;
            StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent;
            Text                = "Select Tileset";
            Controls.AddRange(new System.Windows.Forms.Control[] { labelTitle, listBox, btnOk, btnCancel });
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Label   labelTitle;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button  btnOk;
        private System.Windows.Forms.Button  btnCancel;
    }
}
