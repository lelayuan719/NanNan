using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInterface : MonoBehaviour
{
    public void GiveItem(string name)
    {
        GameManager.GM.inventory.GiveItem(name);
    }

    public void RemoveItem(string name)
    {
        GameManager.GM.inventory.RemoveItem(name);
    }
}