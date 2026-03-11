namespace csharp_editor.Helpers;

public struct ExternError {
    
    public string priority;
    public string category;
    public string message;

    public void SetError(string priority, string category, string message) {
        this.priority = priority;
        this.category = category;
        this.message = message;
    }
}