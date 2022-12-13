using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorClicked : MonoBehaviour
{
    public Dialog invalidDialog;
    SceneChange sceneChange;
    public bool open = true;

    private void Start()
    {
        sceneChange = GetComponent<SceneChange>();
    }

    // Start is called before the first frame update
    void OnMouseDown(){
        if (open)
        {
            sceneChange.ChangeScene();
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
