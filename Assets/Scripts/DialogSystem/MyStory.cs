using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class MyStory : Ink.Runtime.Story
{
    public string currentCharacter;

    public bool canChoose
    {
        get { return currentChoices.Count != 0; }
    }

    public MyStory() : base(string.Empty) { }

    public MyStory(string text) : base(text) { }

    public new string Continue()
    {
        string dialogue = base.Continue();

        string[] splits = dialogue.Split(':');

        // Get the current talking character
        if (splits.Length > 0)
            currentCharacter = splits[0];
        else
            currentCharacter = "";

        return dialogue;
    }

    public List<string> GetChoices()
    {
        return currentChoices.ConvertAll(x => x.text);
    }
}

