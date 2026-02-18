namespace csharp_editor.UserControls {
    partial class TilesetViewer {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripLabelZoom = new System.Windows.Forms.ToolStripLabel();
            toolStripComboBoxZoom = new System.Windows.Forms.ToolStripComboBox();
            panelContainer = new System.Windows.Forms.Panel();
            pictureBoxTexture = new System.Windows.Forms.PictureBox();
            toolStrip.SuspendLayout();
            panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTexture).BeginInit();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripLabelZoom, toolStripComboBoxZoom });
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new System.Drawing.Size(600, 25);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip";
            // 
            // toolStripLabelZoom
            // 
            toolStripLabelZoom.Name = "toolStripLabelZoom";
            toolStripLabelZoom.Size = new System.Drawing.Size(42, 22);
            toolStripLabelZoom.Text = "Zoom:";
            // 
            // toolStripComboBoxZoom
            // 
            toolStripComboBoxZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxZoom.Name = "toolStripComboBoxZoom";
            toolStripComboBoxZoom.Size = new System.Drawing.Size(75, 25);
            // 
            // panelContainer
            // 
            panelContainer.AutoScroll = true;
            panelContainer.Controls.Add(pictureBoxTexture);
            panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContainer.Location = new System.Drawing.Point(0, 25);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new System.Drawing.Size(600, 475);
            panelContainer.TabIndex = 1;
            // 
            // pictureBoxTexture
            // 
            pictureBoxTexture.BackColor = System.Drawing.SystemColors.Control;
            pictureBoxTexture.Location = new System.Drawing.Point(0, 0);
            pictureBoxTexture.Name = "pictureBoxTexture";
            pictureBoxTexture.Size = new System.Drawing.Size(600, 475);
            pictureBoxTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            pictureBoxTexture.TabIndex = 0;
            pictureBoxTexture.TabStop = false;
            // 
            // TilesetViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            Controls.Add(panelContainer);
            Controls.Add(toolStrip);
            Size = new System.Drawing.Size(600, 500);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            panelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxTexture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabelZoom;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxZoom;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.PictureBox pictureBoxTexture;
    }
}
