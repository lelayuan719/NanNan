using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLock : MonoBehaviour
{

    public Texture2D texture;

    // Start is called before the first frame update
    void OnMouseDown(){
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }

    void OnMouseOver(){
        Cursor.SetCursor(texture,Vector2.zero,CursorMode.Auto);
    }
    void OnMouseExit(){
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }
}