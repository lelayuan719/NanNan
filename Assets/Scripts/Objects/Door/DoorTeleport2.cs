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
        TeleportSomething(GameManager.GM.player, true);
    }

    public void TeleportSomething(GameObject obj, bool changeCamera = false)
    {
        GetDestPos(obj);

        obj.transform.position = destPos;

        if (changeCamera)
        {
            GameManager.GM.cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            endCam.gameObject.SetActive(true);
        }
    }

    void GetDestPos(GameObject obj)
    {
        float halfHeight = obj.GetComponent<Collider2D>().bounds.size.y * obj.transform.lossyScale.y / 2;
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