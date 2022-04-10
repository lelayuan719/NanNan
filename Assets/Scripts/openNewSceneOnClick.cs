using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openNewSceneOnClick : MonoBehaviour
{

   // public Texture2D texture;

    public GameObject pauseRootScene;
    public GameObject Puzzle;

    // Start is called before the first frame update
    private void Start()
    {
        //print("whatwhywhere");
        pauseRootScene = GameObject.FindWithTag("pause");
        Puzzle.SetActive(false);
       // pauseRootScene.SetActive(true);

    }
    void OnMouseDown()
    {
        // print(pauseRootScene.isActive())
        // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        pauseRootScene.SetActive(false);
        Puzzle.SetActive(true);
        print("what1");
        //Time.timeScale = 0.00f;
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(innerScene));
        print("what2");
        //SceneManager.LoadScene(innerScene, LoadSceneMode.Additive);
  
        
    }
    public void ClosePuzzle()
    {
        Puzzle.SetActive(false);
        pauseRootScene.SetActive(true);
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
