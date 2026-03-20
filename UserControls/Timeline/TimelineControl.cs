namespace csharp_editor.UserControls.Timeline {
    public class TimelineControl : UserControl {

        // ── Layout ──────────────────────────────────────────────────────────
        private const int HeaderWidth  = 160;
        private const int RulerHeight  = 24;
        private const int TrackHeight  = 30;
        private const int KeyframeSize = 9;

        // ── Colors ──────────────────────────────────────────────────────────
        private static readonly Color ColBackground    = Color.FromArgb(28,  28,  28);
        private static readonly Color ColHeader        = Color.FromArgb(40,  40,  40);
        private static readonly Color ColHeaderBorder  = Color.FromArgb(20,  20,  20);
        private static readonly Color ColRuler         = Color.FromArgb(20,  20,  20);
        private static readonly Color ColRulerText     = Color.FromArgb(170, 170, 170);
        private static readonly Color ColRulerTick     = Color.FromArgb(90,  90,  90);
        private static readonly Color ColTrackEven     = Color.FromArgb(42,  42,  42);
        private static readonly Color ColTrackOdd      = Color.FromArgb(36,  36,  36);
        private static readonly Color ColSeparator     = Color.FromArgb(22,  22,  22);
        private static readonly Color ColPlayhead      = Color.FromArgb(255, 80,  60);
        private static readonly Color ColPlayheadHead  = Color.FromArgb(255, 100, 80);
        private static readonly Color ColKeyframeSel   = Color.White;
        private static readonly Color ColHeaderText    = Color.FromArgb(210, 210, 210);

        // ── State ───────────────────────────────────────────────────────────
        private readonly List<TimelineTrack> _tracks = new();
        private float   _playhead       = 0f;
        private float   _zoom           = 80f;   // pixels per second
        private float   _scrollX        = 0f;    // horizontal scroll offset in pixels
        private bool    _draggingPlayhead   = false;
        private TimelineKeyframe? _draggingKeyframe = null;
        private TimelineTrack?    _draggingKeyframeTrack = null;
        private float   _dragOffsetTime = 0f;

        // ── Public API ──────────────────────────────────────────────────────
        public float  Duration { get; set; } = 10f;
        public float  Playhead => _playhead;
        public IReadOnlyList<TimelineTrack> Tracks => _tracks;

        public event EventHandler<float>? PlayheadChanged;

        public TimelineControl() {
            DoubleBuffered = true;
            BackColor      = ColBackground;
            ResizeRedraw   = true;

            MouseDown  += Timeline_MouseDown;
            MouseMove  += Timeline_MouseMove;
            MouseUp    += Timeline_MouseUp;
            MouseWheel += Timeline_MouseWheel;
        }

        // ── Track management ────────────────────────────────────────────────
        public void AddTrack(TimelineTrack track) {
            _tracks.Add(track);
            Invalidate();
        }

        public void RemoveTrack(TimelineTrack track) {
            _tracks.Remove(track);
            Invalidate();
        }

        public void SetPlayhead(float time) {
            _playhead = Math.Clamp(time, 0f, Duration);
            PlayheadChanged?.Invoke(this, _playhead);
            Invalidate();
        }

        // ── Paint ───────────────────────────────────────────────────────────
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            var g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            DrawTracks(g);
            DrawRuler(g);
            DrawPlayhead(g);
            DrawHeaderBorder(g);
        }

        private void DrawRuler(Graphics g) {
            g.FillRectangle(new SolidBrush(ColRuler),
                HeaderWidth, 0, Width - HeaderWidth, RulerHeight);

            // Corner header area fill
            g.FillRectangle(new SolidBrush(ColHeader), 0, 0, HeaderWidth, RulerHeight);

            using var tickPen  = new Pen(ColRulerTick);
            using var majorPen = new Pen(Color.FromArgb(130, 130, 130));
            using var font     = new Font("Segoe UI", 7.5f);
            using var brush    = new SolidBrush(ColRulerText);

            // Determine tick spacing: adapt to zoom
            float secPerMajor = ChooseMajorTickInterval();
            float secPerMinor = secPerMajor / 5f;

            for (float t = 0; t <= Duration + secPerMinor; t += secPerMinor) {
                int x = TimeToX(t);
                if (x < HeaderWidth || x > Width) continue;

                bool isMajor = (Math.Abs(t % secPerMajor) < secPerMinor * 0.5f);
                int tickH = isMajor ? RulerHeight / 2 : RulerHeight / 4;

                g.DrawLine(isMajor ? majorPen : tickPen, x, RulerHeight - tickH, x, RulerHeight - 1);

                if (isMajor) {
                    string label = FormatTime(t);
                    g.DrawString(label, font, brush, x + 2, 3);
                }
            }
        }

        private void DrawTracks(Graphics g) {
            // Header column background
            g.FillRectangle(new SolidBrush(ColHeader), 0, RulerHeight, HeaderWidth, Height - RulerHeight);

            using var headerFont   = new Font("Segoe UI", 8.5f);
            using var headerBrush  = new SolidBrush(ColHeaderText);
            using var separatorPen = new Pen(ColSeparator);
            using var lockFont     = new Font("Segoe UI", 7f);
            using var dimBrush     = new SolidBrush(Color.FromArgb(100, 100, 100));

            for (int i = 0; i < _tracks.Count; i++) {
                var track = _tracks[i];
                int y = TrackY(i);

                // Lane background
                using var laneBrush = new SolidBrush(i % 2 == 0 ? ColTrackEven : ColTrackOdd);
                g.FillRectangle(laneBrush, HeaderWidth, y, Width - HeaderWidth, TrackHeight);

                // Separator
                g.DrawLine(separatorPen, 0, y + TrackHeight - 1, Width, y + TrackHeight - 1);

                // Track name
                bool dimmed = !track.Visible || track.Locked;
                g.DrawString(track.Name, headerFont,
                    dimmed ? dimBrush : headerBrush,
                    8, y + (TrackHeight - 15) / 2);

                // Visibility / lock badge
                string badge = (!track.Visible ? "●" : "") + (track.Locked ? " 🔒" : "");
                if (!string.IsNullOrEmpty(badge))
                    g.DrawString(badge, lockFont, dimBrush, HeaderWidth - 22, y + (TrackHeight - 11) / 2);

                // Keyframes
                DrawKeyframes(g, track, y);
            }

            // Vertical guide line at header edge
            g.DrawLine(new Pen(ColHeaderBorder), HeaderWidth, RulerHeight, HeaderWidth, Height);
        }

        private void DrawKeyframes(Graphics g, TimelineTrack track, int trackY) {
            int cy = trackY + TrackHeight / 2;

            using var normalBrush = new SolidBrush(track.Color);
            using var selBrush    = new SolidBrush(ColKeyframeSel);
            using var selPen      = new Pen(Color.FromArgb(80, 80, 80));

            foreach (var kf in track.Keyframes) {
                int kx = TimeToX(kf.Time);
                if (kx < HeaderWidth - KeyframeSize || kx > Width + KeyframeSize) continue;

                int h = KeyframeSize;
                var diamond = new Point[] {
                    new(kx,       cy - h),
                    new(kx + h,   cy),
                    new(kx,       cy + h),
                    new(kx - h,   cy),
                };

                g.FillPolygon(kf.Selected ? selBrush : normalBrush, diamond);
                if (kf.Selected)
                    g.DrawPolygon(selPen, diamond);
            }
        }

        private void DrawPlayhead(Graphics g) {
            int x = TimeToX(_playhead);
            if (x < HeaderWidth || x > Width) return;

            using var linePen = new Pen(ColPlayhead, 1.5f);
            g.DrawLine(linePen, x, RulerHeight, x, Height);

            // Triangle handle on ruler
            var handle = new Point[] {
                new(x - 6, 0),
                new(x + 6, 0),
                new(x,     12),
            };
            g.FillPolygon(new SolidBrush(ColPlayheadHead), handle);
            g.DrawPolygon(new Pen(ColPlayhead), handle);

            // Time label next to handle
            using var font  = new Font("Segoe UI", 7f, FontStyle.Bold);
            using var brush = new SolidBrush(Color.White);
            g.DrawString(FormatTime(_playhead), font, brush, x + 8, 4);
        }

        private void DrawHeaderBorder(Graphics g) {
            g.DrawLine(new Pen(ColHeaderBorder, 1), 0, RulerHeight - 1, Width, RulerHeight - 1);
        }

        // ── Mouse input ─────────────────────────────────────────────────────
        private void Timeline_MouseDown(object? sender, MouseEventArgs e) {
            // Click inside ruler → drag playhead
            if (e.Y < RulerHeight && e.X >= HeaderWidth) {
                ClearKeyframeSelection();
                _draggingPlayhead = true;
                SetPlayhead(XToTime(e.X));
                return;
            }

            int trackIdx = HitTestTrack(e.Y);
            if (trackIdx < 0) return;

            var track = _tracks[trackIdx];

            if (e.Button == MouseButtons.Left) {
                var kf = HitTestKeyframe(track, e.X, e.Y);

                if (kf != null) {
                    // Start dragging existing keyframe
                    ClearKeyframeSelection();
                    kf.Selected          = true;
                    _draggingKeyframe    = kf;
                    _draggingKeyframeTrack = track;
                    _dragOffsetTime      = kf.Time - XToTime(e.X);
                    Invalidate();
                }
                else if (e.X > HeaderWidth) {
                    // Place new keyframe
                    ClearKeyframeSelection();
                    float t = SnapToFrame(XToTime(e.X));
                    track.AddKeyframe(t);
                    Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right) {
                // Right-click → remove keyframe under cursor
                var kf = HitTestKeyframe(track, e.X, e.Y);
                if (kf != null) {
                    track.RemoveKeyframe(kf);
                    Invalidate();
                }
            }
        }

        private void Timeline_MouseMove(object? sender, MouseEventArgs e) {
            if (_draggingPlayhead) {
                SetPlayhead(XToTime(e.X));
                return;
            }

            if (_draggingKeyframe != null) {
                float newTime = SnapToFrame(XToTime(e.X) + _dragOffsetTime);
                _draggingKeyframe.Time = Math.Clamp(newTime, 0f, Duration);
                Invalidate();
            }
        }

        private void Timeline_MouseUp(object? sender, MouseEventArgs e) {
            _draggingPlayhead  = false;
            _draggingKeyframe  = null;
            _draggingKeyframeTrack = null;
        }

        private void Timeline_MouseWheel(object? sender, MouseEventArgs e) {
            if (ModifierKeys.HasFlag(Keys.Control)) {
                // Zoom around mouse cursor
                float timeAtCursor = XToTime(e.X);
                _zoom     = Math.Clamp(_zoom * (e.Delta > 0 ? 1.15f : 0.87f), 8f, 800f);
                _scrollX  = Math.Max(0f, TimeToXRaw(timeAtCursor) - (e.X - HeaderWidth));
            }
            else {
                // Horizontal scroll
                _scrollX  = Math.Clamp(_scrollX - e.Delta * 0.4f, 0f, Duration * _zoom);
            }
            Invalidate();
        }

        // ── Hit testing ─────────────────────────────────────────────────────
        private int HitTestTrack(int y) {
            int idx = (y - RulerHeight) / TrackHeight;
            return (idx >= 0 && idx < _tracks.Count) ? idx : -1;
        }

        private TimelineKeyframe? HitTestKeyframe(TimelineTrack track, int mx, int my) {
            int cy = TrackY(_tracks.IndexOf(track)) + TrackHeight / 2;

            foreach (var kf in track.Keyframes) {
                int kx = TimeToX(kf.Time);
                int dx = Math.Abs(mx - kx);
                int dy = Math.Abs(my - cy);
                if (dx + dy <= KeyframeSize + 2)
                    return kf;
            }
            return null;
        }

        private void ClearKeyframeSelection() {
            foreach (var t in _tracks)
                foreach (var k in t.Keyframes)
                    k.Selected = false;
        }

        // ── Helpers ──────────────────────────────────────────────────────────
        private int   TrackY(int index) => RulerHeight + index * TrackHeight;
        private int   TimeToX(float t)  => (int)(HeaderWidth + t * _zoom - _scrollX);
        private float TimeToXRaw(float t) => t * _zoom - _scrollX;
        private float XToTime(int x)    => Math.Clamp((x - HeaderWidth + _scrollX) / _zoom, 0f, Duration);
        private float SnapToFrame(float t, float fps = 24f) => MathF.Round(t * fps) / fps;

        private float ChooseMajorTickInterval() {
            // Pick a sensible interval so labels aren't crowded
            float[] candidates = { 0.1f, 0.25f, 0.5f, 1f, 2f, 5f, 10f, 30f, 60f };
            float minPixels = 60f;
            foreach (var c in candidates)
                if (c * _zoom >= minPixels) return c;
            return 60f;
        }

        private static string FormatTime(float t) {
            int mins = (int)t / 60;
            float secs = t % 60;
            return mins > 0 ? $"{mins}:{secs:00.0}" : $"{secs:0.0}s";
        }
    }
}
