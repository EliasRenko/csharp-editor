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
        private void InitializeComponent() {
            this.panelInfo = new System.Windows.Forms.Panel();
            this.labelTransparent = new System.Windows.Forms.Label();
            this.labelDataLength = new System.Windows.Forms.Label();
            this.labelBPP = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.tilesetViewer = new TilesetViewer();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelInfo.Controls.Add(this.labelTransparent);
            this.panelInfo.Controls.Add(this.labelDataLength);
            this.panelInfo.Controls.Add(this.labelBPP);
            this.panelInfo.Controls.Add(this.labelHeight);
            this.panelInfo.Controls.Add(this.labelWidth);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Padding = new System.Windows.Forms.Padding(10);
            this.panelInfo.Size = new System.Drawing.Size(600, 120);
            this.panelInfo.TabIndex = 0;
            // 
            // labelTransparent
            // 
            this.labelTransparent.AutoSize = true;
            this.labelTransparent.ForeColor = System.Drawing.Color.White;
            this.labelTransparent.Location = new System.Drawing.Point(13, 90);
            this.labelTransparent.Name = "labelTransparent";
            this.labelTransparent.Size = new System.Drawing.Size(81, 15);
            this.labelTransparent.TabIndex = 4;
            this.labelTransparent.Text = "Transparent: -";
            // 
            // labelDataLength
            // 
            this.labelDataLength.AutoSize = true;
            this.labelDataLength.ForeColor = System.Drawing.Color.White;
            this.labelDataLength.Location = new System.Drawing.Point(13, 70);
            this.labelDataLength.Name = "labelDataLength";
            this.labelDataLength.Size = new System.Drawing.Size(85, 15);
            this.labelDataLength.TabIndex = 3;
            this.labelDataLength.Text = "Data Length: -";
            // 
            // labelBPP
            // 
            this.labelBPP.AutoSize = true;
            this.labelBPP.ForeColor = System.Drawing.Color.White;
            this.labelBPP.Location = new System.Drawing.Point(13, 50);
            this.labelBPP.Name = "labelBPP";
            this.labelBPP.Size = new System.Drawing.Size(108, 15);
            this.labelBPP.TabIndex = 2;
            this.labelBPP.Text = "Bytes Per Pixel: -";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.ForeColor = System.Drawing.Color.White;
            this.labelHeight.Location = new System.Drawing.Point(13, 30);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(54, 15);
            this.labelHeight.TabIndex = 1;
            this.labelHeight.Text = "Height: -";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.ForeColor = System.Drawing.Color.White;
            this.labelWidth.Location = new System.Drawing.Point(13, 10);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(51, 15);
            this.labelWidth.TabIndex = 0;
            this.labelWidth.Text = "Width: -";
            // 
            // tilesetViewer
            // 
            this.tilesetViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tilesetViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetViewer.Location = new System.Drawing.Point(0, 120);
            this.tilesetViewer.Name = "tilesetViewer";
            this.tilesetViewer.Size = new System.Drawing.Size(600, 380);
            this.tilesetViewer.TabIndex = 1;
            // 
            // TextureViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.tilesetViewer);
            this.Controls.Add(this.panelInfo);
            this.Name = "TextureViewer";
            this.Size = new System.Drawing.Size(600, 500);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelBPP;
        private System.Windows.Forms.Label labelDataLength;
        private System.Windows.Forms.Label labelTransparent;
        private TilesetViewer tilesetViewer;
    }
}
