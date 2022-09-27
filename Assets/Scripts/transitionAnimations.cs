using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimations : MonoBehaviour
{
    public GameObject element1;
    public GameObject element2;
    public GameObject element3;
    private Animation anim1;
    private Animation anim2;
    private Animation anim3;
    //public GameObject edd;

    // Start is called before the first frame update
    void Start()
    {
        anim1 = element1.GetComponent<Animation>();
        //anim2 = element2.GetComponent<Animation>();
        //anim3 = element3.GetComponent<Animation>();

    }
    void OnMouseDown()
    {
        anim1.Play("clouds_animation_scene1");
        //anim2.Play("shadow_transition_mountainw2");
        // anim3.Play("shadow_transition_mountain3");
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
