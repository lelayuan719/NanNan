using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] float minAlpha = 0;
    [Range(0f, 1f)] [SerializeField] float maxAlpha = 1;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Fade(float time, int direction)
    {
        StartCoroutine(FadeCR(time, direction));
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
        float startAlpha = sr.color.a;
        float targetAlpha = direction == +1 ? maxAlpha : minAlpha; // Set target to either min or max
        float elapsedTime;
        float a;

        do
        {
            // Set color to interpolated alpha
            elapsedTime = Time.time - startTime;
            a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / time);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
            yield return new WaitForEndOfFrame();
        }
        while (elapsedTime < time);

        // Set color to final value
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, targetAlpha);
    }
}
