using AdofaiToCircle.Adofai;
using AdofaiToCircle.Circle;
using AdofaiToCircle.Circle.Elements;

namespace AdofaiToCircle.Convert
{
    public class CircleBeatmapConverter : BeatmapConverter<AdofaiFile, CircleFile>
    {
        public override CircleFile Convert(AdofaiFile adofai)
        {
            if (adofai.AngleData == null)
                adofai.AngleData = parsePathData(adofai.PathData);

            CircleFile circle = new CircleFile
            {
                AngleData = adofai.AngleData,
                Settings = new Settings
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
                    PlanetEasing = convertEasing(parseEase(adofai.Settings.PlanetEase)),
                    PreviewSongStart = adofai.Settings.PreviewSongStart,
                    PreviewSongDuration = adofai.Settings.PreviewSongDuration,
                    SeparateCountdownTime = convertToggle(parseToggle(adofai.Settings.SeparateCountdownTime)),
                    Song = adofai.Settings.Song,
                    SongFileName = adofai.Settings.SongFilename,
                    RelativeTo = parseRelativity(adofai.Settings.RelativeTo),
                    Position = adofai.Settings.Position,
                    Rotation = adofai.Settings.Rotation,
                    Zoom = adofai.Settings.Zoom,
                }
            };
            List<Actions> actions = new List<Actions>();

            // 액션 적용.
            foreach (var action in adofai.Actions)
            {
                Actions newAction = new Actions
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

                if (action.EventType != AdofaiEventType.MoveCamera)
                    newAction.RelativeTo = null;

                newAction.SpeedType = action.SpeedType;
                if (action.BeatsPerMinute != 0 && newAction.SpeedType == null)
                    newAction.SpeedType = SpeedType.Bpm;
                else if (action.BPMMultiplier != 0 && newAction.SpeedType == null)
                    newAction.SpeedType = SpeedType.Multiplier;

                actions.Add(newAction);
            }

            circle.Actions = actions.ToArray();

            return circle;
        }

        private float[] parsePathData(string pathData)
        {
            List<float> angleData = new List<float>();

            foreach (char angle in pathData)
            {
                switch (angle)
                {
                    case 'R':
                        angleData.Add(0);
                        break;

                    case 'U':
                        angleData.Add(90);
                        break;

                    case 'D':
                        angleData.Add(270);
                        break;

                    case 'L':
                        angleData.Add(180);
                        break;

                    case 'J':
                        angleData.Add(30);
                        break;

                    case 'T':
                        angleData.Add(60);
                        break;

                    case 'M':
                        angleData.Add(330);
                        break;

                    case 'B':
                        angleData.Add(300);
                        break;

                    case 'F':
                        angleData.Add(240);
                        break;

                    case 'N':
                        angleData.Add(210);
                        break;

                    case 'H':
                        angleData.Add(150);
                        break;

                    case 'G':
                        angleData.Add(120);
                        break;

                    case 'Q':
                        angleData.Add(135);
                        break;

                    case 'E':
                        angleData.Add(45);
                        break;

                    case 'C':
                        angleData.Add(315);
                        break;

                    case 'Z':
                        angleData.Add(225);
                        break;

                    case '!':
                        angleData.Add(999);
                        break;

                    case 'p':
                        angleData.Add(15);
                        break;

                    case 'o':
                        angleData.Add(75);
                        break;

                    case 'q':
                        angleData.Add(105);
                        break;

                    case 'W':
                        angleData.Add(165);
                        break;

                    case 'x':
                        angleData.Add(195);
                        break;

                    case 'V':
                        angleData.Add(255);
                        break;

                    case 'Y':
                        angleData.Add(285);
                        break;

                    case 'A':
                        angleData.Add(345);
                        break;

                    // 처리 할 수 없는 타일.
                    default:
                        angleData.Add(0);
                        break;
                }
            }

            return angleData.ToArray();
        }

