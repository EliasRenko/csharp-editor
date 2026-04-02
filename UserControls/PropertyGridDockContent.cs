using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;

namespace csharp_editor.UserControls;

/// <summary>Dockable wrapper for the Properties panel.</summary>
public sealed class PropertyGridDockContent : DockContent
{
    private System.Windows.Forms.PropertyGrid propertyGrid;
    private object? _lastSelectedObject = null;

    public PropertyGridDockContent()
    {
        propertyGrid = new System.Windows.Forms.PropertyGrid();
        propertyGrid.Dock = DockStyle.Fill;
        propertyGrid.HelpVisible = false;
        propertyGrid.Margin = new System.Windows.Forms.Padding(0);
        propertyGrid.Name = "propertyGrid";
        propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
        propertyGrid.TabIndex = 0;
        propertyGrid.ToolbarVisible = false;
        //Padding = new System.Windows.Forms.Padding(5);
        Controls.Add(propertyGrid);

        propertyGrid.KeyDown += PropertyGrid_KeyDown;
        propertyGrid.SelectedObjectsChanged += PropertyGrid_SelectedObjectsChanged;
        // run once now and again after the native handle is created
        RemoveLeftIndent(propertyGrid);
        propertyGrid.HandleCreated += (s, e) => RemoveLeftIndent(propertyGrid);

        TabText     = "Properties";
        Text        = "Properties";
        HideOnClose = true;
        DockAreas   = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float;
    }

    private static void RemoveLeftIndent(System.Windows.Forms.PropertyGrid grid)
    {
        foreach (Control c in grid.Controls)
        {
            if (c.GetType().Name == "PropertyGridView")
            {
                var fields = c.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                foreach (var f in fields)
                {
                    if (f.FieldType == typeof(int))
                        Console.WriteLine($"[PropertyGridView int field] {f.Name} = {f.GetValue(c)}");
                }
                break;
            }
        }
    }

    private void PropertyGrid_SelectedObjectsChanged(object? sender, System.EventArgs e)
    {
        _lastSelectedObject = propertyGrid.SelectedObject;
    }

    private void PropertyGrid_KeyDown(object? sender, KeyEventArgs e)
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

    public System.Windows.Forms.PropertyGrid PropertyGrid => propertyGrid;
}
