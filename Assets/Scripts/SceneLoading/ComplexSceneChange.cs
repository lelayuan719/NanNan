using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexSceneChange : SceneChange
{
    [SerializeField] private SceneTransitionSettings[] transitionSequence;

    // Start is called before the first frame update
    public override void ChangeScene()
    {
        GameManager.GM.LoadScene(NextScene, transitionSequence);
    }
}
