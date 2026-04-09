using System.Windows.Forms;

namespace csharp_editor.Models {
    public class LayerNode {
        public string Name { get; set; } = "";
        public LayerType Type { get; set; } = LayerType.TileLayer;
        public bool Visible { get; set; } = true;
        public bool Locked { get; set; } = false;
        public string TilesetName { get; set; } = ""; // For TileLayer only
        public int TileSize { get; set; } = 0;        // For TileLayer only
        public TreeNode TreeNodeRef { get; set; } = null!; // assigned when node created

        public override string ToString() {
            return Name;
        }
    }
}
