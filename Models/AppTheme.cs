namespace csharp_editor.Models {

    /// <summary>
    /// Stores all named color slots for the application's visual theme.
    /// Colors are persisted as "#AARRGGBB" hex strings.
    /// </summary>
    public class AppTheme {

        // ── Renderer ────────────────────────────────────────────────────────

        /// <summary>Viewport clear / background color.</summary>
        public string ViewportBackground { get; set; } = "#FF2D2D30";

        /// <summary>Grid line color drawn over the canvas.</summary>
        public string GridColor          { get; set; } = "#40808080";

        /// <summary>Fill color of the tile/region selection rectangle.</summary>
        public string SelectionFill      { get; set; } = "#400E639C";

        /// <summary>Border color of the tile/region selection rectangle.</summary>
        public string SelectionBorder    { get; set; } = "#FF0E99FF";

        /// <summary>Hover cursor highlight drawn under the active tile cursor.</summary>
        public string CursorHighlight    { get; set; } = "#60FFFFFF";

        /// <summary>Default map background color (behind all layers).</summary>
        public string MapBackground      { get; set; } = "#FF1E1E1E";

        // ── User Interface ───────────────────────────────────────────────────

        /// <summary>Main window / dialog background.</summary>
        public string WindowBackground  { get; set; } = "#FF2D2D2D";

        /// <summary>Secondary panel / dock header background.</summary>
        public string PanelBackground   { get; set; } = "#FF252526";

        /// <summary>Background of text inputs, combo boxes, numerics.</summary>
        public string InputBackground   { get; set; } = "#FF3C3C3C";

        /// <summary>Text color inside inputs.</summary>
        public string InputForeground   { get; set; } = "#FFD4D4D4";

        /// <summary>Primary label / body text color.</summary>
        public string TextPrimary       { get; set; } = "#FFD4D4D4";

        /// <summary>Muted / hint text color.</summary>
        public string TextMuted         { get; set; } = "#FF999999";

        /// <summary>Accent button background (e.g. "OK", "Add").</summary>
        public string AccentBackground  { get; set; } = "#FF0E639C";

        /// <summary>Accent button text color.</summary>
        public string AccentForeground  { get; set; } = "#FFFFFFFF";

        /// <summary>Control borders, dividers, and separators.</summary>
        public string BorderColor       { get; set; } = "#FF3F3F46";

        // ── Helpers ──────────────────────────────────────────────────────────

        /// <summary>Returns a fresh theme instance using all default values.</summary>
        public static AppTheme Default => new();

        /// <summary>
        /// Parses a "#AARRGGBB" or "#RRGGBB" string into a <see cref="System.Drawing.Color"/>.
        /// Returns <see cref="System.Drawing.Color.Magenta"/> on parse failure as a visible error indicator.
        /// </summary>
        public static System.Drawing.Color ParseColor(string hexArgb) {
            try {
                var s = hexArgb.TrimStart('#');
                if (s.Length == 6) s = "FF" + s;
                if (s.Length != 8) return System.Drawing.Color.Magenta;
                int val = Convert.ToInt32(s, 16);
                return System.Drawing.Color.FromArgb(val);
            } catch {
                return System.Drawing.Color.Magenta;
            }
        }

        /// <summary>Formats a <see cref="System.Drawing.Color"/> as "#AARRGGBB".</summary>
        public static string FormatColor(System.Drawing.Color c) =>
            $"#{c.A:X2}{c.R:X2}{c.G:X2}{c.B:X2}";
    }
}
