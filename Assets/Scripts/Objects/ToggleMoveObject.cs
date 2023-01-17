using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMoveObject : CyclingObjectTemplate
{
    [SerializeField] Transform destination;
    [SerializeField] bool canMoveAgain = true;

    protected override void Awake()
    {
        otherDestinations = new List<Transform>() { destination };
        base.Awake();
    }

    protected override void StopMoving() 
    { 
        if (canMoveAgain)
            GetComponent<Collider2D>().enabled = true; 
    }
}
