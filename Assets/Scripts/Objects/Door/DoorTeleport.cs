using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport : MonoBehaviour
{
    public bool opened = true;
    public GameObject Nannan;
    public GameObject cam;
    public Transform destination;

    // Start is called before the first frame update
    private void Start()
    {
    }

    void OnMouseDown()
    {
        if (opened)
        {
            TransportPlayer();
        }
    }

    public void TransportPlayer()
    {
        cam.transform.position = destination.position + cam.GetComponent<CameraMovement>().offset;
        Nannan.transform.position = destination.position;
    }
    
    public void Unlock()
    {
        opened = true;
    }
}