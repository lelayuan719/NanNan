using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePuzzle : MonoBehaviour
{
    public GameObject Puzzle;
    private GameObject pauseRootScene;
    // Start is called before the first frame update
    void Awake()
    {
        pauseRootScene = GameObject.Find("book").GetComponent<openNewSceneOnClick>().pauseRootScene;
        pauseRootScene = GameObject.FindWithTag("pause");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClosingPuzzle()
    {
       // pauseRootScene = GameObject.Find("book").GetComponent<openNewSceneOnClick>().pauseRootScene;
        print("closing 1");
        if (Puzzle.activeSelf)
        {
            pauseRootScene.SetActive(true);
            print("closing");
            Puzzle.SetActive(false);
        }
        else
        {
            print("can't close before open");
        }
    }
}
