using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be attached to a global singleton controller, not the floor
// TODO

public class CurveAtStart : MonoBehaviour
{
    Renderer render;

    void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        render.sharedMaterial.SetFloat("_Direction", -1);
        render.sharedMaterial.SetFloat("_Radius", 900);

    }

    private void OnApplicationQuit()
    {
        render.sharedMaterial.SetFloat("_Direction", 0);
        render.sharedMaterial.SetFloat("_Radius", 1000000);
    }
}
