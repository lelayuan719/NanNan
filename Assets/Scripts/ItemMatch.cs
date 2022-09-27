using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMatch: MonoBehaviour
{
    public Item item;
    public bool consumeItem = true;
    //public GameObject interactionItem;
    public string itemCheck;
    public bool success;
    private UIItem collectedItem;

    // Start is called before the first frame update
    void Start()
    {
        collectedItem = GameManager.GM.inventoryUI.selectedItem;
        item = collectedItem.item;
        success = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        //gets the information from inventory UI to figure out what item is currently selected 
        item = collectedItem.item;
        print("does this work");
        //checks if item is correct to assigned item
        if (item == null)
         {
             print("whattt");
         }
         else if(item.title == itemCheck)
        {
            //if correct item is selected success bool will be true
            success = true;
            print(itemCheck + " match successful");

            collectedItem.UpdateItem(null);
            GameManager.GM.inventory.RemoveItem(item);
        } else
        {
            print("wrong item match");
        }
    }

}
