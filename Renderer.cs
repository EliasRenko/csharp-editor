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
        public delegate void CallbackDelegate([MarshalAs(UnmanagedType.BStr)]string result);

        [DllImport("libRenderer.dll", EntryPoint = "init")]
        public static extern void Init(CallbackDelegate callback);

        [DllImport("libRenderer.dll", EntryPoint = "draw")]
        public static extern void Draw();

        [DllImport("libRenderer.dll", EntryPoint = "getHandle")]
        public static extern IntPtr GetHandle();

        [DllImport("libRenderer.dll", EntryPoint = "Add", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Add(int a, int b, string input, CallbackDelegate callback);

        [DllImport("libRenderer.dll", EntryPoint = "setLogDispacher", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void SetLogDispacher(CallbackDelegate callback);

        #region WinAPI Entry Points

        [DllImport("user32.dll")]
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

        #endregion
    }
}
