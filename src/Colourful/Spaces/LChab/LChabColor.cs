﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful;

/// <summary>
/// CIE L*C*h°, cylindrical form of <see cref="LabColor">CIE L*a*b* (1976)</see>.
/// </summary>
[SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "They're immutable, and we don't need getters.")]
public readonly struct LChabColor : IColorSpace, IColorVector, IEquatable<LChabColor>
{
    #region Constructor

    /// <param name="l">L* (lightness) (from 0 to 100).</param>
    /// <param name="c">C* (chroma) (from 0 to 100).</param>
    /// <param name="h">h° (hue in degrees) (from 0 to 360).</param>
    public LChabColor(in double l, in double c, in double h)
    {
        L = l;
        C = c;
        this.h = h;
    }

    /// <param name="vector"><see cref="Vector" />, expected 3 dimensions.</param>
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not checking this for brevity.")]
    public LChabColor(in double[] vector)
        : this(in vector[0], in vector[1], in vector[2])
    {
    }

    #endregion

    #region Channels

    /// <summary>
    /// L* (lightness).
    /// Ranges usuallyfrom 0 to 100.
    /// </summary>
    public readonly double L;

    /// <summary>
    /// C* (chroma).
    /// Ranges usually from 0 to 100.
    /// </summary>
    public readonly double C;

    /// <summary>
    /// h° (hue in degrees).
    /// Ranges usually from 0 to 360.
    /// </summary>
    public readonly double h;

    /// <inheritdoc />
    [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Array for performance reasons.")]
    public double[] Vector => new[] { L, C, h };

    #endregion

    #region Saturation

    /// <summary>
    /// Computes saturation of the color (chroma normalized by lightness).
    /// Ranges from 0 to 100.
    /// </summary>
    public double Saturation => CylindricalFormulas.GetSaturation(in L, in C);

    /// <summary>
    /// Constructs the color using saturation instead of chromas.
    /// </summary>
    public static LChabColor FromSaturation(in double lightness, in double hue, in double saturation)
    {
        var chroma = CylindricalFormulas.GetChroma(in saturation, in lightness);
        return new LChabColor(in lightness, in chroma, in hue);
    }

    #endregion

    #region Equality

    /// <inheritdoc />
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public bool Equals(LChabColor other) =>
        L == other.L &&
        C == other.C &&
        h == other.h;

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is LChabColor other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = L.GetHashCode();
            hashCode = (hashCode * 397) ^ C.GetHashCode();
            hashCode = (hashCode * 397) ^ h.GetHashCode();
            return hashCode;
        }
    }

    /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
    [ExcludeFromCodeCoverage]
#endif
    public static bool operator ==(LChabColor left, LChabColor right) => Equals(left, right);

    /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
    [ExcludeFromCodeCoverage]
#endif
    public static bool operator !=(LChabColor left, LChabColor right) => !Equals(left, right);

    #endregion

    #region Deconstructor

    /// <summary>
    /// Deconstructs color into individual channels.
    /// </summary>
    public void Deconstruct(out double l, out double c, out double h)
    {
        l = L;
        c = C;
        h = this.h;
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    public override string ToString() => string.Format(CultureInfo.InvariantCulture, "LChab [L={0:0.##}, C={1:0.##}, h={2:0.##}]", L, C, h);

    #endregion
}
