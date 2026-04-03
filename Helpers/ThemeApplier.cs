using System.Runtime.Versioning;
using csharp_editor.Models;

namespace csharp_editor.Helpers {

    /// <summary>
    /// Recursively applies <see cref="AppTheme"/> colors to a WinForms control tree.
    /// Controls that manage their own appearance (UserControls, ToolStrips, DockPanels, etc.)
    /// are skipped to avoid breaking custom-painted or third-party controls.
    /// <para>
    /// To opt a <see cref="System.Windows.Forms.Button"/> into accent styling, set its
    /// <c>Tag = "accent"</c>. To opt a <see cref="System.Windows.Forms.Panel"/> into the
    /// secondary panel background, set its <c>Tag = "panel"</c>.
    /// To suppress a subtree entirely, set the root control's <c>Tag = "skip-theme"</c>.
    /// </para>
    /// </summary>
    [SupportedOSPlatform("windows")]
    public static class ThemeApplier {

        // Control type names that should not be recursed into
        private static readonly HashSet<string> _skipTypes = new(StringComparer.Ordinal) {
            "ExternView",
            "DockPanel",
            "MenuStrip",
            "ToolStrip",
            "StatusStrip",
            "ToolStripDropDownMenu",
            "ToolStripPanel",
            "ToolStripOverflowButton",
        };

        /// <summary>
        /// Applies <paramref name="theme"/> colors to <paramref name="root"/> and
        /// all its accessible child controls.
        /// </summary>
        public static void Apply(Control root, AppTheme theme) =>
            ApplyRecursive(root, theme, isRoot: true);

        private static void ApplyRecursive(Control c, AppTheme theme, bool isRoot = false) {
            // Honor explicit opt-out
            if (!isRoot && string.Equals(c.Tag as string, "skip-theme", StringComparison.Ordinal))
                return;

            // Skip third-party / custom-painted control subtrees
            if (!isRoot && _skipTypes.Contains(c.GetType().Name))
                return;

            // Skip UserControls — they manage their own appearance
            if (!isRoot && c is UserControl)
                return;

            switch (c) {
                case Form:
                    c.BackColor = AppTheme.ParseColor(theme.WindowBackground);
                    break;

                case Panel p when string.Equals(p.Tag as string, "panel", StringComparison.Ordinal):
                    p.BackColor = AppTheme.ParseColor(theme.PanelBackground);
                    break;

                case Panel:
                    c.BackColor = AppTheme.ParseColor(theme.WindowBackground);
                    break;

                case TextBox tb:
                    tb.BackColor = AppTheme.ParseColor(theme.InputBackground);
                    tb.ForeColor = AppTheme.ParseColor(theme.InputForeground);
                    break;

                case ComboBox cb:
                    cb.BackColor = AppTheme.ParseColor(theme.InputBackground);
                    cb.ForeColor = AppTheme.ParseColor(theme.InputForeground);
                    break;

                case NumericUpDown n:
                    n.BackColor = AppTheme.ParseColor(theme.InputBackground);
                    n.ForeColor = AppTheme.ParseColor(theme.InputForeground);
                    break;

                case Label lbl when string.Equals(lbl.Tag as string, "muted", StringComparison.Ordinal):
                    lbl.ForeColor = AppTheme.ParseColor(theme.TextMuted);
                    break;

                case Label lbl:
                    lbl.ForeColor = AppTheme.ParseColor(theme.TextPrimary);
                    break;

                case Button btn when string.Equals(btn.Tag as string, "accent", StringComparison.Ordinal):
                    btn.BackColor = AppTheme.ParseColor(theme.AccentBackground);
                    btn.ForeColor = AppTheme.ParseColor(theme.AccentForeground);
                    break;
            }

            foreach (Control child in c.Controls)
                ApplyRecursive(child, theme);
        }
    }
}
