using AdofaiToCircle.Adofai;
using AdofaiToCircle.Circle;
using AdofaiToCircle.Circle.Elements;

namespace AdofaiToCircle.Convert.Circle
{
    public class CircleBeatmapConverter : BeatmapConverter<AdofaiFile, CircleFile>
    {
        public override CircleFile Convert(AdofaiFile adofai)
        {
            if (adofai.AngleData == null)
                adofai.AngleData = ParseAngleData(adofai.PathData);

            CircleFile circle = new CircleFile
            {
                AngleData = adofai.AngleData,
                Settings = new CircleSettings
                {
                    Artist = adofai.Settings.Artist,
                    Author = adofai.Settings.Author,
                    BgImage = adofai.Settings.BgImage,
                    BgVideo = adofai.Settings.BgVideo,
                    BeatmapDesc = adofai.Settings.LevelDesc,
                    CountdownTicks = adofai.Settings.CountdownTicks,
                    Difficulty = adofai.Settings.Difficulty,
                    Bpm = adofai.Settings.Bpm,
                    Offset = adofai.Settings.Offset,
                    VidOffset = adofai.Settings.VidOffset,
                    Pitch = adofai.Settings.Pitch,
                    Volume = adofai.Settings.Volume,
                    PlanetEasing = convertEasing(ParseEase(adofai.Settings.PlanetEase)),
                    PreviewSongStart = adofai.Settings.PreviewSongStart,
                    PreviewSongDuration = adofai.Settings.PreviewSongDuration,
                    SeparateCountdownTime = convertToggle(ParseToggle(adofai.Settings.SeparateCountdownTime)),
                    Song = adofai.Settings.Song,
                    SongFileName = adofai.Settings.SongFilename,
                    RelativeTo = ParseRelativity(adofai.Settings.RelativeTo),
                    Position = adofai.Settings.Position,
                    Rotation = adofai.Settings.Rotation,
                    Zoom = adofai.Settings.Zoom,
                }
            };
            List<CircleActions> actions = new List<CircleActions>();

            // 액션 적용.
            foreach (var action in adofai.Actions)
            {
                CircleActions newAction = new CircleActions
                {
                    Floor = action.Floor,
                    EventType = convertEventType(action.EventType),
                    RelativeTo = action.RelativeTo,
                    BpmMultiplier = (float)action.BPMMultiplier,
                    BeatsPerMinute = (float)action.BeatsPerMinute,
                    Ease = convertEasing(action.Ease),
                    Duration = action.Duration,
                    Rotation = action.Rotation,
                    AngleOffset = action.AngleOffset,
                    Position = action.Position,
                    Zoom = action.Zoom,
                    Repetitions = action.Repetitions,
                    Interval = action.Interval,
                    Tag = action.Tag,
                    EventTag = action.EventTag,
                };

                if (action.EventType != EventType.MoveCamera)
                    newAction.RelativeTo = null;

                newAction.SpeedType = action.SpeedType;
                if (action.BeatsPerMinute != 0 && newAction.SpeedType == null)
                    newAction.SpeedType = CircleSpeedType.Bpm;
                else if (action.BPMMultiplier != 0 && newAction.SpeedType == null)
                    newAction.SpeedType = CircleSpeedType.Multiplier;

                actions.Add(newAction);
            }

            circle.Actions = actions.ToArray();

            return circle;
        }

        private bool convertToggle(Toggle toggle)
        {
            switch (toggle)
            {
                case Toggle.Disabled:
                    return false;

                case Toggle.Enabled:
                    return true;

                default:
                    return false;
            }
        }

        private CircleEasing convertEasing(Ease ease)
        {
            switch (ease)
            {
                case Ease.Unset:
                    return CircleEasing.None;

                case Ease.OutSine:
                    return CircleEasing.OutSine;

                case Ease.OutQuint:
                    return CircleEasing.OutQuint;

                case Ease.OutQuart:
                    return CircleEasing.OutQuart;

                case Ease.OutQuad:
                    return CircleEasing.OutQuad;

                case Ease.OutExpo:
                    return CircleEasing.OutExpo;

                case Ease.OutElastic:
                    return CircleEasing.OutElastic;

                case Ease.OutCubic:
                    return CircleEasing.OutCubic;

                case Ease.OutCirc:
                    return CircleEasing.OutCirc;

                case Ease.OutBounce:
                    return CircleEasing.OutBounce;

                case Ease.OutBack:
                    return CircleEasing.OutBack;

                // 비슷한 에이징.
                case Ease.OutFlash:
                    return CircleEasing.OutExpo;

                case Ease.Linear:
                    return CircleEasing.None;

                case Ease.InSine:
                    return CircleEasing.InSine;

                case Ease.InQuint:
                    return CircleEasing.InQuint;

                case Ease.InQuart:
                    return CircleEasing.InQuart;

                case Ease.InQuad:
                    return CircleEasing.InQuad;

                case Ease.InOutSine:
                    return CircleEasing.InOutSine;

                case Ease.InOutQuint:
                    return CircleEasing.InOutQuint;

                case Ease.InOutQuart:
                    return CircleEasing.InOutQuart;

                case Ease.InOutQuad:
                    return CircleEasing.InOutQuad;

                case Ease.InOutExpo:
                    return CircleEasing.InOutExpo;

                case Ease.InOutElastic:
                    return CircleEasing.InOutElastic;

                case Ease.InOutCubic:
                    return CircleEasing.InOutCubic;

                case Ease.InOutCirc:
                    return CircleEasing.InOutCirc;

                case Ease.InOutBounce:
                    return CircleEasing.InOutBounce;

                case Ease.InOutBack:
                    return CircleEasing.InOutBack;

                case Ease.InExpo:
                    return CircleEasing.InExpo;

                case Ease.InElastic:
                    return CircleEasing.InElastic;

                case Ease.InCubic:
                    return CircleEasing.InCubic;

                case Ease.InCirc:
                    return CircleEasing.InCirc;

                case Ease.InBounce:
                    return CircleEasing.InBounce;

                case Ease.InBack:
                    return CircleEasing.InBack;

                case Ease.InFlash:
                    return CircleEasing.InExpo;

                default:
                    return CircleEasing.None;
            }
        }

        private CircleEventType convertEventType(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.Twirl:
                    return CircleEventType.Twirl;

                case EventType.SetSpeed:
                    return CircleEventType.SetSpeed;

                case EventType.SetPlanetRotation:
                    return CircleEventType.SetPlanetRotation;

                case EventType.MoveCamera:
                    return CircleEventType.MoveCamera;

                case EventType.RepeatEvents:
                    return CircleEventType.RepeatEvents;

                default:
                    return CircleEventType.Other;
            }
        }
    }
}
