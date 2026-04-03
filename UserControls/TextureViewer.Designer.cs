namespace csharp_editor.UserControls {
    partial class TextureViewer {
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
            toolStripComboBoxZoom = new System.Windows.Forms.ToolStripComboBox();
            toolStripLabelZoom = new System.Windows.Forms.ToolStripLabel();
            toolStripButtonChecker = new System.Windows.Forms.ToolStripButton();
            toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            toolStripButtonAntiAlias = new System.Windows.Forms.ToolStripButton();
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
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripComboBoxZoom, toolStripLabelZoom, toolStripButtonChecker, toolStripButton1, toolStripButtonAntiAlias });
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new System.Drawing.Size(600, 25);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip";
            // 
            // toolStripComboBoxZoom
            // 
            toolStripComboBoxZoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripComboBoxZoom.AutoSize = false;
            toolStripComboBoxZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxZoom.IntegralHeight = false;
            toolStripComboBoxZoom.Name = "toolStripComboBoxZoom";
            toolStripComboBoxZoom.Size = new System.Drawing.Size(55, 23);
            // 
            // toolStripLabelZoom
            // 
            toolStripLabelZoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripLabelZoom.Name = "toolStripLabelZoom";
            toolStripLabelZoom.Size = new System.Drawing.Size(42, 22);
            toolStripLabelZoom.Text = "Zoom:";
            // 
            // toolStripButtonChecker
            // 
            toolStripButtonChecker.CheckOnClick = true;
            toolStripButtonChecker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonChecker.Image = global::csharp_editor.Properties.Resources.checkerboard;
            toolStripButtonChecker.Name = "toolStripButtonChecker";
            toolStripButtonChecker.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripButton1
            // 
            toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = global::csharp_editor.Properties.Resources.arrow_out;
            toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton1.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripButtonAntiAlias
            // 
            toolStripButtonAntiAlias.CheckOnClick = true;
            toolStripButtonAntiAlias.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonAntiAlias.Image = global::csharp_editor.Properties.Resources.style;
            toolStripButtonAntiAlias.Name = "toolStripButtonAntiAlias";
            toolStripButtonAntiAlias.Size = new System.Drawing.Size(23, 22);
            toolStripButtonAntiAlias.ToolTipText = "Toggle anti-aliasing";
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
            pictureBoxTexture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBoxTexture.Location = new System.Drawing.Point(0, 0);
            pictureBoxTexture.Name = "pictureBoxTexture";
            pictureBoxTexture.Size = new System.Drawing.Size(600, 475);
            pictureBoxTexture.TabIndex = 0;
            pictureBoxTexture.TabStop = false;
            // 
            // TextureViewer
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

        private System.Windows.Forms.ToolStripButton toolStripButton1;

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabelZoom;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxZoom;
        private System.Windows.Forms.ToolStripButton toolStripButtonChecker;
        private System.Windows.Forms.ToolStripButton toolStripButtonAntiAlias;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.PictureBox pictureBoxTexture;
    }
}
