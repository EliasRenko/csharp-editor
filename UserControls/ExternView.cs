using System.Runtime.InteropServices;
using static csharp_editor.Externs;

namespace csharp_editor.UserControls {
    public struct MapInfoStruct {
        public string? idd;
        public string? name;
        public int worldx;
        public int worldy;
        public int width;
        public int height;
        public int tileSizeX;
        public int tileSizeY;
        public int bgColor;
        public int gridColor;
    }

    public partial class ExternView : UserControl {

        public CallbackDelegate logCallback = null!; // initialized in Init()
        public bool active = false;

        // Fired on the UI thread whenever the C++ engine reports a selection change
        public event EventHandler? EntitySelectionChanged;

        // Keep a strong reference so the delegate isn't GC'd while the C++ side holds a pointer
        private Externs.EntitySelectionChangedCallback? _entitySelectionChangedCallback;

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

            // Register entity selection callback
            _entitySelectionChangedCallback = () => {
                // Marshal back to the UI thread
                BeginInvoke(() => EntitySelectionChanged?.Invoke(this, EventArgs.Empty));
            };
            Externs.SetEntitySelectionChangedCallback(_entitySelectionChangedCallback);

            // Get the SDL window handle
            sdlWindowHandle = Externs.GetWindowHandle();
            if (sdlWindowHandle == IntPtr.Zero) {
                MessageBox.Show("Failed to get SDL window handle", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Make the SDL window a plain child window (no chrome/borders).
            Externs.ApplyChildWindowStyle(sdlWindowHandle);

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
        
        private void OnMouseMotion(object? sender, MouseEventArgs e) {
            Externs.OnMouseMotion(e.X, e.Y);
        }

        public void OnMouseButtonDown(int x, int y, int button) {
            Externs.OnMouseButtonDown(x, y, button);
        }

        public void OnMouseButtonUp(int x, int y, int button) {
            Externs.OnMouseButtonUp(x, y, button);
        }

        public void OnMouseWheel(float x, float y, float delta) {
            Externs.OnMouseWheel(x, y, delta);
        }

        public void OnKeyboardDown(int keyCode) {
            Externs.OnKeyboardDown(keyCode);
        }

        public void OnKeyboardUp(int keyCode) {
            Externs.OnKeyboardUp(keyCode);
        }

        private void ExternView_Resize(object? sender, EventArgs e) {
            if (sdlWindowHandle != IntPtr.Zero && active && panel_extern != null) {
                Externs.MoveWindow(sdlWindowHandle, 0, 0, panel_extern.Width, panel_extern.Height, true);
                //Externs.SetWindowSize(panel_extern.Width, panel_extern.Height);
            }
        }

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

        public string? CreateTileset(string texturePath, string name) {
            IntPtr result = Externs.CreateTileset(texturePath, name);
            if (result == IntPtr.Zero) {
                return null;
            }
            return Marshal.PtrToStringAnsi(result);
        }

        /// <summary>Deletes a tileset. Returns null on success, or an error message on failure.</summary>
        public string? DeleteTileset(string name) {
            IntPtr result = Externs.DeleteTileset(name);
            return result == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(result);
        }

        public int GetActiveTile() {
            return Externs.GetActiveTile();
        }
        
        public void SetActiveTile(int tileRegionId) {
            Externs.SetActiveTile(tileRegionId);
        }

        public void SetToolType(ToolType toolType) {
            Externs.SetToolType(toolType);
        }

        public ToolType GetToolType() {
            return Externs.GetToolType();
        }

        public int ImportMap(string path) {
            return Externs.ImportMap(path);
        }

        public void LoadState(int id) {
            Externs.LoadState(id);
        }

        public int SetActiveState(int stateId) {
            return Externs.SetActiveState(stateId);
        }

        public int ReleaseState(int stateId) {
            return Externs.ReleaseState(stateId);
        }

        public int NewEditorState() {
            return Externs.NewEditorState();
        }
        
        public void ExportMap(string path) {
            Externs.ExportMap(path);
        }

        /// <summary>Saves the current project state to <paramref name="filePath"/>.</summary>
        /// <returns>Non-zero on success.</returns>
        public int ExportProject(string filePath, string projectName) {
            return Externs.ExportProject(filePath, projectName);
        }

        /// <summary>Loads a project from <paramref name="filePath"/> into the engine.</summary>
        /// <returns>Non-zero on success.</returns>
        public int ImportProject(string filePath) {
            return Externs.ImportProject(filePath);
        }

        /// <summary>Returns the project name embedded in the file at <paramref name="filePath"/>, or null.</summary>
        public string? GetProjectName(string filePath) {
            IntPtr ptr = Externs.GetProjectName(filePath);
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : null;
        }

        /// <summary>Returns the file path of the project that is currently active in the engine, or null.</summary>
        public string? GetActiveProjectPath() {
            IntPtr ptr = Externs.GetActiveProjectPath();
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : null;
        }
        
        #endregion
        
        #region Layer Management
        
        public void CreateTilemapLayer(string layerName, string tilesetName, int tileSize, int index) {
            Externs.CreateTilemapLayer(layerName, tilesetName, tileSize, index);
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

        public string? GetMapProps(out MapInfoStruct outInfo) {
            MapProps temp;
            IntPtr result = Externs.GetMapProps(out temp);
            
            if (result == IntPtr.Zero) {
                
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
                    gridColor = temp.gridColor
                };
                
                return null;
            }
            
            outInfo = default;
            return Marshal.PtrToStringAnsi(result);
        }

        public string? SetMapProps(MapInfoStruct info) {
            MapProps temp = new MapProps {
                idd = Marshal.StringToHGlobalAnsi(info.idd ?? ""),
                name = Marshal.StringToHGlobalAnsi(info.name ?? ""),
                worldx = info.worldx,
                worldy = info.worldy,
                width = info.width,
                height = info.height,
                tileSizeX = info.tileSizeX,
                tileSizeY = info.tileSizeY,
                bgColor = info.bgColor,
                gridColor = info.gridColor
            };
            
            try {
                IntPtr result = Externs.SetMapProps(ref temp);
                if (result == IntPtr.Zero) {
                    return null;
                }
                
                return Marshal.PtrToStringAnsi(result);
            } finally {
                Marshal.FreeHGlobal(temp.idd);
                Marshal.FreeHGlobal(temp.name);
            }
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
        
        public int MoveLayerTo(string layerName, int newIndex) {
            return Externs.MoveLayerTo(layerName, newIndex);
        }
        
        public int MoveLayerUpByIndex(int index) {
            return Externs.MoveLayerUpByIndex(index);
        }
        
        public int MoveLayerDownByIndex(int index) {
            return Externs.MoveLayerDownByIndex(index);
        }
        
        // Entity Management
        
        public string? CreateEntity(string entityName, ref Externs.EntityDataStruct data) {
            IntPtr result = Externs.CreateEntity(entityName, ref data);
            return result == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(result);
        }

        public string? EditEntity(string entityName, ref Externs.EntityDataStruct data) {
            IntPtr result = Externs.EditEntity(entityName, ref data);
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
        
        public int RemoveEntity(string entityName) {
            return Externs.RemoveEntity(entityName);
        }
        
        /// <summary>Deletes an entity definition. Returns null on success, or an error message on failure.</summary>
        public string? DeleteEntityDef(string entityName) {
            IntPtr result = Externs.DeleteEntityDef(entityName);
            return result == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(result);
        }
        
        public int SetActiveEntity(string entityName) {
            return Externs.SetActiveEntity(entityName);
        }
        
        public int PlaceEntity(int x, int y) {
            return Externs.PlaceEntity(x, y);
        }

        public int GetEntitySelectionCount() {
            return Externs.GetEntitySelectionCount();
        }

        public int GetEntitySelectionInfo(int index, out Externs.EntityStruct outData) {
            return Externs.GetEntitySelectionInfo(index, out outData);
        }

        public bool SelectEntityByUID(string uid) {
            return Externs.SelectEntityByUID(uid);
        }

        public bool SelectEntityInLayerByUID(string layerName, string uid) {
            return Externs.SelectEntityInLayerByUID(layerName, uid);
        }

        public void DeselectEntity() {
            Externs.DeselectEntity();
        }

        public int GetEntityLayerInstanceCount(string layerName, int batchIndex = -1) {
            return Externs.GetEntityLayerInstanceCount(layerName, batchIndex);
        }

        public int GetEntityLayerInstanceAt(string layerName, int batchIndex, int instanceIndex, out Externs.EntityStruct outData) {
            return Externs.GetEntityLayerInstanceAt(layerName, batchIndex, instanceIndex, out outData);
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
