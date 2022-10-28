using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBearCh3 : MonoBehaviour
{
    public void Activate()
    {
        GameManager.GM.inventory.RemoveItem("bear");
    }
}
