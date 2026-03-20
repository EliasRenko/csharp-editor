using csharp_editor.UserControls.Timeline;

namespace csharp_editor.Dialogs {
    public class TimelineDialog : Form {

        private TimelineControl _timeline      = null!;
        private Label           _labelTime     = null!;
        private Button          _buttonAddTrack = null!;
        private Button          _buttonClose   = null!;
        private Panel           _toolbar       = null!;

        public TimelineDialog() {
            Text            = "Timeline";
            Size            = new Size(900, 420);
            MinimumSize     = new Size(600, 320);
            StartPosition   = FormStartPosition.CenterParent;
            BackColor       = Color.FromArgb(32, 32, 32);
            ForeColor       = Color.Gainsboro;

            BuildLayout();
            SeedDemoTracks();
        }

        private void BuildLayout() {
            // ── Toolbar ────────────────────────────────────────────────────
            _toolbar = new Panel {
                Dock      = DockStyle.Top,
                Height    = 34,
                BackColor = Color.FromArgb(45, 45, 45),
                Padding   = new Padding(4, 4, 4, 0),
            };

            _buttonAddTrack = new Button {
                Text      = "+ Add Track",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.Gainsboro,
                Size      = new Size(90, 24),
                Location  = new Point(4, 5),
                Cursor    = Cursors.Hand,
            };
            _buttonAddTrack.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
            _buttonAddTrack.Click += ButtonAddTrack_Click;

            _labelTime = new Label {
                Text      = "▶  0.0s",
                ForeColor = Color.FromArgb(255, 140, 80),
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(108, 9),
            };

            _buttonClose = new Button {
                Text      = "Close",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.Gainsboro,
                Size      = new Size(60, 24),
                Anchor    = AnchorStyles.Top | AnchorStyles.Right,
                Cursor    = Cursors.Hand,
            };
            _buttonClose.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
            _buttonClose.Location = new Point(_toolbar.Width - _buttonClose.Width - 6, 5);
            _buttonClose.Click   += (_, _) => Close();

            _toolbar.Controls.AddRange(new Control[] {
                _buttonAddTrack, _labelTime, _buttonClose
            });
            _toolbar.Resize += (_, _) =>
                _buttonClose.Location = new Point(_toolbar.Width - _buttonClose.Width - 6, 5);

            // ── Timeline control ──────────────────────────────────────────
            _timeline = new TimelineControl {
                Dock     = DockStyle.Fill,
                Duration = 20f,
            };
            _timeline.PlayheadChanged += (_, t) =>
                _labelTime.Text = $"▶  {FormatTime(t)}";

            Controls.Add(_timeline);
            Controls.Add(_toolbar);   // Add after Fill so toolbar stays on top
        }

        // ── Demo data ──────────────────────────────────────────────────────
        private void SeedDemoTracks() {
            var position = new TimelineTrack("Position") {
                Color = Color.FromArgb(100, 200, 255)
            };
            position.AddKeyframe(0f);
            position.AddKeyframe(2.5f);
            position.AddKeyframe(5f);
            position.AddKeyframe(8f);

            var rotation = new TimelineTrack("Rotation") {
                Color = Color.FromArgb(255, 160, 60)
            };
            rotation.AddKeyframe(0f);
            rotation.AddKeyframe(4f);
            rotation.AddKeyframe(10f);

            var scale = new TimelineTrack("Scale") {
                Color = Color.FromArgb(120, 220, 120)
            };
            scale.AddKeyframe(1f);
            scale.AddKeyframe(6f);

            var alpha = new TimelineTrack("Alpha") {
                Color = Color.FromArgb(200, 100, 220)
            };
            alpha.AddKeyframe(0f);
            alpha.AddKeyframe(3f);
            alpha.AddKeyframe(7.5f);

            _timeline.AddTrack(position);
            _timeline.AddTrack(rotation);
            _timeline.AddTrack(scale);
            _timeline.AddTrack(alpha);
        }

        // ── Add track ──────────────────────────────────────────────────────
        private int _trackCounter = 5;

        private void ButtonAddTrack_Click(object? sender, EventArgs e) {
            var colors = new[] {
                Color.FromArgb(100, 200, 255),
                Color.FromArgb(255, 160,  60),
                Color.FromArgb(120, 220, 120),
                Color.FromArgb(200, 100, 220),
                Color.FromArgb(255, 100, 100),
            };
            var track = new TimelineTrack($"Track {_trackCounter++}") {
                Color = colors[_trackCounter % colors.Length]
            };
            _timeline.AddTrack(track);
        }

        private static string FormatTime(float t) {
            int mins = (int)t / 60;
            float secs = t % 60;
            return mins > 0 ? $"{mins}:{secs:00.0}s" : $"{secs:0.0}s";
        }
    }
}
