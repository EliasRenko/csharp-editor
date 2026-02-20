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
