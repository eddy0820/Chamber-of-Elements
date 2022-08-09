using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class HorizontalLineAttribute : PropertyAttribute
{
    public const float DefaultHeight = 2.0f;

    public const EColor DefaultColor = EColor.Gray;

    public float Height { get; private set; }
    public EColor Color { get; private set; }

    public HorizontalLineAttribute(float height = DefaultHeight, EColor color = DefaultColor)
    {
        Height = height;
        Color = color;
    }
}
