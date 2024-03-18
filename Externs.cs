using System;
using System.Runtime.InteropServices;

namespace csharp_editor;

public partial class Externs
{
    [DllImport("main.dll", EntryPoint = "trace")]
    public static extern void Trace();

    [DllImport("main.dll", EntryPoint = "init")]
    public static extern void Init();

    // [DllImport("main.dll", EntryPoint = "init")]
    // public static extern void Init();

    // [DllImport("main.dll", EntryPoint = "setLogDispacher")]
    // public static extern void SetLogDispacher(Func<string, void> f);

    [DllImport("main.dll", EntryPoint = "getWindowHandle")]
    public static extern IntPtr GetWindowHandle();

    [DllImport("main.dll", EntryPoint = "runLoop")]
    public static extern IntPtr RunLoop();

    [DllImport("user32.dll")]
    public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll")]
	public static extern IntPtr ShowWindow(IntPtr handle, int command);

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

    [DllImport("ConsoleApplication1.dll", EntryPoint = "main")]
	public static extern void main();
}