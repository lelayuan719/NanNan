using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSceneChange : SceneChange
{
    [SerializeField] private SceneTransitionSettings.TransitionType outTransitionType = SceneTransitionSettings.TransitionType.Fade;
    [SerializeField] private float outTransitionTime = 2;
    [SerializeField] private SceneTransitionSettings.TransitionType inTransitionType = SceneTransitionSettings.TransitionType.Fade;
    [SerializeField] private float inTransitionTime = 1;

    // Start is called before the first frame update
    public override void ChangeScene()
    {
        SceneTransitionSettings outSceneTransitionSettings = new SceneTransitionSettings(
            outTransitionType, outTransitionTime, +1, 1);

        SceneTransitionSettings loadScene = new SceneTransitionSettings(
            SceneTransitionSettings.TransitionType.LoadScene, 0, 0, 0);

        SceneTransitionSettings inSceneTransitionSettings = new SceneTransitionSettings(
            inTransitionType, inTransitionTime, -1, 1);

        GameManager.GM.LoadScene(NextScene, new SceneTransitionSettings[] { outSceneTransitionSettings, loadScene, inSceneTransitionSettings });
    }
}
