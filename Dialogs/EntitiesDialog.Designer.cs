namespace csharp_editor.Dialogs {
    partial class EntitiesDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxExisting = new System.Windows.Forms.GroupBox();
            buttonDelete = new System.Windows.Forms.Button();
            listBoxEntities = new System.Windows.Forms.ListBox();
            groupBoxNew = new System.Windows.Forms.GroupBox();
            labelRegionInfo = new System.Windows.Forms.Label();
            buttonSelectRegion = new System.Windows.Forms.Button();
            comboBoxTilemap = new System.Windows.Forms.ComboBox();
            buttonAdd = new System.Windows.Forms.Button();
            numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            textBoxName = new System.Windows.Forms.TextBox();
            labelTilemap = new System.Windows.Forms.Label();
            labelHeight = new System.Windows.Forms.Label();
            labelWidth = new System.Windows.Forms.Label();
            labelName = new System.Windows.Forms.Label();
            buttonClose = new System.Windows.Forms.Button();
            groupBoxExisting.SuspendLayout();
            groupBoxNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).BeginInit();
            SuspendLayout();
            // 
            // groupBoxExisting
            // 
            groupBoxExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            groupBoxExisting.Controls.Add(buttonDelete);
            groupBoxExisting.Controls.Add(listBoxEntities);
            groupBoxExisting.Location = new System.Drawing.Point(12, 12);
            groupBoxExisting.Name = "groupBoxExisting";
            groupBoxExisting.Size = new System.Drawing.Size(560, 180);
            groupBoxExisting.TabIndex = 0;
            groupBoxExisting.TabStop = false;
            groupBoxExisting.Text = "Existing Entities";
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonDelete.Enabled = false;
            buttonDelete.Location = new System.Drawing.Point(465, 140);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(85, 25);
            buttonDelete.TabIndex = 1;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // listBoxEntities
            // 
            listBoxEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            listBoxEntities.FormattingEnabled = true;
            listBoxEntities.ItemHeight = 15;
            listBoxEntities.Location = new System.Drawing.Point(10, 25);
            listBoxEntities.Name = "listBoxEntities";
            listBoxEntities.Size = new System.Drawing.Size(540, 109);
            listBoxEntities.TabIndex = 0;
            listBoxEntities.SelectedIndexChanged += listBoxEntities_SelectedIndexChanged;
            // 
            // groupBoxNew
            // 
            groupBoxNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            groupBoxNew.Controls.Add(labelRegionInfo);
            groupBoxNew.Controls.Add(buttonSelectRegion);
            groupBoxNew.Controls.Add(comboBoxTilemap);
            groupBoxNew.Controls.Add(buttonAdd);
            groupBoxNew.Controls.Add(numericUpDownHeight);
            groupBoxNew.Controls.Add(numericUpDownWidth);
            groupBoxNew.Controls.Add(textBoxName);
            groupBoxNew.Controls.Add(labelTilemap);
            groupBoxNew.Controls.Add(labelHeight);
            groupBoxNew.Controls.Add(labelWidth);
            groupBoxNew.Controls.Add(labelName);
            groupBoxNew.Location = new System.Drawing.Point(12, 198);
            groupBoxNew.Name = "groupBoxNew";
            groupBoxNew.Size = new System.Drawing.Size(560, 200);
            groupBoxNew.TabIndex = 1;
            groupBoxNew.TabStop = false;
            groupBoxNew.Text = "Add New Entity";
            // 
            // labelRegionInfo
            // 
            labelRegionInfo.AutoSize = true;
            labelRegionInfo.Location = new System.Drawing.Point(90, 148);
            labelRegionInfo.Name = "labelRegionInfo";
            labelRegionInfo.Size = new System.Drawing.Size(120, 15);
            labelRegionInfo.TabIndex = 12;
            labelRegionInfo.Text = "Region: (0,0) 1×1 tiles";
            // 
            // buttonSelectRegion
            // 
            buttonSelectRegion.Location = new System.Drawing.Point(10, 144);
            buttonSelectRegion.Name = "buttonSelectRegion";
            buttonSelectRegion.Size = new System.Drawing.Size(74, 23);
            buttonSelectRegion.TabIndex = 11;
            buttonSelectRegion.Text = "Region...";
            buttonSelectRegion.UseVisualStyleBackColor = true;
            // 
            // comboBoxTilemap
            // 
            comboBoxTilemap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            comboBoxTilemap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxTilemap.FormattingEnabled = true;
            comboBoxTilemap.Location = new System.Drawing.Point(90, 115);
            comboBoxTilemap.Name = "comboBoxTilemap";
            comboBoxTilemap.Size = new System.Drawing.Size(460, 23);
            comboBoxTilemap.TabIndex = 9;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonAdd.Location = new System.Drawing.Point(465, 169);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(85, 25);
            buttonAdd.TabIndex = 10;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // numericUpDownHeight
            // 
            numericUpDownHeight.Location = new System.Drawing.Point(90, 85);
            numericUpDownHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numericUpDownHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownHeight.Name = "numericUpDownHeight";
            numericUpDownHeight.Size = new System.Drawing.Size(150, 23);
            numericUpDownHeight.TabIndex = 5;
            numericUpDownHeight.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // numericUpDownWidth
            // 
            numericUpDownWidth.Location = new System.Drawing.Point(90, 55);
            numericUpDownWidth.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numericUpDownWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownWidth.Name = "numericUpDownWidth";
            numericUpDownWidth.Size = new System.Drawing.Size(150, 23);
            numericUpDownWidth.TabIndex = 3;
            numericUpDownWidth.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // textBoxName
            // 
            textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            textBoxName.Location = new System.Drawing.Point(90, 25);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(460, 23);
            textBoxName.TabIndex = 1;
            // 
            // labelTilemap
            // 
            labelTilemap.AutoSize = true;
            labelTilemap.Location = new System.Drawing.Point(10, 118);
            labelTilemap.Name = "labelTilemap";
            labelTilemap.Size = new System.Drawing.Size(52, 15);
            labelTilemap.TabIndex = 8;
            labelTilemap.Text = "Tilemap:";
            // 
            // labelHeight
            // 
            labelHeight.AutoSize = true;
            labelHeight.Location = new System.Drawing.Point(10, 87);
            labelHeight.Name = "labelHeight";
            labelHeight.Size = new System.Drawing.Size(46, 15);
            labelHeight.TabIndex = 4;
            labelHeight.Text = "Height:";
            // 
            // labelWidth
            // 
            labelWidth.AutoSize = true;
            labelWidth.Location = new System.Drawing.Point(10, 57);
            labelWidth.Name = "labelWidth";
            labelWidth.Size = new System.Drawing.Size(42, 15);
            labelWidth.TabIndex = 2;
            labelWidth.Text = "Width:";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(10, 28);
            labelName.Name = "labelName";
            labelName.Size = new System.Drawing.Size(42, 15);
            labelName.TabIndex = 0;
            labelName.Text = "Name:";
            // 
            // buttonClose
            // 
            buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonClose.Location = new System.Drawing.Point(477, 404);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(95, 30);
            buttonClose.TabIndex = 2;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // EntitiesDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 446);
            Controls.Add(buttonClose);
            Controls.Add(groupBoxNew);
            Controls.Add(groupBoxExisting);
            MinimumSize = new System.Drawing.Size(600, 485);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Entity Manager";
            groupBoxExisting.ResumeLayout(false);
            groupBoxNew.ResumeLayout(false);
            groupBoxNew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxExisting;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ListBox listBoxEntities;
        private System.Windows.Forms.GroupBox groupBoxNew;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelTilemap;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ComboBox comboBoxTilemap;
        private System.Windows.Forms.Button buttonSelectRegion;
        private System.Windows.Forms.Label labelRegionInfo;
    }
}
