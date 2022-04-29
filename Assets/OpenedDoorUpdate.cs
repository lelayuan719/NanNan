using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedDoorUpdate : MonoBehaviour
{
    private bool updated;
    private bool opened;
    public Sprite openedDoor;
    // Start is called before the first frame update
    void Start()
    {
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        opened = gameObject.GetComponent<ItemMatch>().success;
        if (opened && !updated){
            gameObject.GetComponent<SpriteRenderer>().sprite = openedDoor;
        }
    }
}
