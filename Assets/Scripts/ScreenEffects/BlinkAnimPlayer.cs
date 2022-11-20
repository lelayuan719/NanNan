using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class BlinkAnimPlayer : MonoBehaviour
{
    [SerializeField] Renderer2DData rendererData;
    BlinkFeature blinkFeature;

    public float progress;
    private bool finished;

    public UnityEvent onFinish;

    // Start is called before the first frame update
    void Start()
    {
        blinkFeature = rendererData.rendererFeatures.OfType<BlinkFeature>().FirstOrDefault();
        if (blinkFeature == null) return;

        blinkFeature.passSettings.progress = progress;
        rendererData.SetDirty();
    }

    // Update is called once per frame
    void Update()
    {
        // Update blink feature only if we're not done blinking
        if (progress != 1)
        {
            blinkFeature.passSettings.progress = progress;
            rendererData.SetDirty();
        }
        // The player can move when we've finished
        else if (!finished)
        {
            // Finalize progress
            progress = 1;
            blinkFeature.passSettings.progress = progress;
            rendererData.SetDirty();

            // Player can move
            onFinish.Invoke();
            finished = true;
        }
    }

    // Resets the blinking stage if quit midway
    private void OnApplicationQuit()
    {
        blinkFeature.passSettings.progress = 1;
        rendererData.SetDirty();
    }

    public void OnFinishDialog()
    {
        GameManager.GM.player.GetComponent<PlayerController>().playerCanMove = true;
    }
}
