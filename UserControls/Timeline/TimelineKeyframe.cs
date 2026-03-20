namespace csharp_editor.UserControls.Timeline {
    public class TimelineKeyframe {
        public float Time     { get; set; }
        public bool  Selected { get; set; }

        public TimelineKeyframe(float time) {
            Time = time;
        }
    }
}
