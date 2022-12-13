using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    public UIItem selectedItem;
    public GameObject slotPrefab;
    public GameObject notification;
    public Tooltip tooltip;
    public Transform slotPanel;
    public GameObject warlockBookButton;
    public int numberOfSlots = 6;
    public InventoryButtonHelper inventoryButtonHelper;
    public int mostRecentSlot;
    [HideInInspector] public bool hasNewItem;
    [HideInInspector] public bool isOpen;

    Animator notifAnim;

    private void Awake(){
        for(int i = 0; i < numberOfSlots; i++){
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            instance.GetComponentInChildren<UIItem>().slot = i;
            instance.GetComponentInChildren<UIItem>().seen = true;
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    private void Start()
    {
        notifAnim = notification.GetComponent<Animator>();
    }

    private void Update(){
        //if (Input.GetKeyDown(KeyCode.Escape)){
        //    if (selectedItem != null) {
        //        print("Escape key was pressed");
        //        //AddNewItem(selectedItem.item);
        //        UpdateSlot(mostRecentSlot, selectedItem.item);
        //        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        //        selectedItem.UpdateItem(null);
        //    }
        //}
    }

    public void UpdateSlot(int slot, Item item){
        uiItems[slot].UpdateItem(item);
        uiItems[slot].slot = slot;
    }

    public void AddNewItem(Item item){
        int slotIndex = uiItems.FindIndex(i => i.item == null);
        UpdateSlot(slotIndex, item);

        // Display notification dot if unopened
        if (!isOpen)
        {
            uiItems[slotIndex].seen = false;
            RefreshNotification();
        }

        // Make it unread
        uiItems[slotIndex].read = false;
    }

    public void RemoveItem(Item item){
        // Try in top items
        int foundIndex = uiItems.FindIndex(i => i.item == item);
        if (foundIndex != -1)
        {
            UpdateSlot(foundIndex, null);
            uiItems[foundIndex].seen = true;
            uiItems[foundIndex].read = true;
            RefreshNotification();
        }
        // Try in selected item
        else if (selectedItem.item == item)
        {
            selectedItem.UpdateItem(null);
        }
    }

    // Swaps the items in the slot and the selected
    public void SwapItems(int slot)
    {
        UIItem uiItem = uiItems[slot];

        if (uiItem.item != null)
        {
            if (selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(uiItem.item);
                uiItem.UpdateItem(clone);
                mostRecentSlot = slot;
            }
            else
            {
                selectedItem.UpdateItem(uiItem.item);
                uiItem.UpdateItem(null);
                mostRecentSlot = slot;
            }
        }
        else if (selectedItem.item != null)
        {
            uiItem.UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }

    public void ToggleOpen()
    {
        ChangeOpen(!isOpen);
    }

    public void ChangeOpen(bool open)
    {
        isOpen = open;

        // Makes all items seen
        if (isOpen)
        {
            foreach (var item in uiItems)
            {
                item.seen = true;
            }
        }

        RefreshNotification();
    }

    // Changes the visibility of the notification dot
    public void RefreshNotification()
    {
        // Open hides it
        if (isOpen)
        {
            notifAnim.SetBool("isPulsating", false);
        }
        // Closed hides it if there's something we haven't seen
        else
        {
            foreach (var item in uiItems)
            {
                // If there's something new, make it pulsating
                if (!item.seen)
                {
                    notifAnim.SetBool("isPulsating", true);
                    return;
                }
            }

            // Otherwise, set it to not pulsating
            notifAnim.SetBool("isPulsating", false);
        }
    }

    // Unlocks the Warlock book
    public void UnlockWarlockBook()
    {
        warlockBookButton.SetActive(true);
    }
}
