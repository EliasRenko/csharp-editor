using System.Windows.Forms;
using static NativeHaxeRuntime.CExterns;

namespace NativeHaxeRuntime;

public class Runtime : Form {
    
    public bool Active {
        get => active;
    }
    
    protected bool active = false;
    protected CallbackDelegate logHandler;
    protected ExternError lastError;
    
    public Runtime() {
        logHandler = LogHandler;
    }

    public virtual void UpdateFrame(float deltaTime) {
        CExterns.UpdateFrame(deltaTime);
    }

    public virtual void Render() {
        CExterns.Render();
    }

    public virtual void SwapBuffers() {
        CExterns.SwapBuffers();
    }

    protected virtual void Log(string priority, string category, string message) {
        // Override in derived class to log to UI
    }

    protected string GetLastErrorMessage() {
        if (!string.IsNullOrWhiteSpace(lastError.message)) {
            return $"{lastError.priority} - {lastError.category} - {lastError.message}";
        }
        return "Unknown native error.";
    }

    private void LogHandler(string priority, string category, string message) {
        lastError.SetError(priority, category, message);
        Log(priority, category, message);
    }
}