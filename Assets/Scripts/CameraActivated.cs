using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivated : MonoBehaviour
{
    void Start()
    {
        GameManager.GM.cam = gameObject;
        GameManager.GM.cine = GetComponent<CinemachineBrain>();
    }
}
