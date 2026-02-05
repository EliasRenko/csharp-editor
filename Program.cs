using System.Diagnostics;

namespace csharp_bmfg {
    static class Program {
        private static Editor? editor;

        [STAThread]
        static void Main() {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Basic editor form
            editor = new Editor();
            editor.Show();

            // Delta time calculation
            Stopwatch stopwatch = Stopwatch.StartNew();
            double lastTime = 0.0;

            while (editor.active == true) {
                Application.DoEvents();

                double currentTime = stopwatch.Elapsed.TotalSeconds;
                float deltaTime = (float)(currentTime - lastTime);
                lastTime = currentTime;

                // Update
                editor.UpdateFrame(deltaTime);

                // Rendering
                editor.PreRender();
                editor.Render();
                editor.SwapBuffers();
            }

            Application.Exit();
        }
    }
}