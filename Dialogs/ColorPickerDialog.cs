using System.Runtime.Versioning;

namespace csharp_editor.Dialogs {

    /// <summary>
    /// A custom color-picker dialog with:
    ///   • SV gradient canvas (saturation/value square)
    ///   • Hue rainbow strip
    ///   • Old/New color preview swatch
    ///   • Hex input
    ///   • RGB ↔ HSB channel inputs toggled by a mode button
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class ColorPickerDialog : Form {

        // ── Public result ─────────────────────────────────────────────────────
        public Color SelectedColor { get; private set; }

        // ── HSV state  (H ∈ [0,360), S ∈ [0,1], V ∈ [0,1]) ──────────────────
        private float _h, _s, _v;
        private int   _alpha = 255;

        // ── Mode ──────────────────────────────────────────────────────────────
        private bool _rgbMode = true;   // false → HSB mode

        // ── Cached bitmaps ────────────────────────────────────────────────────
        private Bitmap? _svBitmap;
        private Bitmap? _hueBitmap;

        // ── Drag state ────────────────────────────────────────────────────────
        private bool _draggingSv  = false;
        private bool _draggingHue = false;

        // ── Colours (matches ThemeDialog palette) ─────────────────────────────
        private static readonly Color CBg      = Color.FromArgb(45, 45, 48);
        private static readonly Color CBgPanel = Color.FromArgb(37, 37, 38);
        private static readonly Color CBgInput = Color.FromArgb(60, 60, 60);
        private static readonly Color CFg      = Color.FromArgb(212, 212, 212);
        private static readonly Color CFgMuted = Color.FromArgb(153, 153, 153);
        private static readonly Color CAccent  = Color.FromArgb(14, 99, 156);
        private static readonly Color CNeutral = Color.FromArgb(62, 62, 66);
        private static readonly Font  UiFont   = new("Segoe UI", 9f);
        private static readonly Font  LblFont  = new("Segoe UI", 8f);
        private static readonly Font  HexFont  = new("Consolas", 9f);

        // ── Constructor ───────────────────────────────────────────────────────

        public ColorPickerDialog(Color initial) {
            SelectedColor = initial;
            ColorToHsv(initial, out _h, out _s, out _v);
            _alpha = initial.A;
            InitializeComponent();
            previewOld.BackColor = initial;
            RebuildSvBitmap();
            RebuildHueBitmap();
            UpdateAllFromHsv(syncHex: true, syncInputs: true);
            UpdateChannelLabels();
        }

        // ─────────────────────────────────────────────────────────────────────
        // SV Canvas
        // ─────────────────────────────────────────────────────────────────────

        private void RebuildSvBitmap() {
            int w = pickerCanvas.Width;
            int h = pickerCanvas.Height;
            if (w <= 0 || h <= 0) return;

            _svBitmap?.Dispose();
            _svBitmap = new Bitmap(w, h);

            Color hueColor = HsvToColor(_h, 1f, 1f);

            for (int px = 0; px < w; px++) {
                float s = (float)px / (w - 1);
                for (int py = 0; py < h; py++) {
                    float v  = 1f - (float)py / (h - 1);
                    // Blend hueColor towards white (S) and towards black (V)
                    int r = (int)((1 - s) * 255 + s * (hueColor.R * v));
                    int g = (int)((1 - s) * 255 + s * (hueColor.G * v));
                    int b = (int)((1 - s) * 255 + s * (hueColor.B * v));
                    _svBitmap.SetPixel(px, py, Color.FromArgb(
                        Math.Clamp(r, 0, 255),
                        Math.Clamp(g, 0, 255),
                        Math.Clamp(b, 0, 255)));
                }
            }
        }

        private void RebuildHueBitmap() {
            int w = hueStrip.Width;
            int h = hueStrip.Height;
            if (w <= 0 || h <= 0) return;

            _hueBitmap?.Dispose();
            _hueBitmap = new Bitmap(w, h);

            for (int px = 0; px < w; px++) {
                float hue = (float)px / (w - 1) * 360f;
                Color c   = HsvToColor(hue, 1f, 1f);
                for (int py = 0; py < h; py++)
                    _hueBitmap.SetPixel(px, py, c);
            }
        }

        private void pickerCanvas_Paint(object? sender, PaintEventArgs e) {
            if (_svBitmap != null)
                e.Graphics.DrawImage(_svBitmap, 0, 0);

            // Draw cross-hair cursor
            int cx = (int)(_s * (pickerCanvas.Width  - 1));
            int cy = (int)((1 - _v) * (pickerCanvas.Height - 1));
            DrawCrosshair(e.Graphics, cx, cy, pickerCanvas.Width, pickerCanvas.Height);
        }

        private static void DrawCrosshair(Graphics g, int cx, int cy, int canvasW, int canvasH) {
            const int R = 7;
            using var pen = new Pen(Color.White, 1.5f);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawEllipse(pen, cx - R, cy - R, R * 2, R * 2);
            // thin dark outline for contrast
            using var penDark = new Pen(Color.Black, 0.5f);
            g.DrawEllipse(penDark, cx - R - 1, cy - R - 1, R * 2 + 2, R * 2 + 2);
        }

        private void pickerCanvas_MouseDown(object? sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                _draggingSv = true;
                UpdateSvFromPoint(e.X, e.Y);
            }
        }

