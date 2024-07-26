using static csharp_editor.Renderer;

namespace csharp_editor {
    public partial class MainView : UserControl {

        public CallbackDelegate ?logCallback;

        public MainView() {

            InitializeComponent();

            // ** Events

            MouseClick += MainView_MouseClick;
        }

        public void Init(CallbackDelegate callback) {

            logCallback = callback;

            //Renderer.Init();
            Renderer.InitWithCallback(callback);

            Renderer.CreateWindowFrom(Handle);

            //IntPtr sdlHandle = Renderer.GetHandle();

            // ** Get the parent window handle.
            IntPtr windowHandle = Handle;

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

            Renderer.Release();
        }

        public void AddEntity(int id) {

            Renderer.AddEntity(id);
        }

        public void SelectEntity(int id) {

            Renderer.SelectEntity(id);
        }

        public void DeselectEntity() {

            Renderer.DeselectEntity();
        }

        public void UpdateEntity(int id, int x, int y)
        {

            Renderer.UpdateEntity(id, x, y);
        }

        public void UpdateMap(string hex) {

            Renderer.UpdateMap(color);
        }

        public void PreRender() {

            Renderer.PreRender();
        }

        public void Render() {

            Renderer.Render();
        }

        public void PostRender() {

            Renderer.PostRender();
        }

        public void Tick() {

            Renderer.Update();
        }

        private void MainView_MouseClick(object? sender, MouseEventArgs e) {

            //logCallback?.Invoke("X: " + e.X + " Y: " + e.Y);

            Renderer.OnMouseClick(e.X, e.Y);
        }
    }
}
