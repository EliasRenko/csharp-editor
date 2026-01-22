using System;
using System.Windows.Forms;

namespace csharp_editor
{
    static class Program
    {
        public static Editor editor;

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ---

            editor = new Editor();
            editor.Show();

            while (editor.active)
            {

                editor.Tick();
                editor.PreRender();
                editor.Render();
                editor.PostRender();

                Application.DoEvents();
            }

            Application.Exit();
        }
    }
}