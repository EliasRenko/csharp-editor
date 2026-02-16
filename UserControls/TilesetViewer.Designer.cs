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
            pictureBoxTexture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTexture).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxTexture
            // 
            pictureBoxTexture.BackColor = System.Drawing.SystemColors.Control;
            pictureBoxTexture.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBoxTexture.Location = new System.Drawing.Point(0, 0);
            pictureBoxTexture.Name = "pictureBoxTexture";
            pictureBoxTexture.Size = new System.Drawing.Size(600, 500);
            pictureBoxTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxTexture.TabIndex = 0;
            pictureBoxTexture.TabStop = false;
            // 
            // TilesetViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            Controls.Add(pictureBoxTexture);
            Size = new System.Drawing.Size(600, 500);
            ((System.ComponentModel.ISupportInitialize)pictureBoxTexture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxTexture;
    }
}
