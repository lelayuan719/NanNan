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
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
    }

    void OnMouseOver(){
        Cursor.SetCursor(texture,Vector2.zero,CursorMode.Auto);
    }
    void OnMouseExit(){
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }
}
