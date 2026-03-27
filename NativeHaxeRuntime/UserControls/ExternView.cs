using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NativeHaxeRuntime;

namespace csharp_editor.UserControls {
    public class ExternView : UserControl {

        public CExterns.CallbackDelegate logCallback = null!;
        //public bool active = false;

        private string _lastErrorMessage { get; set; } = "Unknown native error.";

        // Fired on the UI thread whenever the C++ engine reports a selection change

        public void SetLastErrorMessage(string message) {
            _lastErrorMessage = string.IsNullOrWhiteSpace(message) ? "Unknown native error." : message;
        }

        public string GetLastErrorMessage() {
            return _lastErrorMessage;
        }

        private IntPtr sdlWindowHandle = IntPtr.Zero;

        public ExternView() {
            InitializeComponent();

            // ** Events
            //MouseClick += MainView_MouseClick;
            MouseMove += OnMouseMotion;
            Resize += ExternView_Resize;
        }

        // CallbackDelegate callback
        public void Init(CExterns.CallbackDelegate callback) {
            logCallback = callback;

            if (!CExterns.InitWithCallback(callback)) {
                MessageBox.Show("Failed to initialize engine", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the SDL window handle
            sdlWindowHandle = CExterns.GetWindowHandle();
            if (sdlWindowHandle == IntPtr.Zero) {
                MessageBox.Show("Failed to get SDL window handle", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Make the SDL window a plain child window (no chrome/borders).
            CExterns.ApplyChildWindowStyle(sdlWindowHandle);

            // Disable rounded corners (Windows 11) - must be done before SetParent
            int preference = CExterns.DWMWCP_DONOTROUND;
            CExterns.DwmSetWindowAttribute(sdlWindowHandle, CExterns.DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(int));

            // Set SDL window as child of panel
            CExterns.SetParent(sdlWindowHandle, panel_extern.Handle);
            CExterns.SetWindowPosition(0, 0);

            // Size the SDL window to match the panel
            CExterns.MoveWindow(sdlWindowHandle, 0, 0, panel_extern.Width, panel_extern.Height, true);
        }

        public void Release() {
            sdlWindowHandle = IntPtr.Zero;
        }

        public void Render() {
            CExterns.Render();
        }

        public void SwapBuffers() {
            CExterns.SwapBuffers();
        }

        public void UpdateFrame(float deltaTime) {
            CExterns.UpdateFrame(deltaTime);
        }

        private void OnMouseMotion(object? sender, MouseEventArgs e) {
            CExterns.OnMouseMotion(e.X, e.Y);
        }

        private void ExternView_Resize(object? sender, EventArgs e) {
            CExterns.MoveWindow(sdlWindowHandle, 0, 0, panel_extern.Width, panel_extern.Height, true);
        }

        private Panel panel_extern = null!; // assigned in InitializeComponent()

        private void InitializeComponent() {
            panel_extern = new Panel();
            SuspendLayout();
            // 
            // panel_extern
            // 
            panel_extern.Dock = DockStyle.Fill;
            panel_extern.Enabled = false;
            panel_extern.Location = new Point(0, 0);
            panel_extern.Name = "panel_extern";
            panel_extern.Size = new Size(150, 150);
            panel_extern.TabIndex = 0;
            // 
            // ExternView
            // 
            Controls.Add(panel_extern);
            Name = "ExternView";
            ResumeLayout(false);
        }
    }
}

