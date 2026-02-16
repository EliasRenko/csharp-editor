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
            panelInfo = new System.Windows.Forms.Panel();
            labelTransparent = new System.Windows.Forms.Label();
            labelDataLength = new System.Windows.Forms.Label();
            labelBPP = new System.Windows.Forms.Label();
            labelHeight = new System.Windows.Forms.Label();
            labelWidth = new System.Windows.Forms.Label();
            tilesetViewer = new csharp_editor.UserControls.TilesetViewer();
            panelInfo.SuspendLayout();
            SuspendLayout();
            // 
            // panelInfo
            // 
            panelInfo.BackColor = System.Drawing.SystemColors.Control;
            panelInfo.Controls.Add(labelTransparent);
            panelInfo.Controls.Add(labelDataLength);
            panelInfo.Controls.Add(labelBPP);
            panelInfo.Controls.Add(labelHeight);
            panelInfo.Controls.Add(labelWidth);
            panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            panelInfo.Location = new System.Drawing.Point(0, 0);
            panelInfo.Name = "panelInfo";
            panelInfo.Padding = new System.Windows.Forms.Padding(10);
            panelInfo.Size = new System.Drawing.Size(600, 120);
            panelInfo.TabIndex = 0;
            // 
            // labelTransparent
            // 
            labelTransparent.AutoSize = true;
            labelTransparent.Location = new System.Drawing.Point(13, 90);
            labelTransparent.Name = "labelTransparent";
            labelTransparent.Size = new System.Drawing.Size(79, 15);
            labelTransparent.TabIndex = 4;
            labelTransparent.Text = "Transparent: -";
            // 
            // labelDataLength
            // 
            labelDataLength.AutoSize = true;
            labelDataLength.Location = new System.Drawing.Point(13, 70);
            labelDataLength.Name = "labelDataLength";
            labelDataLength.Size = new System.Drawing.Size(82, 15);
            labelDataLength.TabIndex = 3;
            labelDataLength.Text = "Data Length: -";
            // 
            // labelBPP
            // 
            labelBPP.AutoSize = true;
            labelBPP.Location = new System.Drawing.Point(13, 50);
            labelBPP.Name = "labelBPP";
            labelBPP.Size = new System.Drawing.Size(94, 15);
            labelBPP.TabIndex = 2;
            labelBPP.Text = "Bytes Per Pixel: -";
            // 
            // labelHeight
            // 
            labelHeight.AutoSize = true;
            labelHeight.Location = new System.Drawing.Point(13, 30);
            labelHeight.Name = "labelHeight";
            labelHeight.Size = new System.Drawing.Size(54, 15);
            labelHeight.TabIndex = 1;
            labelHeight.Text = "Height: -";
            // 
            // labelWidth
            // 
            labelWidth.AutoSize = true;
            labelWidth.Location = new System.Drawing.Point(13, 10);
            labelWidth.Name = "labelWidth";
            labelWidth.Size = new System.Drawing.Size(50, 15);
            labelWidth.TabIndex = 0;
            labelWidth.Text = "Width: -";
            // 
            // tilesetViewer
            // 
            tilesetViewer.BackColor = System.Drawing.SystemColors.Control;
            tilesetViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            tilesetViewer.Location = new System.Drawing.Point(0, 120);
            tilesetViewer.Name = "tilesetViewer";
            tilesetViewer.Size = new System.Drawing.Size(600, 380);
            tilesetViewer.TabIndex = 1;
            // 
            // TextureViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)((byte)30)), ((int)((byte)30)), ((int)((byte)30)));
            Controls.Add(tilesetViewer);
            Controls.Add(panelInfo);
            Size = new System.Drawing.Size(600, 500);
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelBPP;
        private System.Windows.Forms.Label labelDataLength;
        private System.Windows.Forms.Label labelTransparent;
        private csharp_editor.UserControls.TilesetViewer tilesetViewer;
    }
}
