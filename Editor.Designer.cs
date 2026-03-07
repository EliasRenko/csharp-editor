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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            view_extern = new ExternView();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem_open = new ToolStripMenuItem();
            toolStripMenuItem_export = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            ToolStripMenuItem_textureInfo = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            console = new csharp_editor.UserControls.DebugConsole();
            toolStripMenuItem1 = new ToolStripMenuItem();
            panelRight = new Panel();
            entitySelector = new EntitySelector();
            textureViewer = new TextureViewer();
            hierarchyTree = new HierarchyTree();
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButton5 = new ToolStripButton();
            toolStripButton6 = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton_tilesets = new ToolStripButton();
            toolStripButton_entitiesDefs = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripButton7 = new ToolStripButton();
            propertyGridPanel1 = new PropertyGridPanel();
            toolStrip2 = new ToolStrip();
            toolStripButton_tileDraw = new ToolStripButton();
            toolStripButton_tileErase = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripButton_entityAdd = new ToolStripButton();
            toolStripButton_entitySelect = new ToolStripButton();
            menuStrip1.SuspendLayout();
            panelRight.SuspendLayout();
            toolStrip1.SuspendLayout();
            toolStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // view_extern
            // 
            view_extern.BackColor = SystemColors.ControlDark;
            view_extern.Dock = DockStyle.Fill;
            view_extern.Location = new Point(265, 49);
            view_extern.Name = "view_extern";
            view_extern.Size = new Size(530, 569);
            view_extern.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, toolStripMenuItem2, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1058, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem_open, toolStripMenuItem_export });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem_open
            // 
            toolStripMenuItem_open.Name = "toolStripMenuItem_open";
            toolStripMenuItem_open.Size = new Size(108, 22);
            toolStripMenuItem_open.Text = "Open";
            // 
            // toolStripMenuItem_export
            // 
            toolStripMenuItem_export.Name = "toolStripMenuItem_export";
            toolStripMenuItem_export.Size = new Size(108, 22);
            toolStripMenuItem_export.Text = "Export";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItem_textureInfo });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(46, 20);
            toolStripMenuItem2.Text = "Tools";
            // 
            // ToolStripMenuItem_textureInfo
            // 
            ToolStripMenuItem_textureInfo.Name = "ToolStripMenuItem_textureInfo";
            ToolStripMenuItem_textureInfo.Size = new Size(136, 22);
            ToolStripMenuItem_textureInfo.Text = "Texture info";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 746);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1058, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // console
            // 
            console.BackColor = SystemColors.Control;
            console.Dock = DockStyle.Bottom;
            console.Location = new Point(0, 618);
            console.Margin = new Padding(4);
            console.Name = "console";
            console.Padding = new Padding(4);
            console.Size = new Size(1058, 128);
            console.TabIndex = 3;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(32, 19);
            // 
            // panelRight
            // 
            panelRight.BackColor = SystemColors.ControlDark;
            panelRight.Controls.Add(entitySelector);
            panelRight.Controls.Add(textureViewer);
            panelRight.Controls.Add(hierarchyTree);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(795, 49);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(263, 569);
            panelRight.TabIndex = 7;
            // 
            // entitySelector
            // 
            entitySelector.Dock = DockStyle.Fill;
            entitySelector.Location = new Point(0, 344);
            entitySelector.Name = "entitySelector";
            entitySelector.Size = new Size(263, 225);
            entitySelector.TabIndex = 2;
            entitySelector.Visible = false;
            // 
            // textureViewer
            // 
            textureViewer.BackColor = SystemColors.ControlDark;
            textureViewer.Dock = DockStyle.Fill;
            textureViewer.Location = new Point(0, 344);
            textureViewer.Name = "textureViewer";
            textureViewer.RegionSelectionMode = false;
            textureViewer.Size = new Size(263, 225);
            textureViewer.SnapToGrid = true;
            textureViewer.TabIndex = 1;
            // 
            // hierarchyTree
            // 
            hierarchyTree.BackColor = SystemColors.Control;
            hierarchyTree.Dock = DockStyle.Top;
            hierarchyTree.Location = new Point(0, 0);
            hierarchyTree.Name = "hierarchyTree";
            hierarchyTree.Size = new Size(263, 344);
            hierarchyTree.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripSeparator1, toolStripButton5, toolStripButton6, toolStripSeparator2, toolStripButton_tilesets, toolStripButton_entitiesDefs, toolStripSeparator3, toolStripButton7 });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1058, 25);
            toolStrip1.TabIndex = 8;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = Properties.Resources.page_white;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = Properties.Resources.folder_page;
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = Properties.Resources.disk;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(23, 22);
            toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = Properties.Resources.disk_multiple;
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(23, 22);
            toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton5.Image = Properties.Resources.application_view_list;
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(23, 22);
            toolStripButton5.Text = "toolStripButton5";
            // 
            // toolStripButton6
            // 
            toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton6.Image = Properties.Resources.control_play;
            toolStripButton6.ImageTransparentColor = Color.Magenta;
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new Size(23, 22);
            toolStripButton6.Text = "toolStripButton6";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripButton_tilesets
            // 
            toolStripButton_tilesets.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_tilesets.Image = Properties.Resources.folder_image;
            toolStripButton_tilesets.ImageTransparentColor = Color.Magenta;
            toolStripButton_tilesets.Name = "toolStripButton_tilesets";
            toolStripButton_tilesets.Size = new Size(23, 22);
            toolStripButton_tilesets.Text = "toolStripButton7";
            // 
            // toolStripButton_entitiesDefs
            // 
            toolStripButton_entitiesDefs.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_entitiesDefs.Image = Properties.Resources.folder_lightbulb;
            toolStripButton_entitiesDefs.ImageTransparentColor = Color.Magenta;
            toolStripButton_entitiesDefs.Name = "toolStripButton_entitiesDefs";
            toolStripButton_entitiesDefs.Size = new Size(23, 22);
            toolStripButton_entitiesDefs.Text = "toolStripButton8";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // toolStripButton7
            // 
            toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton7.Image = Properties.Resources.shading;
            toolStripButton7.ImageTransparentColor = Color.Magenta;
            toolStripButton7.Name = "toolStripButton7";
            toolStripButton7.Size = new Size(23, 22);
            toolStripButton7.Text = "toolStripButton7";
            // 
            // propertyGridPanel1
            // 
            propertyGridPanel1.Dock = DockStyle.Left;
            propertyGridPanel1.Location = new Point(0, 49);
            propertyGridPanel1.Name = "propertyGridPanel1";
            propertyGridPanel1.Size = new Size(265, 569);
            propertyGridPanel1.TabIndex = 9;
            // 
            // toolStrip2
            // 
            toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip2.Items.AddRange(new ToolStripItem[] { toolStripButton_tileDraw, toolStripButton_tileErase, toolStripSeparator4, toolStripButton_entityAdd, toolStripButton_entitySelect });
            toolStrip2.Location = new Point(265, 49);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new Size(530, 25);
            toolStrip2.TabIndex = 10;
            toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton_tileDraw
            // 
            toolStripButton_tileDraw.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_tileDraw.Image = (Image)resources.GetObject("toolStripButton_tileDraw.Image");
            toolStripButton_tileDraw.ImageTransparentColor = Color.Magenta;
            toolStripButton_tileDraw.Name = "toolStripButton_tileDraw";
            toolStripButton_tileDraw.Size = new Size(23, 22);
            toolStripButton_tileDraw.Text = "toolStripButton1";
            // 
            // toolStripButton_tileErase
            // 
            toolStripButton_tileErase.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_tileErase.Image = (Image)resources.GetObject("toolStripButton_tileErase.Image");
            toolStripButton_tileErase.ImageTransparentColor = Color.Magenta;
            toolStripButton_tileErase.Name = "toolStripButton_tileErase";
            toolStripButton_tileErase.Size = new Size(23, 22);
            toolStripButton_tileErase.Text = "toolStripButton2";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // toolStripButton_entityAdd
            // 
            toolStripButton_entityAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_entityAdd.Image = Properties.Resources.lightbulb;
            toolStripButton_entityAdd.ImageTransparentColor = Color.Magenta;
            toolStripButton_entityAdd.Name = "toolStripButton_entityAdd";
            toolStripButton_entityAdd.Size = new Size(23, 22);
            toolStripButton_entityAdd.Text = "toolStripButton5";
            // 
            // toolStripButton_entitySelect
            // 
            toolStripButton_entitySelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_entitySelect.Image = Properties.Resources.cursor;
            toolStripButton_entitySelect.ImageTransparentColor = Color.Magenta;
            toolStripButton_entitySelect.Name = "toolStripButton_entitySelect";
            toolStripButton_entitySelect.Size = new Size(23, 22);
            toolStripButton_entitySelect.Text = "toolStripButton6";
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 768);
            Controls.Add(toolStrip2);
            Controls.Add(view_extern);
            Controls.Add(propertyGridPanel1);
            Controls.Add(panelRight);
            Controls.Add(toolStrip1);
            Controls.Add(console);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Editor";
            Text = "Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelRight.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton_tileDraw;
        private System.Windows.Forms.ToolStripButton toolStripButton_tileErase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton_entityAdd;
        private System.Windows.Forms.ToolStripButton toolStripButton_entitySelect;

        private csharp_editor.UserControls.PropertyGridPanel propertyGridPanel1;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

        #endregion

        private csharp_editor.UserControls.ExternView view_extern;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem_open;
        private ToolStripMenuItem toolStripMenuItem_export;
        private StatusStrip statusStrip1;
        private csharp_editor.UserControls.DebugConsole console;
        private System.Windows.Forms.Panel panelRight;
        private csharp_editor.UserControls.HierarchyTree hierarchyTree;
        private csharp_editor.UserControls.TextureViewer textureViewer;
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
        private ToolStripButton toolStripButton_entitiesDefs;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem ToolStripMenuItem_textureInfo;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton7;
    }
}