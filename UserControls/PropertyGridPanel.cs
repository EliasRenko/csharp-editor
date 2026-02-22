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

        private void InitializeComponent()
        {
            propertyGrid = new System.Windows.Forms.PropertyGrid();
            SuspendLayout();
            // 
            // propertyGrid
            // 
            propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            propertyGrid.Location = new System.Drawing.Point(0, 0);
            propertyGrid.Name = "propertyGrid";
            propertyGrid.Size = new System.Drawing.Size(300, 400);
            propertyGrid.TabIndex = 0;
            // 
            // PropertyGridPanel
            // 
            Controls.Add(propertyGrid);
            Size = new System.Drawing.Size(300, 400);
            ResumeLayout(false);
        }

        public PropertyGrid PropertyGrid => propertyGrid;
    }
}
