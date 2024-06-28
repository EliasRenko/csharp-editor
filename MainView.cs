using static csharp_editor.Renderer;

namespace csharp_editor {
    public partial class MainView : UserControl {
        public MainView() {

            InitializeComponent();

            this.MouseClick += MainView_MouseClick;
        }

        public void Init(CallbackDelegate callback) {

            //Renderer.Init();
            Renderer.InitWithCallback(callback);

            IntPtr sdlHandle = Renderer.GetHandle();

            // ** Get the parent window handle.
            IntPtr windowHandle = Handle;

            //Renderer.SetWindowPos(
            //    Handle,
            //    sdlHandle,
            //    0,
            //    0,
            //    0,
            //    0,
            //    0x0401 // NOSIZE | SHOWWINDOW
            //);

            // Attach the SDL2 window to the panel
            Renderer.SetParent(sdlHandle, Handle);
            //Renderer.ShowWindow(sdlHandle, 1); // SHOWNORMAL
        }

        public void Release() {

            Renderer.Stop();
        }

        public void Draw() {

            Renderer.Update();

            Renderer.Draw();
        }

        private void MainView_MouseClick(object? sender, MouseEventArgs e) {

            
        }
    }
}
