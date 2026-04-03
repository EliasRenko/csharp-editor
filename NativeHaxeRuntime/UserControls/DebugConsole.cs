using System.Windows.Forms;

public partial class DebugConsole : UserControl {

        public DebugConsole() {
            InitializeComponent();

            button_copy.Click += Button_copy_Click;
            button_clear.Click += (s, e) => logView.Clear();

            
        }

        private void Button_copy_Click(object sender, EventArgs e) {
            string? textToCopy = logView.CopyText();
            if (!string.IsNullOrEmpty(textToCopy)) {
                Clipboard.SetText(textToCopy);
            }
        }

        public void Log(string priority, string category, string message) {
            if (!IsDisposed && logView != null && !logView.IsDisposed) {
                System.Drawing.Color color = priority switch {
                    "ERROR" => System.Drawing.Color.FromArgb(255, 100,  80),
                    "WARN"  => System.Drawing.Color.FromArgb(255, 165,   0),
                    "DEBUG" => System.Drawing.Color.FromArgb( 86, 156, 214),
                    _       => System.Drawing.Color.Empty
                };
                logView.Log($"{priority} - {category} - {message}", color);
            }
        }
    }
