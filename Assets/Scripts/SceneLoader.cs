using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    string sceneName;
    bool loadedScene = false;
    SceneTransitionSettings outTransition;
    SceneTransitionSettings inTransition;

    [SerializeField] SceneTransition[] transitions;

    public void LoadScene(string sceneName, SceneTransitionSettings outTransition, SceneTransitionSettings inTransition)
    {
        if (GameManager.GM.player) GameManager.GM.player.GetComponent<GenericController>().playerCanMove = false;
        loadedScene = false;
        this.sceneName = sceneName;
        this.outTransition = outTransition;
        this.inTransition = inTransition;
        StartTransition(outTransition, +1);
    }

    void StartTransition(SceneTransitionSettings transition, int direction)
    {
        if (transition.transitionType == SceneTransitionSettings.SceneTransitionType.Instant)
        {
            ContinueLoad();
        }
        else
        {
            transitions[(int)transition.transitionType - 1].Transition(transition.transitionTime, direction);
        }
    }

    void StopTransition(SceneTransitionSettings transition)
    {
        if (transition.transitionType == SceneTransitionSettings.SceneTransitionType.Instant)
        {
            // Nothing
        }
        else
        {
            transitions[(int)transition.transitionType - 1].StopTransition();
        }
    }

    public void ContinueLoad()
    {
        // If we haven't loaded the scene, start it then start the inside transition
        if (!loadedScene)
        {
            loadedScene = true;
            StartCoroutine(LoadSceneInternal());
        }
        // Otherwise, stop loading
        else if (loadedScene)
        {
            StopTransition(inTransition);
        }
    }

    IEnumerator LoadSceneInternal()
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        StartTransition(inTransition, -1);

        if (outTransition.transitionType != inTransition.transitionType)
        {
            StopTransition(outTransition);
        }
    }
}


[System.Serializable]
public class SceneTransitionSettings
{
    public enum SceneTransitionType
    {
        Instant,
        Fade,
    }

    public SceneTransitionType transitionType;
    public float transitionTime;

    public SceneTransitionSettings()
    {
        transitionType = SceneTransitionType.Instant;
        transitionTime = 1;
    }
}