using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport2 : MonoBehaviour
{
    public bool opened = true;
    public Transform destination;
    [SerializeField] CinemachineVirtualCamera endCam;

    private Vector3 destPos;

    private void Start()
    {
        destPos = destination.position;
        destPos.z = 1;
    }

    void OnMouseDown()
    {
        if (opened && GameManager.GM.player.GetComponent<GenericController>().playerCanMove)
        { 
            GameManager.GM.player.transform.position = destPos;
            GameManager.GM.cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            endCam.gameObject.SetActive(true);
        }
    }

    public void Unlock()
    {
        opened = true;
    }
}