        private Toggle parseToggle(string toggle)
        {
            foreach (Toggle parsed in Enum.GetValues(typeof(Toggle)))
            {
                if (toggle == parsed.ToString())
                    return parsed;
            }

            return Toggle.Disabled;
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

        private AdofaiEase parseEase(string ease)
        {
            foreach (AdofaiEase adofaiEase in Enum.GetValues(typeof(AdofaiEase)))
            {
                if (ease == adofaiEase.ToString())
                    return adofaiEase;
            }

            return AdofaiEase.Unset;
        }

        private Relativity parseRelativity(string relativeTo)
        {
            foreach (Relativity relativity in Enum.GetValues(typeof(Relativity)))
            {
                if (relativeTo == relativity.ToString())
                    return relativity;
            }

            return Relativity.Player;
        }

        private Easing convertEasing(AdofaiEase ease)
        {
            switch (ease)
            {
                case AdofaiEase.Unset:
                    return Easing.None;

                case AdofaiEase.OutSine:
                    return Easing.OutSine;

                case AdofaiEase.OutQuint:
                    return Easing.OutQuint;

                case AdofaiEase.OutQuart:
                    return Easing.OutQuart;

                case AdofaiEase.OutQuad:
                    return Easing.OutQuad;

                case AdofaiEase.OutExpo:
                    return Easing.OutExpo;

                case AdofaiEase.OutElastic:
                    return Easing.OutElastic;

                case AdofaiEase.OutCubic:
                    return Easing.OutCubic;

                case AdofaiEase.OutCirc:
                    return Easing.OutCirc;

                case AdofaiEase.OutBounce:
                    return Easing.OutBounce;

                case AdofaiEase.OutBack:
                    return Easing.OutBack;

                // 비슷한 에이징.
                case AdofaiEase.OutFlash:
                    return Easing.OutExpo;

                case AdofaiEase.Linear:
                    return Easing.None;

                case AdofaiEase.InSine:
                    return Easing.InSine;

                case AdofaiEase.InQuint:
                    return Easing.InQuint;

                case AdofaiEase.InQuart:
                    return Easing.InQuart;

                case AdofaiEase.InQuad:
                    return Easing.InQuad;

                case AdofaiEase.InOutSine:
                    return Easing.InOutSine;

                case AdofaiEase.InOutQuint:
                    return Easing.InOutQuint;

                case AdofaiEase.InOutQuart:
                    return Easing.InOutQuart;

                case AdofaiEase.InOutQuad:
                    return Easing.InOutQuad;

                case AdofaiEase.InOutExpo:
                    return Easing.InOutExpo;

                case AdofaiEase.InOutElastic:
                    return Easing.InOutElastic;

                case AdofaiEase.InOutCubic:
                    return Easing.InOutCubic;

                case AdofaiEase.InOutCirc:
                    return Easing.InOutCirc;

                case AdofaiEase.InOutBounce:
                    return Easing.InOutBounce;

                case AdofaiEase.InOutBack:
                    return Easing.InOutBack;

                case AdofaiEase.InExpo:
                    return Easing.InExpo;

                case AdofaiEase.InElastic:
                    return Easing.InElastic;

                case AdofaiEase.InCubic:
                    return Easing.InCubic;

                case AdofaiEase.InCirc:
                    return Easing.InCirc;

                case AdofaiEase.InBounce:
                    return Easing.InBounce;

                case AdofaiEase.InBack:
                    return Easing.InBack;

                case AdofaiEase.InFlash:
                    return Easing.InExpo;

                default:
                    return Easing.None;
            }
        }

        private EventType convertEventType(AdofaiEventType eventType)
        {
            switch (eventType)
            {
                case AdofaiEventType.Twirl:
                    return EventType.Twirl;

                case AdofaiEventType.SetSpeed:
                    return EventType.SetSpeed;

                case AdofaiEventType.SetPlanetRotation:
                    return EventType.SetPlanetRotation;

                case AdofaiEventType.MoveCamera:
                    return EventType.MoveCamera;

                case AdofaiEventType.RepeatEvents:
                    return EventType.RepeatEvents;

                default:
                    return EventType.Other;
            }
        }
    }
}
