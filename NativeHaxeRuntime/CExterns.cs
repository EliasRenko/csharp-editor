using System.Runtime.InteropServices;

namespace NativeHaxeRuntime;

public class CExterns {
    
    public const string DLL = "Editor-debug.dll";
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CallbackDelegate([MarshalAs(UnmanagedType.LPStr)] string priority, [MarshalAs(UnmanagedType.LPStr)] string category, [MarshalAs(UnmanagedType.LPStr)] string message);
    
    #region Lifecycle functions

    [DllImport(DLL, EntryPoint = "init")]
    public static extern bool Init();
        
    [DllImport(DLL, EntryPoint = "initWithCallback")]
    public static extern bool InitWithCallback(CallbackDelegate callback);
        
    [DllImport(DLL, EntryPoint = "release")]
    public static extern void Release();

    [DllImport(DLL, EntryPoint = "isRunning")]
    public static extern bool IsRunning();

    [DllImport(DLL, EntryPoint = "updateFrame")]
    public static extern void UpdateFrame(float deltaTime);

    [DllImport(DLL, EntryPoint = "render")]
    public static extern void Render();

    [DllImport(DLL, EntryPoint = "swapBuffers")]
    public static extern void SwapBuffers();

    #endregion
}