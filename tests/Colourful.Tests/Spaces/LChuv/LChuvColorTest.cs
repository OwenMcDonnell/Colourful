﻿using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests;

public class LChuvColorTest
{
    [Fact]
    public void Equals_Same()
    {
        var first = new LChuvColor(l: 10, c: 20.5, h: 45.445);
        var second = new LChuvColor(l: 10, c: 20.5, h: 45.445);
        CustomAssert.EqualsWithHashCode(first, second);
    }

    [Fact]
    public void Equals_Different()
    {
        var first = new LChuvColor(l: 11, c: 20.5, h: 45.445);
        var second = new LChuvColor(l: 10, c: 20.5, h: 45.445);
        CustomAssert.NotEqualsWithHashCode(first, second);
    }

    [Fact]
    public void VectorCtor()
    {
        var first = new LChuvColor(l: 10, c: 20.5, h: 45.445);
        var vector = new[] { 10, 20.5, 45.445 };
        var second = new LChuvColor(vector);
        CustomAssert.EqualsWithHashCode(first, second);
        Assert.Equal(vector, second.Vector);
    }

    [Fact]
    public void FromSaturationCtor()
    {
        var first = new LChuvColor(l: 10, c: 3, h: 20);
        const double saturation = 30d;
        var second = LChuvColor.FromSaturation(lightness: 10, hue: 20, saturation);
        CustomAssert.EqualsWithHashCode(first, second);
        Assert.Equal(saturation, second.Saturation);
    }

    [Fact]
    public void ToString_Simple()
    {
        var color = new LChuvColor(l: 10, c: 20.5, h: 45.445);
        Assert.Equal("LChuv [L=10, C=20.5, h=45.45]", color.ToString());
    }

    [Fact]
    public void Dctor()
    {
        const double l1 = 10;
        const double c1 = 20.5;
        const double h1 = 45.445;
        var (l2, c2, h2) = new LChuvColor(l1, c1, h1);
        Assert.Equal(l1, l2);
        Assert.Equal(c1, c2);
        Assert.Equal(h1, h2);
    }
}
