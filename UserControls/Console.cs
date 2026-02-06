using System;
using System.Collections.Generic;
namespace csharp_editor.UserControls {
    public partial class Console : UserControl {

        public Console() {
            InitializeComponent();

            //button_copy.Click += Button_copy_Click;
        }

        private void Button_copy_Click(object sender, EventArgs e) {
            // Copy functionality can be added to LogView if needed
        }

        public void Log(string message) {
            if (!IsDisposed && logView != null && !logView.IsDisposed) {
                logView.Log(message);
            }
        }
    }
}
