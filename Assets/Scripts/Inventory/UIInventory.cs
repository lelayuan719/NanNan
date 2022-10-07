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
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    public void UpdateSlot(int slot, Item item){
        uIItems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item){
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item){
        int foundIndex = uIItems.FindIndex(i => i.item == item);
        if (foundIndex != -1)
        {
            UpdateSlot(foundIndex, null);
            Debug.Log("item removed");
        }
    }
}
