using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvatureController : MonoBehaviour
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
        ResetCurvature();
    }

    public void ResetCurvature()
    {
        render.sharedMaterial.SetFloat("_Direction", 0);
        render.sharedMaterial.SetFloat("_Radius", 1000000);
    }
}
