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
            textureViewer = new csharp_editor.UserControls.TextureViewer();
            buttonNew = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            groupBoxExisting.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxExisting
            // 
            groupBoxExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Bottom))));
            groupBoxExisting.Controls.Add(buttonUse);
            groupBoxExisting.Controls.Add(buttonRemove);
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
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // listBoxTilesets
            // 
            listBoxTilesets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            listBoxTilesets.FormattingEnabled = true;
            listBoxTilesets.ItemHeight = 15;
            listBoxTilesets.Location = new System.Drawing.Point(10, 25);
            listBoxTilesets.Name = "listBoxTilesets";
            listBoxTilesets.Size = new System.Drawing.Size(280, 379);
            listBoxTilesets.TabIndex = 0;
            listBoxTilesets.SelectedIndexChanged += listBoxTilesets_SelectedIndexChanged;
            // 
            // textureViewer
            // 
            textureViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            textureViewer.Location = new System.Drawing.Point(320, 12);
            textureViewer.Name = "textureViewer";
            textureViewer.Size = new System.Drawing.Size(572, 450);
            textureViewer.TabIndex = 3;
            // 
            // buttonNew
            // 
            buttonNew.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            buttonNew.Location = new System.Drawing.Point(12, 470);
            buttonNew.Name = "buttonNew";
            buttonNew.Size = new System.Drawing.Size(85, 28);
            buttonNew.TabIndex = 1;
            buttonNew.Text = "New...";
            buttonNew.UseVisualStyleBackColor = true;
            buttonNew.Click += buttonNew_Click;
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
            // TilesetImportDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(904, 510);
            Controls.Add(textureViewer);
            Controls.Add(buttonNew);
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
        private System.Windows.Forms.ListBox listBoxTilesets;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonUse;
        private csharp_editor.UserControls.TextureViewer textureViewer;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonClose;
    }
}
