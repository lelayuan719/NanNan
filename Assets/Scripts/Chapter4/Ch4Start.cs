using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch4Start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.inventory.GiveItem("amulet");
    }
}
