using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader : Fader
{
    SpriteRenderer sr;

    protected override void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected override float InitStartValue()
    {
        return sr.color.a;
    }

    protected override void SetValue(float value)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, value);
    }
}
