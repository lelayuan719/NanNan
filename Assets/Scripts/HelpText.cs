using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpText : MonoBehaviour
{
    [SerializeField] float fadeTime;
    [SerializeField] float stayTime;

    TextFader fader;
    TextMeshProUGUI helpText;
    Coroutine cr;

    private void Awake()
    {
        fader = GetComponent<TextFader>();
        helpText = GetComponent<TextMeshProUGUI>();
    }

    public void ShowHelpText(string text)
    {
        helpText.text = text;
        if (cr != null) StopCoroutine(cr);
        cr = StartCoroutine(ShowHelpTextCR());
    }

    IEnumerator ShowHelpTextCR()
    {
        fader.FadeIn(fadeTime);
        yield return new WaitForSeconds(fadeTime+stayTime);
        fader.FadeOut(fadeTime);
    }
}
