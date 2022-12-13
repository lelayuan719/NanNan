using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    SceneChange sceneChange;

    private void Start()
    {
        sceneChange = GetComponent<SceneChange>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sceneChange.ChangeScene();
    }
}
