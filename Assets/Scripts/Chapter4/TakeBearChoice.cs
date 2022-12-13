using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeBearChoice : ChoiceHandler
{
    [SerializeField] private string NextScene;

    private SceneChange sceneChange;

    private void Start()
    {
        sceneChange = GetComponent<SceneChange>();
    }

    public override void MakeChoice(int choiceIndex)
    {
        switch (choiceIndex)
        {
            case 0:
                GetComponent<SpriteRenderer>().enabled = false;
                GameManager.GM.inventory.GiveItem("bear");
                sceneChange.ChangeScene();
            break;
            case 1:
                break;
        }
    }
}
