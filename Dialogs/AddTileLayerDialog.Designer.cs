namespace csharp_editor.Dialogs {
    partial class AddTileLayerDialog {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent() {
            labelName             = new System.Windows.Forms.Label();
            textBoxName           = new System.Windows.Forms.TextBox();
            labelTileset          = new System.Windows.Forms.Label();
            comboBoxTileset       = new System.Windows.Forms.ComboBox();
            labelTileSize         = new System.Windows.Forms.Label();
            numericUpDownTileSize = new System.Windows.Forms.NumericUpDown();
            buttonAdd             = new System.Windows.Forms.Button();
            buttonCancel          = new System.Windows.Forms.Button();
            panelBottom           = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTileSize).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();

            // ── Palette ──────────────────────────────────────────────────────
            var bg       = System.Drawing.Color.FromArgb(45, 45, 48);
            var bgInput  = System.Drawing.Color.FromArgb(60, 60, 60);
            var bgPanel  = System.Drawing.Color.FromArgb(37, 37, 38);
            var fg       = System.Drawing.Color.FromArgb(212, 212, 212);
            var fgMuted  = System.Drawing.Color.FromArgb(153, 153, 153);
            var accentBg = System.Drawing.Color.FromArgb(14, 99, 156);
            var cancelBg = System.Drawing.Color.FromArgb(62, 62, 66);
            var uiFont   = new System.Drawing.Font("Segoe UI", 9f);
            var lblFont  = new System.Drawing.Font("Segoe UI", 8.25f);

            // labelName
            labelName.AutoSize  = true;
            labelName.Location  = new System.Drawing.Point(16, 16);
            labelName.Name      = "labelName";
            labelName.Text      = "Layer Name";
            labelName.ForeColor = fgMuted;
            labelName.Font      = lblFont;

            // textBoxName
            textBoxName.Anchor      = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxName.Location    = new System.Drawing.Point(16, 34);
            textBoxName.Name        = "textBoxName";
            textBoxName.Size        = new System.Drawing.Size(348, 23);
            textBoxName.TabIndex    = 0;
            textBoxName.BackColor   = bgInput;
            textBoxName.ForeColor   = fg;
            textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxName.Font        = uiFont;

            // labelTileset
            labelTileset.AutoSize  = true;
            labelTileset.Location  = new System.Drawing.Point(16, 72);
            labelTileset.Name      = "labelTileset";
            labelTileset.Text      = "Tileset";
            labelTileset.ForeColor = fgMuted;
            labelTileset.Font      = lblFont;

            // comboBoxTileset
            comboBoxTileset.Anchor            = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxTileset.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxTileset.FormattingEnabled = true;
            comboBoxTileset.Location          = new System.Drawing.Point(16, 90);
            comboBoxTileset.Name              = "comboBoxTileset";
            comboBoxTileset.Size              = new System.Drawing.Size(348, 23);
            comboBoxTileset.TabIndex          = 1;
            comboBoxTileset.BackColor         = bgInput;
            comboBoxTileset.ForeColor         = fg;
            comboBoxTileset.FlatStyle         = System.Windows.Forms.FlatStyle.Flat;
            comboBoxTileset.Font              = uiFont;

            // labelTileSize
            labelTileSize.AutoSize  = true;
            labelTileSize.Location  = new System.Drawing.Point(16, 128);
            labelTileSize.Name      = "labelTileSize";
            labelTileSize.Text      = "Tile Size";
            labelTileSize.ForeColor = fgMuted;
            labelTileSize.Font      = lblFont;

            // numericUpDownTileSize
            numericUpDownTileSize.Anchor      = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            numericUpDownTileSize.Location    = new System.Drawing.Point(16, 146);
            numericUpDownTileSize.Minimum     = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownTileSize.Maximum     = new decimal(new int[] { 512, 0, 0, 0 });
            numericUpDownTileSize.Name        = "numericUpDownTileSize";
            numericUpDownTileSize.Size        = new System.Drawing.Size(348, 23);
            numericUpDownTileSize.TabIndex    = 2;
            numericUpDownTileSize.Value       = new decimal(new int[] { 32, 0, 0, 0 });
            numericUpDownTileSize.BackColor   = bgInput;
            numericUpDownTileSize.ForeColor   = fg;
            numericUpDownTileSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            numericUpDownTileSize.Font        = uiFont;

            // buttonAdd
            buttonAdd.Anchor                       = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonAdd.Location                     = new System.Drawing.Point(188, 12);
            buttonAdd.Name                         = "buttonAdd";
            buttonAdd.Size                         = new System.Drawing.Size(80, 28);
            buttonAdd.TabIndex                     = 3;
            buttonAdd.Text                         = "Add";
            buttonAdd.BackColor                    = accentBg;
            buttonAdd.ForeColor                    = System.Drawing.Color.White;
            buttonAdd.FlatStyle                    = System.Windows.Forms.FlatStyle.Flat;
            buttonAdd.FlatAppearance.BorderSize    = 0;
            buttonAdd.Font                         = uiFont;
            buttonAdd.Cursor                       = System.Windows.Forms.Cursors.Hand;
            buttonAdd.Click                       += buttonAdd_Click;

            // buttonCancel
            buttonCancel.Anchor                    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCancel.Location                  = new System.Drawing.Point(276, 12);
            buttonCancel.Name                      = "buttonCancel";
            buttonCancel.Size                      = new System.Drawing.Size(80, 28);
            buttonCancel.TabIndex                  = 4;
            buttonCancel.Text                      = "Cancel";
            buttonCancel.BackColor                 = cancelBg;
            buttonCancel.ForeColor                 = fg;
            buttonCancel.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            buttonCancel.FlatAppearance.BorderSize = 0;
            buttonCancel.Font                      = uiFont;
            buttonCancel.Cursor                    = System.Windows.Forms.Cursors.Hand;
            buttonCancel.Click                    += buttonCancel_Click;

            // panelBottom
            panelBottom.Anchor    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelBottom.Location  = new System.Drawing.Point(0, 190);
            panelBottom.Name      = "panelBottom";
            panelBottom.Size      = new System.Drawing.Size(380, 52);
            panelBottom.BackColor = bgPanel;
            panelBottom.Controls.AddRange(new System.Windows.Forms.Control[] { buttonAdd, buttonCancel });

            // AddTileLayerDialog
            AcceptButton        = buttonAdd;
            CancelButton        = buttonCancel;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(380, 242);
            BackColor           = bg;
            Font                = uiFont;
            Controls.AddRange(new System.Windows.Forms.Control[] {
                labelName, textBoxName,
                labelTileset, comboBoxTileset,
                labelTileSize, numericUpDownTileSize,
                panelBottom
            });
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox     = false;
            MinimizeBox     = false;
            Name            = "AddTileLayerDialog";
            StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            Text            = "Add Tile Layer";
            ((System.ComponentModel.ISupportInitialize)numericUpDownTileSize).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label         labelName;
        private System.Windows.Forms.TextBox       textBoxName;
        private System.Windows.Forms.Label         labelTileset;
        private System.Windows.Forms.ComboBox      comboBoxTileset;
        private System.Windows.Forms.Label         labelTileSize;
        private System.Windows.Forms.NumericUpDown numericUpDownTileSize;
        private System.Windows.Forms.Button        buttonAdd;
        private System.Windows.Forms.Button        buttonCancel;
        private System.Windows.Forms.Panel         panelBottom;
    }
}
