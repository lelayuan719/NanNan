using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemMatch: MonoBehaviour
{
    public Item item;
    public Dialog invalidDialog;
    public bool consumeItem = true;
    //public GameObject interactionItem;
    public string itemCheck;
    [HideInInspector] public bool success;
    private UIItem collectedItem;
    public UnityEvent onMatch;

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
        //checks if item is correct to assigned item
        if((item != null) && (item.title == itemCheck))
        {
            //if correct item is selected success bool will be true
            success = true;
            print(itemCheck + " match successful");

            collectedItem.UpdateItem(null);
            GameManager.GM.inventory.RemoveItem(item);
            onMatch.Invoke();
        } else if (!success && invalidDialog)
        {
            invalidDialog.TriggerDialog();
        }
    }
}
