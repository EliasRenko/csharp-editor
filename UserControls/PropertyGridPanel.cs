using System.Reflection;
using System.Windows.Forms;

namespace csharp_editor.UserControls
{
        public partial class PropertyGridPanel : UserControl
        {
            private PropertyGrid propertyGrid;
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

        private void InitializeComponent() {
            propertyGrid = new PropertyGrid();
            SuspendLayout();
            // 
            // propertyGrid
            // 
            propertyGrid.Dock = DockStyle.Fill;
            propertyGrid.HelpVisible = false;
            propertyGrid.Location = new Point(0, 0);
            propertyGrid.Name = "propertyGrid";
            propertyGrid.PropertySort = PropertySort.NoSort;
            propertyGrid.Size = new Size(300, 400);
            propertyGrid.TabIndex = 0;
            propertyGrid.ToolbarVisible = false;
            // 
            // PropertyGridPanel
            // 
            Controls.Add(propertyGrid);
            Name = "PropertyGridPanel";
            Size = new Size(300, 400);
            ResumeLayout(false);
        }

        public PropertyGrid PropertyGrid => propertyGrid;
    }
}
