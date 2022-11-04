using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public UIItem selectedItem;
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 6;
    public InventoryButtonHelper inventoryButtonHelper;

    private void Awake(){
        for(int i = 0; i < numberOfSlots; i++){
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            instance.GetComponentInChildren<UIItem>().slot = i;
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (selectedItem != null) {
                print("Escape key was pressed");
                //AddNewItem(selectedItem.item);
                UpdateSlot(mostRecentSlot, selectedItem.item);
                //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                selectedItem.UpdateItem(null);
            }
        }
    }

    public void UpdateSlot(int slot, Item item){
        uIItems[slot].UpdateItem(item);
        uIItems[slot].slot = slot;
    }

    public void AddNewItem(Item item){
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item){
        // Try in top items
        int foundIndex = uIItems.FindIndex(i => i.item == item);
        if (foundIndex != -1)
        {
            UpdateSlot(foundIndex, null);
            Debug.Log("item removed");
        }
        // Try in selected item
        else if (selectedItem.item == item)
        {
            selectedItem.UpdateItem(null);
        }
    }
}
