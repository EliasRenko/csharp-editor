using System.Runtime.InteropServices;
using NativeHaxeRuntime;

namespace csharp_editor.Dialogs {

    /// <summary>
    /// Lists all tilesets registered in the current project and lets the user pick one.
    /// </summary>
    public partial class TilesetSelectionDialog : Form {

        /// <summary>
        /// The tileset name chosen by the user, or <see langword="null"/> if the dialog
        /// was cancelled or the list is empty.
        /// </summary>
        public string? SelectedTileset => listBox.SelectedItem?.ToString();

        public TilesetSelectionDialog() {
            InitializeComponent();
            Load += TilesetSelectionDialog_Load;
        }

        private void TilesetSelectionDialog_Load(object? sender, EventArgs e) {
            int count = CExternsEditor.GetTilesetCount();
            for (int i = 0; i < count; i++) {
                CExternsEditor.TilesetInfoStruct info = new CExternsEditor.TilesetInfoStruct();
                if (CExternsEditor.GetTilesetAt(i, out info)) {
                    string name = Marshal.PtrToStringAnsi(info.name) ?? "";
                    if (!string.IsNullOrEmpty(name))
                        listBox.Items.Add(name);
                }
            }
            if (listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;
        }
    }
}
