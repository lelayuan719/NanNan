using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Transition(float transitionTime, int direction)
    {
        StartCoroutine(TransitionInternal(transitionTime, direction));
    }

    public void StopTransition()
    {
        anim.SetTrigger("StopTransition");
    }

    IEnumerator TransitionInternal(float transitionTime, int direction)
    {
        anim.speed = 1 / transitionTime;

        if (direction == +1)
            anim.SetTrigger("StartOutTransition");
        else
            anim.SetTrigger("StartInTransition");

        yield return new WaitForSecondsRealtime(transitionTime);
        GameManager.GM.sceneLoader.ContinueLoad();
    }
}