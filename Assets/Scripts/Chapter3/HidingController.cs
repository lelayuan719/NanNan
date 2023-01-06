using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HidingController : MonoBehaviour
{
    PlayerController ctrl;
    public bool hiding;
    public bool canHide = false;

    SpriteRenderer sr;
    float hideDarken = 0.4f;

    void Start()
    {
        ctrl = GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void ToggleHide()
    {
        if (!hiding)
            Hide();
        else
            Emerge();
    }

    public void Hide()
    {
        //sr.color = Color.HSVToRGB(0, 0, hideDarken);
        ctrl.FreezeCharacter();
        hiding = true;
    }

    public void Emerge()
    {
        //sr.color = Color.white;
        ctrl.UnfreezeCharacter();
        hiding = false;
    }

    public void SetCanHide(bool canHide)
    {
        this.canHide = canHide;
    }
}