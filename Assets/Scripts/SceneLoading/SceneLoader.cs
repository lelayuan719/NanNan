using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    string sceneName;
    UnityEvent onTransition;
    bool canLoad = true;
    bool unfreeze;
    int idx = 0;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI subtitleText;
    SceneTransitionSettings[] transitionSequence;

    [SerializeField] SceneTransition[] transitions;

    List<SceneTransitionSettings.TransitionType> noCleanupTransitions = new List<SceneTransitionSettings.TransitionType> { 
        SceneTransitionSettings.TransitionType.Instant, 
        SceneTransitionSettings.TransitionType.LoadScene, 
        SceneTransitionSettings.TransitionType.Event 
    };

    public void LoadScene(string sceneName)
    {
        if (!canLoad) return;

        if (GameManager.GM.player) GameManager.GM.player.GetComponent<GenericController>().playerCanMove = false;
        idx = 0;
        this.sceneName = sceneName;
        this.onTransition = null;
        StartCoroutine(LoadSceneInternal());
    }

    public void LoadScene(string sceneName, SceneTransitionSettings[] transitionSequence)
    {
        if (!canLoad) return;

        canLoad = false;
        if (GameManager.GM.player) GameManager.GM.player.GetComponent<GenericController>().playerCanMove = false;
        idx = 0;
        this.sceneName = sceneName;
        this.transitionSequence = transitionSequence;
        StartTransition(transitionSequence[idx]);
    }

    public void TransitionSameScene(UnityEvent onTransition, SceneTransitionSettings[] transitionSequence, bool unfreeze)
    {
        if (!canLoad) return;

        canLoad = false;
        if (GameManager.GM.player) GameManager.GM.player.GetComponent<GenericController>().FreezeCharacter();
        idx = 0;
        this.sceneName = "";
        this.onTransition = onTransition;
        this.unfreeze = unfreeze;
        this.transitionSequence = transitionSequence;
        StartTransition(transitionSequence[idx]);
    }

    public void TransitionSameScene(UnityEvent onTransition, SceneTransitionSettings[] transitionSequence)
    {
        TransitionSameScene(onTransition, transitionSequence, true);
    }

    void StartTransition(SceneTransitionSettings transition)
    {
        if (transition.transitionType == SceneTransitionSettings.TransitionType.Instant)
        {
            ContinueLoad();
            idx--;
        }
        else if (transition.transitionType == SceneTransitionSettings.TransitionType.LoadScene)
        {
            StartCoroutine(LoadSceneInternal());
        }
        else if (transition.transitionType == SceneTransitionSettings.TransitionType.Event)
        {
            onTransition.Invoke();
            if (unfreeze) GameManager.GM.player.GetComponent<GenericController>().UnfreezeCharacter();
            ContinueLoad();
        }
        else
        {
            SceneTransition transitionObj = transitions[(int)transition.transitionType - 1];
            transitionObj.gameObject.SetActive(true);
            transitionObj.Transition(transition.transitionTime, transition.direction);
        }

        // Decrease the persistence until it goes to 0, then we stop a transition completely
        for (int i = 0; i < idx; i++)
        {
            transitionSequence[i].persistence--;

            if ((transitionSequence[i].persistence < 0) && 
                (transitionSequence[idx].transitionType != transitionSequence[i].transitionType))
            {
                StopTransition(transitionSequence[i]);
            }
        }
    }

    void StopTransition(SceneTransitionSettings transition)
    {
        // Only cleanup if we need to
        if (!noCleanupTransitions.Contains(transition.transitionType))
        {
            SceneTransition transitionObj = transitions[(int)transition.transitionType - 1];
            transitionObj.gameObject.SetActive(false);
            transitionObj.StopTransition();
        }
    }

    public void ContinueLoad()
    {
        idx++;

        if (idx < transitionSequence.Length)
            StartTransition(transitionSequence[idx]);
        else if (transitionSequence.Length > 0)
        {
            StopTransition(transitionSequence[idx - 1]);
            canLoad = true;
        }
    }

    IEnumerator LoadSceneInternal()
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        //if (unfreeze) GameManager.GM.player.GetComponent<GenericController>().UnfreezeCharacter();
        ContinueLoad();
    }

    public void SetChapterText(string title, string subtitle)
    {
        titleText.text = title;
        subtitleText.text = subtitle;
    }
}

[System.Serializable]
public class SceneTransitionSettings
{
    public enum TransitionType
    {
        Instant,
        Fade,
        TextFade,
        LoadScene,
        Event,
    }

    public TransitionType transitionType;
    public float transitionTime;
    public int direction;
    public int persistence;

    public SceneTransitionSettings()
    {
        transitionType = TransitionType.Instant;
        transitionTime = 1;
        direction = +1;
        persistence = 1;
    }

    public SceneTransitionSettings(TransitionType transitionType, float transitionTime, int direction, int persistence)
    {
        this.transitionType = transitionType;
        this.transitionTime = transitionTime;
        this.direction = direction;
        this.persistence = persistence;
    }
}