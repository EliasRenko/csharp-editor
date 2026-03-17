namespace csharp_editor.Dialogs {
    partial class TextureCollectionDialog {
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
            textBoxFilter = new System.Windows.Forms.TextBox();
            listBoxTilesets = new System.Windows.Forms.ListBox();
            textureViewer = new csharp_editor.UserControls.TextureViewer();
            labelTilesetMeta = new System.Windows.Forms.Label();
            labelTilesetPath = new System.Windows.Forms.Label();
            buttonImport = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            groupBoxExisting.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxExisting
            // 
            groupBoxExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Bottom))));
            groupBoxExisting.Controls.Add(buttonUse);
            groupBoxExisting.Controls.Add(buttonRemove);
            groupBoxExisting.Controls.Add(textBoxFilter);
            groupBoxExisting.Controls.Add(listBoxTilesets);
            groupBoxExisting.Location = new System.Drawing.Point(12, 12);
            groupBoxExisting.Name = "groupBoxExisting";
            groupBoxExisting.Size = new System.Drawing.Size(300, 450);
            groupBoxExisting.TabIndex = 0;
            groupBoxExisting.TabStop = false;
            groupBoxExisting.Text = "Imported Tilesets";
            // 
            // buttonUse
            // 
            buttonUse.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonUse.Location = new System.Drawing.Point(205, 415);
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
            buttonRemove.Location = new System.Drawing.Point(115, 415);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new System.Drawing.Size(85, 25);
            buttonRemove.TabIndex = 1;
            buttonRemove.Text = "Delete";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // textBoxFilter
            // 
            textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            textBoxFilter.Location = new System.Drawing.Point(10, 22);
            textBoxFilter.Name = "textBoxFilter";
            textBoxFilter.PlaceholderText = "🔍  Filter by name...";
            textBoxFilter.Size = new System.Drawing.Size(280, 23);
            textBoxFilter.TabIndex = 3;
            textBoxFilter.TextChanged += textBoxFilter_TextChanged;
            // 
            // listBoxTilesets
            // 
            listBoxTilesets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            listBoxTilesets.FormattingEnabled = true;
            listBoxTilesets.ItemHeight = 15;
            listBoxTilesets.Location = new System.Drawing.Point(10, 50);
            listBoxTilesets.Name = "listBoxTilesets";
            listBoxTilesets.Size = new System.Drawing.Size(280, 354);
            listBoxTilesets.TabIndex = 0;
            listBoxTilesets.SelectedIndexChanged += listBoxTilesets_SelectedIndexChanged;
            listBoxTilesets.DoubleClick += listBoxTilesets_DoubleClick;
            // 
            // textureViewer
            // 
            textureViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            textureViewer.Location = new System.Drawing.Point(320, 12);
            textureViewer.Name = "textureViewer";
            textureViewer.Size = new System.Drawing.Size(572, 410);
            textureViewer.TabIndex = 3;
            // 
            // labelTilesetMeta
            // 
            labelTilesetMeta.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            labelTilesetMeta.Location = new System.Drawing.Point(320, 426);
            labelTilesetMeta.Name = "labelTilesetMeta";
            labelTilesetMeta.Size = new System.Drawing.Size(572, 18);
            labelTilesetMeta.TabIndex = 4;
            labelTilesetMeta.Text = "";
            // 
            // labelTilesetPath
            // 
            labelTilesetPath.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            labelTilesetPath.AutoEllipsis = true;
            labelTilesetPath.ForeColor = System.Drawing.SystemColors.GrayText;
            labelTilesetPath.Location = new System.Drawing.Point(320, 445);
            labelTilesetPath.Name = "labelTilesetPath";
            labelTilesetPath.Size = new System.Drawing.Size(572, 18);
            labelTilesetPath.TabIndex = 5;
            labelTilesetPath.Text = "";
            // 
            // buttonImport
            // 
            buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            buttonImport.Location = new System.Drawing.Point(12, 470);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new System.Drawing.Size(85, 28);
            buttonImport.TabIndex = 1;
            buttonImport.Text = "Import...";
            buttonImport.UseVisualStyleBackColor = true;
            buttonImport.Click += buttonImport_Click;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonClose.Location = new System.Drawing.Point(797, 470);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(95, 28);
            buttonClose.TabIndex = 2;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // TextureCollectionDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(904, 510);
            Controls.Add(labelTilesetPath);
            Controls.Add(labelTilesetMeta);
            Controls.Add(textureViewer);
            Controls.Add(buttonImport);
            Controls.Add(buttonClose);
            Controls.Add(groupBoxExisting);
            MinimumSize = new System.Drawing.Size(700, 450);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Tilesets";
            groupBoxExisting.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxExisting;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.ListBox listBoxTilesets;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonUse;
        private csharp_editor.UserControls.TextureViewer textureViewer;
        private System.Windows.Forms.Label labelTilesetMeta;
        private System.Windows.Forms.Label labelTilesetPath;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonClose;
    }
}
