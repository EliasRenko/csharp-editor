using Microsoft.VisualBasic.Logging;

namespace csharp_editor;

static class Program
{
    public static Form1? form1;

    public static bool isRunning = true;

    [STAThread]
    static void Main() {

        Renderer.CallbackDelegate callback = (value) => {

            Log(value);
        };

        ApplicationConfiguration.Initialize();

        // ---

        form1 = new Form1(callback);
        form1.Show();

        while (isRunning) {

            form1.Tick();
            form1.Render();

            Application.DoEvents();
        }

        Application.Exit();
    }    

    public static void Log(string message) {

        if (form1 != null) {

            form1.Log(message);
        }
    }
}