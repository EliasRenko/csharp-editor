using WeifenLuo.WinFormsUI.Docking;

namespace csharp_editor.UserControls;

/// <summary>
/// Permanent document-area tab that hosts the Welcome / start screen.
/// It has no close button and cannot be floated or dragged out.
/// </summary>
public sealed class WelcomeDockContent : DockContent
{
    private readonly WelcomePanel _panel;
    public WelcomePanel Panel => _panel;

    public WelcomeDockContent(WelcomePanel panel)
    {
        _panel      = panel;
        _panel.Dock = DockStyle.Fill;
        Controls.Add(_panel);

        TabText = "Welcome";
        Text    = "Welcome";

        HideOnClose        = true;   // clicking × would only hide, but we hide the button anyway
        CloseButtonVisible = false;  // no × on this tab
        DockAreas          = DockAreas.Document;
    }
}
