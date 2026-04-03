using System.Runtime.Versioning;
using csharp_editor.Helpers;
using csharp_editor.Models;

namespace csharp_editor.Dialogs {

    /// <summary>
    /// Dialog that lets the user assign colors to all named application theme slots.
    /// Organized in a 3-column grid with "Renderer" and "User Interface" sections.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class ThemeDialog : Form {

        // ── State ─────────────────────────────────────────────────────────────
        private AppTheme _working;
        private readonly AppTheme _original;
        private readonly Dictionary<string, (TextBox Hex, Button Swatch)> _slots = new();

        // ── Palette — shared with Designer.cs ─────────────────────────────────
        internal static readonly Color ColorBg      = Color.FromArgb(45, 45, 48);
        internal static readonly Color ColorBgPanel = Color.FromArgb(37, 37, 38);
        internal static readonly Color ColorBgInput = Color.FromArgb(60, 60, 60);
        internal static readonly Color ColorFg      = Color.FromArgb(212, 212, 212);
        internal static readonly Color ColorFgMuted = Color.FromArgb(153, 153, 153);
        internal static readonly Color ColorAccent  = Color.FromArgb(14, 99, 156);
        internal static readonly Color ColorNeutral = Color.FromArgb(62, 62, 66);
        internal static readonly Color ColorDivider = Color.FromArgb(63, 63, 70);

        internal static readonly Font UiFont  = new("Segoe UI", 9f);
        internal static readonly Font LblFont = new("Segoe UI", 8f);
        internal static readonly Font SecFont = new("Segoe UI", 8.5f, FontStyle.Bold);
        internal static readonly Font HexFont = new("Consolas", 8.5f);

        // ── Layout constants ──────────────────────────────────────────────────
        private const int FormW   = 576;
        private const int PadX   = 16;
        private const int ColGap  = 8;
        private const int SlotW   = (FormW - PadX * 2 - ColGap * 2) / 3; // 174
        private const int SlotH   = 52;
        private const int RowGap  = 10;
        private const int SecH    = 22;
        private const int SecGap  = 6;
        private const int SwatchW = 28;
        private const int HexW    = SlotW - SwatchW - 4;

        // ── Constructor ───────────────────────────────────────────────────────

        public ThemeDialog() {
            _original = AppThemeManager.Clone(AppThemeManager.Current);
            _working  = AppThemeManager.Clone(_original);
            InitializeComponent();
            BuildSlots();
        }

        // ── Slot building ─────────────────────────────────────────────────────

        private void BuildSlots() {
            int y = PadX;

            var rendererSlots = new (string Prop, string Label)[] {
                ("ViewportBackground", "Viewport background"),
                ("GridColor",          "Grid color"),
                ("SelectionFill",      "Selection fill"),
                ("SelectionBorder",    "Selection border"),
                ("CursorHighlight",    "Cursor highlight"),
                ("MapBackground",      "Map background"),
            };

            AddSectionHeader("Renderer", ref y);
            AddSlotRow(rendererSlots, 0, ref y);
            AddSlotRow(rendererSlots, 3, ref y);

            y += SecGap + 6;

            var uiSlots = new (string Prop, string Label)[] {
                ("WindowBackground", "Window background"),
                ("PanelBackground",  "Panel background"),
                ("InputBackground",  "Input background"),
                ("InputForeground",  "Input text"),
                ("TextPrimary",      "Text primary"),
                ("TextMuted",        "Text muted"),
                ("AccentBackground", "Accent background"),
                ("AccentForeground", "Accent text"),
                ("BorderColor",      "Border color"),
            };

            AddSectionHeader("User Interface", ref y);
            for (int row = 0; row < 3; row++)
                AddSlotRow(uiSlots, row * 3, ref y);

            y += PadX;
            panelScroll.AutoScrollMinSize = new Size(0, y);
        }

        private void AddSectionHeader(string title, ref int y) {
            var line = new Panel {
                Location  = new Point(PadX, y),
                Size      = new Size(FormW - PadX * 2, 1),
                BackColor = ColorDivider
            };
            var lbl = new Label {
                Text      = title.ToUpperInvariant(),
                Font      = SecFont,
                ForeColor = ColorFgMuted,
                AutoSize  = true,
                Location  = new Point(PadX, y + 5)
            };
            panelScroll.Controls.Add(line);
            panelScroll.Controls.Add(lbl);
            y += SecH + SecGap;
        }

