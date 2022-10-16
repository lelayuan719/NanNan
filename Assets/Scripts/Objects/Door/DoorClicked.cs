using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorClicked : MonoBehaviour
{
    public string NextScene;
    public Dialog invalidDialog;
    public bool open = true;

    // Start is called before the first frame update
    void OnMouseDown(){
        if (open)
        {
            SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        } else if (invalidDialog)
        {
            invalidDialog.TriggerDialog();
        }
    }

    public void Unlock()
    {
        open = true;
    }
}
