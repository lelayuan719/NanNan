using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTransitionSceneChange : SceneChange
{
    [SerializeField] private SceneTransitionSettings.TransitionType outTransitionType = SceneTransitionSettings.TransitionType.Fade;
    [SerializeField] private float outTransitionTime = 2;
    [SerializeField] private float textInTime = 3;
    [SerializeField] private string title;
    [SerializeField] private string subtitle;
    [SerializeField] private float textOutTime = 2;
    [SerializeField] private SceneTransitionSettings.TransitionType inTransitionType = SceneTransitionSettings.TransitionType.Fade;
    [SerializeField] private float inTransitionTime = 1;

    // Start is called before the first frame update
    public override void ChangeScene()
    {
        GameManager.GM.sceneLoader.SetChapterText(title, subtitle);

        SceneTransitionSettings[] transitionSequence = new SceneTransitionSettings[] {
            new SceneTransitionSettings(outTransitionType, outTransitionTime, +1, 3),
            new SceneTransitionSettings(SceneTransitionSettings.TransitionType.TextFade, textInTime, +1, 2),
            new SceneTransitionSettings(SceneTransitionSettings.TransitionType.LoadScene, 0, 0, 0),
            new SceneTransitionSettings(SceneTransitionSettings.TransitionType.TextFade, textOutTime, -1, 1),
            new SceneTransitionSettings(inTransitionType, inTransitionTime, -1, 1),
        };

        GameManager.GM.LoadScene(NextScene, transitionSequence);
    }
}
