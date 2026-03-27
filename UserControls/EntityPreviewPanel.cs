using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace csharp_editor.UserControls {
    /// <summary>
    /// Lightweight preview panel that shows a cropped entity sprite region,
    /// scaled to fill the panel (aspect-ratio preserved), with a checker
    /// background and a pivot cross overlay.
    /// </summary>
    public class EntityPreviewPanel : Panel {
        private Bitmap? _regionBitmap;
        private float   _pivotX = 0.5f;
        private float   _pivotY = 1.0f;

        public EntityPreviewPanel() {
            DoubleBuffered = true;
            BackColor      = Color.FromArgb(45, 45, 45);
            BorderStyle    = BorderStyle.FixedSingle;
            Size           = new Size(256, 256);
        }

        /// <summary>
        /// Crops <paramref name="region"/> from <paramref name="textureData"/>,
        /// stores the result and redraws at the current pivot position.
        /// </summary>
        public void SetPreview(CExternsEditor.TextureDataStruct textureData,
                               Rectangle region, float pivotX, float pivotY) {
            _pivotX = pivotX;
            _pivotY = pivotY;
            _regionBitmap?.Dispose();
            _regionBitmap = null;

            if (textureData.Data == IntPtr.Zero || region.Width <= 0 || region.Height <= 0) {
                Invalidate();
                return;
            }

            try {
                using var full = CreateBitmapFromTextureData(textureData);
                int cx = Math.Max(0, Math.Min(region.X, full.Width  - 1));
                int cy = Math.Max(0, Math.Min(region.Y, full.Height - 1));
                int cw = Math.Min(region.Width,  full.Width  - cx);
                int ch = Math.Min(region.Height, full.Height - cy);
                if (cw > 0 && ch > 0)
                    _regionBitmap = full.Clone(new Rectangle(cx, cy, cw, ch), full.PixelFormat);
            }
            catch { /* silently swallow bitmap errors */ }

            Invalidate();
        }

        /// <summary>Update pivot without reloading the texture.</summary>
        public void UpdatePivot(float pivotX, float pivotY) {
            _pivotX = pivotX;
            _pivotY = pivotY;
            Invalidate();
        }

        public void Clear() {
            _regionBitmap?.Dispose();
            _regionBitmap = null;
            Invalidate();
        }

        // ── Paint ────────────────────────────────────────────────────────────────────

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            var g  = e.Graphics;
            int pw = ClientRectangle.Width;
            int ph = ClientRectangle.Height;

            DrawChecker(g, pw, ph);

            if (_regionBitmap == null) return;

            // Fit-to-panel, preserving aspect ratio
            float scale = Math.Min((float)pw / _regionBitmap.Width,
                                   (float)ph / _regionBitmap.Height);
            int dw = (int)(_regionBitmap.Width  * scale);
            int dh = (int)(_regionBitmap.Height * scale);
            int dx = (pw - dw) / 2;
            int dy = (ph - dh) / 2;

            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode   = PixelOffsetMode.Half;
            g.DrawImage(_regionBitmap, dx, dy, dw, dh);

            // Pivot cross
            int px  = dx + (int)(_pivotX * dw);
            int py  = dy + (int)(_pivotY * dh);
            int arm = 9;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var shadow = new Pen(Color.Black, 3f)) {
                g.DrawLine(shadow, px - arm, py, px + arm, py);
                g.DrawLine(shadow, px, py - arm, px, py + arm);
            }
            using (var cross = new Pen(Color.White, 1.5f)) {
                g.DrawLine(cross, px - arm, py, px + arm, py);
                g.DrawLine(cross, px, py - arm, px, py + arm);
            }
            g.FillEllipse(Brushes.White, px - 2, py - 2, 4, 4);
        }

        // ── Helpers ──────────────────────────────────────────────────────────────────

        private static void DrawChecker(Graphics g, int width, int height) {
            const int size = 8;
            using var b1 = new SolidBrush(Color.FromArgb(48, 48, 48));
            using var b2 = new SolidBrush(Color.FromArgb(72, 72, 72));
            for (int y = 0; y < height; y += size)
                for (int x = 0; x < width; x += size)
                    g.FillRectangle(((x / size + y / size) % 2 == 0) ? b1 : b2,
                                    x, y, size, size);
        }

        private static Bitmap CreateBitmapFromTextureData(CExternsEditor.TextureDataStruct td) {
            PixelFormat fmt = td.BytesPerPixel switch {
                1 => PixelFormat.Format8bppIndexed,
                3 => PixelFormat.Format24bppRgb,
                4 => PixelFormat.Format32bppArgb,
                _ => PixelFormat.Format32bppArgb
            };

            var bmp  = new Bitmap(td.Width, td.Height, fmt);
            var data = bmp.LockBits(new Rectangle(0, 0, td.Width, td.Height),
                                    ImageLockMode.WriteOnly, bmp.PixelFormat);
            int stride    = Math.Abs(data.Stride);
            int imageSize = stride * td.Height;

            if (td.BytesPerPixel == 3) {
                byte[] src = new byte[td.DataLength];
                Marshal.Copy(td.Data, src, 0, td.DataLength);
                byte[] dst = new byte[imageSize];
                for (int y = 0; y < td.Height; y++)
                    for (int x = 0; x < td.Width; x++) {
                        int s = (y * td.Width + x) * 3, d = y * stride + x * 3;
                        if (s + 2 < src.Length && d + 2 < dst.Length) {
                            dst[d] = src[s + 2]; dst[d + 1] = src[s + 1]; dst[d + 2] = src[s];
                        }
                    }
                Marshal.Copy(dst, 0, data.Scan0, Math.Min(dst.Length, imageSize));
            }
            else if (td.BytesPerPixel == 4) {
                byte[] src = new byte[td.DataLength];
                Marshal.Copy(td.Data, src, 0, td.DataLength);
                byte[] dst = new byte[imageSize];
                for (int y = 0; y < td.Height; y++)
                    for (int x = 0; x < td.Width; x++) {
                        int s = (y * td.Width + x) * 4, d = y * stride + x * 4;
                        if (s + 3 < src.Length && d + 3 < dst.Length) {
                            dst[d] = src[s + 2]; dst[d + 1] = src[s + 1];
                            dst[d + 2] = src[s]; dst[d + 3] = src[s + 3];
                        }
                    }
                Marshal.Copy(dst, 0, data.Scan0, Math.Min(dst.Length, imageSize));
            }
            else {
                byte[] buf = new byte[Math.Min(td.DataLength, imageSize)];
                Marshal.Copy(td.Data, buf, 0, buf.Length);
                Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            }

            bmp.UnlockBits(data);
            return bmp;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) _regionBitmap?.Dispose();
            base.Dispose(disposing);
        }
    }
}
