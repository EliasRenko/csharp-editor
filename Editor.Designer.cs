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
            view_extern = new ExternView();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem_open = new ToolStripMenuItem();
            toolStripMenuItem_export = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            projectToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveProjectToolStripMenuItem = new ToolStripMenuItem();
            saveAsProjectToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem1 = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            ToolStripMenuItem_timeline = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            statusLabel_project = new ToolStripStatusLabel();
            console = new DebugConsole();
            toolStripMenuItem1 = new ToolStripMenuItem();
            panelRight = new Panel();
            entitySelector = new EntitySelector();
            textureViewer = new TextureViewer();
            hierarchyTree = new HierarchyTree();
            toolStrip1 = new ToolStrip();
            toolStripButton_newMap = new ToolStripButton();
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
            tabControl1 = new TabControl();
            panelMain = new Panel();
            button_cursor = new Button();
            button_entity = new Button();
            button_brush = new Button();
            splitterLeft = new Splitter();
            splitterRight = new Splitter();
            menuStrip1.SuspendLayout();
            panelRight.SuspendLayout();
            toolStrip1.SuspendLayout();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // view_extern
            // 
            view_extern.BackColor = SystemColors.ControlDark;
            view_extern.Dock = DockStyle.Fill;
            view_extern.Location = new Point(269, 34);
            view_extern.Name = "view_extern";
            view_extern.Size = new Size(522, 512);
            view_extern.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, projectToolStripMenuItem, viewToolStripMenuItem, toolStripMenuItem2, helpToolStripMenuItem });
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
            // projectToolStripMenuItem
            // 
            projectToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveProjectToolStripMenuItem, saveAsProjectToolStripMenuItem, editToolStripMenuItem1, closeToolStripMenuItem });
            projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            projectToolStripMenuItem.Size = new Size(56, 20);
            projectToolStripMenuItem.Text = "Project";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(195, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // saveProjectToolStripMenuItem
            // 
            saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            saveProjectToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveProjectToolStripMenuItem.Size = new Size(195, 22);
            saveProjectToolStripMenuItem.Text = "Save";
            // 
            // saveAsProjectToolStripMenuItem
            // 
            saveAsProjectToolStripMenuItem.Name = "saveAsProjectToolStripMenuItem";
            saveAsProjectToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsProjectToolStripMenuItem.Size = new Size(195, 22);
            saveAsProjectToolStripMenuItem.Text = "Save As...";
            // 
            // editToolStripMenuItem1
            // 
            editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            editToolStripMenuItem1.Size = new Size(195, 22);
            editToolStripMenuItem1.Text = "Edit";
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(195, 22);
            closeToolStripMenuItem.Text = "Close";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItem_timeline });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(46, 20);
            toolStripMenuItem2.Text = "Tools";
            // 
            // ToolStripMenuItem_timeline
            // 
            ToolStripMenuItem_timeline.Name = "ToolStripMenuItem_timeline";
            ToolStripMenuItem_timeline.Size = new Size(153, 22);
            ToolStripMenuItem_timeline.Text = "Timeline demo";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel_project });
            statusStrip1.Location = new Point(0, 746);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1058, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel_project
            // 
            statusLabel_project.Name = "statusLabel_project";
            statusLabel_project.Text = "No project loaded";
            // 
            // console
            // 
            console.BackColor = SystemColors.Control;
            console.Dock = DockStyle.Bottom;
            console.Location = new Point(0, 595);
            console.Margin = new Padding(4);
            console.Name = "console";
            console.Padding = new Padding(4);
            console.Size = new Size(1058, 151);
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
            panelRight.Location = new Point(795, 34);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(263, 512);
            panelRight.TabIndex = 7;
            // 
            // entitySelector
            // 
            entitySelector.Dock = DockStyle.Fill;
            entitySelector.Location = new Point(0, 344);
            entitySelector.Name = "entitySelector";
            entitySelector.Size = new Size(263, 168);
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
            textureViewer.ShowGrid = false;
            textureViewer.Size = new Size(263, 168);
            textureViewer.SnapToGrid = true;
            textureViewer.TabIndex = 1;
            textureViewer.TileSize = 0;
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
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton_newMap, toolStripButton2, toolStripButton3, toolStripButton4, toolStripSeparator1, toolStripButton5, toolStripButton6, toolStripSeparator2, toolStripButton_tilesets, toolStripButton_entitiesDefs, toolStripSeparator3, toolStripButton7 });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1058, 25);
            toolStrip1.TabIndex = 8;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_newMap
            // 
            toolStripButton_newMap.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_newMap.Image = Properties.Resources.page_white;
            toolStripButton_newMap.ImageTransparentColor = Color.Magenta;
            toolStripButton_newMap.Name = "toolStripButton_newMap";
            toolStripButton_newMap.Size = new Size(23, 22);
            toolStripButton_newMap.Text = "New map";
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
            propertyGridPanel1.Location = new Point(0, 34);
            propertyGridPanel1.Name = "propertyGridPanel1";
            propertyGridPanel1.Size = new Size(265, 512);
            propertyGridPanel1.TabIndex = 9;
            // 
            // tabControl1
            // 
            tabControl1.Dock = DockStyle.Top;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.ItemSize = new Size(160, 28);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1058, 34);
            tabControl1.TabIndex = 12;
            tabControl1.Visible = false;
            // 
            // panelMain
            // 
            panelMain.Controls.Add(button_cursor);
            panelMain.Controls.Add(button_entity);
            panelMain.Controls.Add(button_brush);
            panelMain.Controls.Add(view_extern);
            panelMain.Controls.Add(splitterLeft);
            panelMain.Controls.Add(propertyGridPanel1);
            panelMain.Controls.Add(splitterRight);
            panelMain.Controls.Add(panelRight);
            panelMain.Controls.Add(tabControl1);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 49);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1058, 546);
            panelMain.TabIndex = 11;
            panelMain.Visible = false;
            // 
            // button_cursor
            // 
            button_cursor.BackColor = SystemColors.ControlDark;
            button_cursor.Image = Properties.Resources.icon_cursor;
            button_cursor.Location = new Point(275, 136);
            button_cursor.Name = "button_cursor";
            button_cursor.Size = new Size(42, 42);
            button_cursor.TabIndex = 17;
            button_cursor.UseVisualStyleBackColor = false;
            // 
            // button_entity
            // 
            button_entity.BackColor = SystemColors.ControlDark;
            button_entity.Image = Properties.Resources.entity;
            button_entity.Location = new Point(275, 88);
            button_entity.Name = "button_entity";
            button_entity.Size = new Size(42, 42);
            button_entity.TabIndex = 16;
            button_entity.UseVisualStyleBackColor = false;
            // 
            // button_brush
            // 
            button_brush.BackColor = SystemColors.ControlDark;
            button_brush.Image = Properties.Resources.brush;
            button_brush.Location = new Point(275, 40);
            button_brush.Name = "button_brush";
            button_brush.Size = new Size(42, 42);
            button_brush.TabIndex = 15;
            button_brush.UseVisualStyleBackColor = false;
            // 
            // splitterLeft
            // 
            splitterLeft.Location = new Point(265, 34);
            splitterLeft.MinExtra = 200;
            splitterLeft.MinSize = 120;
            splitterLeft.Name = "splitterLeft";
            splitterLeft.Size = new Size(4, 512);
            splitterLeft.TabIndex = 13;
            splitterLeft.TabStop = false;
            // 
            // splitterRight
            // 
            splitterRight.Dock = DockStyle.Right;
            splitterRight.Location = new Point(791, 34);
            splitterRight.MinExtra = 200;
            splitterRight.MinSize = 150;
            splitterRight.Name = "splitterRight";
            splitterRight.Size = new Size(4, 512);
            splitterRight.TabIndex = 14;
            splitterRight.TabStop = false;
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 768);
            Controls.Add(panelMain);
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
            panelMain.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button button_cursor;

        private System.Windows.Forms.Button button_entity;

        private System.Windows.Forms.Button button_brush;

        private csharp_editor.UserControls.PropertyGridPanel propertyGridPanel1;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Splitter splitterLeft;
        private System.Windows.Forms.Splitter splitterRight;
        private csharp_editor.UserControls.ExternView view_extern;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem_open;
        private ToolStripMenuItem toolStripMenuItem_export;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel_project;
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
        private ToolStripMenuItem ToolStripMenuItem_timeline;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton7;
        private ToolStripMenuItem projectToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveProjectToolStripMenuItem;
        private ToolStripMenuItem saveAsProjectToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem1;
        private ToolStripMenuItem closeToolStripMenuItem;
    }
}