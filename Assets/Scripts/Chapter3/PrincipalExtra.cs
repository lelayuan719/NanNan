using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalExtra : MonoBehaviour
{
    [SerializeField] DoorTeleport2 floor2Door;

    public void GoDownstairs()
    {
        floor2Door.TeleportSomething(gameObject);
    }
}
