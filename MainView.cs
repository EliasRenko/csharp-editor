using static csharp_editor.Renderer;

namespace csharp_editor {
    public partial class MainView : UserControl {

        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOZORDER = 0x0004;

        public MainView() {

            InitializeComponent();

            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.StandardClick, false);

            this.MouseClick += MainView_MouseClick;
        }

        public void Init(CallbackDelegate callback) {

            //Renderer.Init();
            Renderer.InitWithCallback(callback);

            IntPtr sdlHandle = Renderer.GetHandle();

            // ** Get the parent window handle.
            IntPtr windowHandle = Handle;

            Renderer.SetWindowPos(
                sdlHandle,
                Handle,
                0,
                0,
                0,
                0,
                0x0401 | SWP_NOSIZE | SWP_NOZORDER // NOSIZE | SHOWWINDOW
            );

            // Attach the SDL2 window to the panel
            Renderer.SetParent(sdlHandle, Handle);
            //Renderer.ShowWindow(sdlHandle, 1); // SHOWNORMAL
        }

        public void Release() {

            Renderer.Release();
        }

        public void Render() {

            Renderer.Render();
        }

        public void Tick() {

            Renderer.Update();
        }

        private void MainView_MouseClick(object? sender, MouseEventArgs e) {

        }

        protected override void OnGotFocus(EventArgs e) {

            //base.OnGotFocus(e);
        }
    }
}
