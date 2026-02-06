namespace csharp_editor.UserControls {
    public partial class LogView : UserControl {
        private System.Windows.Forms.Panel panel_border;
        private System.Windows.Forms.RichTextBox richTextBox_log;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel_border = new System.Windows.Forms.Panel();
            richTextBox_log = new System.Windows.Forms.RichTextBox();
            panel_border.SuspendLayout();
            SuspendLayout();
            // 
            // panel_border
            // 
            panel_border.BackColor = System.Drawing.Color.Gray;
            panel_border.Controls.Add(richTextBox_log);
            panel_border.Dock = System.Windows.Forms.DockStyle.Fill;
            panel_border.Location = new System.Drawing.Point(0, 0);
            panel_border.Name = "panel_border";
            panel_border.Padding = new System.Windows.Forms.Padding(1);
            panel_border.Size = new System.Drawing.Size(300, 200);
            panel_border.TabIndex = 0;
            // 
            // richTextBox_log
            // 
            richTextBox_log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBox_log.Dock = System.Windows.Forms.DockStyle.Fill;
            richTextBox_log.Location = new System.Drawing.Point(1, 1);
            richTextBox_log.Name = "richTextBox_log";
            richTextBox_log.ReadOnly = true;
            richTextBox_log.Size = new System.Drawing.Size(298, 198);
            richTextBox_log.TabIndex = 0;
            richTextBox_log.Text = "";
            // 
            // LogView
            // 
            Controls.Add(panel_border);
            Size = new System.Drawing.Size(300, 200);
            panel_border.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
