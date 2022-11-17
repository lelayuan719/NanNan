using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericController : MonoBehaviour
{
    public bool playerCanMove = true;

    public virtual void FreezeCharacter()
    {
        playerCanMove = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
