using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ch3ShelfFall : MonoBehaviour
{
    public PlayerController player;
    public Transform playerDest;

    public UnityEvent onFinishedWalking;

    void Start()
    {

    }

    void OnMouseDown()
    {
        Activate();
    }

    public void Activate()
    {
        // Starts walking and activates event when finished
        player.MoveTo(playerDest, onFinishedWalking, false);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}