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
    private SimpleTransitionSameScene transition;

    private void Awake()
    {
        transition = GetComponent<SimpleTransitionSameScene>();
    }

    void OnMouseDown()
    {
        if (opened && GameManager.GM.player.GetComponent<GenericController>().playerCanMove)
        {
            transition.Transition();
        }
    }

    public void Teleport()
    {
        GetDestPos();

        GameManager.GM.player.transform.position = destPos;
        GameManager.GM.cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
        endCam.gameObject.SetActive(true);
    }

    void GetDestPos()
    {
        float halfHeight = GameManager.GM.player.GetComponent<BoxCollider2D>().size.y * GameManager.GM.player.transform.lossyScale.y / 2;
        Vector2 halfHeightOffset = new Vector2(0, halfHeight);

        var raycasts = Physics2D.RaycastAll(destination.position, Vector2.down, 4);
        foreach(var raycast in raycasts)
        {
            if (raycast.transform.gameObject.name == "Ground")
            {
                destPos = raycast.point + halfHeightOffset;
                break;
            }
        }

        destPos.z = 1;
    }

    public void Unlock()
    {
        opened = true;
    }
}