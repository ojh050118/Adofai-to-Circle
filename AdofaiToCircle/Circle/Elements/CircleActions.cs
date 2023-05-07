using AdofaiToCircle.Adofai;

namespace AdofaiToCircle.Circle.Elements
{
    public struct CircleActions
    {
        public int Floor;
        public CircleEventType? EventType;
        public CircleSpeedType? SpeedType;
        public float BeatsPerMinute;
        public float BpmMultiplier;
        public Relativity? RelativeTo;
        public CircleEasing Ease;
        public double Duration;
        public float? Rotation;
        public float AngleOffset;
        public float?[] Position;
        public int? Zoom;
        public int Repetitions;
        public double Interval;
        public string Tag;
        public string EventTag;
    }
}
