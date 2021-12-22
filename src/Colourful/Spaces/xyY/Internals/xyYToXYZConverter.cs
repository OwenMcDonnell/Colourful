﻿namespace Colourful.Internals;

/// <inheritdoc />
public class xyYToXYZConverter : IColorConverter<xyYColor, XYZColor>
{
    /// <inheritdoc />
    public XYZColor Convert(in xyYColor sourceColor)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (sourceColor.y == 0)
            return new XYZColor(x: 0, y: 0, sourceColor.Luminance);

        var X = sourceColor.x * sourceColor.Luminance / sourceColor.y;
        var Y = sourceColor.Luminance;
        var Z = (1 - sourceColor.x - sourceColor.y) * Y / sourceColor.y;

        var targetColor = new XYZColor(in X, in Y, in Z);
        return targetColor;
    }
}
