﻿using static System.Math;
using static Colourful.Internals.MathUtils;

namespace Colourful;

/// <summary>
/// Can compute chromaticity from CCT (Correlated color temperature) and also
/// compute CCT of given chromaticity.
/// </summary>
public static class CCTConverter
{
    /// <summary>
    /// Returns chromaticity of given CCT (specified in K).
    /// </summary>
    public static xyChromaticity GetChromaticityOfCCT(in double temperature)
    {
        // approximation described here: http://en.wikipedia.org/wiki/Planckian_locus#Approximation

        double x_c;

        if (temperature <= 4000) // correctly 1667 <= T <= 4000
            x_c = -0.2661239 * (1000000000 / Pow3(in temperature)) - 0.2343580 * (1000000 / Pow2(in temperature)) + 0.8776956 * (1000 / temperature) + 0.179910;

        else // correctly 4000 <= T <= 25000
            x_c = -3.0258469 * (1000000000 / Pow3(in temperature)) + 2.1070379 * (1000000 / Pow2(in temperature)) + 0.2226347 * (1000 / temperature) + 0.240390;

        double y_c;

        if (temperature <= 2222) // correctly 1667 <= T <= 2222
            y_c = -1.1063814 * Pow3(in x_c) - 1.34811020 * Pow2(in x_c) + 2.18555832 * x_c - 0.20219683;

        else if (temperature <= 4000) // correctly 2222 <= T <= 4000
            y_c = -0.9549476 * Pow3(in x_c) - 1.37418593 * Pow2(in x_c) + 2.09137015 * x_c - 0.16748867;

        else // correctly 4000 <= T <= 25000
            y_c = +3.0817580 * Pow3(in x_c) - 5.87338670 * Pow2(in x_c) + 3.75112997 * x_c - 0.37001483;

        return new xyChromaticity(in x_c, in y_c);
    }

    /// <summary>
    /// Returns CCT (specified in K) of given chromaticity.
    /// Ranges usually from around 0 to 25000.
    /// </summary>
    public static double GetCCTOfChromaticity(in xyChromaticity chromaticity)
    {
        // approximation described here: http://en.wikipedia.org/wiki/Color_temperature#Approximation

        const double xe = 0.3366;
        const double ye = 0.1735;
        const double A0 = -949.86315;
        const double A1 = 6253.80338;
        const double t1 = 0.92159;
        const double A2 = 28.70599;
        const double t2 = 0.20039;
        const double A3 = 0.00004;
        const double t3 = 0.07125;
        var n = (chromaticity.x - xe) / (chromaticity.y - ye);
        var cct = A0 + A1 * Exp(-n / t1) + A2 * Exp(-n / t2) + A3 * Exp(-n / t3);
        return cct;
    }
}
