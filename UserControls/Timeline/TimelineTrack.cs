namespace csharp_editor.UserControls.Timeline {
    public class TimelineTrack {
        public string Name    { get; set; } = "Track";
        public bool   Visible { get; set; } = true;
        public bool   Locked  { get; set; } = false;
        public Color  Color   { get; set; } = Color.FromArgb(255, 200, 60);

        public List<TimelineKeyframe> Keyframes { get; } = new();

        public TimelineTrack(string name) {
            Name = name;
        }

        public void AddKeyframe(float time) {
            if (!Keyframes.Any(k => Math.Abs(k.Time - time) < 0.001f))
                Keyframes.Add(new TimelineKeyframe(time));
        }

        public void RemoveKeyframe(TimelineKeyframe kf) {
            Keyframes.Remove(kf);
        }
    }
}
