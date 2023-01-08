using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Listens at the current transform for the volume of source, then plays how loud it would be at destination
public class AudioBleedthrough : MonoBehaviour
{
    [SerializeField] AudioSource sourceAudio;
    [SerializeField] Transform destination;
    [Min(0)] float volumeScale = 0.75f;

    AudioSource destAudio;
    AnimationCurve sourceRolloff;

    private void Start()
    {
        sourceRolloff = sourceAudio.GetCustomCurve(AudioSourceCurveType.CustomRolloff);

        // Copy source component
        SetupDestAudio();
    }

    void SetupDestAudio()
    {
        destAudio = destination.gameObject.AddComponent<AudioSource>();
        destination.gameObject.AddComponent<AudioReverbFilter>();
        destAudio.clip = sourceAudio.clip;
        destAudio.loop = true;
        destAudio.spatialBlend = sourceAudio.spatialBlend;
        destAudio.rolloffMode = AudioRolloffMode.Custom;
        destAudio.SetCustomCurve(AudioSourceCurveType.CustomRolloff, GetNoDropoffCurve());
        destAudio.Play();
        destAudio.Pause();
    }

    AnimationCurve GetNoDropoffCurve()
    {
        return new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));
    }

    private void FixedUpdate()
    {
        // Only do calculations if source is playing
        if (!sourceAudio.isPlaying)
        {
            destAudio.Pause();
            return;
        }

        // Get modeled distance from source to player
        float distToSource = Vector2.Distance(transform.position, sourceAudio.transform.position);
        float distToPlayer = Vector2.Distance(destAudio.transform.position, GameManager.GM.player.transform.position);
        float totalDist = distToSource + distToPlayer;
        // Stop playing if too far away
        if (totalDist > sourceAudio.maxDistance)
        {
            destAudio.Pause();
            return;
        }

        // Get new volume
        destAudio.volume = volumeScale * sourceRolloff.Evaluate(totalDist / sourceAudio.maxDistance);

        // Start playing if we need to
        if (!destAudio.isPlaying)
        {
            destAudio.UnPause();
        }
    }
}
