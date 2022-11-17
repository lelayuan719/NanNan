using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsateInteractible : MonoBehaviour
{
    private Animator anim;

    [SerializeField] float pulseThreshold = 3;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float d = Vector2.Distance(transform.position, GameManager.GM.player.transform.position);
        if (d < pulseThreshold)
        {
            anim.SetBool("isPulsating", true);
        }
        else
        {
            anim.SetBool("isPulsating", false);
        }
    }
}
