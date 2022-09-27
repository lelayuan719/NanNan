using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport : MonoBehaviour
{
    private bool opened;
    public GameObject Nannan;
    private Transform destination;

    // Start is called before the first frame update
    private void Start()
    {
        destination = transform.Find("Destination");
    }

    void OnMouseDown()
    {
        opened = gameObject.GetComponent<ItemMatch>().success;
        if (opened)
        {
            Nannan.transform.position = destination.position;
        }
    }
}