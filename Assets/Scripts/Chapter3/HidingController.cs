using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HidingController : MonoBehaviour
{
    PlayerController ctrl;
    public bool hiding;
    public bool canHide = false;

    void Start()
    {
        ctrl = GetComponent<PlayerController>();
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
        ctrl.FreezeCharacter();
        hiding = true;
    }

    public void Emerge()
    {
        ctrl.UnfreezeCharacter();
        hiding = false;
    }

    public void SetCanHide(bool canHide)
    {
        this.canHide = canHide;
    }
}