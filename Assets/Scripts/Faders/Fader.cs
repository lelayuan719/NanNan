using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fader : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] float min = 0;
    [Range(0f, 1f)] [SerializeField] float max = 1;

    Coroutine cr;

    protected virtual void Awake()
    {

    }

    private void Reset()
    {
        Awake();
        max = InitStartValue();
    }

    protected abstract float InitStartValue();
    
    protected abstract void SetValue(float value);

    public void Fade(float time, int direction)
    {
        if (cr != null) StopCoroutine(cr);
        cr = StartCoroutine(FadeCR(time, direction));
    }

    public void FadeIn(float time)
    {
        Fade(time, +1);
    }

    public void FadeOut(float time)
    {
        Fade(time, -1);
    }

    IEnumerator FadeCR(float time, int direction)
    {
        float startTime = Time.time;
        float startValue = InitStartValue();
        float endValue = direction == +1 ? max : min; // Set end to either min or max
        float elapsedTime;
        float value;

        do
        {
            // Set current value to interpolated value
            elapsedTime = Time.time - startTime;
            value = Mathf.Lerp(startValue, endValue, elapsedTime / time);
            SetValue(value);
            yield return new WaitForEndOfFrame();
        }
        while (elapsedTime < time);

        // Set value to final value
        SetValue(endValue);
    }
}