        private void AddSlotRow((string Prop, string Label)[] slots, int startIdx, ref int y) {
            int count = Math.Min(3, slots.Length - startIdx);
            for (int i = 0; i < count; i++) {
                var (prop, label) = slots[startIdx + i];
                AddSlot(prop, label, PadX + i * (SlotW + ColGap), y);
            }
            y += SlotH + RowGap;
        }

        private void AddSlot(string prop, string label, int x, int y) {
            string hexStr = GetPropValue(prop);
            Color  color  = AppTheme.ParseColor(hexStr);

            var lbl = new Label {
                Text      = label,
                ForeColor = ColorFgMuted,
                Font      = LblFont,
                AutoSize  = false,
                Size      = new Size(SlotW, 16),
                Location  = new Point(x, y),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var hex = new TextBox {
                Text        = hexStr,
                Location    = new Point(x, y + 20),
                Size        = new Size(HexW, 24),
                BackColor   = ColorBgInput,
                ForeColor   = ColorFg,
                BorderStyle = BorderStyle.FixedSingle,
                Font        = HexFont
            };

            var swatch = new Button {
                Location  = new Point(x + HexW + 4, y + 20),
                Size      = new Size(SwatchW, 24),
                BackColor = color,
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand,
                Text      = ""
            };
            swatch.FlatAppearance.BorderSize  = 1;
            swatch.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);

            _slots[prop] = (hex, swatch);

            string capturedProp = prop;

            swatch.Click += (_, _) => {
                using var cd = new ColorPickerDialog(swatch.BackColor);
                if (cd.ShowDialog(this) != DialogResult.OK) return;
                swatch.BackColor = cd.SelectedColor;
                hex.Text = AppTheme.FormatColor(cd.SelectedColor);
                SetPropValue(capturedProp, hex.Text);
                NotifyLiveChange();
            };

            hex.Leave += (_, _) => {
                if (TryParseHex(hex.Text, out Color parsed)) {
                    swatch.BackColor = parsed;
                    SetPropValue(capturedProp, AppTheme.FormatColor(parsed));
                    NotifyLiveChange();
                } else {
                    hex.Text = GetPropValue(capturedProp);
                }
            };

            hex.KeyDown += (_, e) => {
                if (e.KeyCode == Keys.Enter) {
                    e.SuppressKeyPress = true;
                    panelScroll.Focus();
                }
            };

            panelScroll.Controls.Add(lbl);
            panelScroll.Controls.Add(hex);
            panelScroll.Controls.Add(swatch);
        }

        // ── Button handlers ───────────────────────────────────────────────────

        private void BtnSave_Click(object? sender, EventArgs e) {
            AppThemeManager.Save(_working);
        }

        private void BtnCancel_Click(object? sender, EventArgs e) {
            // Revert any live-previewed changes back to what was on disk when the dialog opened
            AppThemeManager.Apply(_original);
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnReset_Click(object? sender, EventArgs e) {
            if (MessageBox.Show(this, "Reset all colors to their defaults?", "Reset Theme",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _working = AppTheme.Default;
            foreach (var (prop, (hex, swatch)) in _slots) {
                string hexStr = GetPropValue(prop);
                hex.Text         = hexStr;
                swatch.BackColor = AppTheme.ParseColor(hexStr);
            }
            NotifyLiveChange();
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        /// <summary>Pushes _working to AppThemeManager without saving to disk (live preview).</summary>
        private void NotifyLiveChange() => AppThemeManager.Apply(_working);

        private string GetPropValue(string prop) =>
            typeof(AppTheme).GetProperty(prop)?.GetValue(_working) as string ?? "#FFFFFFFF";

        private void SetPropValue(string prop, string value) =>
            typeof(AppTheme).GetProperty(prop)?.SetValue(_working, value);

        private static bool TryParseHex(string text, out Color color) {
            color = Color.Empty;
            var s = text.TrimStart('#');
            if (s.Length == 6) s = "FF" + s;
            if (s.Length != 8) return false;
            try {
                color = Color.FromArgb(Convert.ToInt32(s, 16));
                return true;
            } catch {
                return false;
            }
        }
    }
}
