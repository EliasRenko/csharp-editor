namespace csharp_editor {
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
        private void InitializeComponent() {
            this.groupBoxExisting = new System.Windows.Forms.GroupBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonUse = new System.Windows.Forms.Button();
            this.listBoxTilesets = new System.Windows.Forms.ListBox();
            this.groupBoxNew = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.numericUpDownTileSize = new System.Windows.Forms.NumericUpDown();
            this.textBoxImagePath = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelTileSize = new System.Windows.Forms.Label();
            this.labelImagePath = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxExisting.SuspendLayout();
            this.groupBoxNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTileSize)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxExisting
            // 
            this.groupBoxExisting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxExisting.Controls.Add(this.buttonUse);
            this.groupBoxExisting.Controls.Add(this.buttonRemove);
            this.groupBoxExisting.Controls.Add(this.listBoxTilesets);
            this.groupBoxExisting.ForeColor = System.Drawing.Color.White;
            this.groupBoxExisting.Location = new System.Drawing.Point(12, 12);
            this.groupBoxExisting.Name = "groupBoxExisting";
            this.groupBoxExisting.Size = new System.Drawing.Size(560, 180);
            this.groupBoxExisting.TabIndex = 0;
            this.groupBoxExisting.TabStop = false;
            this.groupBoxExisting.Text = "Imported Tilesets";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.ForeColor = System.Drawing.Color.Black;
            this.buttonRemove.Location = new System.Drawing.Point(375, 145);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(85, 25);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonUse
            // 
            this.buttonUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUse.ForeColor = System.Drawing.Color.Black;
            this.buttonUse.Location = new System.Drawing.Point(465, 145);
            this.buttonUse.Name = "buttonUse";
            this.buttonUse.Size = new System.Drawing.Size(85, 25);
            this.buttonUse.TabIndex = 2;
            this.buttonUse.Text = "Use";
            this.buttonUse.UseVisualStyleBackColor = true;
            this.buttonUse.Click += new System.EventHandler(this.buttonUse_Click);
            // 
            // listBoxTilesets
            // 
            this.listBoxTilesets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxTilesets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.listBoxTilesets.ForeColor = System.Drawing.Color.White;
            this.listBoxTilesets.FormattingEnabled = true;
            this.listBoxTilesets.ItemHeight = 15;
            this.listBoxTilesets.Location = new System.Drawing.Point(10, 25);
            this.listBoxTilesets.Name = "listBoxTilesets";
            this.listBoxTilesets.Size = new System.Drawing.Size(540, 109);
            this.listBoxTilesets.TabIndex = 0;
            // 
            // groupBoxNew
            // 
            this.groupBoxNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxNew.Controls.Add(this.buttonBrowse);
            this.groupBoxNew.Controls.Add(this.buttonAdd);
            this.groupBoxNew.Controls.Add(this.numericUpDownTileSize);
            this.groupBoxNew.Controls.Add(this.textBoxImagePath);
            this.groupBoxNew.Controls.Add(this.textBoxName);
            this.groupBoxNew.Controls.Add(this.labelTileSize);
            this.groupBoxNew.Controls.Add(this.labelImagePath);
            this.groupBoxNew.Controls.Add(this.labelName);
            this.groupBoxNew.ForeColor = System.Drawing.Color.White;
            this.groupBoxNew.Location = new System.Drawing.Point(12, 198);
            this.groupBoxNew.Name = "groupBoxNew";
            this.groupBoxNew.Size = new System.Drawing.Size(560, 160);
            this.groupBoxNew.TabIndex = 1;
            this.groupBoxNew.TabStop = false;
            this.groupBoxNew.Text = "Add New Tileset";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.ForeColor = System.Drawing.Color.Black;
            this.buttonBrowse.Location = new System.Drawing.Point(465, 55);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(85, 23);
            this.buttonBrowse.TabIndex = 4;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.ForeColor = System.Drawing.Color.Black;
            this.buttonAdd.Location = new System.Drawing.Point(465, 125);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(85, 25);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // numericUpDownTileSize
            // 
            this.numericUpDownTileSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownTileSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.numericUpDownTileSize.ForeColor = System.Drawing.Color.White;
            this.numericUpDownTileSize.Location = new System.Drawing.Point(90, 85);
            this.numericUpDownTileSize.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.numericUpDownTileSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTileSize.Name = "numericUpDownTileSize";
            this.numericUpDownTileSize.Size = new System.Drawing.Size(369, 23);
            this.numericUpDownTileSize.TabIndex = 5;
            this.numericUpDownTileSize.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // textBoxImagePath
            // 
            this.textBoxImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxImagePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.textBoxImagePath.ForeColor = System.Drawing.Color.White;
            this.textBoxImagePath.Location = new System.Drawing.Point(90, 55);
            this.textBoxImagePath.Name = "textBoxImagePath";
            this.textBoxImagePath.Size = new System.Drawing.Size(369, 23);
            this.textBoxImagePath.TabIndex = 3;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.textBoxName.ForeColor = System.Drawing.Color.White;
            this.textBoxName.Location = new System.Drawing.Point(90, 25);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(460, 23);
            this.textBoxName.TabIndex = 1;
            // 
            // labelTileSize
            // 
            this.labelTileSize.AutoSize = true;
            this.labelTileSize.Location = new System.Drawing.Point(10, 87);
            this.labelTileSize.Name = "labelTileSize";
            this.labelTileSize.Size = new System.Drawing.Size(54, 15);
            this.labelTileSize.TabIndex = 2;
            this.labelTileSize.Text = "Tile Size:";
            // 
            // labelImagePath
            // 
            this.labelImagePath.AutoSize = true;
            this.labelImagePath.Location = new System.Drawing.Point(10, 58);
            this.labelImagePath.Name = "labelImagePath";
            this.labelImagePath.Size = new System.Drawing.Size(73, 15);
            this.labelImagePath.TabIndex = 1;
            this.labelImagePath.Text = "Image Path:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(10, 28);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(42, 15);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(477, 368);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(95, 30);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // TilesetImportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(584, 410);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxNew);
            this.Controls.Add(this.groupBoxExisting);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "TilesetImportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tileset Import";
            this.groupBoxExisting.ResumeLayout(false);
            this.groupBoxNew.ResumeLayout(false);
            this.groupBoxNew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTileSize)).EndInit();
            this.ResumeLayout(false);
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
