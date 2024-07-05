using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace csharp_editor
{
    public class Renderer
    {
        public delegate void CallbackDelegate(string result);

        [DllImport("libRenderer.dll", EntryPoint = "init")]
        public static extern void Init();

        [DllImport("libRenderer.dll", EntryPoint = "release")]
        public static extern void Release();

        [DllImport("libRenderer.dll", EntryPoint = "initWithCallback")]
        public static extern void InitWithCallback(CallbackDelegate callback);

        [DllImport("libRenderer.dll", EntryPoint = "render")]
        public static extern void Render();

        [DllImport("libRenderer.dll", EntryPoint = "update")]
        public static extern void Update();

        [DllImport("libRenderer.dll", EntryPoint = "getHandle")]
        public static extern IntPtr GetHandle();

        [DllImport("libRenderer.dll", EntryPoint = "createWindowFrom")]
        public static extern void CreateWindowFrom(IntPtr handle);

        [DllImport("libRenderer.dll", EntryPoint = "Add", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Add(int a, int b, string input, CallbackDelegate callback);

        [DllImport("libRenderer.dll", EntryPoint = "setLogDispacher", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void SetLogDispacher(CallbackDelegate callback);

        [DllImport("libRenderer.dll", EntryPoint = "onMouseClick")]
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
