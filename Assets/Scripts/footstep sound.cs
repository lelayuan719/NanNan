using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioSource footstep;

    public void playFootstep()
    {
        footstep.Play();
    }
}
