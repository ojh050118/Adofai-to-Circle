namespace AdofaiToCircle.IO.Convert
{
    public abstract class BeatmapConverter<TTarget, TResult>
        where TTarget : class
        where TResult : class
    {
        public abstract TResult Convert(TTarget level);
    }
}
