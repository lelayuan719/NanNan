using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport : MonoBehaviour
{
    public bool opened = true;
    public Transform destination;

    void OnMouseDown()
    {
        if (opened && GameManager.GM.player.GetComponent<GenericController>().playerCanMove)
        {
            GameManager.GM.player.GetComponent<PlayerController>().TeleportTo(destination);
        }
    }
    
    public void Unlock()
    {
        opened = true;
    }
}