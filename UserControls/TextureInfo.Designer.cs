namespace csharp_editor.UserControls {
    partial class TextureInfo {
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
            textureViewer = new csharp_editor.UserControls.TextureViewer();
            SuspendLayout();
            // 
            // textureViewer
            // 
            textureViewer.BackColor = System.Drawing.SystemColors.Control;
            textureViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            textureViewer.Location = new System.Drawing.Point(0, 0);
            textureViewer.Name = "textureViewer";
            textureViewer.RegionSelectionMode = false;
            textureViewer.Size = new System.Drawing.Size(600, 500);
            textureViewer.SnapToGrid = true;
            textureViewer.TabIndex = 1;
            // 
            // TextureInfo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)((byte)30)), ((int)((byte)30)), ((int)((byte)30)));
            Controls.Add(textureViewer);
            Size = new System.Drawing.Size(600, 500);
            ResumeLayout(false);
        }

        #endregion

        private csharp_editor.UserControls.TextureViewer textureViewer;
    }
}
