using System;
using System.Runtime.InteropServices;

namespace csharp_editor {
    public static class Externs
    {
        public const string DLL = "Editor-debug.dll";

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CallbackDelegate([MarshalAs(UnmanagedType.LPStr)] string message);

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
            public int tileSize;             // Size of each tile in pixels
            public int tilesPerRow;          // Number of tiles per row in atlas
            public int tilesPerCol;          // Number of tiles per column in atlas
            public int regionCount;          // Total number of tile regions
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct LayerInfoStruct {
            public IntPtr name;              // Layer name (use Marshal.PtrToStringAnsi to read)
            public int type;                 // Layer type (0 = TileLayer, 1 = EntityLayer)
            public IntPtr tilesetName;       // Tileset name for TileLayers (use Marshal.PtrToStringAnsi to read)
            public int visible;              // Visibility flag (0 = hidden, 1 = visible)
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
        }
        
        // Core functionality
        [DllImport(DLL, EntryPoint = "init")]
        public static extern int Init();
        
        [DllImport(DLL, EntryPoint = "initWithCallback")]
        public static extern int InitWithCallback(CallbackDelegate callback);
        
        [DllImport(DLL, EntryPoint = "release")]
        public static extern void Release();
        
        [DllImport(DLL, EntryPoint = "updateFrame")]
        public static extern void UpdateFrame(float deltaTime);

        [DllImport(DLL, EntryPoint = "preRender")]
        public static extern void PreRender();

        [DllImport(DLL, EntryPoint = "render")]
        public static extern void Render();

        [DllImport(DLL, EntryPoint = "postRender")]
        public static extern void PostRender();

        [DllImport(DLL, EntryPoint = "swapBuffers")]
        public static extern void SwapBuffers();
        
        // Resources
        [DllImport(DLL, EntryPoint = "loadTexture", CharSet = CharSet.Ansi)]
        public static extern void LoadTexture(string filepath, int tileSize, string id);

        // --- States

        [DllImport(DLL, EntryPoint = "loadState")]
        public static extern void LoadState(int id);
        
        // Window
        #region Window
        
        [DllImport(DLL, EntryPoint = "getWindowHandle")]
        public static extern IntPtr GetWindowHandle();

        [DllImport(DLL, EntryPoint = "setWindowPosition")]
        public static extern void SetWindowPosition(int x, int y);

        [DllImport(DLL, EntryPoint = "setWindowSize")]
        public static extern void SetWindowSize(int width, int height);

        #endregion
        
        // Input
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

        #endregion
        
        #region Textures
        
        [DllImport(DLL, EntryPoint = "getTextureData", CharSet = CharSet.Ansi)]
        public static extern void GetTextureData(string path, out TextureDataStruct outData);
        
        #endregion

        #region Tilesets
        
        [DllImport(DLL, EntryPoint = "getTileset", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTileset(string tilesetName, out TilesetInfoStruct outInfo);
        
        [DllImport(DLL, EntryPoint = "getTilesetAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTilesetAt(int index, out TilesetInfoStruct outInfo);
        
        [DllImport(DLL, EntryPoint = "getTilesetCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTilesetCount();
        
        [DllImport(DLL, EntryPoint = "setTileset", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetTileset(string texturePath, string name, int tileSize);
        
        [DllImport(DLL, EntryPoint = "setActiveTileset", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SetActiveTileset(string tilesetName);
        
        [DllImport(DLL, EntryPoint = "setActiveTile")]
        public static extern int SetActiveTile(int tileRegionId);
        
        #endregion
        
        [DllImport(DLL, EntryPoint = "exportMap")]
        public static extern int ExportMap(string path);
        
        [DllImport(DLL, EntryPoint = "importMap")]
        public static extern int ImportMap(string path);
        
        // Layer Management
        
        [DllImport(DLL, EntryPoint = "createTilemapLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CreateTilemapLayer(string layerName, string tilesetName, int index);
        
        [DllImport(DLL, EntryPoint = "createEntityLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CreateEntityLayer(string layerName, string tilesetName);
        
        [DllImport(DLL, EntryPoint = "createFolderLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CreateFolderLayer(string layerName);
        
        // --
        
        [DllImport(DLL, EntryPoint = "setActiveLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int SetActiveLayer(string layerName);
        
        [DllImport(DLL, EntryPoint = "setActiveLayerAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetActiveLayerAt(int index);
        
        [DllImport(DLL, EntryPoint = "removeLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int RemoveLayer(string layerName);
        
        [DllImport(DLL, EntryPoint = "removeLayerByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RemoveLayerByIndex(int index);
        
        [DllImport(DLL, EntryPoint = "getLayerCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLayerCount();
        
        [DllImport(DLL, EntryPoint = "getLayerInfoAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLayerInfoAt(int index, out LayerInfoStruct outInfo);
        
        [DllImport(DLL, EntryPoint = "getLayerInfo", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetLayerInfo(string layerName, out LayerInfoStruct outInfo);
        
        // Entity Management
        
        [DllImport(DLL, EntryPoint = "getEntity", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void GetEntity(string entityName, out EntityDataStruct outData);
        
        [DllImport(DLL, EntryPoint = "getEntityAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetEntityAt(int index, out EntityDataStruct outData);
        
        [DllImport(DLL, EntryPoint = "getEntityCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityCount();
        
        [DllImport(DLL, EntryPoint = "setEntity", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void SetEntity(string entityName, int width, int height, string tilesetName);
        
        [DllImport(DLL, EntryPoint = "setEntityRegion", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void SetEntityRegion(string entityName, int x, int y, int width, int height);
        
        [DllImport(DLL, EntryPoint = "removeEntity", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int RemoveEntity(string entityName);
        
        [DllImport(DLL, EntryPoint = "setActiveEntity", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int SetActiveEntity(string entityName);
        
        [DllImport(DLL, EntryPoint = "placeEntity", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PlaceEntity(int x, int y);
        
        [DllImport(DLL, EntryPoint = "moveLayerUp", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int MoveLayerUp(string layerName);
        
        [DllImport(DLL, EntryPoint = "moveLayerDown", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int MoveLayerDown(string layerName);
        
        [DllImport(DLL, EntryPoint = "moveLayerUpByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MoveLayerUpByIndex(int index);
        
        [DllImport(DLL, EntryPoint = "moveLayerDownByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MoveLayerDownByIndex(int index);
        
        // BMFG

        [DllImport(DLL, EntryPoint = "importFont", CharSet = CharSet.Ansi)]
        public static extern void ImportFont(string fontPath, float fontSize);

        [DllImport(DLL, EntryPoint = "exportFont", CharSet = CharSet.Ansi)]
        public static extern void ExportFont(string fontPath);

        [DllImport(DLL, EntryPoint = "loadFont", CharSet = CharSet.Ansi)]
        public static extern void LoadFont(string outputName);

        [DllImport(DLL, EntryPoint = "rebakeFont")]
        public static extern void RebakeFont(float fontSize, int atlasWidth, int atlasHeight, int firstChar, int numChars);

        #region WinAPI Entry Points

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
}
