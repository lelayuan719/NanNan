using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSceneChange : MonoBehaviour
{
    public string NextScene;
    [SerializeField] SceneTransitionSettings outSceneTransition;
    [SerializeField] SceneTransitionSettings inSceneTransition;

    // Start is called before the first frame update
    public void ChangeScene()
    {
        GameManager.GM.LoadScene(NextScene, outSceneTransition, inSceneTransition);
    }
}
