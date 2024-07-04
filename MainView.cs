using System.Diagnostics;
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

        const Int64 WS_THICKFRAME = 0x00040000L; //The window has a sizing border. Same as the WS_SIZEBOX style.
        const Int64 WS_CHILD = 0x40000000L; // The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.
        const Int64 WS_EX_NOACTIVATE = 0x08000000L;
        const Int64 WS_EX_TOOLWINDOW = 0x00000080L;

        protected override CreateParams CreateParams {
            get {
                CreateParams ret = base.CreateParams;
                //ret.Style = (int)WS_THICKFRAME |
                //   (int)WS_CHILD;
                ret.ExStyle |= (int)WS_EX_NOACTIVATE |
                   (int)WS_EX_TOOLWINDOW;
                return ret;
            }
        }
    }
}
