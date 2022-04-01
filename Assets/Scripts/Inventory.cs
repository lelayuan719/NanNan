using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;

    public void Start(){
        GiveItem("book");
        GiveItem("shard");
        GiveItem("matches");
        GiveItem("amulet");
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

    public void RemoveItem(int id){
        Item item = CheckForItem(id);
        if (item != null){
            characterItems.Remove(item);
            inventoryUI.RemoveItem(item);
            Debug.Log("Item removed: " + item.title);
        }
    }


}
