using System;
using System.Runtime.InteropServices;
using NativeHaxeRuntime;

    public static class CExterns {

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CallbackDelegate([MarshalAs(UnmanagedType.LPStr)] string priority, [MarshalAs(UnmanagedType.LPStr)] string category, [MarshalAs(UnmanagedType.LPStr)] string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EntitySelectionChangedCallback();

        // Structs
        
        [StructLayout(LayoutKind.Sequential)]
        public struct TextureDataStruct {
            public IntPtr Data; // unsigned char*
            public int Width;
            public int Height;
            public int BytesPerPixel;
            public int DataLength;
            public int Transparent;
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TilesetInfoStruct {
            public IntPtr name;              // Tileset name (use Marshal.PtrToStringAnsi to read)
            public IntPtr texturePath;       // Resource path to texture (use Marshal.PtrToStringAnsi to read)
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct LayerInfoStruct {
            public IntPtr name;              // Layer name (use Marshal.PtrToStringAnsi to read)
            public int type;                 // Layer type (0 = TileLayer, 1 = EntityLayer)
            public IntPtr tilesetName;       // Tileset name for TileLayers (use Marshal.PtrToStringAnsi to read)
            public int tileSize;             // Tile size in pixels (TilemapLayer only, 0 for others)
            public int visible;              // Visibility flag (0 = hidden, 1 = visible)
            public bool silhouette;
            public int silhouetteColor;      // RGBA hex color for silhouette
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct EntityDataStruct {
            public IntPtr name;              // Entity name (use Marshal.PtrToStringAnsi to read)
            public int width;                // Entity width in pixels
            public int height;               // Entity height in pixels
            public IntPtr tilesetName;       // Tileset name (use Marshal.PtrToStringAnsi to read)
            public int regionX;              // Region X in tiles
            public int regionY;              // Region Y in tiles
            public int regionWidth;          // Region width in tiles
            public int regionHeight;         // Region height in tiles
            public float pivotX;
            public float pivotY;
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct EntityStruct {
            public IntPtr uid;               // Unique instance ID (use Marshal.PtrToStringAnsi to read)
            public IntPtr name;              // Entity definition name (use Marshal.PtrToStringAnsi to read)
            public int width;                // Width in pixels
            public int height;               // Height in pixels
            public int x;                    // World X position
            public int y;                    // World Y position
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MapProps {
            public IntPtr idd;               // id string
            public IntPtr name;              // name string
            public int worldx;
            public int worldy;
            public int width;
            public int height;
            public int tileSizeX;
            public int tileSizeY;
            public int bgColor;              // rgba hex
            public int gridColor;            // rgba hex
            public IntPtr projectFilePath;   // null / empty if map is not linked to a project
            public IntPtr projectName;       // null / empty if map is not linked to a project
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct ProjectProps {
            public IntPtr filePath;          // Project file path
            public IntPtr projectName;       // Project name
            public int defaultTileSizeX;
            public int defaultTileSizeY;
        }

        #region State managment

        [DllImport(DLL, EntryPoint = "newEditorState")]
        public static extern int NewEditorState();

        [DllImport(DLL, EntryPoint = "setActiveState")]
        public static extern int SetActiveState(int index);

        [DllImport(DLL, EntryPoint = "releaseState")]
        public static extern int ReleaseState(int index);

        #endregion

        #region Window

        [DllImport(DLL, EntryPoint = "getWindowHandle")]
        public static extern IntPtr GetWindowHandle();

        [DllImport(DLL, EntryPoint = "setWindowPosition")]
        public static extern void SetWindowPosition(int x, int y);

        [DllImport(DLL, EntryPoint = "setWindowSize")]
        public static extern void SetWindowSize(int width, int height);

        #endregion

        #region Input

        [DllImport(DLL, EntryPoint = "onMouseMotion")]
        public static extern void OnMouseMotion(int x, int y);

        [DllImport(DLL, EntryPoint = "onMouseButtonDown")]
        public static extern void OnMouseButtonDown(int x, int y, int button);

        [DllImport(DLL, EntryPoint = "onMouseButtonUp")]
        public static extern void OnMouseButtonUp(int x, int y, int button);

        [DllImport(DLL, EntryPoint = "onKeyboardDown")]
        public static extern void OnKeyboardDown(int keyCode);

        [DllImport(DLL, EntryPoint = "onKeyboardUp")]
        public static extern void OnKeyboardUp(int keyCode);

        [DllImport(DLL, EntryPoint = "onMouseWheel")]
        public static extern void OnMouseWheel(float x, float y, float delta);

        #endregion

        #region Project management

        [DllImport(DLL, EntryPoint = "exportProject", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool ExportProject(string filePath, string projectName);

        [DllImport(DLL, EntryPoint = "importProject", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool ImportProject(string filePath);

        [DllImport(DLL, EntryPoint = "getProjectProps", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetProjectProps(out ProjectProps outProps);

        [DllImport(DLL, EntryPoint = "editProject", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool EditProject(ref ProjectProps inProps);

        #endregion

        #region Map management

        [DllImport(DLL, EntryPoint = "exportMap", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool ExportMap(string path);
        
        [DllImport(DLL, EntryPoint = "importMap", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int ImportMap(string path);
        
        [DllImport(DLL, EntryPoint = "getMapProps", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool GetMapProps(out MapProps outInfo);
        
        [DllImport(DLL, EntryPoint = "setMapProps", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SetMapProps(ref MapProps info);

        #endregion

        #region Textures

        [DllImport(DLL, EntryPoint = "getTextureData", CharSet = CharSet.Ansi)]
        public static extern void GetTextureData(string path, out TextureDataStruct outData);
        
        #endregion

        #region Tileset managment
        
        [DllImport(DLL, EntryPoint = "createTileset", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool CreateTileset(string texturePath, string name);
        
        [DllImport(DLL, EntryPoint = "deleteTileset", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool DeleteTileset(string name);
        
        [DllImport(DLL, EntryPoint = "getTileset", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetTileset(string tilesetName, out TilesetInfoStruct outInfo);
        
        [DllImport(DLL, EntryPoint = "getTilesetAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetTilesetAt(int index, out TilesetInfoStruct outInfo);
        
        [DllImport(DLL, EntryPoint = "getTilesetCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTilesetCount();
        
        [DllImport(DLL, EntryPoint = "setActiveTileset", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SetActiveTileset(string tilesetName);

        [DllImport(DLL, EntryPoint = "getActiveTile")]
        public static extern int GetActiveTile();
        
        [DllImport(DLL, EntryPoint = "setActiveTile")]
        public static extern void SetActiveTile(int tileRegionId);

        #endregion

        #region Entity definitions managment

        [DllImport(DLL, EntryPoint = "createEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool CreateEntity(string entityName, ref EntityDataStruct data);
        
        [DllImport(DLL, EntryPoint = "editEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool EditEntity(string entityName, ref EntityDataStruct data);
        
        [DllImport(DLL, EntryPoint = "deleteEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool DeleteEntityDef(string entityName);
        
        [DllImport(DLL, EntryPoint = "getEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool GetEntity(string entityName, out EntityDataStruct outData);
        
        [DllImport(DLL, EntryPoint = "getEntityDefAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetEntityAt(int index, out EntityDataStruct outData);
        
        [DllImport(DLL, EntryPoint = "getEntityDefCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityCount();
        
        [DllImport(DLL, EntryPoint = "setActiveEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SetActiveEntity(string entityName);

        #endregion

        #region Entity instances management

        [DllImport(DLL, EntryPoint = "setEntitySelectionChangedCallback", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntitySelectionChangedCallback(EntitySelectionChangedCallback callback);

        [DllImport(DLL, EntryPoint = "getEntitySelectionCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntitySelectionCount();

        [DllImport(DLL, EntryPoint = "getEntitySelectionInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetEntitySelectionInfo(int index, out EntityStruct outData);
        
        [DllImport(DLL, EntryPoint = "selectEntityByUID", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SelectEntityByUID(string uid);

        [DllImport(DLL, EntryPoint = "selectEntityInLayerByUID", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SelectEntityInLayerByUID(string layerName, string uid);

        [DllImport(DLL, EntryPoint = "deselectEntity", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeselectEntity();

        #endregion

        #region  Layer managment
        
        [DllImport(DLL, EntryPoint = "createTilemapLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CreateTilemapLayer(string layerName, string tilesetName, int tileSize, int index);
        
        [DllImport(DLL, EntryPoint = "createEntityLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CreateEntityLayer(string layerName);
        
        [DllImport(DLL, EntryPoint = "createFolderLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CreateFolderLayer(string layerName);
        
        [DllImport(DLL, EntryPoint = "getLayerCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLayerCount();
        
        [DllImport(DLL, EntryPoint = "getLayerInfo", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool GetLayerInfo(string layerName, out LayerInfoStruct outInfo);
        
        [DllImport(DLL, EntryPoint = "getLayerInfoAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetLayerInfoAt(int index, out LayerInfoStruct outInfo);
        
        [DllImport(DLL, EntryPoint = "replaceLayerTileset", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool ReplaceLayerTileset(string layerName, string tilesetName);

        [DllImport(DLL, EntryPoint = "setActiveLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SetActiveLayer(string layerName);
        
        [DllImport(DLL, EntryPoint = "setActiveLayerAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetActiveLayerAt(int index);
        
        [DllImport(DLL, EntryPoint = "setLayerProperties", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SetLayerProperties(string layerName, ref LayerInfoStruct properties);

        [DllImport(DLL, EntryPoint = "setLayerPropertiesAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetLayerPropertiesAt(int index, ref LayerInfoStruct properties);

        [DllImport(DLL, EntryPoint = "removeLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool RemoveLayer(string layerName);
        
        [DllImport(DLL, EntryPoint = "removeLayerByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RemoveLayerByIndex(int index);

        [DllImport(DLL, EntryPoint = "moveLayerUp", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveLayerUp(string layerName);
        
        [DllImport(DLL, EntryPoint = "moveLayerDown", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveLayerDown(string layerName);
        
        [DllImport(DLL, EntryPoint = "moveLayerTo", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveLayerTo(string layerName, int newIndex);
        
        [DllImport(DLL, EntryPoint = "moveLayerUpByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveLayerUpByIndex(int index);
        
        [DllImport(DLL, EntryPoint = "moveLayerDownByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveLayerDownByIndex(int index);
        
        [DllImport(DLL, EntryPoint = "setToolType")]
        public static extern void SetToolType(int toolType);
        
        [DllImport(DLL, EntryPoint = "getToolType")]
        public static extern int GetToolType();

        #endregion
        
        #region Layer batch managment
        
        [DllImport(DLL, EntryPoint = "getEntityLayerBatchCount", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetEntityLayerBatchCount(string layerName);

        [DllImport(DLL, EntryPoint = "getEntityLayerBatchCountAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityLayerBatchCountAt(int index);

        [DllImport(DLL, EntryPoint = "getEntityLayerBatchTilesetName", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetEntityLayerBatchTilesetName(string layerName, int batchIndex);

        [DllImport(DLL, EntryPoint = "getEntityLayerInstanceCount", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetEntityLayerInstanceCount(string layerName, int batchIndex);

        [DllImport(DLL, EntryPoint = "getEntityLayerInstanceAt", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetEntityLayerInstanceAt(string layerName, int batchIndex, int instanceIndex, out EntityStruct outData);

        [DllImport(DLL, EntryPoint = "moveEntityLayerBatchUp", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveEntityLayerBatchUp(string layerName, int batchIndex);

        [DllImport(DLL, EntryPoint = "moveEntityLayerBatchDown", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveEntityLayerBatchDown(string layerName, int batchIndex);

        [DllImport(DLL, EntryPoint = "moveEntityLayerBatchTo", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveEntityLayerBatchTo(string layerName, int batchIndex, int newIndex);

        [DllImport(DLL, EntryPoint = "moveEntityLayerBatchUpByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveEntityLayerBatchUpByIndex(int layerIndex, int batchIndex);
        
        [DllImport(DLL, EntryPoint = "moveEntityLayerBatchDownByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveEntityLayerBatchDownByIndex(int layerIndex, int batchIndex);
        
        [DllImport(DLL, EntryPoint = "moveEntityLayerBatchToByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveEntityLayerBatchToByIndex(int layerIndex, int batchIndex, int newIndex);

        #endregion
        
        #region WinAPI Entry Points

        public static void ApplyChildWindowStyle(IntPtr windowHandle) {
            if (windowHandle == IntPtr.Zero) return;

            const long RemoveFlags =
                WS_CAPTION |
                WS_THICKFRAME |
                WS_MINIMIZE |
                WS_MAXIMIZE |
                WS_SYSMENU |
                WS_BORDER |
                WS_DLGFRAME;

            const long AddFlags = WS_CHILD | WS_VISIBLE;

            long style = GetWindowLong(windowHandle, GWL_STYLE);
            style = (style & ~RemoveFlags) | AddFlags;
            SetWindowLong(windowHandle, GWL_STYLE, style);
            SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE);
        }

        [DllImport("user32.dll") ]
        public static extern IntPtr SetWindowPos(
            IntPtr handle,
            IntPtr handleAfter,
            int x,
            int y,
            int cx,
            int cy,
            uint flags
        );

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr newParent);

        [DllImport("user32.dll")]
        public static extern IntPtr ShowWindow(IntPtr handle, int command);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongA")]
        public static extern long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongA")]
        public static extern long SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll")]
        public static extern long SetWindowLongA(IntPtr hWnd, int nIndex, long dwNewLong);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        // DWM constants
        public const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        public const int DWMWCP_DONOTROUND = 1;

        // Window style constants
        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;
        public const long WS_CAPTION = 0x00C00000L;
        public const long WS_THICKFRAME = 0x00040000L;
        public const long WS_MINIMIZE = 0x20000000L;
        public const long WS_MAXIMIZE = 0x01000000L;
        public const long WS_SYSMENU = 0x00080000L;
        public const long WS_BORDER = 0x00800000L;
        public const long WS_DLGFRAME = 0x00400000L;
        public const long WS_CHILD = 0x40000000L;
        public const long WS_VISIBLE = 0x10000000L;
        
        // Extended window styles
        public const long WS_EX_CLIENTEDGE = 0x00000200L;
        public const long WS_EX_WINDOWEDGE = 0x00000100L;
        public const long WS_EX_STATICEDGE = 0x00020000L;
        public const long WS_EX_DLGMODALFRAME = 0x00000001L;
        
        // SetWindowPos flags
        public const uint SWP_FRAMECHANGED = 0x0020;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOACTIVATE = 0x0010;

        #endregion
    }
