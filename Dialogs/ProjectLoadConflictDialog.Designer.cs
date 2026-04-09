namespace csharp_editor.Dialogs {
    partial class ProjectLoadConflictDialog {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent() {
            labelMessage = new System.Windows.Forms.Label();
            btnPanel     = new System.Windows.Forms.FlowLayoutPanel();
            btnAbort     = new System.Windows.Forms.Button();
            btnAdd       = new System.Windows.Forms.Button();
            btnClose     = new System.Windows.Forms.Button();
            btnSaveAll   = new System.Windows.Forms.Button();
            btnPanel.SuspendLayout();
            SuspendLayout();

            // labelMessage
            labelMessage.Dock      = System.Windows.Forms.DockStyle.Top;
            labelMessage.Height    = 65;
            labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            labelMessage.Padding   = new System.Windows.Forms.Padding(12, 10, 12, 0);
            labelMessage.Name      = "labelMessage";

            // btnPanel
            btnPanel.Dock          = System.Windows.Forms.DockStyle.Bottom;
            btnPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            btnPanel.Height        = 45;
            btnPanel.Padding       = new System.Windows.Forms.Padding(8, 6, 8, 0);
            btnPanel.Name          = "btnPanel";
            btnPanel.Controls.AddRange(new System.Windows.Forms.Control[] { btnAbort, btnAdd, btnClose, btnSaveAll });

            // btnAbort
            btnAbort.Text   = "Abort";
            btnAbort.Width  = 80;
            btnAbort.Height = 30;
            btnAbort.Name   = "btnAbort";

            // btnAdd
            btnAdd.Text   = "Add";
            btnAdd.Width  = 80;
            btnAdd.Height = 30;
            btnAdd.Name   = "btnAdd";

            // btnClose
            btnClose.Text   = "Close All";
            btnClose.Width  = 90;
            btnClose.Height = 30;
            btnClose.Name   = "btnClose";

            // btnSaveAll
            btnSaveAll.Text   = "Save All & Close";
            btnSaveAll.Width  = 120;
            btnSaveAll.Height = 30;
            btnSaveAll.Name   = "btnSaveAll";

            // ProjectLoadConflictDialog
            CancelButton        = btnAbort;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(492, 120);
            FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox         = false;
            MinimizeBox         = false;
            Name                = "ProjectLoadConflictDialog";
            ShowInTaskbar       = false;
            StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent;
            Text                = "Project Already Loaded";
            Controls.Add(labelMessage);
            Controls.Add(btnPanel);
            btnPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Label          labelMessage;
        private System.Windows.Forms.FlowLayoutPanel btnPanel;
        private System.Windows.Forms.Button          btnAbort;
        private System.Windows.Forms.Button          btnAdd;
        private System.Windows.Forms.Button          btnClose;
        private System.Windows.Forms.Button          btnSaveAll;
    }
}
