using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSortingLayer : MonoBehaviour
{
    SpriteRenderer sr;
    string defaultLayer;
    int defaultOrder;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultLayer = sr.sortingLayerName;
        defaultOrder = sr.sortingOrder;
    }

    public void ChangeLayer(string newLayer)
    {
        sr.sortingLayerName = newLayer;
    }

    public void ChangeOrder(int newOrder)
    {
        sr.sortingOrder = newOrder;
    }

    public void PutBehindPlayer()
    {
        GameManager.GM.player.GetComponent<ChangeSortingLayer>().ChangeLayer("Player");
        GameManager.GM.player.GetComponent<ChangeSortingLayer>().ChangeOrder(0);
    }

    public void PutInFrontPlayer()
    {
        GameManager.GM.player.GetComponent<ChangeSortingLayer>().ChangeLayer(defaultLayer);
        GameManager.GM.player.GetComponent<ChangeSortingLayer>().ChangeOrder(defaultOrder-1);
    }
}
