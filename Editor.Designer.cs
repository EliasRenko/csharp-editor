using csharp_bmfg.UserControls;
using csharp_bmfg;

namespace csharp_bmfg {
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
            view_extern = new ExternView();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem_open = new ToolStripMenuItem();
            toolStripMenuItem_export = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            console = new UserControls.Console();
            numericUpDown_size = new NumericUpDown();
            button_rebake = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_size).BeginInit();
            SuspendLayout();
            // 
            // view_extern
            // 
            view_extern.BackColor = SystemColors.ControlDark;
            view_extern.Dock = DockStyle.Fill;
            view_extern.Location = new Point(0, 24);
            view_extern.Name = "view_extern";
            view_extern.Size = new Size(624, 395);
            view_extern.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(624, 24);
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
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 419);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(624, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // console
            // 
            console.Dock = DockStyle.Bottom;
            console.Location = new Point(0, 291);
            console.Name = "console";
            console.Size = new Size(624, 128);
            console.TabIndex = 3;
            // 
            // numericUpDown_size
            // 
            numericUpDown_size.BorderStyle = BorderStyle.None;
            numericUpDown_size.Location = new Point(570, 31);
            numericUpDown_size.Name = "numericUpDown_size";
            numericUpDown_size.Size = new Size(42, 19);
            numericUpDown_size.TabIndex = 4;
            numericUpDown_size.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // button_rebake
            // 
            button_rebake.Location = new Point(489, 27);
            button_rebake.Name = "button_rebake";
            button_rebake.Size = new Size(75, 23);
            button_rebake.TabIndex = 5;
            button_rebake.Text = "Rebake";
            button_rebake.UseVisualStyleBackColor = true;
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 441);
            Controls.Add(button_rebake);
            Controls.Add(numericUpDown_size);
            Controls.Add(console);
            Controls.Add(view_extern);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Editor";
            Text = "Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_size).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

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
        private NumericUpDown numericUpDown_size;
        private Button button_rebake;
    }
}