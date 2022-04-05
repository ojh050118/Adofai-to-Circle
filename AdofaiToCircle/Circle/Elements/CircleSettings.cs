using AdofaiToCircle.Adofai;

namespace AdofaiToCircle.Circle.Elements
{
    public struct CircleSettings
    {
        public string Artist;
        public string Song;
        public string SongFileName;
        public string Author;
        public bool SeparateCountdownTime;
        public int PreviewSongStart;
        public int PreviewSongDuration;
        public string BeatmapDesc;
        public int Difficulty;
        public float Bpm;
        public int Volume;
        public int Offset;
        public float VidOffset;
        public int Pitch;
        public int CountdownTicks;
        public string BgImage;
        public string BgVideo;
        public CircleEasing PlanetEasing;
        public Relativity RelativeTo;
        public float[] Position;
        public float Rotation;
        public float Zoom;
    }
}
