using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorClicked : MonoBehaviour
{
    public string NextScene;
    public Dialog invalidDialog;
    [SerializeField] SceneTransitionSettings outSceneTransition;
    [SerializeField] SceneTransitionSettings inSceneTransition;
    public bool open = true;

    // Start is called before the first frame update
    void OnMouseDown(){
        if (open)
        {
            GameManager.GM.LoadScene(NextScene, outSceneTransition, inSceneTransition);
        }
        else if (invalidDialog)
        {
            invalidDialog.TriggerDialog();
        }
    }

    public void Unlock()
    {
        open = true;
    }
}
