using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport : MonoBehaviour
{
    public bool opened = true;
    public GameObject player;
    public CinemachineVirtualCamera cam;
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
        cam.OnTargetObjectWarped(player.transform, destination.position - player.transform.position);
        player.transform.position = destination.position;
    }
    
    public void Unlock()
    {
        opened = true;
    }
}