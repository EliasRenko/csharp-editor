using csharp_editor.UserControls;
using csharp_editor;
using WeifenLuo.WinFormsUI.Docking;

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
            view_extern = new csharp_editor.UserControls.ExternView();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_open = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_export = new System.Windows.Forms.ToolStripMenuItem();
            editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            editToolStripMenuItem_editProject = new System.Windows.Forms.ToolStripMenuItem();
            closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewMenuItem_properties = new System.Windows.Forms.ToolStripMenuItem();
            viewMenuItem_hierarchy = new System.Windows.Forms.ToolStripMenuItem();
            viewMenuItem_console = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            ToolStripMenuItem_timeline = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_theme = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            statusLabel_project = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            button_cursor = new System.Windows.Forms.Button();
            button_entity = new System.Windows.Forms.Button();
            button_brush = new System.Windows.Forms.Button();
            dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            toolStripButton_toggleLabels = new System.Windows.Forms.ToolStripButton();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // view_extern
            // 
            view_extern.BackColor = System.Drawing.SystemColors.ControlDark;
            view_extern.Dock = System.Windows.Forms.DockStyle.Fill;
            view_extern.Location = new System.Drawing.Point(269, 0);
            view_extern.Name = "view_extern";
            view_extern.Size = new System.Drawing.Size(522, 463);
            view_extern.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, projectToolStripMenuItem, viewToolStripMenuItem, toolStripMenuItem2, helpToolStripMenuItem });
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
            // projectToolStripMenuItem
            // 
            projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openToolStripMenuItem, saveProjectToolStripMenuItem, saveAsProjectToolStripMenuItem, editToolStripMenuItem_editProject, closeToolStripMenuItem });
            projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            projectToolStripMenuItem.Text = "Project";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // saveProjectToolStripMenuItem
            // 
            saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            saveProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            saveProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            saveProjectToolStripMenuItem.Text = "Save";
            // 
            // saveAsProjectToolStripMenuItem
            // 
            saveAsProjectToolStripMenuItem.Name = "saveAsProjectToolStripMenuItem";
            saveAsProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) | System.Windows.Forms.Keys.S));
            saveAsProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            saveAsProjectToolStripMenuItem.Text = "Save As...";
            // 
            // editToolStripMenuItem_editProject
            // 
            editToolStripMenuItem_editProject.Name = "editToolStripMenuItem_editProject";
            editToolStripMenuItem_editProject.Size = new System.Drawing.Size(195, 22);
            editToolStripMenuItem_editProject.Text = "Edit";
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            closeToolStripMenuItem.Text = "Close";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { viewMenuItem_properties, viewMenuItem_hierarchy, viewMenuItem_console });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // viewMenuItem_properties
            // 
            viewMenuItem_properties.Name = "viewMenuItem_properties";
            viewMenuItem_properties.Size = new System.Drawing.Size(127, 22);
            viewMenuItem_properties.Text = "Properties";
            // 
            // viewMenuItem_hierarchy
            // 
            viewMenuItem_hierarchy.Name = "viewMenuItem_hierarchy";
            viewMenuItem_hierarchy.Size = new System.Drawing.Size(127, 22);
            viewMenuItem_hierarchy.Text = "Hierarchy";
            // 
            // viewMenuItem_console
            // 
            viewMenuItem_console.Name = "viewMenuItem_console";
            viewMenuItem_console.Size = new System.Drawing.Size(127, 22);
            viewMenuItem_console.Text = "Console";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { ToolStripMenuItem_timeline, toolStripMenuItem_theme });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(46, 20);
            toolStripMenuItem2.Text = "Tools";
            // 
            // ToolStripMenuItem_timeline
            // 
            ToolStripMenuItem_timeline.Name = "ToolStripMenuItem_timeline";
            ToolStripMenuItem_timeline.Size = new System.Drawing.Size(153, 22);
            ToolStripMenuItem_timeline.Text = "Timeline demo";
            // 
            // toolStripMenuItem_theme
            // 
            toolStripMenuItem_theme.Name = "toolStripMenuItem_theme";
            toolStripMenuItem_theme.Size = new System.Drawing.Size(153, 22);
            toolStripMenuItem_theme.Text = "Theme...";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabel_project });
            statusStrip1.Location = new System.Drawing.Point(0, 746);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(1058, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel_project
            // 
            statusLabel_project.Name = "statusLabel_project";
            statusLabel_project.Size = new System.Drawing.Size(102, 17);
            statusLabel_project.Text = "No project loaded";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // entitySelector
            // 
            entitySelector.Dock = System.Windows.Forms.DockStyle.Fill;
            entitySelector.Location = new System.Drawing.Point(0, 344);
            entitySelector.Name = "entitySelector";
            entitySelector.Size = new System.Drawing.Size(263, 119);
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
            textureViewer.ShowGrid = false;
            textureViewer.Size = new System.Drawing.Size(263, 119);
            textureViewer.SnapToGrid = true;
            textureViewer.TabIndex = 1;
            textureViewer.TileSize = 0;
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
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton_newMap, toolStripButton2, toolStripButton3, toolStripButton4, toolStripSeparator1, toolStripButton5, toolStripButton6, toolStripSeparator2, toolStripButton_tilesets, toolStripButton_entitiesDefs, toolStripSeparator3, toolStripButton7, toolStripButton_toggleLabels });
            toolStrip1.Location = new System.Drawing.Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(1058, 25);
            toolStrip1.TabIndex = 8;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_newMap
            // 
            toolStripButton_newMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_newMap.Image = global::csharp_editor.Properties.Resources.page;
            toolStripButton_newMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_newMap.Margin = new System.Windows.Forms.Padding(6, 1, 0, 2);
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
            toolStripButton3.Margin = new System.Windows.Forms.Padding(1, 1, 0, 2);
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
            toolStripButton5.Image = global::csharp_editor.Properties.Resources.arrow_undo;
            toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new System.Drawing.Size(23, 22);
            toolStripButton5.Text = "toolStripButton5";
            // 
            // toolStripButton6
            // 
            toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton6.Image = global::csharp_editor.Properties.Resources.arrow_redo;
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
            // button_cursor
            // 
            button_cursor.BackColor = System.Drawing.SystemColors.ControlDark;
            button_cursor.Image = global::csharp_editor.Properties.Resources.icon_cursor;
            button_cursor.Location = new System.Drawing.Point(275, 136);
            button_cursor.Name = "button_cursor";
            button_cursor.Size = new System.Drawing.Size(42, 42);
            button_cursor.TabIndex = 17;
            button_cursor.UseVisualStyleBackColor = false;
            // 
            // button_entity
            // 
            button_entity.BackColor = System.Drawing.SystemColors.ControlDark;
            button_entity.Image = global::csharp_editor.Properties.Resources.entity;
            button_entity.Location = new System.Drawing.Point(275, 88);
            button_entity.Name = "button_entity";
            button_entity.Size = new System.Drawing.Size(42, 42);
            button_entity.TabIndex = 16;
            button_entity.UseVisualStyleBackColor = false;
            // 
            // button_brush
            // 
            button_brush.BackColor = System.Drawing.SystemColors.ControlDark;
            button_brush.Image = global::csharp_editor.Properties.Resources.brush;
            button_brush.Location = new System.Drawing.Point(275, 40);
            button_brush.Name = "button_brush";
            button_brush.Size = new System.Drawing.Size(42, 42);
            button_brush.TabIndex = 15;
            button_brush.UseVisualStyleBackColor = false;
            // 
            // dockPanel
            // 
            dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            dockPanel.Location = new System.Drawing.Point(0, 49);
            dockPanel.Name = "dockPanel";
            dockPanel.Size = new System.Drawing.Size(1058, 697);
            dockPanel.TabIndex = 3;
            // 
            // toolStripButton_toggleLabels
            // 
            toolStripButton_toggleLabels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_toggleLabels.Image = global::csharp_editor.Properties.Resources.tag;
            toolStripButton_toggleLabels.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton_toggleLabels.Name = "toolStripButton_toggleLabels";
            toolStripButton_toggleLabels.Size = new System.Drawing.Size(23, 22);
            // 
            // Editor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1058, 768);
            Controls.Add(dockPanel);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Text = "Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ToolStripButton toolStripButton_toggleLabels;

        private DockPanel dockPanel;

        private System.Windows.Forms.Button button_cursor;

        private System.Windows.Forms.Button button_entity;

        private System.Windows.Forms.Button button_brush;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

        #endregion

        private csharp_editor.UserControls.ExternView view_extern;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem viewMenuItem_properties;
        private ToolStripMenuItem viewMenuItem_hierarchy;
        private ToolStripMenuItem viewMenuItem_console;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem_open;
        private ToolStripMenuItem toolStripMenuItem_export;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel_project;

        private csharp_editor.UserControls.HierarchyTree hierarchyTree;
        private csharp_editor.UserControls.TextureViewer textureViewer;
        private csharp_editor.UserControls.EntitySelector entitySelector;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_newMap;
        private ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton_tilesets;
        private ToolStripButton toolStripButton_entitiesDefs;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem ToolStripMenuItem_timeline;
        private ToolStripMenuItem toolStripMenuItem_theme;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton7;
        private ToolStripMenuItem projectToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveProjectToolStripMenuItem;
        private ToolStripMenuItem saveAsProjectToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem_editProject;
        private ToolStripMenuItem closeToolStripMenuItem;
    }
}