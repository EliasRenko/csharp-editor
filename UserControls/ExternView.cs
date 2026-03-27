using System.Runtime.InteropServices;
using NativeHaxeRuntime;
using csharp_editor.Models;

namespace csharp_editor.UserControls {
    public partial class ExternView : UserControl {

        public CExterns.CallbackDelegate logCallback = null!; // initialized in Init()
        public bool active = false;

        private string _lastErrorMessage { get; set; } = "Unknown native error.";

        // Fired on the UI thread whenever the C++ engine reports a selection change
        public event EventHandler? EntitySelectionChanged;

        public void SetLastErrorMessage(string message) {
            _lastErrorMessage = string.IsNullOrWhiteSpace(message) ? "Unknown native error." : message;
        }

        public string GetLastErrorMessage() {
            return _lastErrorMessage;
        }

        // Keep a strong reference so the delegate isn't GC'd while the C++ side holds a pointer
        private CExternsEditor.EntitySelectionChangedCallback? _entitySelectionChangedCallback;

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

            // Register entity selection callback
            _entitySelectionChangedCallback = () => {
                // Marshal back to the UI thread
                BeginInvoke(() => EntitySelectionChanged?.Invoke(this, EventArgs.Empty));
            };
            CExternsEditor.SetEntitySelectionChangedCallback(_entitySelectionChangedCallback);

