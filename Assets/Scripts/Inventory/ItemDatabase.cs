using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake(){
        BuildDatabase();
    }

    public Item GetItem(int id){
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName){
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase(){
        items = new List<Item>(){
            new Item(0,"bear","A stuffed bear"),
            new Item(1,"book","A book"),
            new Item(2,"amulet","A magical amulet"),
            new Item(3,"matches","Matches"),
            new Item(4,"shard","A sharp shard"),
            new Item(5,"key", "A strange key"),
            new Item(6,"fruit","A tasty fruit"),
        };
    }
}
