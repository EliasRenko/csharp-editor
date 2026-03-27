using System.Windows.Forms;
using static NativeHaxeRuntime.CExterns;

namespace NativeHaxeRuntime;

public class Runtime : Form {
    
    public bool Active {
        get => active;
    }
    
    protected bool active = false;
    protected CallbackDelegate logHandler;
    
    private ExternError lastError;
    
    public Runtime() {
        logHandler = LogHandler;
    }

    protected virtual void Log(string text) {
        
    }

    private void LogHandler(string priority, string category, string message) {
        lastError.SetError(priority, category, message);
        Log($"{priority} - {category} - {message}");
    }
}