using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] AudioSource footstep;
    [SerializeField] float velocityThresh = 0.01f;

    [SerializeField] Rigidbody2D rb;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        footstep.Play();
        footstep.Pause();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < velocityThresh)
        {
            footstep.Pause();
        }
        else if (!footstep.isPlaying)
        {
            footstep.UnPause();
        }
        footstep.GetCustomCurve(AudioSourceCurveType.CustomRolloff);
    }
}