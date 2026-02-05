namespace csharp_bmfg.UserControls {
    public partial class InputView : UserControl {
        private System.Windows.Forms.Panel panel_border;
        private System.Windows.Forms.TextBox textBox_input;

        public InputView() {
            InitializeComponent();
        }

        public string Text {
            get => textBox_input?.Text ?? string.Empty;
            set {
                if (textBox_input != null) {
                    textBox_input.Text = value;
                }
            }
        }

        public new event EventHandler? TextChanged {
            add => textBox_input.TextChanged += value;
            remove => textBox_input.TextChanged -= value;
        }

        public new event KeyEventHandler? KeyDown {
            add => textBox_input.KeyDown += value;
            remove => textBox_input.KeyDown -= value;
        }

        public new event KeyPressEventHandler? KeyPress {
            add => textBox_input.KeyPress += value;
            remove => textBox_input.KeyPress -= value;
        }

        public new event KeyEventHandler? KeyUp {
            add => textBox_input.KeyUp += value;
            remove => textBox_input.KeyUp -= value;
        }

        public void Clear() {
            if (!IsDisposed && textBox_input != null && !textBox_input.IsDisposed) {
                textBox_input.Clear();
            }
        }

        public new void Focus() {
            if (!IsDisposed && textBox_input != null && !textBox_input.IsDisposed) {
                textBox_input.Focus();
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel_border = new System.Windows.Forms.Panel();
            textBox_input = new System.Windows.Forms.TextBox();
            panel_border.SuspendLayout();
            SuspendLayout();
            // 
            // panel_border
            // 
            panel_border.BackColor = System.Drawing.Color.Gray;
            panel_border.Controls.Add(textBox_input);
            panel_border.Dock = System.Windows.Forms.DockStyle.Fill;
            panel_border.Location = new System.Drawing.Point(0, 0);
            panel_border.Margin = new System.Windows.Forms.Padding(1);
            panel_border.Name = "panel_border";
            panel_border.Size = new System.Drawing.Size(258, 18);
            panel_border.TabIndex = 0;
            // 
            // textBox_input
            // 
            textBox_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            textBox_input.BackColor = System.Drawing.SystemColors.Control;
            textBox_input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_input.Location = new System.Drawing.Point(1, 1);
            textBox_input.Name = "textBox_input";
            textBox_input.Size = new System.Drawing.Size(256, 16);
            textBox_input.TabIndex = 0;
            // 
            // InputView
            // 
            Controls.Add(panel_border);
            Size = new System.Drawing.Size(258, 18);
            panel_border.ResumeLayout(false);
            panel_border.PerformLayout();
            ResumeLayout(false);
        }
    }
}