            // Get the SDL window handle
            sdlWindowHandle = CExternsEditor.GetWindowHandle();
            if (sdlWindowHandle == IntPtr.Zero) {
                MessageBox.Show("Failed to get SDL window handle", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Make the SDL window a plain child window (no chrome/borders).
            CExternsEditor.ApplyChildWindowStyle(sdlWindowHandle);

            // Disable rounded corners (Windows 11) - must be done before SetParent
            int preference = CExternsEditor.DWMWCP_DONOTROUND;
            CExternsEditor.DwmSetWindowAttribute(sdlWindowHandle, CExternsEditor.DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(int));

            // Set SDL window as child of panel
            CExternsEditor.SetParent(sdlWindowHandle, panel_extern.Handle);
            CExternsEditor.SetWindowPosition(0, 0);

            // Size the SDL window to match the panel
            CExternsEditor.MoveWindow(sdlWindowHandle, 0, 0, panel_extern.Width, panel_extern.Height, true);

            active = true;
        }

        public void Release() {
            if (active) {
                active = false;
                CExterns.Release();

                if (sdlWindowHandle != IntPtr.Zero) {
                    sdlWindowHandle = IntPtr.Zero;
                }
            }
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
            CExternsEditor.OnMouseMotion(e.X, e.Y);
        }

        private void ExternView_Resize(object? sender, EventArgs e) {
            if (sdlWindowHandle != IntPtr.Zero && active && panel_extern != null) {
                CExternsEditor.MoveWindow(sdlWindowHandle, 0, 0, panel_extern.Width, panel_extern.Height, true);
            }
        }

        /// <summary>Returns project properties if a project is loaded. Returns false and default outInfo if no project is loaded.</summary>
        public bool GetProjectProps(out ProjectInfoStruct outInfo) {
            bool result = CExternsEditor.GetProjectProps(out CExternsEditor.ProjectProps temp);
            if (result) {
                outInfo = new ProjectInfoStruct {
                    FilePath = Marshal.PtrToStringAnsi(temp.filePath),
                    ProjectName = Marshal.PtrToStringAnsi(temp.projectName),
                    DefaultTileSizeX = temp.defaultTileSizeX,
                    DefaultTileSizeY = temp.defaultTileSizeY
                };
            } else {
                outInfo = default;
            }
            return result;
        }

        public bool EditProject(ProjectInfoStruct info) {
            IntPtr filePathPtr = Marshal.StringToHGlobalAnsi(info.FilePath ?? "");
            IntPtr projectNamePtr = Marshal.StringToHGlobalAnsi(info.ProjectName ?? "");
            try {
                var native = new CExternsEditor.ProjectProps {
                    filePath = filePathPtr,
                    projectName = projectNamePtr,
                    defaultTileSizeX = info.DefaultTileSizeX,
                    defaultTileSizeY = info.DefaultTileSizeY
                };

                return CExternsEditor.EditProject(ref native);
            } finally {
                Marshal.FreeHGlobal(filePathPtr);
                Marshal.FreeHGlobal(projectNamePtr);
            }
        }

        public bool GetMapProps(out MapInfoStruct outInfo) {
            CExternsEditor.MapProps temp;
            bool success = CExternsEditor.GetMapProps(out temp);

            if (!success) {
                outInfo = default;
                return false;
            }

            outInfo = new MapInfoStruct {
                idd = Marshal.PtrToStringAnsi(temp.idd),
                name = Marshal.PtrToStringAnsi(temp.name),
                worldx = temp.worldx,
                worldy = temp.worldy,
                width = temp.width,
                height = temp.height,
                tileSizeX = temp.tileSizeX,
                tileSizeY = temp.tileSizeY,
                bgColor = temp.bgColor,
                gridColor = temp.gridColor,
                projectFilePath = Marshal.PtrToStringAnsi(temp.projectFilePath),
                projectName = Marshal.PtrToStringAnsi(temp.projectName)
            };

            return true;
        }

        public bool SetMapProps(MapInfoStruct info) {
            IntPtr projectFilePathPtr = Marshal.StringToHGlobalAnsi(info.projectFilePath ?? "");
            IntPtr projectNamePtr = Marshal.StringToHGlobalAnsi(info.projectName ?? "");

            CExternsEditor.MapProps temp = new CExternsEditor.MapProps {
                idd = Marshal.StringToHGlobalAnsi(info.idd ?? ""),
                name = Marshal.StringToHGlobalAnsi(info.name ?? ""),
                worldx = info.worldx,
                worldy = info.worldy,
                width = info.width,
                height = info.height,
                tileSizeX = info.tileSizeX,
                tileSizeY = info.tileSizeY,
                bgColor = info.bgColor,
                gridColor = info.gridColor,
                projectFilePath = projectFilePathPtr,
                projectName = projectNamePtr
            };

            try {
                return CExternsEditor.SetMapProps(ref temp);
            } finally {
                Marshal.FreeHGlobal(temp.idd);
                Marshal.FreeHGlobal(temp.name);
                Marshal.FreeHGlobal(temp.projectFilePath);
                Marshal.FreeHGlobal(temp.projectName);
            }
        }

        public bool SetLayerProperties(string originalName, string newName, bool visible, string? tilesetName = null, int type = 0, bool silhouette = false, System.Drawing.Color silhouetteColor = default) {
            IntPtr namePtr = Marshal.StringToHGlobalAnsi(newName);
            IntPtr tilesetNamePtr = tilesetName != null ? Marshal.StringToHGlobalAnsi(tilesetName) : IntPtr.Zero;
            try {
                // Convert Color to RGBA (0xRRGGBBAA)
                int rgba = (silhouetteColor.R << 24) | (silhouetteColor.G << 16) | (silhouetteColor.B << 8) | silhouetteColor.A;
                var info = new CExternsEditor.LayerInfoStruct {
                    name = namePtr,
                    tilesetName = tilesetNamePtr,
                    type = type,
                    visible = visible ? 1 : 0,
                    silhouette = silhouette,
                    silhouetteColor = rgba
                };
                return CExternsEditor.SetLayerProperties(originalName, ref info);
            } finally {
                Marshal.FreeHGlobal(namePtr);
                if (tilesetNamePtr != IntPtr.Zero) {
                    Marshal.FreeHGlobal(tilesetNamePtr);
                }
            }
        }

        public string? GetEntityLayerBatchTilesetName(string layerName, int batchIndex) {
            IntPtr ptr = CExternsEditor.GetEntityLayerBatchTilesetName(layerName, batchIndex);
            return ptr == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(ptr);
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

