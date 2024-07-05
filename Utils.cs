using System;

namespace csharp_editor {
    public class Utils {

        public static string OpenFile(String startingPath) {

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
    }
}
