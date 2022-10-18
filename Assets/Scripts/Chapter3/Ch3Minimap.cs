using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3Minimap : MonoBehaviour
{
    public Transform player;
    public Transform playerIcon;
    public GameObject maze;

    private float scale;

    void Start()
    {
        Vector2 mazeSize = maze.GetComponent<Renderer>().bounds.size;
        Vector2 mapSize = GetComponent<RectTransform>().rect.size;

        scale = mapSize.magnitude / mazeSize.magnitude;
    }

    void Update()
    {
        Vector2 delPos = player.position - maze.transform.position;
        Vector2 iconPos = delPos * scale;
        playerIcon.localPosition = iconPos;
    }
}
