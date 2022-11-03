using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ch2ActivateWell : MonoBehaviour
{
    public PlayerController player;
    public Transform playerDest;

    public CinemachineConfiner2D confiner;
    public PolygonCollider2D finalBoundingShape;

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
        player.MoveTo(playerDest, onFinishedWalking);
    }

    public void ChangeConfiner()
    {
        // Change camera
        confiner.m_BoundingShape2D = finalBoundingShape;
        confiner.InvalidateCache();
    }
}
