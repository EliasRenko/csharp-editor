namespace csharp_editor.Dialogs {
    partial class ThemeDialog {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent() {
            panelBottom = new System.Windows.Forms.Panel();
            panelScroll = new System.Windows.Forms.Panel();
            btnSave     = new System.Windows.Forms.Button();
            btnCancel   = new System.Windows.Forms.Button();
            btnReset    = new System.Windows.Forms.Button();
            panelBottom.SuspendLayout();
            SuspendLayout();

            // btnReset
            btnReset.BackColor                 = System.Drawing.Color.FromArgb(62, 62, 66);
            btnReset.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.ForeColor                 = System.Drawing.Color.FromArgb(212, 212, 212);
            btnReset.Location                  = new System.Drawing.Point(16, 12);
            btnReset.Name                      = "btnReset";
            btnReset.Size                      = new System.Drawing.Size(110, 28);
            btnReset.Text                      = "Reset Defaults";
            btnReset.Font                      = new System.Drawing.Font("Segoe UI", 9f);
            btnReset.Cursor                    = System.Windows.Forms.Cursors.Hand;
            btnReset.Click                    += BtnReset_Click;

            // btnSave
            btnSave.Anchor                    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnSave.BackColor                 = System.Drawing.Color.FromArgb(14, 99, 156);
            btnSave.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.ForeColor                 = System.Drawing.Color.White;
            btnSave.Location                  = new System.Drawing.Point(392, 12);
            btnSave.Name                      = "btnSave";
            btnSave.Size                      = new System.Drawing.Size(80, 28);
            btnSave.Text                      = "Save";
            btnSave.Font                      = new System.Drawing.Font("Segoe UI", 9f);
            btnSave.Cursor                    = System.Windows.Forms.Cursors.Hand;
            btnSave.Click                    += BtnSave_Click;

            // btnCancel
            btnCancel.Anchor                    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.BackColor                 = System.Drawing.Color.FromArgb(62, 62, 66);
            btnCancel.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.ForeColor                 = System.Drawing.Color.FromArgb(212, 212, 212);
            btnCancel.Location                  = new System.Drawing.Point(480, 12);
            btnCancel.Name                      = "btnCancel";
            btnCancel.Size                      = new System.Drawing.Size(80, 28);
            btnCancel.Text                      = "Cancel";
            btnCancel.Font                      = new System.Drawing.Font("Segoe UI", 9f);
            btnCancel.Cursor                    = System.Windows.Forms.Cursors.Hand;
            btnCancel.Click                    += BtnCancel_Click;

            // panelBottom
            panelBottom.Dock      = System.Windows.Forms.DockStyle.Bottom;
            panelBottom.Height    = 52;
            panelBottom.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            panelBottom.Name      = "panelBottom";
            panelBottom.Controls.AddRange(new System.Windows.Forms.Control[] { btnReset, btnSave, btnCancel });

            // panelScroll
            panelScroll.AutoScroll = true;
            panelScroll.BackColor  = System.Drawing.Color.FromArgb(45, 45, 48);
            panelScroll.Dock       = System.Windows.Forms.DockStyle.Fill;
            panelScroll.Name       = "panelScroll";

            // ThemeDialog
            AcceptButton        = btnSave;
            CancelButton        = btnCancel;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            BackColor           = System.Drawing.Color.FromArgb(45, 45, 48);
            ClientSize          = new System.Drawing.Size(576, 560);
            Font                = new System.Drawing.Font("Segoe UI", 9f);
            FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox         = false;
            MinimizeBox         = false;
            Name                = "ThemeDialog";
            ShowInTaskbar       = false;
            StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent;
            Text                = "Theme Settings";
            Controls.Add(panelScroll);
            Controls.Add(panelBottom);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel  panelBottom;
        private System.Windows.Forms.Panel  panelScroll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
    }
}
