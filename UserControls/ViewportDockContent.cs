using WeifenLuo.WinFormsUI.Docking;
using csharp_editor.UserControls;

namespace csharp_editor.UserControls;

/// <summary>
/// Off-screen holder for the shared ExternView.  It is created once (via
/// CreateControl) so that the native SDL window gets a valid parent HWND,
/// but it is never shown inside the DockPanel.  The ExternView (and tool
/// buttons) are reparented into whichever MapDocContent is currently active.
/// </summary>
public sealed class ViewportDockContent : DockContent
{
    public ViewportDockContent(ExternView externView)
    {
        externView.Dock = DockStyle.Fill;
        Controls.Add(externView);

        TabText            = "Viewport";
        Text               = "Viewport";
        HideOnClose        = true;
        CloseButtonVisible = false;
        DockAreas          = DockAreas.Document; // no float → no drag-out
    }
}
