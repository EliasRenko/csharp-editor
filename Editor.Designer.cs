using csharp_editor.UserControls;
using csharp_editor;

namespace csharp_editor {
    partial class Editor {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            view_extern = new csharp_editor.ExternView();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_open = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_export = new System.Windows.Forms.ToolStripMenuItem();
            editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            console = new csharp_editor.UserControls.Console();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            buttonTextureView = new System.Windows.Forms.Button();
            buttonTilesets = new System.Windows.Forms.Button();
            buttonEntities = new System.Windows.Forms.Button();
            hierarchyTree = new csharp_editor.HierarchyTree();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // view_extern
            // 
            view_extern.BackColor = System.Drawing.SystemColors.ControlDark;
            view_extern.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            view_extern.Location = new System.Drawing.Point(0, 24);
            view_extern.Name = "view_extern";
            view_extern.Size = new System.Drawing.Size(624, 395);
            view_extern.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(624, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_open, toolStripMenuItem_export });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem_open
            // 
            toolStripMenuItem_open.Name = "toolStripMenuItem_open";
            toolStripMenuItem_open.Size = new System.Drawing.Size(108, 22);
            toolStripMenuItem_open.Text = "Open";
            // 
            // toolStripMenuItem_export
            // 
            toolStripMenuItem_export.Name = "toolStripMenuItem_export";
            toolStripMenuItem_export.Size = new System.Drawing.Size(108, 22);
            toolStripMenuItem_export.Text = "Export";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new System.Drawing.Point(0, 419);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(624, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // console
            // 
            console.BackColor = System.Drawing.SystemColors.Control;
            console.Dock = System.Windows.Forms.DockStyle.Bottom;
            console.Location = new System.Drawing.Point(0, 291);
            console.Margin = new System.Windows.Forms.Padding(4);
            console.Name = "console";
            console.Padding = new System.Windows.Forms.Padding(4);
            console.Size = new System.Drawing.Size(624, 128);
            console.TabIndex = 3;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // buttonTextureView
            // 
            buttonTextureView.Location = new System.Drawing.Point(12, 27);
            buttonTextureView.Name = "buttonTextureView";
            buttonTextureView.Size = new System.Drawing.Size(75, 23);
            buttonTextureView.TabIndex = 4;
            buttonTextureView.Text = "View Texture";
            buttonTextureView.UseVisualStyleBackColor = true;
            // 
            // buttonTilesets
            // 
            buttonTilesets.Location = new System.Drawing.Point(93, 27);
            buttonTilesets.Name = "buttonTilesets";
            buttonTilesets.Size = new System.Drawing.Size(75, 23);
            buttonTilesets.TabIndex = 5;
            buttonTilesets.Text = "Tilesets";
            buttonTilesets.UseVisualStyleBackColor = true;
            // 
            // buttonEntities
            // 
            buttonEntities.Location = new System.Drawing.Point(174, 27);
            buttonEntities.Name = "buttonEntities";
            buttonEntities.Size = new System.Drawing.Size(75, 23);
            buttonEntities.TabIndex = 6;
            buttonEntities.Text = "Entities";
            buttonEntities.UseVisualStyleBackColor = true;
            // 
            // hierarchyTree
            // 
            hierarchyTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            hierarchyTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            hierarchyTree.Location = new System.Drawing.Point(624, 24);
            hierarchyTree.Name = "hierarchyTree";
            hierarchyTree.Size = new System.Drawing.Size(250, 395);
            hierarchyTree.TabIndex = 7;
            // 
            // Editor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(874, 441);
            Controls.Add(hierarchyTree);
            Controls.Add(buttonEntities);
            Controls.Add(buttonTilesets);
            Controls.Add(buttonTextureView);
            Controls.Add(console);
            Controls.Add(view_extern);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            MainMenuStrip = menuStrip1;
            Text = "Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button buttonEntities;

        private System.Windows.Forms.Button buttonTilesets;

        private System.Windows.Forms.Button buttonTextureView;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

        #endregion

        private ExternView view_extern;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem_open;
        private ToolStripMenuItem toolStripMenuItem_export;
        private StatusStrip statusStrip1;
        private UserControls.Console console;
        private HierarchyTree hierarchyTree;
    }
}