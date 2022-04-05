using AdofaiToCircle.Adofai.Elements;

namespace AdofaiToCircle.Adofai
{
    public class AdofaiFile
    {
        public string PathData { get; set; }
        public float[] AngleData { get; set; }
        public AdofaiSettings Settings { get; set; }
        public AdofaiAction[] Actions { get; set; }
    }
}
