using System.Windows.Forms;

namespace csharp_editor
{
    partial class MainView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_view = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // MainView
            // 
            this.panel_view.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_view.Location = new System.Drawing.Point(0, 0);
            this.panel_view.Name = "MainView";
            this.panel_view.Size = new System.Drawing.Size(150, 150);
            this.panel_view.TabIndex = 0;
            // 
            // view
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.panel_view);
            this.Name = "view";
            this.Load += new System.EventHandler(this.view_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel_view;
    }
}
