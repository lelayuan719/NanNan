using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeToken : MonoBehaviour
{
    public GameObject boxPuzzleTile;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boxPuzzleTile.SetActive(true);
            other.GetComponent<TopDownController>().tokens++;
            gameObject.SetActive(false);
        }
    }
}
