using System;
using System.Windows.Forms;

namespace csharp_editor {
    public class Utils {

        public static string OpenFile(String startingPath)
        {
            string sFileName = "";

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "All Files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK) {

                sFileName = dialog.FileName;
                string[] arrAllFiles = dialog.FileNames; //used when Multiselect = true           
            }

            return sFileName;
        }
        public static bool SaveFile(string startingPath, string data, string name, string exten)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();

                // Set up file filter based on extension
                dialog.Filter = $"{exten.ToUpper()} Files (*.{exten})|*.{exten}|All Files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.InitialDirectory = startingPath;
                dialog.FileName = name;
                dialog.DefaultExt = exten;
                dialog.AddExtension = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string sFileName = dialog.FileName;

                    // Write the data to the selected file
                    System.IO.File.WriteAllText(sFileName, data);

                    return true;
                }

                return false; // User canceled
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static String ToHex(System.Drawing.Color c)
        => $"{c.R:X2}{c.G:X2}{c.B:X2}";
    }
}
