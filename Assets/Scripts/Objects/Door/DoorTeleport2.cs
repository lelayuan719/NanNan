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
            // Make transition instant if being chased
            if (GameManager.GM.player.GetComponent<PlayerController>().instantDoors)
            {
                transition.outTransitionType = SceneTransitionSettings.TransitionType.Instant;
            }
            transition.Transition();
        }
    }

    public void Teleport()
    {
        TeleportSomething(GameManager.GM.player, true);

        // Reset transition
        transition.outTransitionType = SceneTransitionSettings.TransitionType.Fade;
    }

    public void TeleportSomething(GameObject obj, bool changeCamera = false)
    {
        GetDestPos(obj);

        obj.transform.position = destPos;

        if (changeCamera)
        {
            GameManager.GM.ChangeActiveCam(endCam.gameObject);
        }
    }

    void GetDestPos(GameObject obj)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();
        float halfHeight = collider.bounds.size.y / 2;
        float halfOffset = halfHeight - (collider.offset.y * obj.transform.lossyScale.y);

        var raycast = Physics2D.Raycast(destination.position, Vector2.down, 4, LayerMask.GetMask("Ground"));
        destPos = raycast.point + new Vector2(0, halfOffset);
        destPos.z = 1;
    }

    public void Unlock()
    {
        opened = true;
    }
}