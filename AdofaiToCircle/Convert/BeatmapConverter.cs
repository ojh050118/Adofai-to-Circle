﻿using AdofaiToCircle.Adofai;

namespace AdofaiToCircle.Convert
{
    public abstract class BeatmapConverter<TTarget, TResult>
        where TTarget : class
        where TResult : class
    {
        public abstract TResult Convert(TTarget level);

        public float[] ParseAngleData(string pathData) => AdofaiParser.ParseAngleData(pathData);

        public Toggle ParseToggle(string toggle) => AdofaiParser.ParseToggle(toggle);

        public Relativity ParseRelativity(string relativeTo) => AdofaiParser.ParseRelativity(relativeTo);

        public Ease ParseEase(string ease) => AdofaiParser.ParseEase(ease);
    }
}
