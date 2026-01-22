using System;
using System.Runtime.InteropServices;

namespace csharp_editor
{
    public class Renderer
    {
        public const string DLL = "Main-debug.dll";

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CallbackDelegate([MarshalAs(UnmanagedType.LPStr)] string message);

        [DllImport(DLL, EntryPoint = "init")]
        public static extern int Init();

        //[DllImport(DLL, EntryPoint = "initWithCallback", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int InitWithCallback(CallbackDelegate callback);

        [DllImport(DLL, EntryPoint = "loadTexture", CharSet = CharSet.Ansi)]
        public static extern void LoadTexture(string filepath, int tileSize, string id);

        [DllImport(DLL, EntryPoint = "release")]
        public static extern void Release();

        [DllImport(DLL, EntryPoint = "initWithCallback")]
        public static extern int InitWithCallback(CallbackDelegate callback);

        [DllImport(DLL, EntryPoint = "updateFrame")]
        public static extern void UpdateFrame();

        [DllImport(DLL, EntryPoint = "preRender")]
        public static extern void PreRender();

        [DllImport(DLL, EntryPoint = "render")]
        public static extern void Render();

        [DllImport(DLL, EntryPoint = "postRender")]
        public static extern void PostRender();

        // --- States

        [DllImport(DLL, EntryPoint = "loadState")]
        public static extern void LoadState(int id);

        // ---

        [DllImport(DLL, EntryPoint = "addEntity")]
        public static extern void AddEntity(int id);

        [DllImport(DLL, EntryPoint = "selectEntity")]
        public static extern void SelectEntity(int id);

        [DllImport(DLL, EntryPoint = "deselectEntity")]
        public static extern void DeselectEntity();

        [DllImport(DLL, EntryPoint = "updateEntity")]
        public static extern void UpdateEntity(int id, int x, int y);

        [DllImport(DLL, EntryPoint = "updateMap")]
        public static extern void UpdateMap(string hex);

        [DllImport(DLL, EntryPoint = "update")]
        public static extern void Update();


        // Window
        [DllImport(DLL, EntryPoint = "getWindowHandle")]
        public static extern IntPtr GetWindowHandle();

        [DllImport(DLL, EntryPoint = "createWindowFrom")]
        public static extern void CreateWindowFrom(IntPtr handle);

        [DllImport(DLL, EntryPoint = "setWindowPosition")]
        public static extern void SetWindowPosition(int x, int y);

        [DllImport(DLL, EntryPoint = "Add", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Add(int a, int b, string input, CallbackDelegate callback);

        [DllImport(DLL, EntryPoint = "setLogDispacher", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void SetLogDispacher(CallbackDelegate callback);

        [DllImport(DLL, EntryPoint = "onMouseClick")]
        public static extern void OnMouseClick(int x, int y);

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
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        public static extern long SetWindowLongA(IntPtr hWnd, int nIndex, long dwNewLong);

        #endregion
    }
}
