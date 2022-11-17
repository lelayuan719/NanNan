using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericController : MonoBehaviour
{
    public bool playerCanMove = true;

    protected virtual void Start()
    {
        GameManager.GM.player = gameObject;
    }

    public virtual void FreezeCharacter()
    {
        playerCanMove = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
