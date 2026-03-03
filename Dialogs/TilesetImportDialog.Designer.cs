namespace csharp_editor.Dialogs {
    partial class TilesetImportDialog {
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
            buttonUse = new System.Windows.Forms.Button();
            buttonRemove = new System.Windows.Forms.Button();
            listBoxTilesets = new System.Windows.Forms.ListBox();
            groupBoxNew = new System.Windows.Forms.GroupBox();
            buttonBrowse = new System.Windows.Forms.Button();
            buttonAdd = new System.Windows.Forms.Button();
            numericUpDownTileSize = new System.Windows.Forms.NumericUpDown();
            textBoxImagePath = new System.Windows.Forms.TextBox();
            textBoxName = new System.Windows.Forms.TextBox();
            labelTileSize = new System.Windows.Forms.Label();
            labelImagePath = new System.Windows.Forms.Label();
            labelName = new System.Windows.Forms.Label();
            buttonClose = new System.Windows.Forms.Button();
            groupBoxExisting.SuspendLayout();
            groupBoxNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTileSize).BeginInit();
            SuspendLayout();
            // 
            // groupBoxExisting
            // 
            groupBoxExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            groupBoxExisting.Controls.Add(buttonUse);
            groupBoxExisting.Controls.Add(buttonRemove);
            groupBoxExisting.Controls.Add(listBoxTilesets);
            groupBoxExisting.Location = new System.Drawing.Point(12, 12);
            groupBoxExisting.Name = "groupBoxExisting";
            groupBoxExisting.Size = new System.Drawing.Size(560, 180);
            groupBoxExisting.TabIndex = 0;
            groupBoxExisting.TabStop = false;
            groupBoxExisting.Text = "Imported Tilesets";
            // 
            // buttonUse
            // 
            buttonUse.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonUse.Location = new System.Drawing.Point(465, 145);
            buttonUse.Name = "buttonUse";
            buttonUse.Size = new System.Drawing.Size(85, 25);
            buttonUse.TabIndex = 2;
            buttonUse.Text = "Use";
            buttonUse.UseVisualStyleBackColor = true;
            buttonUse.Click += buttonUse_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonRemove.Location = new System.Drawing.Point(375, 145);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new System.Drawing.Size(85, 25);
            buttonRemove.TabIndex = 1;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // listBoxTilesets
            // 
            listBoxTilesets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            listBoxTilesets.FormattingEnabled = true;
            listBoxTilesets.ItemHeight = 15;
            listBoxTilesets.Location = new System.Drawing.Point(10, 25);
            listBoxTilesets.Name = "listBoxTilesets";
            listBoxTilesets.Size = new System.Drawing.Size(540, 109);
            listBoxTilesets.TabIndex = 0;
            // 
            // groupBoxNew
            // 
            groupBoxNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            groupBoxNew.Controls.Add(buttonBrowse);
            groupBoxNew.Controls.Add(buttonAdd);
            groupBoxNew.Controls.Add(numericUpDownTileSize);
            groupBoxNew.Controls.Add(textBoxImagePath);
            groupBoxNew.Controls.Add(textBoxName);
            groupBoxNew.Controls.Add(labelTileSize);
            groupBoxNew.Controls.Add(labelImagePath);
            groupBoxNew.Controls.Add(labelName);
            groupBoxNew.ForeColor = System.Drawing.Color.White;
            groupBoxNew.Location = new System.Drawing.Point(12, 198);
            groupBoxNew.Name = "groupBoxNew";
            groupBoxNew.Size = new System.Drawing.Size(560, 160);
            groupBoxNew.TabIndex = 1;
            groupBoxNew.TabStop = false;
            groupBoxNew.Text = "Add New Tileset";
            // 
            // buttonBrowse
            // 
            buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            buttonBrowse.ForeColor = System.Drawing.Color.Black;
            buttonBrowse.Location = new System.Drawing.Point(465, 55);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new System.Drawing.Size(85, 23);
            buttonBrowse.TabIndex = 4;
            buttonBrowse.Text = "Browse...";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonAdd.ForeColor = System.Drawing.Color.Black;
            buttonAdd.Location = new System.Drawing.Point(465, 125);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(85, 25);
            buttonAdd.TabIndex = 6;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // numericUpDownTileSize
            // 
            numericUpDownTileSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            numericUpDownTileSize.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            numericUpDownTileSize.ForeColor = System.Drawing.Color.White;
            numericUpDownTileSize.Location = new System.Drawing.Point(90, 85);
            numericUpDownTileSize.Maximum = new decimal(new int[] { 512, 0, 0, 0 });
            numericUpDownTileSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownTileSize.Name = "numericUpDownTileSize";
            numericUpDownTileSize.Size = new System.Drawing.Size(369, 23);
            numericUpDownTileSize.TabIndex = 5;
            numericUpDownTileSize.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // textBoxImagePath
            // 
            textBoxImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            textBoxImagePath.Location = new System.Drawing.Point(90, 55);
            textBoxImagePath.Name = "textBoxImagePath";
            textBoxImagePath.Size = new System.Drawing.Size(369, 23);
            textBoxImagePath.TabIndex = 3;
            // 
            // textBoxName
            // 
            textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            textBoxName.Location = new System.Drawing.Point(90, 25);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(460, 23);
            textBoxName.TabIndex = 1;
            // 
            // labelTileSize
            // 
            labelTileSize.AutoSize = true;
            labelTileSize.BackColor = System.Drawing.SystemColors.Control;
            labelTileSize.ForeColor = System.Drawing.Color.Black;
            labelTileSize.Location = new System.Drawing.Point(10, 87);
            labelTileSize.Name = "labelTileSize";
            labelTileSize.Size = new System.Drawing.Size(51, 15);
            labelTileSize.TabIndex = 2;
            labelTileSize.Text = "Tile Size:";
            // 
            // labelImagePath
            // 
            labelImagePath.AutoSize = true;
            labelImagePath.ForeColor = System.Drawing.Color.Black;
            labelImagePath.Location = new System.Drawing.Point(10, 58);
            labelImagePath.Name = "labelImagePath";
            labelImagePath.Size = new System.Drawing.Size(70, 15);
            labelImagePath.TabIndex = 1;
            labelImagePath.Text = "Image Path:";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.ForeColor = System.Drawing.Color.Black;
            labelName.Location = new System.Drawing.Point(10, 28);
            labelName.Name = "labelName";
            labelName.Size = new System.Drawing.Size(42, 15);
            labelName.TabIndex = 0;
            labelName.Text = "Name:";
            // 
            // buttonClose
            // 
            buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonClose.Location = new System.Drawing.Point(477, 368);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(95, 30);
            buttonClose.TabIndex = 2;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // TilesetImportDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 410);
            Controls.Add(buttonClose);
            Controls.Add(groupBoxNew);
            Controls.Add(groupBoxExisting);
            MinimumSize = new System.Drawing.Size(600, 400);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Tileset Import";
            groupBoxExisting.ResumeLayout(false);
            groupBoxNew.ResumeLayout(false);
            groupBoxNew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTileSize).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxExisting;
        private System.Windows.Forms.ListBox listBoxTilesets;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonUse;
        private System.Windows.Forms.GroupBox groupBoxNew;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelImagePath;
        private System.Windows.Forms.Label labelTileSize;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxImagePath;
        private System.Windows.Forms.NumericUpDown numericUpDownTileSize;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonClose;
    }
}
