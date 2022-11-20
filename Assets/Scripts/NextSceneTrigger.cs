using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    public string sceneName;

    [SerializeField] SceneTransitionSettings outSceneTransition;
    [SerializeField] SceneTransitionSettings inSceneTransition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.GM.LoadScene(sceneName, outSceneTransition, inSceneTransition);
    }
}
