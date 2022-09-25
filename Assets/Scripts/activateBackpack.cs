using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBackpack : MonoBehaviour
{
    public GameObject InventoryButton;
    public GameObject InventoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        InventoryButton.SetActive(false);
        InventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        InventoryButton.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }
}
