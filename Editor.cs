namespace csharp_editor {
    public partial class Editor : Form {

        public bool active = false;
        public Editor() {
            InitializeComponent();

            active = true;

            Externs.CallbackDelegate callback = (value) => {
                Log(value);
            };

            view_extern.Init(callback);

            // Toolstrip Events
            toolStripMenuItem_open.MouseUp += toolStripButton_openFile;
            toolStripMenuItem_export.MouseUp += toolStripButton_export;
            
            // Enable keyboard events at form level
            KeyPreview = true;

            // Editor Events
            FormClosing += Editor_FormClosing;
            KeyDown += Editor_KeyDown;
            KeyUp += Editor_KeyUp;

            // ExternView Events
            view_extern.MouseDown += view_extern_MouseDown;
            view_extern.MouseUp += view_extern_MouseUp;
        }

        public void UpdateFrame(float deltaTime) {
            view_extern.UpdateFrame(deltaTime);
        }

        public void PreRender() {
            //view_extern.PreRender();
        }

        public void Render() {
            view_extern.Render();
        }

        public void SwapBuffers() {
            view_extern.SwapBuffers();
        }

        #region Core

        private void LoadJson(string path) {
            view_extern.LoadFont(path);
        }

        #endregion

        #region Log
        public void Log(string text) {
            // Check if form and console are not disposed
            if (!IsDisposed && console != null && !console.IsDisposed) {
                console.Log(text);
            }
        }

        #endregion

        #region Events

        private void Editor_FormClosing(object sender, FormClosingEventArgs e) {
            active = false;
            Application.DoEvents(); // Process remaining messages
            System.Threading.Thread.Sleep(50); // Give loop time to exit
            view_extern.Release();
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e) {
            // Toggle console with tilde key (~) or F1
            if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.F1) {
                console.Visible = !console.Visible;
                e.Handled = true;
                return; // Don't pass console toggle to SDL
            }

            // Convert C# KeyCode to SDL Scancode and pass to SDL
            view_extern.OnKeyboardDown(KeyMapper.ToSDLScancode(e.KeyCode));
        }

        private void Editor_KeyUp(object sender, KeyEventArgs e) {
            // Toggle console with tilde key (~) or F1
            if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.F1) {
                e.Handled = true;
                return; // Don't pass console toggle to SDL
            }
            
            // Convert C# KeyCode to SDL Scancode and pass to SDL
            view_extern.OnKeyboardUp(KeyMapper.ToSDLScancode(e.KeyCode));
        }

        #endregion

        private void view_extern_MouseDown(object sender, MouseEventArgs e) {
            int button = MouseButtonMapper.ToSDLMouseButton(e.Button);
            view_extern.OnMouseButtonDown(e.X, e.Y, button);
        }

        private void view_extern_MouseUp(object sender, MouseEventArgs e) {
            int button = MouseButtonMapper.ToSDLMouseButton(e.Button);
            view_extern.OnMouseButtonUp(e.X, e.Y, button);
        }

        private void toolStripButton_openFile(object sender, MouseEventArgs e) {
            string path = Utils.OpenFile("");
            
            // User cancelled or invalid path
            if (string.IsNullOrEmpty(path)) {
                return;
            }

            string ext = Path.GetExtension(path).ToLowerInvariant();

            switch (ext) {
                case ".json":
                    LoadJson(path);
                    break;

                default:
                    MessageBox.Show($"Unsupported file type: {ext}\nSupported types: .json, .ttf",
                        "Invalid File Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void toolStripButton_export(object sender, MouseEventArgs e) {
            string startingPath = AppContext.BaseDirectory;
            string name = "default";
            string exten = "json";

            try {
                using (var dialog = new SaveFileDialog()) {
                    dialog.Filter = $"{exten.ToUpper()} Files (*.{exten})|*.{exten}|All Files (*.*)|*.*";
                    dialog.FilterIndex = 1;
                    dialog.InitialDirectory = startingPath;
                    dialog.FileName = name;
                    dialog.DefaultExt = exten;
                    dialog.AddExtension = true;

                    if (dialog.ShowDialog() == DialogResult.OK) {
                        view_extern.ExportFont(dialog.FileName);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error saving file: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}