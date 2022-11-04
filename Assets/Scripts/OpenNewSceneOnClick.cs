using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenNewSceneOnClick : MonoBehaviour
{

   // public Texture2D texture;

    public GameObject pauseRootScene;
    public GameObject Puzzle;
    //private bool opened;

    // Start is called before the first frame update
    private void Start()
    {
        //print("whatwhywhere");
        //pauseRootScene = GameObject.FindWithTag("pause");
        Puzzle.SetActive(false);
        //opened = false;
        // pauseRootScene.SetActive(true);

    }
    void OnMouseDown()
    {
        // print(pauseRootScene.isActive())
        // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Puzzle.SetActive(true);
        //pauseRootScene.SetActive(false);
       
        // print(Puzzle.name);
        //Time.timeScale = 0.00f;
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(innerScene));
        //opened = true;

        //SceneManager.LoadScene(innerScene, LoadSceneMode.Additive);
  
        
    }
    public void ClosePuzzle()
    {
        print("closing 1");
        if (Puzzle.activeSelf)
        {
            print("closing");
            Puzzle.SetActive(false);
            //pauseRootScene.SetActive(true);
        }
        else
        {
            print("can't close before open");
        }
    }

    void OnMouseOver()
    {
        //Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }
    void OnMouseExit()
    {
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
