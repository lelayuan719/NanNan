using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraConfiner : MonoBehaviour
{
    public void ChangeConfiner(PolygonCollider2D newConfiner)
    {
        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
        confiner.m_BoundingShape2D = newConfiner;
        confiner.InvalidateCache();
    }
}
