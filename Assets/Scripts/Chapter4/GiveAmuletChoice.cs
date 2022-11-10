using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAmuletChoice : ChoiceHandler
{
    public override void MakeChoice(int choiceIndex)
    {
        switch (choiceIndex)
        {
            case 0:
                GameManager.GM.GiveAmulet(true);
                break;
            case 1:
                GameManager.GM.GiveAmulet(false);
                break;
        }
    }
}
