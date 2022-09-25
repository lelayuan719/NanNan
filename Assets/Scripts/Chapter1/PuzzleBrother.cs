using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBrother : MonoBehaviour
{
    private bool success;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       


    }
    void OnMouseDown()
    {
        success = gameObject.GetComponent<ItemMatch>().success;
        if (success)
        {
            UIInventory inventoryItem = GameObject.Find("InventoryPanel").GetComponent<UIInventory>();
            Item amulet = GameObject.Find("Nan_Nan_Side_Right").GetComponent<Inventory>().itemDatabase.GetItem("amulet");
            inventoryItem.AddNewItem(amulet);
            print("added");
        }
    }
}
