using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3End : MonoBehaviour
{
    public CameraMovement cam;
    public Transform door;
    [Range(0,5)][SerializeField] private float smoothFactor;

    public void Activate()
    {
        cam.player = door;
        cam.smoothFactor = smoothFactor;
    }
}
