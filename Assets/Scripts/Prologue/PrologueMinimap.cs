using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueMinimap : MonoBehaviour
{
    private float startX;
    private float spiralM;
    private float minimapSpiralM;
    private float markerStartRotation;
    private int rotations = 0;

    private float[] distances = new float[1000];
    private float[] thetas = new float[1000];
    private int posIndex = 0;
    private float alpha = 0.0f;
    private Image markerRender;

    [SerializeField] private float outerR = 4720.751f;
    [SerializeField] private float minimapR;
    [SerializeField] private float fadeRate;
    [Range(0,1)][SerializeField] private float maxAlpha;
    public Transform player;
    public Image render;
    public RectTransform marker;

    private void Awake()
    {
        markerRender = marker.GetComponent<Image>();

        spiralM = outerR / 4;
        minimapSpiralM = minimapR / 4;

        PrecalcDistances();
    }

    void Start()
    {
        startX = player.position.x;
        markerStartRotation = marker.rotation.eulerAngles.z;

        render.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        markerRender.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Calculates distances along the spiral through integration
    void PrecalcDistances()
    {
        float R = outerR;
        float c = spiralM / (2 * Mathf.PI);
        int maxRot = 3;
        float n = distances.Length;

        // https://www.wolframalpha.com/input?i2d=true&i=integrate+Sqrt%5BPower%5B%5C%2840%29R-c*t%2C2%5D-c%5D+with+respect+to+t
        Func<float, double> indef_int = th => (0.5 * (Math.Sqrt(Mathf.Pow(R - c * th, 2) - c) * (c * th - R) / c + Mathf.Log(Mathf.Sqrt(Mathf.Pow(R - c * th, 2) - c) - c * th + R)));

        double startDist = indef_int(0);

        // Update angles and distances
        for (int i = 0; i < n; i++)
        {
            thetas[i] = (i / n) * maxRot * 2 * Mathf.PI;
            distances[i] = (float)(indef_int(thetas[i]) - startDist);
        }
    }

    Vector3 GetMarkerPos(float theta)
    {
        float c = minimapSpiralM / (2 * Mathf.PI);
        float r = minimapR - c * theta;

        Vector3 pos = new Vector3(
            r * Mathf.Cos(theta - Mathf.PI / 2),
            r * Mathf.Sin(theta - Mathf.PI / 2),
            0
        );

        return pos;
    }

    void Update()
    {
        float distance = player.position.x - startX;

        // Update interpolating distance
        while ((posIndex < (distances.Length-2)) && (distance > distances[posIndex]))
        {
            posIndex++;
        }
        while ((posIndex > 0) && (distance < distances[posIndex]))
        {
            posIndex--;
        }

        // Angle along the spiral
        float theta = Mathf.Lerp(thetas[posIndex], thetas[posIndex + 1], (distance - distances[posIndex]) / (distances[posIndex + 1] - distances[posIndex]));

        marker.localPosition = GetMarkerPos(theta);
        marker.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg*theta + markerStartRotation);
        if (theta/(2*Mathf.PI) >= 1+rotations) {
            rotations++;
            Debug.LogFormat("Completed {0} rotation(s)", rotations);
        }
        UpdateAlpha(Time.deltaTime);
    }

    void UpdateAlpha(float time)
    {
        if (player.GetComponent<Animator>().GetBool("isWalking"))
        {
            alpha += time * fadeRate * maxAlpha;
        } 
        else
        {
            alpha -= time * fadeRate * maxAlpha;
        }
        alpha = Mathf.Clamp(alpha, 0, maxAlpha);
        render.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        markerRender.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
}
