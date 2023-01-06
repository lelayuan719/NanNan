using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    float fadeInTime = 0.5f;
    float fadeOutTime = 0.5f;

    [HideInInspector] public bool inDark = false;

    LightFader fader;

    void Start()
    {
        fader = GetComponentInChildren<LightFader>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "darkness"){
            fader.FadeIn(fadeInTime);
            inDark = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "darkness"){
            fader.FadeOut(fadeOutTime);
            inDark = false;
        }
    }
}
