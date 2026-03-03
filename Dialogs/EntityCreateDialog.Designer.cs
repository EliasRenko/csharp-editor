namespace csharp_editor.Dialogs {
    partial class EntityCreateDialog {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            labelName = new System.Windows.Forms.Label();
            textBoxName = new System.Windows.Forms.TextBox();
            labelWidth = new System.Windows.Forms.Label();
            numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            labelHeight = new System.Windows.Forms.Label();
            numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            labelTilemap = new System.Windows.Forms.Label();
            comboBoxTilemap = new System.Windows.Forms.ComboBox();
            buttonSelectRegion = new System.Windows.Forms.Button();
            labelRegionInfo = new System.Windows.Forms.Label();
            buttonCreate = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).BeginInit();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(12, 18);
            labelName.Name = "labelName";
            labelName.Size = new System.Drawing.Size(42, 15);
            labelName.TabIndex = 0;
            labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            textBoxName.Location = new System.Drawing.Point(95, 15);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(465, 23);
            textBoxName.TabIndex = 1;
            // 
            // labelWidth
            // 
            labelWidth.AutoSize = true;
            labelWidth.Location = new System.Drawing.Point(12, 48);
            labelWidth.Name = "labelWidth";
            labelWidth.Size = new System.Drawing.Size(42, 15);
            labelWidth.TabIndex = 2;
            labelWidth.Text = "Width:";
            // 
            // numericUpDownWidth
            // 
            numericUpDownWidth.Location = new System.Drawing.Point(95, 45);
            numericUpDownWidth.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numericUpDownWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownWidth.Name = "numericUpDownWidth";
            numericUpDownWidth.Size = new System.Drawing.Size(150, 23);
            numericUpDownWidth.TabIndex = 3;
            numericUpDownWidth.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // labelHeight
            // 
            labelHeight.AutoSize = true;
            labelHeight.Location = new System.Drawing.Point(12, 78);
            labelHeight.Name = "labelHeight";
            labelHeight.Size = new System.Drawing.Size(46, 15);
            labelHeight.TabIndex = 4;
            labelHeight.Text = "Height:";
            // 
            // numericUpDownHeight
            // 
            numericUpDownHeight.Location = new System.Drawing.Point(95, 75);
            numericUpDownHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numericUpDownHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownHeight.Name = "numericUpDownHeight";
            numericUpDownHeight.Size = new System.Drawing.Size(150, 23);
            numericUpDownHeight.TabIndex = 5;
            numericUpDownHeight.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // labelTilemap
            // 
            labelTilemap.AutoSize = true;
            labelTilemap.Location = new System.Drawing.Point(12, 108);
            labelTilemap.Name = "labelTilemap";
            labelTilemap.Size = new System.Drawing.Size(52, 15);
            labelTilemap.TabIndex = 6;
            labelTilemap.Text = "Tilemap:";
            // 
            // comboBoxTilemap
            // 
            comboBoxTilemap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            comboBoxTilemap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxTilemap.FormattingEnabled = true;
            comboBoxTilemap.Location = new System.Drawing.Point(95, 105);
            comboBoxTilemap.Name = "comboBoxTilemap";
            comboBoxTilemap.Size = new System.Drawing.Size(465, 23);
            comboBoxTilemap.TabIndex = 7;
            // 
            // buttonSelectRegion
            // 
            buttonSelectRegion.Location = new System.Drawing.Point(12, 138);
            buttonSelectRegion.Name = "buttonSelectRegion";
            buttonSelectRegion.Size = new System.Drawing.Size(78, 23);
            buttonSelectRegion.TabIndex = 8;
            buttonSelectRegion.Text = "Region...";
            buttonSelectRegion.UseVisualStyleBackColor = true;
            // 
            // labelRegionInfo
            // 
            labelRegionInfo.AutoSize = true;
            labelRegionInfo.Location = new System.Drawing.Point(96, 142);
            labelRegionInfo.Name = "labelRegionInfo";
            labelRegionInfo.Size = new System.Drawing.Size(120, 15);
            labelRegionInfo.TabIndex = 9;
            labelRegionInfo.Text = "Region: (0,0) 1×1 tiles";
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonCancel.Location = new System.Drawing.Point(389, 175);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(85, 27);
            buttonCancel.TabIndex = 10;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonCreate
            // 
            buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonCreate.Location = new System.Drawing.Point(479, 175);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new System.Drawing.Size(81, 27);
            buttonCreate.TabIndex = 11;
            buttonCreate.Text = "Create";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // EntityCreateDialog
            // 
            AcceptButton = buttonCreate;
            CancelButton = buttonCancel;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 214);
            Controls.Add(buttonCreate);
            Controls.Add(buttonCancel);
            Controls.Add(labelRegionInfo);
            Controls.Add(buttonSelectRegion);
            Controls.Add(comboBoxTilemap);
            Controls.Add(labelTilemap);
            Controls.Add(numericUpDownHeight);
            Controls.Add(labelHeight);
            Controls.Add(numericUpDownWidth);
            Controls.Add(labelWidth);
            Controls.Add(textBoxName);
            Controls.Add(labelName);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "New Entity";
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelTilemap;
        private System.Windows.Forms.ComboBox comboBoxTilemap;
        private System.Windows.Forms.Button buttonSelectRegion;
        private System.Windows.Forms.Label labelRegionInfo;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonCancel;
    }
}
