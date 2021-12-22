﻿using System;
using System.Diagnostics.CodeAnalysis;
using static System.Math;

namespace Colourful.Internals;

/// <summary>
/// Rec. 709 companding function.
/// </summary>
/// <remarks>
/// http://en.wikipedia.org/wiki/Rec._709
/// </remarks>
public class Rec709Companding : ICompanding, IEquatable<Rec709Companding>
{
    /// <inheritdoc />
    public double ConvertToLinear(in double nonLinearChannel)
    {
        var V = nonLinearChannel;
        var L = V < 0.081 ? V / 4.5 : Pow((V + 0.099) / 1.099, 1 / 0.45);
        return L;
    }

    /// <inheritdoc />
    public double ConvertToNonLinear(in double linearChannel)
    {
        var L = linearChannel;
        var V = L < 0.018 ? 4.5 * L : 1.099 * Pow(L, y: 0.45) - 0.099;
        return V;
    }

    #region Equality

    /// <inheritdoc />
    public bool Equals(Rec709Companding other)
    {
        if (other == null)
            return false;

        return true;
    }

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is Rec709Companding;

    /// <inheritdoc />
    public override int GetHashCode() => typeof(Rec709Companding).GetHashCode();

    /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
    [ExcludeFromCodeCoverage]
#endif
    public static bool operator ==(Rec709Companding left, Rec709Companding right) => Equals(left, right);

    /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
    [ExcludeFromCodeCoverage]
#endif
    public static bool operator !=(Rec709Companding left, Rec709Companding right) => !Equals(left, right);

    #endregion
}
