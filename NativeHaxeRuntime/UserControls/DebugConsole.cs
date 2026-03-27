using System;
using System.Collections.Generic;
namespace csharp_editor.UserControls {
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

        public void Log(string message) {
            if (!IsDisposed && logView != null && !logView.IsDisposed) {
                logView.Log(message);
            }
        }
    }
}
