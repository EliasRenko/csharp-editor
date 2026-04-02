using WeifenLuo.WinFormsUI.Docking;

namespace csharp_editor.UserControls;

/// <summary>Dockable wrapper for the Hierarchy / Texture / Entity right panel.</summary>
public sealed class HierarchyTreeDockContent : DockContent
{
    public HierarchyTreeDockContent(HierarchyTree tree, TextureViewer textureViewer, EntitySelector entitySelector)
    {
        // Preserve same stacking order as the original panelRight:
        //   entitySelector & textureViewer (Fill) added first → lower Z-order
        //   hierarchyTree (Top) added last  → docked first by layout engine
        Controls.Add(entitySelector);
        Controls.Add(textureViewer);
        Controls.Add(tree);

        Padding = new System.Windows.Forms.Padding(4);

        TabText     = "Hierarchy";
        Text        = "Hierarchy";
        HideOnClose = true;
        DockAreas   = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float;
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // HierarchyTreeDockContent
        // 
        ClientSize = new System.Drawing.Size(284, 261);
        ResumeLayout(false);
    }
}
