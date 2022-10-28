using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAmuletChoice : ChoiceHandler
{
    public override void MakeChoice(int choiceIndex)
    {
        if (choiceIndex == 0)
            GameManager.GM.GiveAmulet(true);
        else
            GameManager.GM.GiveAmulet(false);
    }
}
