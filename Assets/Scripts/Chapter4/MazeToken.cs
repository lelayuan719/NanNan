using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeToken : MonoBehaviour
{
    public GameObject boxPuzzleTile;
    public GameObject minimapIcon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boxPuzzleTile.SetActive(true);
            minimapIcon.SetActive(false);
            other.GetComponent<TokenTracker>().AddToken();
            GameManager.GM.inventory.GiveItem(gameObject.name);
            gameObject.SetActive(false);
        }
    }
}
