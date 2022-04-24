using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePuzzle : MonoBehaviour
{
    public GameObject Puzzle;
    private GameObject pauseRootScene;
    // Start is called before the first frame update
    void Start()
    {
        pauseRootScene = GameObject.FindWithTag("pause");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClosingPuzzle()
    {
        print("closing 1");
        if (Puzzle.activeSelf)
        {
            print("closing");
            Puzzle.SetActive(false);
            pauseRootScene.SetActive(true);
        }
        else
        {
            print("can't close before open");
        }
    }
}
