using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorClicked : MonoBehaviour
{

    public Texture2D texture;
    public string NextScene;

    // Start is called before the first frame update
    void OnMouseDown(){
        SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    
}
