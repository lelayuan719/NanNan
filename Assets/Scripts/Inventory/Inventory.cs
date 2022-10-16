using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;

    public void Start() {

    }

    public void GiveItem(int id){
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        Debug.Log("added item: " + itemToAdd.title);
    }

    public void GiveItem(string itemName){
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("added item: " + itemToAdd.title);
    }

    public Item CheckForItem(int id){
        return characterItems.Find(item => item.id == id);
    }

    public Item CheckForItem(string name)
    {
        return characterItems.Find(item => item.title == name);
    }

    public void RemoveItem(int id){
        Item item = CheckForItem(id);
        if (item != null){
            RemoveItem(item);
        }
    }

    public void RemoveItem(string name)
    {
        Item item = CheckForItem(name);
        if (item != null)
        {
            RemoveItem(item);
        }
    }

    public void RemoveItem(Item item){
        if (item != null){
            characterItems.Remove(item);
            inventoryUI.RemoveItem(item);
            Debug.Log("Item removed: " + item.title);
        }
    }
}
