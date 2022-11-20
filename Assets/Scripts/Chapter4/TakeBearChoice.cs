using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeBearChoice : ChoiceHandler
{
    [SerializeField] private string NextScene;

    [SerializeField] SceneTransitionSettings outSceneTransition;
    [SerializeField] SceneTransitionSettings inSceneTransition;

    public override void MakeChoice(int choiceIndex)
    {
        switch (choiceIndex)
        {
            case 0:
                GameManager.GM.inventory.GiveItem("bear");
                GameManager.GM.LoadScene(NextScene, outSceneTransition, inSceneTransition);
            break;
            case 1:
                break;
        }
    }
}
