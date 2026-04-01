using WeifenLuo.WinFormsUI.Docking;

namespace csharp_editor.UserControls;

/// <summary>Dockable wrapper for the Properties panel.</summary>
public sealed class PropertyGridDockContent : DockContent
{
    public PropertyGridDockContent(PropertyGridPanel panel)
    {
        panel.Dock = DockStyle.Fill;
        Controls.Add(panel);

        TabText     = "Properties";
        Text        = "Properties";
        HideOnClose = true;
        DockAreas   = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float;
    }
}
