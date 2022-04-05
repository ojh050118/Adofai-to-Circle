using AdofaiToCircle.Adofai;

namespace AdofaiToCircle.Circle.Elements
{
    public struct Actions
    {
        public int Floor;
        public EventType? EventType;
        public SpeedType? SpeedType;
        public float BeatsPerMinute;
        public float BpmMultiplier;
        public Relativity? RelativeTo;
        public Easing Ease;
        public double Duration;
        public float? Rotation;
        public float AngleOffset;
        public float[] Position;
        public int? Zoom;
        public int Repetitions;
        public double Interval;
        public string Tag;
        public string EventTag;
    }
}
