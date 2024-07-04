using System;

namespace csharp_editor;

partial class Form1 {
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        editToolStripMenuItem = new ToolStripMenuItem();
        viewToolStripMenuItem = new ToolStripMenuItem();
        projectToolStripMenuItem = new ToolStripMenuItem();
        toolsToolStripMenuItem = new ToolStripMenuItem();
        helpToolStripMenuItem = new ToolStripMenuItem();
        toolStrip1 = new ToolStrip();
        toolStripButton1 = new ToolStripButton();
        toolStripButton2 = new ToolStripButton();
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
        panel_main = new MainView();
        textBox_console = new TextBox();
        menuStrip1.SuspendLayout();
        toolStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, projectToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(624, 24);
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
        toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripSeparator1, toolStripButton_cmd, toolStripButton_explorer, toolStripSeparator2, toolStripComboBox1, toolStripButton9, toolStripButton7, toolStripButton8 });
        toolStrip1.Location = new Point(0, 24);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new Size(624, 25);
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
        // toolStripButton2
        // 
        toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
        toolStripButton2.ImageTransparentColor = Color.Magenta;
        toolStripButton2.Name = "toolStripButton2";
        toolStripButton2.Size = new Size(23, 22);
        toolStripButton2.Text = "toolStripButton2";
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
        // panel_main
        // 
        panel_main.Dock = DockStyle.Fill;
        panel_main.Location = new Point(0, 49);
        panel_main.Name = "panel_main";
        panel_main.Size = new Size(624, 392);
        panel_main.TabIndex = 2;
        // 
        // textBox_console
        // 
        textBox_console.Dock = DockStyle.Bottom;
        textBox_console.Enabled = false;
        textBox_console.Location = new Point(0, 393);
        textBox_console.Multiline = true;
        textBox_console.Name = "textBox_console";
        textBox_console.Size = new Size(624, 48);
        textBox_console.TabIndex = 3;
        textBox_console.TabStop = false;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(624, 441);
        Controls.Add(textBox_console);
        Controls.Add(panel_main);
        Controls.Add(toolStrip1);
        Controls.Add(menuStrip1);
        Location = new Point(1972, 52);
        Name = "Form1";
        Text = "Form1";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
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
    private ToolStripButton toolStripButton2;
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
    private MainView panel_main;
    private TextBox textBox_console;
}
