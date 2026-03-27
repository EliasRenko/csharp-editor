using System.Runtime.InteropServices;
using NativeHaxeRuntime;

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
        public string? projectFilePath;
        public string? projectName;
    }

    public struct ProjectInfoStruct {
        public string? FilePath;
        public string? ProjectName;
        public int DefaultTileSizeX;
        public int DefaultTileSizeY;
    }

    public partial class ExternView : UserControl {

        public CExterns.CallbackDelegate logCallback = null!; // initialized in Init()
        public bool active = false;

        private string _lastErrorMessage = "Unknown native error.";

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

            // Load default state (CollisionTestState)
            //CExternsEditor.LoadState(0);

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

        public void OnMouseButtonDown(int x, int y, int button) {
            CExternsEditor.OnMouseButtonDown(x, y, button);
        }

        public void OnMouseButtonUp(int x, int y, int button) {
            CExternsEditor.OnMouseButtonUp(x, y, button);
        }

        public void OnMouseWheel(float x, float y, float delta) {
            CExternsEditor.OnMouseWheel(x, y, delta);
        }

        public void OnKeyboardDown(int keyCode) {
            CExternsEditor.OnKeyboardDown(keyCode);
        }

        public void OnKeyboardUp(int keyCode) {
            CExternsEditor.OnKeyboardUp(keyCode);
        }

        private void ExternView_Resize(object? sender, EventArgs e) {
            if (sdlWindowHandle != IntPtr.Zero && active && panel_extern != null) {
                CExternsEditor.MoveWindow(sdlWindowHandle, 0, 0, panel_extern.Width, panel_extern.Height, true);
                //CExternsEditor.SetWindowSize(panel_extern.Width, panel_extern.Height);
            }
        }

#region Texture

        public void GetTextureData(string path, out CExternsEditor.TextureDataStruct outData) {
            CExternsEditor.GetTextureData(path, out outData);
        }
        
        
        public bool GetTileset(string tilesetName, out CExternsEditor.TilesetInfoStruct outInfo) {
            return CExternsEditor.GetTileset(tilesetName, out outInfo);
        }
        
        public bool GetTilesetAt(int index, out CExternsEditor.TilesetInfoStruct outInfo) {
            return CExternsEditor.GetTilesetAt(index, out outInfo);
        }

        public int GetTilesetCount() {
            return CExternsEditor.GetTilesetCount();
        }

        public bool SetActiveTileset(string tilesetName) {
            return CExternsEditor.SetActiveTileset(tilesetName);
        }

        public bool CreateTileset(string texturePath, string name) {
            return CExternsEditor.CreateTileset(texturePath, name);
        }

        public bool DeleteTileset(string name) {
            return CExternsEditor.DeleteTileset(name);
        }

        public int GetActiveTile() {
            return CExternsEditor.GetActiveTile();
        }
        
        public void SetActiveTile(int tileRegionId) {
            CExternsEditor.SetActiveTile(tileRegionId);
        }

        public void SetToolType(ToolType toolType) {
            CExternsEditor.SetToolType(toolType);
        }

        public ToolType GetToolType() {
            return CExternsEditor.GetToolType();
        }

        public int ImportMap(string path) {
            return CExternsEditor.ImportMap(path);
        }

        public int SetActiveState(int stateId) {
            return CExternsEditor.SetActiveState(stateId);
        }

        public int ReleaseState(int stateId) {
            return CExternsEditor.ReleaseState(stateId);
        }

        public int NewEditorState() {
            return CExternsEditor.NewEditorState();
        }
        
        public bool ExportMap(string path) {
            return CExternsEditor.ExportMap(path);
        }

        /// <summary>Saves the current project state to <paramref name="filePath"/>.</summary>
        /// <returns>Non-zero on success.</returns>
        public bool ExportProject(string filePath, string projectName) {
            return CExternsEditor.ExportProject(filePath, projectName);
        }

        /// <summary>Loads a project from <paramref name="filePath"/> into the engine.</summary>
        /// <returns>Non-zero on success.</returns>
        public bool ImportProject(string filePath) {
            return CExternsEditor.ImportProject(filePath);
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
        
        #endregion
        
        #region Layer Management
        
        public void CreateTilemapLayer(string layerName, string tilesetName, int tileSize, int index) {
            CExternsEditor.CreateTilemapLayer(layerName, tilesetName, tileSize, index);
        }
        
        // layerName is used by backend; tileset selection is no longer part of the API.
        public void CreateEntityLayer(string layerName) {
            CExternsEditor.CreateEntityLayer(layerName);
        }
        
        public void CreateFolderLayer(string layerName) {
            CExternsEditor.CreateFolderLayer(layerName);
        }
        
        public bool SetActiveLayer(string layerName) {
            return CExternsEditor.SetActiveLayer(layerName);
        }

        public bool SetActiveLayerAt(int index) {
            return CExternsEditor.SetActiveLayerAt(index);
        }

public bool RemoveLayer(string layerName) {
            return CExternsEditor.RemoveLayer(layerName);
        }

        public bool RemoveLayerByIndex(int index) {
            return CExternsEditor.RemoveLayerByIndex(index);
        }
        
        public int GetLayerCount() {
            return CExternsEditor.GetLayerCount();
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
        
        public bool GetLayerInfoAt(int index, out CExternsEditor.LayerInfoStruct outInfo) {
            return CExternsEditor.GetLayerInfoAt(index, out outInfo);
        }
        
        public bool GetLayerInfo(string layerName, out CExternsEditor.LayerInfoStruct outInfo) {
            return CExternsEditor.GetLayerInfo(layerName, out outInfo);
        }

        public bool ReplaceLayerTileset(string layerName, string tilesetName) {
            return CExternsEditor.ReplaceLayerTileset(layerName, tilesetName);
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
        
public bool MoveLayerUp(string layerName) {
            return CExternsEditor.MoveLayerUp(layerName);
        }

        public bool MoveLayerDown(string layerName) {
            return CExternsEditor.MoveLayerDown(layerName);
        }

        public bool MoveLayerTo(string layerName, int newIndex) {
            return CExternsEditor.MoveLayerTo(layerName, newIndex);
        }

        public bool MoveLayerUpByIndex(int index) {
            return CExternsEditor.MoveLayerUpByIndex(index);
        }

        public bool MoveLayerDownByIndex(int index) {
            return CExternsEditor.MoveLayerDownByIndex(index);
        }
        
        // Entity Management
        
        public bool CreateEntity(string entityName, ref CExternsEditor.EntityDataStruct data) {
            return CExternsEditor.CreateEntity(entityName, ref data);
        }

        public bool EditEntity(string entityName, ref CExternsEditor.EntityDataStruct data) {
            return CExternsEditor.EditEntity(entityName, ref data);
        }
        
        public Boolean GetEntity(string entityName, out CExternsEditor.EntityDataStruct outData) {
            return CExternsEditor.GetEntity(entityName, out outData);
        }
        
        public Boolean GetEntityAt(int index, out CExternsEditor.EntityDataStruct outData) {
            return CExternsEditor.GetEntityAt(index, out outData);
        }
        
        public int GetEntityCount() {
            return CExternsEditor.GetEntityCount();
        }
        
        public bool DeleteEntityDef(string entityName) {
            return CExternsEditor.DeleteEntityDef(entityName);
        }
        
        public bool SetActiveEntity(string entityName) {
            return CExternsEditor.SetActiveEntity(entityName);
        }

        public int GetEntitySelectionCount() {
            return CExternsEditor.GetEntitySelectionCount();
        }

        public bool GetEntitySelectionInfo(int index, out CExternsEditor.EntityStruct outData) {
            return CExternsEditor.GetEntitySelectionInfo(index, out outData);
        }

        public bool SelectEntityByUID(string uid) {
            return CExternsEditor.SelectEntityByUID(uid);
        }

        public bool SelectEntityInLayerByUID(string layerName, string uid) {
            return CExternsEditor.SelectEntityInLayerByUID(layerName, uid);
        }

        public void DeselectEntity() {
            CExternsEditor.DeselectEntity();
        }

        public int GetEntityLayerInstanceCount(string layerName, int batchIndex = -1) {
            return CExternsEditor.GetEntityLayerInstanceCount(layerName, batchIndex);
        }

        public int GetEntityLayerInstanceAt(string layerName, int batchIndex, int instanceIndex, out CExternsEditor.EntityStruct outData) {
            return CExternsEditor.GetEntityLayerInstanceAt(layerName, batchIndex, instanceIndex, out outData);
        }

        // --- batch group helpers ------------------------------------------------
        public int GetEntityLayerBatchCount(string layerName) {
            return CExternsEditor.GetEntityLayerBatchCount(layerName);
        }

        public int GetEntityLayerBatchCountAt(int index) {
            return CExternsEditor.GetEntityLayerBatchCountAt(index);
        }

        public string? GetEntityLayerBatchTilesetName(string layerName, int batchIndex) {
            IntPtr ptr = CExternsEditor.GetEntityLayerBatchTilesetName(layerName, batchIndex);
            return ptr == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(ptr);
        }

        // movement helpers -------------------------------------------------------
        public bool MoveEntityLayerBatchUp(string layerName, int batchIndex) {
            return CExternsEditor.MoveEntityLayerBatchUp(layerName, batchIndex);
        }
        public bool MoveEntityLayerBatchDown(string layerName, int batchIndex) {
            return CExternsEditor.MoveEntityLayerBatchDown(layerName, batchIndex);
        }
        public bool MoveEntityLayerBatchTo(string layerName, int batchIndex, int newIndex) {
            return CExternsEditor.MoveEntityLayerBatchTo(layerName, batchIndex, newIndex);
        }
        public bool MoveEntityLayerBatchUpByIndex(int layerIndex, int batchIndex) {
            return CExternsEditor.MoveEntityLayerBatchUpByIndex(layerIndex, batchIndex);
        }
        public bool MoveEntityLayerBatchDownByIndex(int layerIndex, int batchIndex) {
            return CExternsEditor.MoveEntityLayerBatchDownByIndex(layerIndex, batchIndex);
        }
        public bool MoveEntityLayerBatchToByIndex(int layerIndex, int batchIndex, int newIndex) {
            return CExternsEditor.MoveEntityLayerBatchToByIndex(layerIndex, batchIndex, newIndex);
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
