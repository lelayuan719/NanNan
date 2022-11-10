using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoChoice : ChoiceHandler
{
    public SpriteRenderer circle;

    public override void MakeChoice(int choiceIndex)
    {
        switch (choiceIndex) {
            case 0: // Red
                circle.color = new Color(1f, 0f, 0f);
                break;
            case 1: // Blue
                circle.color = new Color(0f, 0f, 1f);
                break;
            case 2: // Purple
                circle.color = new Color(0.58f, 0f, 0.83f);
                break;
        }
    }
}
