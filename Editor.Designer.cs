using System;

namespace csharp_editor;

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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        editToolStripMenuItem = new ToolStripMenuItem();
        viewToolStripMenuItem = new ToolStripMenuItem();
        projectToolStripMenuItem = new ToolStripMenuItem();
        toolsToolStripMenuItem = new ToolStripMenuItem();
        helpToolStripMenuItem = new ToolStripMenuItem();
        toolStrip1 = new ToolStrip();
        toolStripButton1 = new ToolStripButton();
        toolStripButton_openFile = new ToolStripButton();
        toolStripButton3 = new ToolStripButton();
        toolStripButton4 = new ToolStripButton();
        toolStripSeparator1 = new ToolStripSeparator();
        toolStripButton_cmd = new ToolStripButton();
        toolStripButton_explorer = new ToolStripButton();
        toolStripSeparator2 = new ToolStripSeparator();
        toolStripComboBox1 = new ToolStripComboBox();
        toolStripButton9 = new ToolStripButton();
        toolStripButton7 = new ToolStripButton();
        toolStripButton8 = new ToolStripButton();
        richTextBox_console = new RichTextBox();
        tabControl1 = new TabControl();
        tabPage_scene = new TabPage();
        treeView1 = new TreeView();
        toolStrip2 = new ToolStrip();
        toolStripButton5 = new ToolStripButton();
        toolStripButton6 = new ToolStripButton();
        toolStripButton_addEntity = new ToolStripButton();
        tabPage_files = new TabPage();
        panel_main = new MainView();
        panel2 = new Panel();
        splitContainer1 = new SplitContainer();
        splitContainer2 = new SplitContainer();
        propertyGrid_main = new PropertyGrid();
        menuStrip1.SuspendLayout();
        toolStrip1.SuspendLayout();
        tabControl1.SuspendLayout();
        tabPage_scene.SuspendLayout();
        toolStrip2.SuspendLayout();
        panel2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
        splitContainer2.Panel1.SuspendLayout();
        splitContainer2.Panel2.SuspendLayout();
        splitContainer2.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, projectToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(944, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
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
        // projectToolStripMenuItem
        // 
        projectToolStripMenuItem.Name = "projectToolStripMenuItem";
        projectToolStripMenuItem.Size = new Size(56, 20);
        projectToolStripMenuItem.Text = "Project";
        // 
        // toolsToolStripMenuItem
        // 
        toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
        toolsToolStripMenuItem.Size = new Size(46, 20);
        toolsToolStripMenuItem.Text = "Tools";
        // 
        // helpToolStripMenuItem
        // 
        helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        helpToolStripMenuItem.Size = new Size(44, 20);
        helpToolStripMenuItem.Text = "Help";
        // 
        // toolStrip1
        // 
        toolStrip1.BackColor = SystemColors.Control;
        toolStrip1.CanOverflow = false;
        toolStrip1.GripMargin = new Padding(8, 2, 2, 2);
        toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton_openFile, toolStripButton3, toolStripButton4, toolStripSeparator1, toolStripButton_cmd, toolStripButton_explorer, toolStripSeparator2, toolStripComboBox1, toolStripButton9, toolStripButton7, toolStripButton8 });
        toolStrip1.Location = new Point(0, 24);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Padding = new Padding(0);
        toolStrip1.RenderMode = ToolStripRenderMode.System;
        toolStrip1.Size = new Size(944, 25);
        toolStrip1.TabIndex = 1;
        toolStrip1.Text = "toolStrip1";
        // 
        // toolStripButton1
        // 
        toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
        toolStripButton1.ImageTransparentColor = Color.Magenta;
        toolStripButton1.Name = "toolStripButton1";
        toolStripButton1.Size = new Size(23, 22);
        toolStripButton1.Text = "toolStripButton1";
        // 
        // toolStripButton_openFile
        // 
        toolStripButton_openFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton_openFile.Image = (Image)resources.GetObject("toolStripButton_openFile.Image");
        toolStripButton_openFile.ImageTransparentColor = Color.Magenta;
        toolStripButton_openFile.Name = "toolStripButton_openFile";
        toolStripButton_openFile.Size = new Size(23, 22);
        toolStripButton_openFile.Text = "Open file";
        // 
        // toolStripButton3
        // 
        toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
        toolStripButton3.ImageTransparentColor = Color.Magenta;
        toolStripButton3.Name = "toolStripButton3";
        toolStripButton3.Size = new Size(23, 22);
        toolStripButton3.Text = "toolStripButton3";
        // 
        // toolStripButton4
        // 
        toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
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
        // toolStripButton_cmd
        // 
        toolStripButton_cmd.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton_cmd.Image = (Image)resources.GetObject("toolStripButton_cmd.Image");
        toolStripButton_cmd.ImageTransparentColor = Color.Magenta;
        toolStripButton_cmd.Name = "toolStripButton_cmd";
        toolStripButton_cmd.Size = new Size(23, 22);
        toolStripButton_cmd.Text = "toolStripButton_cmd";
        // 
        // toolStripButton_explorer
        // 
        toolStripButton_explorer.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton_explorer.Image = (Image)resources.GetObject("toolStripButton_explorer.Image");
        toolStripButton_explorer.ImageTransparentColor = Color.Magenta;
        toolStripButton_explorer.Name = "toolStripButton_explorer";
        toolStripButton_explorer.Size = new Size(23, 22);
        toolStripButton_explorer.Text = "toolStripButton6";
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(6, 25);
        // 
        // toolStripComboBox1
        // 
        toolStripComboBox1.Name = "toolStripComboBox1";
        toolStripComboBox1.Size = new Size(121, 25);
        // 
        // toolStripButton9
        // 
        toolStripButton9.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton9.Image = (Image)resources.GetObject("toolStripButton9.Image");
        toolStripButton9.ImageTransparentColor = Color.Magenta;
        toolStripButton9.Name = "toolStripButton9";
        toolStripButton9.Size = new Size(23, 22);
        toolStripButton9.Text = "toolStripButton9";
        // 
        // toolStripButton7
        // 
        toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton7.Image = (Image)resources.GetObject("toolStripButton7.Image");
        toolStripButton7.ImageTransparentColor = Color.Magenta;
        toolStripButton7.Name = "toolStripButton7";
        toolStripButton7.Size = new Size(23, 22);
        toolStripButton7.Text = "toolStripButton7";
        // 
        // toolStripButton8
        // 
        toolStripButton8.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton8.Image = (Image)resources.GetObject("toolStripButton8.Image");
        toolStripButton8.ImageTransparentColor = Color.Magenta;
        toolStripButton8.Name = "toolStripButton8";
        toolStripButton8.Size = new Size(23, 22);
        toolStripButton8.Text = "toolStripButton8";
        // 
        // richTextBox_console
        // 
        richTextBox_console.BorderStyle = BorderStyle.None;
        richTextBox_console.Dock = DockStyle.Bottom;
        richTextBox_console.Enabled = false;
        richTextBox_console.Location = new Point(0, 438);
        richTextBox_console.Name = "richTextBox_console";
        richTextBox_console.Size = new Size(944, 63);
        richTextBox_console.TabIndex = 3;
        richTextBox_console.Text = "";
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage_scene);
        tabControl1.Controls.Add(tabPage_files);
        tabControl1.Dock = DockStyle.Fill;
        tabControl1.ItemSize = new Size(64, 26);
        tabControl1.Location = new Point(0, 0);
        tabControl1.Margin = new Padding(0);
        tabControl1.Name = "tabControl1";
        tabControl1.Padding = new Point(0, 0);
        tabControl1.RightToLeft = RightToLeft.Yes;
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(160, 256);
        tabControl1.SizeMode = TabSizeMode.Fixed;
        tabControl1.TabIndex = 6;
        // 
        // tabPage_scene
        // 
        tabPage_scene.Controls.Add(treeView1);
        tabPage_scene.Controls.Add(toolStrip2);
        tabPage_scene.Location = new Point(4, 30);
        tabPage_scene.Margin = new Padding(0);
        tabPage_scene.Name = "tabPage_scene";
        tabPage_scene.Size = new Size(152, 222);
        tabPage_scene.TabIndex = 0;
        tabPage_scene.Text = "Scene";
        tabPage_scene.UseVisualStyleBackColor = true;
        // 
        // treeView1
        // 
        treeView1.BorderStyle = BorderStyle.None;
        treeView1.Dock = DockStyle.Fill;
        treeView1.Location = new Point(0, 0);
        treeView1.Margin = new Padding(0);
        treeView1.Name = "treeView1";
        treeView1.Size = new Size(152, 197);
        treeView1.TabIndex = 3;
        // 
        // toolStrip2
        // 
        toolStrip2.BackColor = SystemColors.Window;
        toolStrip2.CanOverflow = false;
        toolStrip2.Dock = DockStyle.Bottom;
        toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
        toolStrip2.Items.AddRange(new ToolStripItem[] { toolStripButton5, toolStripButton6, toolStripButton_addEntity });
        toolStrip2.Location = new Point(0, 197);
        toolStrip2.Name = "toolStrip2";
        toolStrip2.Padding = new Padding(0);
        toolStrip2.RenderMode = ToolStripRenderMode.System;
        toolStrip2.Size = new Size(152, 25);
        toolStrip2.TabIndex = 4;
        toolStrip2.Text = "toolStrip2";
        // 
        // toolStripButton5
        // 
        toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
        toolStripButton5.ImageTransparentColor = Color.Magenta;
        toolStripButton5.Name = "toolStripButton5";
        toolStripButton5.Size = new Size(23, 22);
        toolStripButton5.Text = "toolStripButton5";
        // 
        // toolStripButton6
        // 
        toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton6.Image = (Image)resources.GetObject("toolStripButton6.Image");
        toolStripButton6.ImageTransparentColor = Color.Magenta;
        toolStripButton6.Name = "toolStripButton6";
        toolStripButton6.Size = new Size(23, 22);
        toolStripButton6.Text = "toolStripButton6";
        // 
        // toolStripButton_addEntity
        // 
        toolStripButton_addEntity.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton_addEntity.Image = (Image)resources.GetObject("toolStripButton_addEntity.Image");
        toolStripButton_addEntity.ImageTransparentColor = Color.Magenta;
        toolStripButton_addEntity.Name = "toolStripButton_addEntity";
        toolStripButton_addEntity.Size = new Size(23, 22);
        toolStripButton_addEntity.Text = "toolStripButton2";
        // 
        // tabPage_files
        // 
        tabPage_files.Location = new Point(4, 30);
        tabPage_files.Name = "tabPage_files";
        tabPage_files.Size = new Size(152, 222);
        tabPage_files.TabIndex = 1;
        tabPage_files.Text = "Files";
        tabPage_files.UseVisualStyleBackColor = true;
        // 
        // panel_main
        // 
        panel_main.BorderStyle = BorderStyle.FixedSingle;
        panel_main.Dock = DockStyle.Fill;
        panel_main.Location = new Point(0, 0);
        panel_main.Name = "panel_main";
        panel_main.Size = new Size(775, 383);
        panel_main.TabIndex = 2;
        // 
        // panel2
        // 
        panel2.Controls.Add(splitContainer1);
        panel2.Dock = DockStyle.Fill;
        panel2.Location = new Point(0, 49);
        panel2.Margin = new Padding(0);
        panel2.Name = "panel2";
        panel2.Padding = new Padding(3);
        panel2.Size = new Size(944, 389);
        panel2.TabIndex = 5;
        // 
        // splitContainer1
        // 
        splitContainer1.Dock = DockStyle.Fill;
        splitContainer1.Location = new Point(3, 3);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(splitContainer2);
        splitContainer1.Panel1MinSize = 64;
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(panel_main);
        splitContainer1.Panel2MinSize = 640;
        splitContainer1.Size = new Size(938, 383);
        splitContainer1.SplitterDistance = 160;
        splitContainer1.SplitterWidth = 3;
        splitContainer1.TabIndex = 5;
        // 
        // splitContainer2
        // 
        splitContainer2.Dock = DockStyle.Fill;
        splitContainer2.Location = new Point(0, 0);
        splitContainer2.Name = "splitContainer2";
        splitContainer2.Orientation = Orientation.Horizontal;
        // 
        // splitContainer2.Panel1
        // 
        splitContainer2.Panel1.Controls.Add(tabControl1);
        // 
        // splitContainer2.Panel2
        // 
        splitContainer2.Panel2.Controls.Add(propertyGrid_main);
        splitContainer2.Size = new Size(160, 383);
        splitContainer2.SplitterDistance = 256;
        splitContainer2.TabIndex = 4;
        // 
        // propertyGrid_main
        // 
        propertyGrid_main.Dock = DockStyle.Fill;
        propertyGrid_main.Location = new Point(0, 0);
        propertyGrid_main.Name = "propertyGrid_main";
        propertyGrid_main.Size = new Size(160, 123);
        propertyGrid_main.TabIndex = 0;
        propertyGrid_main.ToolbarVisible = false;
        // 
        // Editor
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(944, 501);
        Controls.Add(panel2);
        Controls.Add(toolStrip1);
        Controls.Add(menuStrip1);
        Controls.Add(richTextBox_console);
        Location = new Point(1972, 52);
        Name = "Editor";
        Text = "Form1";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        tabControl1.ResumeLayout(false);
        tabPage_scene.ResumeLayout(false);
        tabPage_scene.PerformLayout();
        toolStrip2.ResumeLayout(false);
        toolStrip2.PerformLayout();
        panel2.ResumeLayout(false);
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        splitContainer2.Panel1.ResumeLayout(false);
        splitContainer2.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
        splitContainer2.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem viewToolStripMenuItem;
    private ToolStripMenuItem projectToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem toolsToolStripMenuItem;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButton1;
    private ToolStripButton toolStripButton_openFile;
    private ToolStripButton toolStripButton3;
    private ToolStripButton toolStripButton4;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton toolStripButton_cmd;
    private ToolStripButton toolStripButton_explorer;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripComboBox toolStripComboBox1;
    private ToolStripButton toolStripButton7;
    private ToolStripButton toolStripButton8;
    private ToolStripButton toolStripButton9;
    private RichTextBox richTextBox_console;
    private MainView panel_main;
    private Panel panel2;
    private SplitContainer splitContainer1;
    private TabControl tabControl1;
    private TabPage tabPage_scene;
    private TabPage tabPage_files;
    private TreeView treeView1;
    private ToolStrip toolStrip2;
    private ToolStripButton toolStripButton_addEntity;
    private ToolStripButton toolStripButton5;
    private SplitContainer splitContainer2;
    private ToolStripButton toolStripButton6;
    private PropertyGrid propertyGrid_main;
}
