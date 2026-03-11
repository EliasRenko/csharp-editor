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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            view_extern = new csharp_editor.UserControls.ExternView();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_open = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_export = new System.Windows.Forms.ToolStripMenuItem();
            editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            ToolStripMenuItem_textureInfo = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            console = new csharp_editor.UserControls.DebugConsole();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            panelRight = new System.Windows.Forms.Panel();
            entitySelector = new csharp_editor.UserControls.EntitySelector();
            textureViewer = new csharp_editor.UserControls.TextureViewer();
            hierarchyTree = new csharp_editor.UserControls.HierarchyTree();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStripButton_newMap = new System.Windows.Forms.ToolStripButton();
            toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButton_tilesets = new System.Windows.Forms.ToolStripButton();
            toolStripButton_entitiesDefs = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            propertyGridPanel1 = new csharp_editor.UserControls.PropertyGridPanel();
            toolStrip2 = new System.Windows.Forms.ToolStrip();
            toolStripButton_tileDraw = new System.Windows.Forms.ToolStripButton();
            toolStripButton_tileErase = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButton_entityAdd = new System.Windows.Forms.ToolStripButton();
            toolStripButton_entitySelect = new System.Windows.Forms.ToolStripButton();
            tabControl1 = new System.Windows.Forms.TabControl();
            panelMain = new System.Windows.Forms.Panel();
            menuStrip1.SuspendLayout();
            panelRight.SuspendLayout();
            toolStrip1.SuspendLayout();
            toolStrip2.SuspendLayout();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // view_extern
            // 
            view_extern.BackColor = System.Drawing.SystemColors.ControlDark;
            view_extern.Dock = System.Windows.Forms.DockStyle.Fill;
            view_extern.Location = new System.Drawing.Point(265, 39);
            view_extern.Name = "view_extern";
            view_extern.Size = new System.Drawing.Size(530, 502);
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
            toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { ToolStripMenuItem_textureInfo });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(46, 20);
            toolStripMenuItem2.Text = "Tools";
            // 
            // ToolStripMenuItem_textureInfo
            // 
            ToolStripMenuItem_textureInfo.Name = "ToolStripMenuItem_textureInfo";
            ToolStripMenuItem_textureInfo.Size = new System.Drawing.Size(136, 22);
            ToolStripMenuItem_textureInfo.Text = "Texture info";
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
            panelRight.Controls.Add(textureViewer);
            panelRight.Controls.Add(hierarchyTree);
            panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            panelRight.Location = new System.Drawing.Point(795, 39);
            panelRight.Name = "panelRight";
            panelRight.Size = new System.Drawing.Size(263, 502);
            panelRight.TabIndex = 7;
            // 
            // entitySelector
            // 
            entitySelector.Dock = System.Windows.Forms.DockStyle.Fill;
            entitySelector.Location = new System.Drawing.Point(0, 344);
            entitySelector.Name = "entitySelector";
            entitySelector.Size = new System.Drawing.Size(263, 158);
            entitySelector.TabIndex = 2;
            entitySelector.Visible = false;
            // 
            // textureViewer
            // 
            textureViewer.BackColor = System.Drawing.SystemColors.ControlDark;
            textureViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            textureViewer.Location = new System.Drawing.Point(0, 344);
            textureViewer.Name = "textureViewer";
            textureViewer.RegionSelectionMode = false;
            textureViewer.Size = new System.Drawing.Size(263, 158);
            textureViewer.SnapToGrid = true;
            textureViewer.TabIndex = 1;
            // 
            // hierarchyTree
            // 
            hierarchyTree.BackColor = System.Drawing.SystemColors.Control;
            hierarchyTree.Dock = System.Windows.Forms.DockStyle.Top;
            hierarchyTree.Location = new System.Drawing.Point(0, 0);
            hierarchyTree.Name = "hierarchyTree";
            hierarchyTree.Size = new System.Drawing.Size(263, 344);
            hierarchyTree.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton_newMap, toolStripButton2, toolStripButton3, toolStripButton4, toolStripSeparator1, toolStripButton5, toolStripButton6, toolStripSeparator2, toolStripButton_tilesets, toolStripButton_entitiesDefs, toolStripSeparator3, toolStripButton7 });
            toolStrip1.Location = new System.Drawing.Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(1058, 25);
            toolStrip1.TabIndex = 8;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_newMap
            // 
            toolStripButton_newMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_newMap.Image = global::csharp_editor.Properties.Resources.page_white;
            toolStripButton_newMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_newMap.Name = "toolStripButton_newMap";
            toolStripButton_newMap.Size = new System.Drawing.Size(23, 22);
            toolStripButton_newMap.Text = "New map";
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
            // toolStripButton_entitiesDefs
            // 
            toolStripButton_entitiesDefs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_entitiesDefs.Image = global::csharp_editor.Properties.Resources.folder_lightbulb;
            toolStripButton_entitiesDefs.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_entitiesDefs.Name = "toolStripButton_entitiesDefs";
            toolStripButton_entitiesDefs.Size = new System.Drawing.Size(23, 22);
            toolStripButton_entitiesDefs.Text = "toolStripButton8";
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
            propertyGridPanel1.Location = new System.Drawing.Point(0, 39);
            propertyGridPanel1.Name = "propertyGridPanel1";
            propertyGridPanel1.Size = new System.Drawing.Size(265, 502);
            propertyGridPanel1.TabIndex = 9;
            // 
            // toolStrip2
            // 
            toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton_tileDraw, toolStripButton_tileErase, toolStripSeparator4, toolStripButton_entityAdd, toolStripButton_entitySelect });
            toolStrip2.Location = new System.Drawing.Point(0, 0);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new System.Drawing.Size(1058, 39);
            toolStrip2.TabIndex = 10;
            toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton_tileDraw
            // 
            toolStripButton_tileDraw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_tileDraw.Image = global::csharp_editor.Properties.Resources.brush;
            toolStripButton_tileDraw.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_tileDraw.Name = "toolStripButton_tileDraw";
            toolStripButton_tileDraw.Size = new System.Drawing.Size(36, 36);
            toolStripButton_tileDraw.Text = "toolStripButton1";
            // 
            // toolStripButton_tileErase
            // 
            toolStripButton_tileErase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_tileErase.Image = ((System.Drawing.Image)resources.GetObject("toolStripButton_tileErase.Image"));
            toolStripButton_tileErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_tileErase.Name = "toolStripButton_tileErase";
            toolStripButton_tileErase.Size = new System.Drawing.Size(36, 36);
            toolStripButton_tileErase.Text = "toolStripButton2";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton_entityAdd
            // 
            toolStripButton_entityAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_entityAdd.Image = global::csharp_editor.Properties.Resources.entity;
            toolStripButton_entityAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_entityAdd.Name = "toolStripButton_entityAdd";
            toolStripButton_entityAdd.Size = new System.Drawing.Size(36, 36);
            toolStripButton_entityAdd.Text = "toolStripButton5";
            // 
            // toolStripButton_entitySelect
            // 
            toolStripButton_entitySelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_entitySelect.Image = global::csharp_editor.Properties.Resources.icon_cursor;
            toolStripButton_entitySelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_entitySelect.Name = "toolStripButton_entitySelect";
            toolStripButton_entitySelect.Size = new System.Drawing.Size(36, 36);
            toolStripButton_entitySelect.Text = "toolStripButton6";
            // 
            // tabControl1
            // 
            tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            tabControl1.ItemSize = new System.Drawing.Size(130, 22);
            tabControl1.Location = new System.Drawing.Point(0, 49);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1058, 28);
            tabControl1.TabIndex = 12;
            // 
            // panelMain
            // 
            panelMain.Controls.Add(view_extern);
            panelMain.Controls.Add(propertyGridPanel1);
            panelMain.Controls.Add(panelRight);
            panelMain.Controls.Add(toolStrip2);
            panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMain.Location = new System.Drawing.Point(0, 77);
            panelMain.Name = "panelMain";
            panelMain.Size = new System.Drawing.Size(1058, 541);
            panelMain.TabIndex = 11;
            panelMain.Visible = false;
            // 
            // Editor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1058, 768);
            Controls.Add(panelMain);
            Controls.Add(tabControl1);
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
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
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

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panelMain;
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
        private System.Windows.Forms.ToolStripButton toolStripButton_newMap;
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