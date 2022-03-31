using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;

    public void Start(){
        GiveItem(0);
        GiveItem("Bear");
        RemoveItem(0);
    }

    public void GiveItem(int id){
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        Debug.Log("added item: " + itemToAdd.title);

    }

    public void GiveItem(string itemName){
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        Debug.Log("added item: " + itemToAdd.title);

    }

    public Item CheckForItem(int id){
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id){
        Item item = CheckForItem(id);
        if (item != null){
            characterItems.Remove(item);
            Debug.Log("Item removed: " + item.title);
        }
    }


}
