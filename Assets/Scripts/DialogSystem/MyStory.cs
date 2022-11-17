using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.Text.RegularExpressions;

public class MyStory : Ink.Runtime.Story
{
    public static Regex emphRgx = new Regex(@"\[\[(.*?)\]\]", RegexOptions.Compiled);
    public string currentCharacter;

    public bool canChoose
    {
        get { return currentChoices.Count != 0; }
    }

    public MyStory() : base(string.Empty) { }

    public MyStory(string text) : base(text) { }

    public new string Continue()
    {
        string dialog = base.Continue();

        // Get the current talking character
        string[] splits = dialog.Split(':');
        if (splits.Length > 0)
            currentCharacter = splits[0];
        else
            currentCharacter = "";

        // Add emphasis colors if necessary
        dialog = emphRgx.Replace(dialog, @"<color=yellow>$1</color>");

        return dialog;
    }

    public new void ChooseChoiceIndex(int idx)
    {
        base.ChooseChoiceIndex(idx);

        // Sees if we are at the end, then rewinds if necessary
        var storedState = state.ToJson();
        Continue();

        if (currentText != "")
        {
            // return to original state
            state.LoadJson(storedState);
        }
    }

    public List<string> GetChoices()
    {
        return currentChoices.ConvertAll(x => x.text);
    }
}

