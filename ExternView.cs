using System.Runtime.InteropServices;
using static csharp_editor.Externs;

namespace csharp_editor {
    public partial class ExternView : UserControl {

        public CallbackDelegate logCallback = null!; // initialized in Init()
        public bool active = false;

        private IntPtr sdlWindowHandle = IntPtr.Zero;

        public ExternView() {

            InitializeComponent();

            // ** Events
            //MouseClick += MainView_MouseClick;
            MouseMove += OnMouseMotion;
            Resize += ExternView_Resize;
        }

        // CallbackDelegate callback
        public void Init(CallbackDelegate callback) {

            logCallback = callback;

            if (Externs.InitWithCallback(callback) != 1) {
                MessageBox.Show("Failed to initialize engine", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the SDL window handle
            sdlWindowHandle = Externs.GetWindowHandle();
            if (sdlWindowHandle == IntPtr.Zero) {
                MessageBox.Show("Failed to get SDL window handle", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Remove window border styles to prevent Vista-style frame
            long style = Externs.GetWindowLong(sdlWindowHandle, Externs.GWL_STYLE);
            style &= ~(Externs.WS_CAPTION | Externs.WS_THICKFRAME | Externs.WS_MINIMIZE |
                       Externs.WS_MAXIMIZE | Externs.WS_SYSMENU | Externs.WS_BORDER | Externs.WS_DLGFRAME);
            style |= Externs.WS_CHILD | Externs.WS_VISIBLE;
            Externs.SetWindowLong(sdlWindowHandle, Externs.GWL_STYLE, style);

            // Force window frame refresh to apply style changes
            Externs.SetWindowPos(sdlWindowHandle, IntPtr.Zero, 0, 0, 0, 0,
                Externs.SWP_FRAMECHANGED | Externs.SWP_NOMOVE | Externs.SWP_NOSIZE |
                Externs.SWP_NOZORDER | Externs.SWP_NOACTIVATE);

            // Disable rounded corners (Windows 11) - must be done before SetParent
            int preference = Externs.DWMWCP_DONOTROUND;
            Externs.DwmSetWindowAttribute(sdlWindowHandle, Externs.DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(int));

            // Set SDL window as child of panel
            SetParent(sdlWindowHandle, panel_extern.Handle);
            Externs.SetWindowPosition(0, 0);

            // Size the SDL window to match the panel
            Externs.MoveWindow(sdlWindowHandle, 0, 0, panel_extern.Width, panel_extern.Height, true);

            // Load default state (CollisionTestState)
            //Externs.LoadState(0);

            active = true;
        }

        public void Release() {
            if (active) {
                active = false;
                Externs.Release();
                
                if (sdlWindowHandle != IntPtr.Zero) {
                    sdlWindowHandle = IntPtr.Zero;
                }
            }
        }

        public void Render() {
            Externs.Render();
        }

        public void SwapBuffers() {
            Externs.SwapBuffers();
        }

        public void UpdateFrame(float deltaTime) {
            Externs.UpdateFrame(deltaTime);
        }

        public void ImportFont(string filename, int size) {
            Externs.ImportFont(filename, size);
        }
        
        // public void OnMouseMove() {
        //     Externs.OnMouseMotion(x, y);
        // }
        
        private void OnMouseMotion(object? sender, MouseEventArgs e) {
            Externs.OnMouseMotion(e.X, e.Y);
        }

        public void OnMouseButtonDown(int x, int y, int button) {
            Externs.OnMouseButtonDown(x, y, button);
        }

        public void OnMouseButtonUp(int x, int y, int button) {
            Externs.OnMouseButtonUp(x, y, button);
        }

        public void OnKeyboardDown(int keyCode) {
            Externs.OnKeyboardDown(keyCode);
        }

        public void OnKeyboardUp(int keyCode) {
            Externs.OnKeyboardUp(keyCode);
        }

        private void ExternView_Resize(object sender, EventArgs e) {
            if (sdlWindowHandle != IntPtr.Zero && active && panel_extern != null) {
                Externs.MoveWindow(sdlWindowHandle, 0, 0, panel_extern.Width, panel_extern.Height, true);
                //Externs.SetWindowSize(panel_extern.Width, panel_extern.Height);
            }
        }
        
        #region Core
        
        public void LoadFont(string filename) {
            Externs.LoadFont(filename);
        }

        public void ExportFont(string filename) {
            Externs.ExportFont(filename);
        }

        public void RebakeFont(float fontSize, int atlasWidth, int atlasHeight, int firstChar, int numChars) {
            Externs.RebakeFont(fontSize, atlasWidth, atlasHeight, firstChar, numChars);
        }
        
        #endregion
        
        #region Texture

        public void GetTextureData(string path, out TextureDataStruct outData) {
            Externs.GetTextureData(path, out outData);
        }
        
        
        public int GetTileset(string tilesetName, out TilesetInfoStruct outInfo) {
            return Externs.GetTileset(tilesetName, out outInfo);
        }
        
        public int GetTilesetAt(int index, out TilesetInfoStruct outInfo) {
            return Externs.GetTilesetAt(index, out outInfo);
        }

        public int GetTilesetCount() {
            return Externs.GetTilesetCount();
        }

        public bool SetActiveTileset(string tilesetName) {
            return Externs.SetActiveTileset(tilesetName);
        }

        public string? CreateTileset(string texturePath, string name, int tileSize) {
            IntPtr result = Externs.CreateTileset(texturePath, name, tileSize);
            if (result == IntPtr.Zero) {
                return null;
            }
            return Marshal.PtrToStringAnsi(result);
        }

        public int GetActiveTile() {
            return Externs.GetActiveTile();
        }
        
        public void SetActiveTile(int tileRegionId) {
            Externs.SetActiveTile(tileRegionId);
        }
        
        public void ImportMap(string path) {
            Externs.ImportMap(path);
        }
        
        public void ExportMap(string path) {
            Externs.ExportMap(path);
        }
        
        #endregion
        
        #region Layer Management
        
        public void CreateTilemapLayer(string layerName, string tilesetName, int index) {
            Externs.CreateTilemapLayer(layerName, tilesetName, index);
        }
        
        // layerName is used by backend; tileset selection is no longer part of the API.
        public void CreateEntityLayer(string layerName) {
            Externs.CreateEntityLayer(layerName);
        }
        
        public void CreateFolderLayer(string layerName) {
            Externs.CreateFolderLayer(layerName);
        }
        
        public int SetActiveLayer(string layerName) {
            return Externs.SetActiveLayer(layerName);
        }

        public int SetActiveLayerAt(int index) {
            return Externs.SetActiveLayerAt(index);
        }

        public int RemoveLayer(string layerName) {
            return Externs.RemoveLayer(layerName);
        }
        
        public int RemoveLayerByIndex(int index) {
            return Externs.RemoveLayerByIndex(index);
        }
        
        public int GetLayerCount() {
            return Externs.GetLayerCount();
        }
        
        public int GetLayerInfoAt(int index, out Externs.LayerInfoStruct outInfo) {
            return Externs.GetLayerInfoAt(index, out outInfo);
        }
        
        public int GetLayerInfo(string layerName, out Externs.LayerInfoStruct outInfo) {
            return Externs.GetLayerInfo(layerName, out outInfo);
        }

        public void ReplaceLayerTileset(string layerName, string tilesetName) {
            Externs.ReplaceLayerTileset(layerName, tilesetName);
        }

        public void SetLayerProperties(string originalName, string newName, bool visible, string? tilesetName = null, int type = 0, bool silhouette = false, System.Drawing.Color silhouetteColor = default) {
            IntPtr namePtr = Marshal.StringToHGlobalAnsi(newName);
            IntPtr tilesetNamePtr = tilesetName != null ? Marshal.StringToHGlobalAnsi(tilesetName) : IntPtr.Zero;
            try {
                // Convert Color to RGBA (0xRRGGBBAA)
                int rgba = (silhouetteColor.R << 24) | (silhouetteColor.G << 16) | (silhouetteColor.B << 8) | silhouetteColor.A;
                var info = new Externs.LayerInfoStruct {
                    name = namePtr,
                    tilesetName = tilesetNamePtr,
                    type = type,
                    visible = visible ? 1 : 0,
                    silhouette = silhouette,
                    silhouetteColor = rgba
                };
                Externs.SetLayerProperties(originalName, ref info);
            } finally {
                Marshal.FreeHGlobal(namePtr);
                if (tilesetNamePtr != IntPtr.Zero) {
                    Marshal.FreeHGlobal(tilesetNamePtr);
                }
            }
        }
        
        public int MoveLayerUp(string layerName) {
            return Externs.MoveLayerUp(layerName);
        }
        
        public int MoveLayerDown(string layerName) {
            return Externs.MoveLayerDown(layerName);
        }
        
        public int MoveLayerUpByIndex(int index) {
            return Externs.MoveLayerUpByIndex(index);
        }
        
        public int MoveLayerDownByIndex(int index) {
            return Externs.MoveLayerDownByIndex(index);
        }
        
        // Entity Management
        
        public string? CreateEntity(string entityName, int width, int height, string tilesetName) {
            IntPtr result = Externs.CreateEntity(entityName, width, height, tilesetName);
            return result == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(result);
        }
        
        public string? GetEntity(string entityName, out Externs.EntityDataStruct outData) {
            var result = Externs.GetEntity(entityName, out outData);
            return result == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(result);
        }
        
        public string? GetEntityAt(int index, out Externs.EntityDataStruct outData) {
            var result = Externs.GetEntityAt(index, out outData);
            return result == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(result);
        }
        
        public int GetEntityCount() {
            return Externs.GetEntityCount();
        }
        
        public void SetEntityRegion(string entityName, int x, int y, int width, int height) {
            Externs.SetEntityRegion(entityName, x, y, width, height);
        }
        
        public int RemoveEntity(string entityName) {
            return Externs.RemoveEntity(entityName);
        }
        
        public int SetActiveEntity(string entityName) {
            return Externs.SetActiveEntity(entityName);
        }
        
        public int PlaceEntity(int x, int y) {
            return Externs.PlaceEntity(x, y);
        }

        // --- batch group helpers ------------------------------------------------
        public int GetEntityLayerBatchCount(string layerName) {
            return Externs.GetEntityLayerBatchCount(layerName);
        }

        public int GetEntityLayerBatchCountAt(int index) {
            return Externs.GetEntityLayerBatchCountAt(index);
        }

        public string? GetEntityLayerBatchTilesetName(string layerName, int batchIndex) {
            IntPtr ptr = Externs.GetEntityLayerBatchTilesetName(layerName, batchIndex);
            return ptr == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(ptr);
        }

        // movement helpers -------------------------------------------------------
        public int MoveEntityLayerBatchUp(string layerName, int batchIndex) {
            return Externs.MoveEntityLayerBatchUp(layerName, batchIndex);
        }
        public int MoveEntityLayerBatchDown(string layerName, int batchIndex) {
            return Externs.MoveEntityLayerBatchDown(layerName, batchIndex);
        }
        public int MoveEntityLayerBatchTo(string layerName, int batchIndex, int newIndex) {
            return Externs.MoveEntityLayerBatchTo(layerName, batchIndex, newIndex);
        }
        public int MoveEntityLayerBatchUpByIndex(int layerIndex, int batchIndex) {
            return Externs.MoveEntityLayerBatchUpByIndex(layerIndex, batchIndex);
        }
        public int MoveEntityLayerBatchDownByIndex(int layerIndex, int batchIndex) {
            return Externs.MoveEntityLayerBatchDownByIndex(layerIndex, batchIndex);
        }
        public int MoveEntityLayerBatchToByIndex(int layerIndex, int batchIndex, int newIndex) {
            return Externs.MoveEntityLayerBatchToByIndex(layerIndex, batchIndex, newIndex);
        }
        
        #endregion
        
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
