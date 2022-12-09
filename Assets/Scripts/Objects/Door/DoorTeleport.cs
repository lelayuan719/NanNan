using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport : MonoBehaviour
{
    public bool opened = true;
    public CinemachineVirtualCamera cam;
    public Transform destination;

    private Vector3 destPos;

    // Start is called before the first frame update
    private void Start()
    {
        // This makes sure we can still raycast through NanNan
        destPos = new Vector3(destination.position.x, destination.position.y, 1);
    }

    void OnMouseDown()
    {
        if (opened && GameManager.GM.player.GetComponent<GenericController>().playerCanMove)
        {
            TransportPlayer();
        }
    }

    public void TransportPlayer()
    {
        cam.OnTargetObjectWarped(GameManager.GM.player.transform, destPos - GameManager.GM.player.transform.position);
        GameManager.GM.player.transform.position = destPos;
    }
    
    public void Unlock()
    {
        opened = true;
    }
}