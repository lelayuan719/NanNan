using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PageTurnController : MonoBehaviour
{
    private AutoFlip flipper;

    private void Start()
    {
        flipper = GetComponent<AutoFlip>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            flipper.FlipRightPage();
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            flipper.FlipLeftPage();
        }
    }
}
