using System.Windows.Forms;

namespace csharp_editor.UserControls
{
    public partial class PropertyGridPanel : UserControl
    {
        private PropertyGrid propertyGrid;

        public PropertyGridPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.propertyGrid = new PropertyGrid();
            this.SuspendLayout();
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(300, 400);
            this.propertyGrid.TabIndex = 0;
            // 
            // PropertyGridPanel
            // 
            this.Controls.Add(this.propertyGrid);
            this.Name = "PropertyGridPanel";
            this.Size = new System.Drawing.Size(300, 400);
            this.ResumeLayout(false);
        }

        public PropertyGrid PropertyGrid => propertyGrid;
    }
}
