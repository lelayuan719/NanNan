using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoveTo : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] bool canMoveAfter;
    [SerializeField] UnityEvent onComplete;

    public void Move()
    {
        GameManager.GM.player.GetComponent<PlayerController>().MoveTo(destination, onComplete, canMoveAfter);
    }
}
