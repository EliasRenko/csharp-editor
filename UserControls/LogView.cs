namespace csharp_editor.UserControls {
    public partial class LogView : UserControl {
        private System.Windows.Forms.Panel panel_border;
        private System.Windows.Forms.RichTextBox richTextBox_log;
        private System.Windows.Forms.Panel panel_toolbar;
        private System.Windows.Forms.Button button_copy;

        public LogView() {
            InitializeComponent();
        }

        public void Log(string text) {
            if (!IsDisposed && richTextBox_log != null && !richTextBox_log.IsDisposed) {
                richTextBox_log.AppendText(text + Environment.NewLine);
                richTextBox_log.ScrollToCaret();
            }
        }

        public void Clear() {
            if (!IsDisposed && richTextBox_log != null && !richTextBox_log.IsDisposed) {
                richTextBox_log.Clear();
            }
        }

        private void button_copy_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox_log.SelectedText))
            {
                Clipboard.SetText(richTextBox_log.SelectedText);
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel_border = new System.Windows.Forms.Panel();
            richTextBox_log = new System.Windows.Forms.RichTextBox();
            panel_toolbar = new System.Windows.Forms.Panel();
            button_copy = new System.Windows.Forms.Button();
            panel_border.SuspendLayout();
            panel_toolbar.SuspendLayout();
            SuspendLayout();
            // 
            // panel_toolbar
            // 
            panel_toolbar.Controls.Add(button_copy);
            panel_toolbar.Dock = System.Windows.Forms.DockStyle.Top;
            panel_toolbar.Location = new System.Drawing.Point(0, 0);
            panel_toolbar.Name = "panel_toolbar";
            panel_toolbar.Size = new System.Drawing.Size(300, 28);
            panel_toolbar.TabIndex = 1;
            // 
            // button_copy
            // 
            button_copy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_copy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button_copy.Location = new System.Drawing.Point(231, 3);
            button_copy.Name = "button_copy";
            button_copy.Size = new System.Drawing.Size(65, 22);
            button_copy.TabIndex = 0;
            button_copy.Text = "Copy";
            button_copy.UseVisualStyleBackColor = true;
            button_copy.Click += button_copy_Click;
            // 
            // panel_border
            // 
            panel_border.BackColor = System.Drawing.Color.Gray;
            panel_border.Controls.Add(richTextBox_log);
            panel_border.Dock = System.Windows.Forms.DockStyle.Fill;
            panel_border.Location = new System.Drawing.Point(0, 28);
            panel_border.Name = "panel_border";
            panel_border.Padding = new System.Windows.Forms.Padding(1);
            panel_border.Size = new System.Drawing.Size(300, 172);
            panel_border.TabIndex = 0;
            // 
            // richTextBox_log
            // 
            richTextBox_log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBox_log.Dock = System.Windows.Forms.DockStyle.Fill;
            richTextBox_log.Location = new System.Drawing.Point(1, 1);
            richTextBox_log.Name = "richTextBox_log";
            richTextBox_log.ReadOnly = true;
            richTextBox_log.Size = new System.Drawing.Size(298, 170);
            richTextBox_log.TabIndex = 0;
            richTextBox_log.Text = "";
            // 
            // LogView
            // 
            Controls.Add(panel_border);
            Controls.Add(panel_toolbar);
            Size = new System.Drawing.Size(300, 200);
            panel_border.ResumeLayout(false);
            panel_toolbar.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
