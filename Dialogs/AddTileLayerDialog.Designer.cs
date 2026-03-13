namespace csharp_editor.Dialogs {
    partial class AddTileLayerDialog {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent() {
            labelName       = new System.Windows.Forms.Label();
            textBoxName     = new System.Windows.Forms.TextBox();
            labelTileset    = new System.Windows.Forms.Label();
            comboBoxTileset = new System.Windows.Forms.ComboBox();
            labelTileSize   = new System.Windows.Forms.Label();
            numericUpDownTileSize = new System.Windows.Forms.NumericUpDown();
            buttonAdd       = new System.Windows.Forms.Button();
            buttonCancel    = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTileSize).BeginInit();
            SuspendLayout();

            // labelName
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(12, 18);
            labelName.Name = "labelName";
            labelName.Text = "Name:";

            // textBoxName
            textBoxName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxName.Location = new System.Drawing.Point(90, 15);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(240, 23);
            textBoxName.TabIndex = 0;

            // labelTileset
            labelTileset.AutoSize = true;
            labelTileset.Location = new System.Drawing.Point(12, 55);
            labelTileset.Name = "labelTileset";
            labelTileset.Text = "Tileset:";

            // comboBoxTileset
            comboBoxTileset.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxTileset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxTileset.FormattingEnabled = true;
            comboBoxTileset.Location = new System.Drawing.Point(90, 52);
            comboBoxTileset.Name = "comboBoxTileset";
            comboBoxTileset.Size = new System.Drawing.Size(240, 23);
            comboBoxTileset.TabIndex = 1;

            // labelTileSize
            labelTileSize.AutoSize = true;
            labelTileSize.Location = new System.Drawing.Point(12, 92);
            labelTileSize.Name = "labelTileSize";
            labelTileSize.Text = "Tile Size:";

            // numericUpDownTileSize
            numericUpDownTileSize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            numericUpDownTileSize.Location = new System.Drawing.Point(90, 89);
            numericUpDownTileSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownTileSize.Maximum = new decimal(new int[] { 512, 0, 0, 0 });
            numericUpDownTileSize.Name = "numericUpDownTileSize";
            numericUpDownTileSize.Size = new System.Drawing.Size(240, 23);
            numericUpDownTileSize.TabIndex = 2;
            numericUpDownTileSize.Value = new decimal(new int[] { 32, 0, 0, 0 });

            // buttonAdd
            buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonAdd.Location = new System.Drawing.Point(174, 135);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(75, 28);
            buttonAdd.TabIndex = 3;
            buttonAdd.Text = "Add";
            buttonAdd.Click += buttonAdd_Click;

            // buttonCancel
            buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCancel.Location = new System.Drawing.Point(255, 135);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(75, 28);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Cancel";
            buttonCancel.Click += buttonCancel_Click;

            // AddTileLayerDialog
            AcceptButton = buttonAdd;
            CancelButton = buttonCancel;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(346, 178);
            Controls.AddRange(new System.Windows.Forms.Control[] {
                labelName, textBoxName,
                labelTileset, comboBoxTileset,
                labelTileSize, numericUpDownTileSize,
                buttonAdd, buttonCancel
            });
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddTileLayerDialog";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Add Tile Layer";
            ((System.ComponentModel.ISupportInitialize)numericUpDownTileSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelTileset;
        private System.Windows.Forms.ComboBox comboBoxTileset;
        private System.Windows.Forms.Label labelTileSize;
        private System.Windows.Forms.NumericUpDown numericUpDownTileSize;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCancel;
    }
}
