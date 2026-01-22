using System;
using System.Windows.Forms;
using static csharp_editor.Renderer;

namespace csharp_editor {
    public partial class MainView : UserControl {

        public CallbackDelegate logCallback;
        public bool active = false;

        private IntPtr sdlWindowHandle = IntPtr.Zero;

        public MainView() {

            InitializeComponent();

            // ** Events
            MouseClick += MainView_MouseClick;
        }

        // CallbackDelegate callback
        public void Init(CallbackDelegate callback) {

            logCallback = callback;

            if (Renderer.InitWithCallback(callback) != 1)
            {
                MessageBox.Show("Failed to initialize engine", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the SDL window handle
            sdlWindowHandle = Renderer.GetWindowHandle();
            if (sdlWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to get SDL window handle", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set SDL window as child of panel
            SetParent(sdlWindowHandle, panel_view.Handle);
            Renderer.SetWindowPosition(0,0);

            //MoveWindow(sdlWindowHandle, 0, 0, panel1.Width, panel1.Height, true);

            // Load default state (CollisionTestState)
            Renderer.LoadState(0);

            //engineInitialized = true;
            //btnRunEngine.Enabled = false;

            // Start delta timer
            //deltaTimer.Start();

            // Start render loop on UI thread - runs continuously
            //renderTimer = new System.Windows.Forms.Timer();
            //renderTimer.Interval = 1; // Run as fast as possible, limited by VSync
            //renderTimer.Tick += RenderFrame;
            //renderTimer.Start();

            active = true;

            //Renderer.Init();
            //Renderer.InitWithCallback(callback);

            //Renderer.CreateWindowFrom(Handle);

            //IntPtr sdlHandle = Renderer.GetHandle();

            // ** Get the parent window handle.
            //IntPtr windowHandle = Handle;

            //Renderer.SetWindowPos(
            //    sdlHandle,
            //    Handle,
            //    0,
            //    0,
            //    0,
            //    0,
            //    0x0401 // NOSIZE | SHOWWINDOW
            //);

            // Attach the SDL2 window to the panel
            //Renderer.SetParent(sdlHandle, Handle);
            //Renderer.ShowWindow(sdlHandle, 1); // SHOWNORMAL
            //Renderer.SetWindowLongA(sdlHandle, 0, 0x80000000L);
            //Renderer.EnableWindow(sdlHandle, true);
        }

        public void Release() {

            if (active == true)
            {
                Renderer.Release();
            }
        }

        public void AddEntity(int id) {

            //Renderer.AddEntity(id);
        }

        public void SelectEntity(int id) {

            //Renderer.SelectEntity(id);
        }

        public void DeselectEntity() {

            //Renderer.DeselectEntity();
        }

        public void UpdateEntity(int id, int x, int y)
        {

            //Renderer.UpdateEntity(id, x, y);
        }

        public void UpdateMap(string hex) {

            //Renderer.UpdateMap(hex);
        }

        public void PreRender() {

            //Renderer.PreRender();
        }

        public void Render()
        {
            Renderer.Render();
        }

        public void PostRender()
        {
            //Renderer.PostRender();
        }

        public void Tick()
        {
            Renderer.UpdateFrame();
        }

        private void MainView_MouseClick(object sender, MouseEventArgs e) {

            //logCallback?.Invoke("X: " + e.X + " Y: " + e.Y);

            //Renderer.OnMouseClick(e.X, e.Y);
        }

        private void view_Load(object sender, EventArgs e)
        {

        }
    }
}
