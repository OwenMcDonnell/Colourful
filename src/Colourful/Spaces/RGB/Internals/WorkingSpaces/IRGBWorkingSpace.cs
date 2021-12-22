﻿namespace Colourful.Internals;

/// <summary>
/// RGB working color space.
/// </summary>
public interface IRGBWorkingSpace
{
    /// <summary>
    /// White point of the color space.
    /// </summary>
    XYZColor WhitePoint { get; }

    /// <summary>
    /// Chromaticity of the primaries.
    /// </summary>
    RGBPrimaries Primaries { get; }

    /// <summary>
    /// The companding function associated with the RGB color system.
    /// Used for conversion to XYZ and backwards.
    /// See this for more information:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
    /// </summary>
    ICompanding Companding { get; }
}
