using System.Numerics;
using AdofaiToCircle.Circle.Elements;
using Newtonsoft.Json.Linq;
using JsonExtensions = Newtonsoft.Json.Linq.Extensions;

namespace AdofaiToCircle.Adofai.Elements
{
    public class Action
    {
        public int Floor { get; set; }
        public EventType EventType { get; set; }
        public string TrackColor { get; set; }
        public string SecondaryTrackColor { get; set; }
        public TrackColorType TrackColorType { get; set; }
        public LegacyTrackStyle TrackStyle { get; set; }
        public ColorPulseType TrackColorPulse { get; set; }
        public int TrackPulseLength { get; set; }
        public double TrackColorAnimDuration { get; set; }
        public CircleSpeedType? SpeedType { get; set; }
        public double BeatsPerMinute { get; set; }
        public double BPMMultiplier { get; set; }
        public int HitsoundVolume { get; set; }
        public string BgImage { get; set; }
        public double Duration { get; set; }
        public FlashPlane Plane { get; set; }
        public string StartColor { get; set; }
        public string EndColor { get; set; }
        public int StartOpacity { get; set; }
        public int EndOpacity { get; set; }
        public int? Zoom { get; set; }
        public Ease Ease { get; set; }
        public DisplayMode BgDisplayMode { get; set; }
        public string ImageColor { get; set; }
        public int UnscaledSize { get; set; }
        public float? Rotation { get; set; }
        public float AngleOffset { get; set; }
        public Relativity? RelativeTo { get; set; }
        public float?[] Position { get; set; }
        public object Tile { get; set; }
        public Vector2 GetDecorationTiling()
        {
            if (Tile == null || !(Tile is JArray tile))
                return Vector2.One;
            float[] array = ((IEnumerable<JToken>)tile).Select(jt => JsonExtensions.Value<float>(jt)).ToArray();
            return new Vector2(array[0], array[1]);
        }
        public string EventTag { get; set; }
        public string Tag { get; set; }
        public int Repetitions { get; set; }
        public double Interval { get; set; }

        public Toggle LoopBG { get; set; }

        public int StartTime { get; set; }
        public int Index { get; set; }
        public int Depth { get; set; }
        public string DecorationImage { get; set; }
        public string DecText { get; set; }
        public string Color { get; set; }
        public object Scale { get; set; }
        public object RotationOffset { get; set; }
        public Toggle EditorOnly { get; set; }
        public Toggle LockRot { get; set; }
        public object Parallax { get; set; }
        public object PositionOffset { get; set; }
        public float[] PivotOffset { get; set; }
        public object[] StartTile { get; set; }
        public object[] EndTile { get; set; }
        public float? Opacity { get; set; }
        public string PerfectTag { get; set; }
        public string HitTag { get; set; }
        public string BarelyTag { get; set; }
        public string MissTag { get; set; }
        public string LossTag { get; set; }
        public double BeatsAhead { get; set; }
        public double BeatsBehind { get; set; }
        public int EaseParts { get; set; }
        public int Strength { get; set; }
        public int Intensity { get; set; }
        public Toggle FadeOut { get; set; }
        public object Components { get; set; }

        public Action Clone() => (Action)MemberwiseClone();

        public Vector2? GetScale()
        {
            if (Scale == null)
                return new Vector2?();
            if (!(Scale is JArray scale))
                return new Vector2?(new Vector2(float.Parse(Scale.ToString())));
            float[] array = ((IEnumerable<JToken>)scale).Select((jt => JsonExtensions.Value<float>(jt))).ToArray();
            return new Vector2?(new Vector2(array[0], array[1]));
        }

        public float? GetRotationOffset() => RotationOffset == null ? new float?() : new float?(float.Parse(RotationOffset.ToString()));

        public float[] GetPositionOffset() => PositionOffset == null ? null : ((IEnumerable<JToken>)(PositionOffset as JArray)).Select(jt => JsonExtensions.Value<float>(jt)).ToArray();

        public string[] Tags => string.IsNullOrEmpty(Tag) ? null : Tag.Split(" ");

        public bool ContainsTag(string tag) => string.IsNullOrEmpty(Tag) ? string.IsNullOrEmpty(tag) : !string.IsNullOrEmpty(tag) && ((IEnumerable<string>)Tags).Contains(tag);

        public bool HasCommonTag(string[] tags)
        {
            if (tags == null || Tags == null)
                return false;
            foreach (string tag in tags)
            {
                if (ContainsTag(tag))
                    return true;
            }
            return false;
        }
    }
}
