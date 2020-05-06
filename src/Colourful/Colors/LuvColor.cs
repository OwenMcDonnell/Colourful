﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE L*u*v* (1976) color
    /// </summary>
    public readonly struct LuvColor : IColorVector, IEquatable<LuvColor>
    {
        /// <summary>
        /// D65 standard illuminant.
        /// Used when reference white is not specified explicitly.
        /// </summary>
        public static readonly XYZColor DefaultWhitePoint = Illuminants.D65;

        #region Constructor

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="u">u* (usually from -100 to 100)</param>
        /// <param name="v">v* (usually from -100 to 100)</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint" /> as white point.</remarks>
        public LuvColor(in double l, in double u, in double v) : this(in l, in u, in v, in DefaultWhitePoint)
        {
        }

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="u">u* (usually from -100 to 100)</param>
        /// <param name="v">v* (usually from -100 to 100)</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants" />)</param>
        public LuvColor(in double l, in double u, in double v, in XYZColor whitePoint)
        {
            L = l;
            this.u = u;
            this.v = v;
            _whitePoint = whitePoint;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint" /> as white point.</remarks>
        public LuvColor(in double[] vector) : this(in vector, in DefaultWhitePoint)
        {
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants" />)</param>
        public LuvColor(in double[] vector, in XYZColor whitePoint)
            : this(in vector[0], in vector[1], in vector[2], in whitePoint)
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// L* (lightness)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 100.
        /// </remarks>
        public double L { get; }

        /// <summary>
        /// u*
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// </remarks>
        public double u { get; }

        /// <summary>
        /// v*
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// </remarks>
        public double v { get; }

        /// <remarks>
        /// <see cref="Illuminants" />
        /// </remarks>
        public XYZColor WhitePoint => _whitePoint ?? DefaultWhitePoint;

        private readonly XYZColor? _whitePoint;

        /// <summary>
        /// <see cref="IColorVector" />
        /// </summary>
        public double[] Vector => new[] { L, u, v };

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(LuvColor other) =>
            L == other.L &&
            u == other.u &&
            v == other.v;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LuvColor other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ u.GetHashCode();
                hashCode = (hashCode * 397) ^ v.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LuvColor left, LuvColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LuvColor left, LuvColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "Luv [L={0:0.##}, u={1:0.##}, v={2:0.##}]", L, u, v);

        #endregion
    }
}