        private void pickerCanvas_MouseMove(object? sender, MouseEventArgs e) {
            if (_draggingSv)
                UpdateSvFromPoint(e.X, e.Y);
        }

        private void pickerCanvas_MouseUp(object? sender, MouseEventArgs e) {
            _draggingSv = false;
        }

        private void UpdateSvFromPoint(int px, int py) {
            _s = Math.Clamp((float)px / (pickerCanvas.Width  - 1), 0f, 1f);
            _v = Math.Clamp(1f - (float)py / (pickerCanvas.Height - 1), 0f, 1f);
            UpdateAllFromHsv(syncHex: true, syncInputs: true);
        }

        private void pickerCanvas_Resize(object? sender, EventArgs e) {
            RebuildSvBitmap();
            pickerCanvas.Invalidate();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Hue strip
        // ─────────────────────────────────────────────────────────────────────

        private void hueStrip_Paint(object? sender, PaintEventArgs e) {
            if (_hueBitmap != null)
                e.Graphics.DrawImage(_hueBitmap, 0, 0);

            // Thumb circle
            int tx = (int)(_h / 360f * (hueStrip.Width - 1));
            int ty = hueStrip.Height / 2;
            const int TR = 8;
            using var pen  = new Pen(Color.White, 2f);
            using var penD = new Pen(Color.Black, 1f);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(pen,  tx - TR, ty - TR, TR * 2, TR * 2);
            e.Graphics.DrawEllipse(penD, tx - TR - 1, ty - TR - 1, TR * 2 + 2, TR * 2 + 2);
        }

        private void hueStrip_MouseDown(object? sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                _draggingHue = true;
                UpdateHueFromPoint(e.X);
            }
        }

        private void hueStrip_MouseMove(object? sender, MouseEventArgs e) {
            if (_draggingHue)
                UpdateHueFromPoint(e.X);
        }

        private void hueStrip_MouseUp(object? sender, MouseEventArgs e) {
            _draggingHue = false;
        }

        private void UpdateHueFromPoint(int px) {
            _h = Math.Clamp((float)px / (hueStrip.Width - 1) * 360f, 0f, 359.99f);
            RebuildSvBitmap();
            UpdateAllFromHsv(syncHex: true, syncInputs: true);
        }

