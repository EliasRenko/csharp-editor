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
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            ToolStripMenuItem_textureViewer = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            console = new csharp_editor.UserControls.Console();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            panelRight = new System.Windows.Forms.Panel();
            entitySelector = new csharp_editor.UserControls.EntitySelector();
            tilesetViewer = new csharp_editor.UserControls.TilesetViewer();
            hierarchyTree = new csharp_editor.HierarchyTree();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButton_tilesets = new System.Windows.Forms.ToolStripButton();
            toolStripButton_entities = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            propertyGridPanel1 = new csharp_editor.UserControls.PropertyGridPanel();
            menuStrip1.SuspendLayout();
            panelRight.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // view_extern
            // 
            view_extern.BackColor = System.Drawing.SystemColors.ControlDark;
            view_extern.Dock = System.Windows.Forms.DockStyle.Fill;
            view_extern.Location = new System.Drawing.Point(265, 49);
            view_extern.Name = "view_extern";
            view_extern.Size = new System.Drawing.Size(476, 569);
            view_extern.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, toolStripMenuItem2, helpToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1058, 24);
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
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { ToolStripMenuItem_textureViewer });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(46, 20);
            toolStripMenuItem2.Text = "Tools";
            // 
            // ToolStripMenuItem_textureViewer
            // 
            ToolStripMenuItem_textureViewer.Name = "ToolStripMenuItem_textureViewer";
            ToolStripMenuItem_textureViewer.Size = new System.Drawing.Size(149, 22);
            ToolStripMenuItem_textureViewer.Text = "Texture viewer";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new System.Drawing.Point(0, 746);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(1058, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // console
            // 
            console.BackColor = System.Drawing.SystemColors.Control;
            console.Dock = System.Windows.Forms.DockStyle.Bottom;
            console.Location = new System.Drawing.Point(0, 618);
            console.Margin = new System.Windows.Forms.Padding(4);
            console.Name = "console";
            console.Padding = new System.Windows.Forms.Padding(4);
            console.Size = new System.Drawing.Size(1058, 128);
            console.TabIndex = 3;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // panelRight
            // 
            panelRight.BackColor = System.Drawing.SystemColors.ControlDark;
            panelRight.Controls.Add(entitySelector);
            panelRight.Controls.Add(tilesetViewer);
            panelRight.Controls.Add(hierarchyTree);
            panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            panelRight.Location = new System.Drawing.Point(741, 49);
            panelRight.Name = "panelRight";
            panelRight.Size = new System.Drawing.Size(317, 569);
            panelRight.TabIndex = 7;
            // 
            // entitySelector
            // 
            entitySelector.Dock = System.Windows.Forms.DockStyle.Fill;
            entitySelector.Location = new System.Drawing.Point(0, 344);
            entitySelector.Name = "entitySelector";
            entitySelector.Size = new System.Drawing.Size(317, 225);
            entitySelector.TabIndex = 2;
            entitySelector.Visible = false;
            // 
            // tilesetViewer
            // 
            tilesetViewer.BackColor = System.Drawing.SystemColors.ControlDark;
            tilesetViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            tilesetViewer.Location = new System.Drawing.Point(0, 344);
            tilesetViewer.Name = "tilesetViewer";
            tilesetViewer.RegionSelectionMode = false;
            tilesetViewer.Size = new System.Drawing.Size(317, 225);
            tilesetViewer.SnapToGrid = true;
            tilesetViewer.TabIndex = 1;
            // 
            // hierarchyTree
            // 
            hierarchyTree.BackColor = System.Drawing.SystemColors.Control;
            hierarchyTree.Dock = System.Windows.Forms.DockStyle.Top;
            hierarchyTree.Location = new System.Drawing.Point(0, 0);
            hierarchyTree.Name = "hierarchyTree";
            hierarchyTree.Size = new System.Drawing.Size(317, 344);
            hierarchyTree.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripSeparator1, toolStripButton5, toolStripButton6, toolStripSeparator2, toolStripButton_tilesets, toolStripButton_entities, toolStripSeparator3, toolStripButton7 });
            toolStrip1.Location = new System.Drawing.Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(1058, 25);
            toolStrip1.TabIndex = 8;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = global::csharp_editor.Properties.Resources.page_white;
            toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new System.Drawing.Size(23, 22);
            toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = global::csharp_editor.Properties.Resources.folder_page;
            toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new System.Drawing.Size(23, 22);
            toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = global::csharp_editor.Properties.Resources.disk;
            toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new System.Drawing.Size(23, 22);
            toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = global::csharp_editor.Properties.Resources.disk_multiple;
            toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new System.Drawing.Size(23, 22);
            toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton5.Image = global::csharp_editor.Properties.Resources.application_view_list;
            toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new System.Drawing.Size(23, 22);
            toolStripButton5.Text = "toolStripButton5";
            // 
            // toolStripButton6
            // 
            toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton6.Image = global::csharp_editor.Properties.Resources.control_play;
            toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new System.Drawing.Size(23, 22);
            toolStripButton6.Text = "toolStripButton6";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_tilesets
            // 
            toolStripButton_tilesets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_tilesets.Image = global::csharp_editor.Properties.Resources.folder_image;
            toolStripButton_tilesets.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_tilesets.Name = "toolStripButton_tilesets";
            toolStripButton_tilesets.Size = new System.Drawing.Size(23, 22);
            toolStripButton_tilesets.Text = "toolStripButton7";
            // 
            // toolStripButton_entities
            // 
            toolStripButton_entities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_entities.Image = global::csharp_editor.Properties.Resources.folder_lightbulb;
            toolStripButton_entities.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_entities.Name = "toolStripButton_entities";
            toolStripButton_entities.Size = new System.Drawing.Size(23, 22);
            toolStripButton_entities.Text = "toolStripButton8";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton7
            // 
            toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton7.Image = global::csharp_editor.Properties.Resources.shading;
            toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton7.Name = "toolStripButton7";
            toolStripButton7.Size = new System.Drawing.Size(23, 22);
            toolStripButton7.Text = "toolStripButton7";
            // 
            // propertyGridPanel1
            // 
            propertyGridPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            propertyGridPanel1.Location = new System.Drawing.Point(0, 49);
            propertyGridPanel1.Name = "propertyGridPanel1";
            propertyGridPanel1.Size = new System.Drawing.Size(265, 569);
            propertyGridPanel1.TabIndex = 9;
            // 
            // Editor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1058, 768);
            Controls.Add(view_extern);
            Controls.Add(propertyGridPanel1);
            Controls.Add(panelRight);
            Controls.Add(toolStrip1);
            Controls.Add(console);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Text = "Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelRight.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private csharp_editor.UserControls.PropertyGridPanel propertyGridPanel1;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

        #endregion

        private csharp_editor.ExternView view_extern;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem_open;
        private ToolStripMenuItem toolStripMenuItem_export;
        private StatusStrip statusStrip1;
        private csharp_editor.UserControls.Console console;
        private System.Windows.Forms.Panel panelRight;
        private csharp_editor.HierarchyTree hierarchyTree;
        private csharp_editor.UserControls.TilesetViewer tilesetViewer;
        private csharp_editor.UserControls.EntitySelector entitySelector;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton5;
        private ToolStripButton toolStripButton6;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton_tilesets;
        private ToolStripButton toolStripButton_entities;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem ToolStripMenuItem_textureViewer;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton7;
    }
}