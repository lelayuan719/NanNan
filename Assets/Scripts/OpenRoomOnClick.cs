using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenRoomOnClick : MonoBehaviour
{

   // public Texture2D texture;

    //public GameObject pauseRootScene;
    // public GameObject Room;
    //private bool opened;
    public Texture2D texture;
    public GameObject Nannan;

    // Start is called before the first frame update
    private void Start()
    {
        //pauseRootScene = GameObject.FindWithTag("pause");
        // Room.SetActive(false);
    }
    void OnMouseDown()
    {

        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);

        Nannan.transform.position = new Vector3(50.41076f, -0.02854919f, 82.30728f);
        // Room.SetActive(true);
        //pauseRootScene.SetActive(false);
    }
    public void CloseRoom()
    {
        //print("closing 1");
        /*if (Room.activeSelf)
        {
            //print("closing");
            Room.SetActive(false);
            //pauseRootScene.SetActive(true);
        }
        else
        {
            //print("can't close before open");
        }*/
    }

    void OnMouseOver()
    {
        Cursor.SetCursor(texture,Vector2.zero,CursorMode.Auto);
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }
}