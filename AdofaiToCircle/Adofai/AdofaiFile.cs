namespace AdofaiToCircle.Adofai
{
    public class AdofaiFile
    {
        public string PathData { get; set; }
        public float[] AngleData { get; set; }
        public Settings Settings { get; set; }
        public Elements.Action[] Actions { get; set; }
    }
}
