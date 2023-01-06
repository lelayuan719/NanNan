using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTransitionSameScene : TransitionSameScene
{
    [SerializeField] private SceneTransitionSettings.SceneTransitionType outTransitionType = SceneTransitionSettings.SceneTransitionType.Fade;
    public float outTransitionTime = 2;
    [SerializeField] private SceneTransitionSettings.SceneTransitionType inTransitionType = SceneTransitionSettings.SceneTransitionType.Fade;
    public float inTransitionTime = 1;

    // Start is called before the first frame update
    public override void Transition()
    {
        SceneTransitionSettings outSceneTransitionSettings = new SceneTransitionSettings(
            outTransitionType, outTransitionTime, +1, 1);

        SceneTransitionSettings eventTransitionSettings = new SceneTransitionSettings(
            SceneTransitionSettings.SceneTransitionType.Event, 0, 0, 0);

        SceneTransitionSettings inSceneTransitionSettings = new SceneTransitionSettings(
            inTransitionType, inTransitionTime, -1, 1);

        GameManager.GM.sceneLoader.TransitionSameScene(onTransition, new SceneTransitionSettings[] { outSceneTransitionSettings, eventTransitionSettings, inSceneTransitionSettings }, unfreeze);
    }
}