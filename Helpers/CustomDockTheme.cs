using WeifenLuo.WinFormsUI.Docking;

namespace csharp_editor.Helpers
{
    /// <summary>
    /// VS2015 Light theme variant that hides the caption grip/drag-handle dots
    /// by setting their color equal to the caption background.
    /// </summary>
    internal sealed class CustomDockTheme : VS2015LightTheme
    {
        public CustomDockTheme() : base()
        {
            // The palette is fully populated by the base constructor.
            // Overwrite the Grip color so dots blend into the caption background.
            ColorPalette.ToolWindowCaptionActive.Grip =
                ColorPalette.ToolWindowCaptionActive.Background;
            ColorPalette.ToolWindowCaptionInactive.Grip =
                ColorPalette.ToolWindowCaptionInactive.Background;
        }
    }
}
