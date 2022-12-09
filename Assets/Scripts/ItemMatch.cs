using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemMatch: MonoBehaviour
{
    public Item item;
    public Dialog invalidDialog;
    public Dialog successDialog;
    public bool consumeItem = true;
    public string itemCheck;
    [SerializeField] CursorTypes newCursor = CursorTypes.Default;
    [HideInInspector] public bool success;
    private UIItem collectedItem;
    public UnityEvent onMatch;

    void Start()
    {
        collectedItem = GameManager.GM.inventoryUI.selectedItem;
        item = collectedItem.item;
        success = false;
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

            // Update inventory
            collectedItem.UpdateItem(null);
            GameManager.GM.inventory.RemoveItem(item);

            // Change hover icon
            if (TryGetComponent(out HoverInteractable interactable))
            {
                interactable.newCursor = newCursor;
                CursorManager.SetCursor(newCursor);
            }

            // Invoke dialog
            if (successDialog)
                successDialog.TriggerDialog();

            // Invoke other events
            onMatch.Invoke();

        } else if (!success && invalidDialog)
        {
            invalidDialog.TriggerDialog();
        }
    }
}
