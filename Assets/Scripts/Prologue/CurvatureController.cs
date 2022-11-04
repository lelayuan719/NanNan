using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvatureController : MonoBehaviour
{
    Renderer render;

    [SerializeField] float startCurvature = 900;
    [SerializeField] float endCurvature = 600;
    [SerializeField] float maxX;
    float startX;

    public Transform character;

    void Start()
    {
        startX = character.position.x;
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

    private void Update()
    {
        float curvature = Mathf.Lerp(startCurvature, endCurvature, (character.position.x - startX) / (maxX - startX));
        render.sharedMaterial.SetFloat("_Radius", curvature);
    }
}
