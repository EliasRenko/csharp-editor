using System.Reflection;
using System.Windows.Forms;

namespace csharp_editor.UserControls
{
        public partial class PropertyGridPanel : UserControl
        {
            private System.Windows.Forms.PropertyGrid propertyGrid;
            private object _lastSelectedObject = null;

        public PropertyGridPanel()
        {
            InitializeComponent();
            propertyGrid.KeyDown += PropertyGrid_KeyDown;
            propertyGrid.SelectedObjectsChanged += PropertyGrid_SelectedObjectsChanged;
            // run once now and again after the native handle is created
            RemoveLeftIndent(propertyGrid);
            propertyGrid.HandleCreated += (s, e) => RemoveLeftIndent(propertyGrid);
        }

        private static void RemoveLeftIndent(PropertyGrid grid) {
            foreach (Control c in grid.Controls) {
                if (c.GetType().Name == "PropertyGridView") {
                    var fields = c.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    foreach (var f in fields) {
                        if (f.FieldType == typeof(int)) {
                            Console.WriteLine($"[PropertyGridView int field] {f.Name} = {f.GetValue(c)}");
                        }
                    }
                    break;
                }
            }
        }

        private void PropertyGrid_SelectedObjectsChanged(object sender, System.EventArgs e)
        {
            _lastSelectedObject = propertyGrid.SelectedObject;
        }

        private void PropertyGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                // Workaround: clear and restore SelectedObject to visually deselect
                if (propertyGrid.SelectedObject != null)
                {
                    var obj = propertyGrid.SelectedObject;
                    propertyGrid.SelectedObject = null;
                    propertyGrid.SelectedObject = obj;
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            propertyGrid = new System.Windows.Forms.PropertyGrid();
            SuspendLayout();
            // 
            // propertyGrid
            // 
            propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            propertyGrid.HelpVisible = false;
            propertyGrid.Location = new System.Drawing.Point(2, 2);
            propertyGrid.Margin = new System.Windows.Forms.Padding(0);
            propertyGrid.Name = "propertyGrid";
            propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            propertyGrid.Size = new System.Drawing.Size(296, 396);
            propertyGrid.TabIndex = 0;
            propertyGrid.ToolbarVisible = false;
            // 
            // PropertyGridPanel
            // 
            Controls.Add(propertyGrid);
            Padding = new System.Windows.Forms.Padding(2);
            Size = new System.Drawing.Size(300, 400);
            ResumeLayout(false);
        }

        public PropertyGrid PropertyGrid => propertyGrid;
    }
}
