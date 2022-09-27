using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueMinimap : MonoBehaviour
{
    private float startX;
    private float length = 4720.751f;
    private int rotations = 0;

    public Transform character;

    void Start()
    {
        startX = character.position.x;
    }

    void Update()
    {
        float rotateAmount = (character.position.x - startX) / length;
        if (rotateAmount >= 1+rotations) {
            rotations++;
            Debug.LogFormat("Completed {0} rotation(s)", rotations);
        }

        transform.eulerAngles = new Vector3(0, 0, 180 + 360 * rotateAmount);
    }
}
