using WeifenLuo.WinFormsUI.Docking;

namespace csharp_editor.UserControls;

/// <summary>
/// A dockable wrapper around <see cref="DebugConsole"/> that integrates with DockPanel Suite.
/// </summary>
public sealed class DebugConsoleDockContent : DockContent
{
    private readonly DebugConsole _console;

    public DebugConsoleDockContent()
    {
        _console = new DebugConsole();
        _console.Dock = DockStyle.Fill;
        Controls.Add(_console);

        TabText     = "Console";
        Text        = "Console";
        HideOnClose = true; // hide instead of disposing when user clicks ×
        DockAreas   = DockAreas.DockBottom
                    | DockAreas.DockTop
                    | DockAreas.DockLeft
                    | DockAreas.DockRight
                    | DockAreas.Float;
    }

    /// <summary>Forwards a log message to the inner console.</summary>
    public void Log(string message) => _console.Log(message);
}