        private void hueStrip_Resize(object? sender, EventArgs e) {
            RebuildHueBitmap();
            hueStrip.Invalidate();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Inputs
        // ─────────────────────────────────────────────────────────────────────

        private void hexBox_Leave(object? sender, EventArgs e)  => ApplyHexInput();
        private void hexBox_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; ApplyHexInput(); }
        }

        private void ApplyHexInput() {
            var s = hexBox.Text.TrimStart('#');
            if (s.Length == 6) s = "FF" + s;
            if (s.Length != 8) { RefreshHexBox(); return; }
            try {
                int argb = Convert.ToInt32(s, 16);
                var c    = Color.FromArgb(argb);
                _alpha   = c.A;
                ColorToHsv(c, out _h, out _s, out _v);
                RebuildSvBitmap();
                UpdateAllFromHsv(syncHex: false, syncInputs: true);
            } catch { RefreshHexBox(); }
        }

        private void ch1Box_Leave(object? sender, EventArgs e)  => ApplyChannelInputs();
        private void ch2Box_Leave(object? sender, EventArgs e)  => ApplyChannelInputs();
        private void ch3Box_Leave(object? sender, EventArgs e)  => ApplyChannelInputs();
        private void ch1Box_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; ApplyChannelInputs(); }
        }
        private void ch2Box_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; ApplyChannelInputs(); }
        }
        private void ch3Box_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; ApplyChannelInputs(); }
        }

        private void ApplyChannelInputs() {
            if (_rgbMode) {
                if (!int.TryParse(ch1Box.Text, out int r)) { RefreshChannelInputs(); return; }
                if (!int.TryParse(ch2Box.Text, out int g)) { RefreshChannelInputs(); return; }
                if (!int.TryParse(ch3Box.Text, out int b)) { RefreshChannelInputs(); return; }
                r = Math.Clamp(r, 0, 255);
                g = Math.Clamp(g, 0, 255);
                b = Math.Clamp(b, 0, 255);
                ColorToHsv(Color.FromArgb(_alpha, r, g, b), out _h, out _s, out _v);
            } else {
                if (!float.TryParse(ch1Box.Text, out float h)) { RefreshChannelInputs(); return; }
                if (!float.TryParse(ch2Box.Text, out float s)) { RefreshChannelInputs(); return; }
                if (!float.TryParse(ch3Box.Text, out float v)) { RefreshChannelInputs(); return; }
                _h = Math.Clamp(h, 0f, 360f);
                _s = Math.Clamp(s, 0f, 100f) / 100f;
                _v = Math.Clamp(v, 0f, 100f) / 100f;
            }
            RebuildSvBitmap();
            UpdateAllFromHsv(syncHex: true, syncInputs: false);
        }

        // ── Mode toggle ───────────────────────────────────────────────────────

        private void btnMode_Click(object? sender, EventArgs e) {
            _rgbMode = !_rgbMode;
            btnMode.Text = _rgbMode ? "RGB" : "HSB";
            UpdateChannelLabels();
            RefreshChannelInputs();
        }

        // ── OK / Cancel ───────────────────────────────────────────────────────

        private void btnOk_Click(object? sender, EventArgs e) {
            SelectedColor = HsvToColor(_h, _s, _v, _alpha);
            DialogResult  = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object? sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Master update
        // ─────────────────────────────────────────────────────────────────────

        private void UpdateAllFromHsv(bool syncHex, bool syncInputs) {
            SelectedColor = HsvToColor(_h, _s, _v, _alpha);

            // Redraw canvas & hue strip
            pickerCanvas.Invalidate();
            hueStrip.Invalidate();

            // Preview swatch
            previewNew.BackColor = SelectedColor;

            if (syncHex)    RefreshHexBox();
            if (syncInputs) RefreshChannelInputs();
        }

        private void RefreshHexBox() {
            Color c    = HsvToColor(_h, _s, _v, _alpha);
            hexBox.Text = $"#{c.A:X2}{c.R:X2}{c.G:X2}{c.B:X2}";
        }

        private void RefreshChannelInputs() {
            Color c = HsvToColor(_h, _s, _v, _alpha);
            if (_rgbMode) {
                ch1Box.Text = c.R.ToString();
                ch2Box.Text = c.G.ToString();
                ch3Box.Text = c.B.ToString();
            } else {
                ch1Box.Text = ((int)Math.Round(_h)).ToString();
                ch2Box.Text = ((int)Math.Round(_s * 100)).ToString();
                ch3Box.Text = ((int)Math.Round(_v * 100)).ToString();
            }
        }

        private void UpdateChannelLabels() {
            if (_rgbMode) {
                lblCh1.Text = "Red";
                lblCh2.Text = "Green";
                lblCh3.Text = "Blue";
            } else {
                lblCh1.Text = "Hue";
                lblCh2.Text = "Sat";
                lblCh3.Text = "Val";
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // HSV <-> Color helpers
        // ─────────────────────────────────────────────────────────────────────

        private static Color HsvToColor(float h, float s, float v, int alpha = 255) {
            float r, g, b;
            if (s == 0f) { r = g = b = v; }
            else {
                h /= 60f;
                int   i = (int)h;
                float f = h - i;
                float p = v * (1 - s);
                float q = v * (1 - s * f);
                float t = v * (1 - s * (1 - f));
                (r, g, b) = i switch {
                    0 => (v, t, p),
                    1 => (q, v, p),
                    2 => (p, v, t),
                    3 => (p, q, v),
                    4 => (t, p, v),
                    _ => (v, p, q),
                };
            }
            return Color.FromArgb(alpha,
                Math.Clamp((int)(r * 255), 0, 255),
                Math.Clamp((int)(g * 255), 0, 255),
                Math.Clamp((int)(b * 255), 0, 255));
        }

        private static void ColorToHsv(Color c, out float h, out float s, out float v) {
            float r = c.R / 255f, g = c.G / 255f, b = c.B / 255f;
            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));
            float delta = max - min;
            v = max;
            s = max == 0 ? 0 : delta / max;
            if (delta == 0) { h = 0; return; }
            if      (max == r) h = 60f * (((g - b) / delta) % 6);
            else if (max == g) h = 60f * (((b - r) / delta) + 2);
            else               h = 60f * (((r - g) / delta) + 4);
            if (h < 0) h += 360f;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _svBitmap?.Dispose();
                _hueBitmap?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
