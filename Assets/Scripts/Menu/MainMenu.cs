using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SceneTransitionSettings outSceneTransition;
    [SerializeField] SceneTransitionSettings inSceneTransition;

    public void PlayGame ()
    {
        GameManager.GM.LoadScene("Prologue_intro", outSceneTransition, inSceneTransition);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
