using Microsoft.VisualBasic.Logging;

namespace csharp_editor;

static class Program
{
    public static Editor? editor;

    [STAThread]
    static void Main() {

        Renderer.CallbackDelegate callback = (value) => {

            Log(value);
        };

        ApplicationConfiguration.Initialize();

        // ---

        editor = new Editor(callback);
        editor.Show();

        while (editor.active) {

            editor.Tick();
            editor.Render();

            Application.DoEvents();
        }

        Application.Exit();
    }    

    public static void Log(string message) {

        if (editor != null) {

            editor.Log(message);
        }
    }
}