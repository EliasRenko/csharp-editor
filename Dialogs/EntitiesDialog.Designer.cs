namespace csharp_editor.Dialogs {
    partial class EntitiesDialog {
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
            groupBoxExisting = new System.Windows.Forms.GroupBox();
            buttonDelete = new System.Windows.Forms.Button();
            listBoxEntities = new System.Windows.Forms.ListBox();
            textureViewer = new csharp_editor.UserControls.TextureViewer();
            labelRegionInfo = new System.Windows.Forms.Label();
            buttonNew = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            groupBoxExisting.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxExisting
            // 
            groupBoxExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left))));
            groupBoxExisting.Controls.Add(buttonDelete);
            groupBoxExisting.Controls.Add(listBoxEntities);
            groupBoxExisting.Location = new System.Drawing.Point(12, 12);
            groupBoxExisting.Name = "groupBoxExisting";
            groupBoxExisting.Size = new System.Drawing.Size(300, 450);
            groupBoxExisting.TabIndex = 0;
            groupBoxExisting.TabStop = false;
            groupBoxExisting.Text = "Entities";
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonDelete.Enabled = false;
            buttonDelete.Location = new System.Drawing.Point(205, 415);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(85, 25);
            buttonDelete.TabIndex = 1;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // listBoxEntities
            // 
            listBoxEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            listBoxEntities.FormattingEnabled = true;
            listBoxEntities.ItemHeight = 15;
            listBoxEntities.Location = new System.Drawing.Point(10, 25);
            listBoxEntities.Name = "listBoxEntities";
            listBoxEntities.Size = new System.Drawing.Size(280, 379);
            listBoxEntities.TabIndex = 0;
            listBoxEntities.SelectedIndexChanged += listBoxEntities_SelectedIndexChanged;
            // 
            // textureViewer
            // 
            textureViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            textureViewer.Location = new System.Drawing.Point(320, 12);
            textureViewer.Name = "textureViewer";
            textureViewer.Size = new System.Drawing.Size(572, 430);
            textureViewer.TabIndex = 3;
            // 
            // labelRegionInfo
            // 
            labelRegionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            labelRegionInfo.ForeColor = System.Drawing.SystemColors.GrayText;
            labelRegionInfo.Location = new System.Drawing.Point(320, 447);
            labelRegionInfo.Name = "labelRegionInfo";
            labelRegionInfo.Size = new System.Drawing.Size(572, 16);
            labelRegionInfo.TabIndex = 4;
            labelRegionInfo.Text = "";
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
            // EntitiesDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(904, 510);
            Controls.Add(labelRegionInfo);
            Controls.Add(textureViewer);
            Controls.Add(buttonNew);
            Controls.Add(buttonClose);
            Controls.Add(groupBoxExisting);
            MinimumSize = new System.Drawing.Size(700, 450);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Entity Manager";
            groupBoxExisting.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxExisting;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ListBox listBoxEntities;
        private csharp_editor.UserControls.TextureViewer textureViewer;
        private System.Windows.Forms.Label labelRegionInfo;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonClose;
    }
}
