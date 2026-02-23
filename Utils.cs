using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_editor {
    internal static class Utils {

        public static string OpenFile(string startingPath) {
            using (var dialog = new OpenFileDialog()) {
                dialog.Filter = "All Files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.Multiselect = false;
                
                if (!string.IsNullOrEmpty(startingPath)) {
                    dialog.InitialDirectory = startingPath;
                }

                if (dialog.ShowDialog() == DialogResult.OK) {
                    return dialog.FileName;
                }
            }

            return string.Empty;
        }
        public static async Task<bool> SaveFileAsync(string startingPath, string data, string name, string exten) {
            try {
                using (var dialog = new SaveFileDialog()) {
                    // Set up file filter based on extension
                    dialog.Filter = $"{exten.ToUpper()} Files (*.{exten})|*.{exten}|All Files (*.*)|*.*";
                    dialog.FilterIndex = 1;
                    dialog.InitialDirectory = startingPath;
                    dialog.FileName = name;
                    dialog.DefaultExt = exten;
                    dialog.AddExtension = true;

                    if (dialog.ShowDialog() == DialogResult.OK) {
                        // Write the data to the selected file asynchronously
                        await File.WriteAllTextAsync(dialog.FileName, data);
                        return true;
                    }

                    return false; // User canceled
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error saving file: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Keep synchronous version for compatibility
        public static bool SaveFile(string startingPath, string data, string name, string exten) {
            return SaveFileAsync(startingPath, data, name, exten).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Converts a 0xRRGGBBAA RGBA integer (backend format) into a <see cref="System.Drawing.Color"/>.
        /// </summary>
        public static System.Drawing.Color ConvertFromRGBA(int rgba)
        {
            byte r = (byte)((rgba >> 24) & 0xFF);
            byte g = (byte)((rgba >> 16) & 0xFF);
            byte b = (byte)((rgba >> 8) & 0xFF);
            byte a = (byte)(rgba & 0xFF);
            return System.Drawing.Color.FromArgb(a, r, g, b);
        }
    }
}
