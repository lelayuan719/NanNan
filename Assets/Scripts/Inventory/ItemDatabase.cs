using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Dictionary<string, Item> items = new Dictionary<string, Item>();

    private void Awake(){
        BuildDatabase();
    }

    public Item GetItem(string itemName){
        //return items.Find(item => item.title == itemName);
        return items[itemName];
    }

    void BuildDatabase(){
        //List<Item> _items = new List<Item>(){
        //    new Item("bear","A stuffed bear"),
        //    new Item("book","A book"),
        //    new Item("amulet","A magical amulet"),
        //    new Item("matches","Matches"),
        //    new Item("shard","A sharp shard"),
        //    new Item("key", "A strange key"),
        //    new Item("fruit","A tasty fruit"),
        //};
        var _items = new List<(string, string, string)>(){
            ("bear", "Bear", "A stuffed bear"),
            ("book", "Book", "A book"),
            ("amulet", "Amulet", "A magical amulet"),
            ("matches", "Matches", "Matches"),
            ("shard", "Shard", "A sharp shard"),
            ("key", "Key", "A strange key"),
            ("fruit", "Fruit", "A tasty fruit"),
        };

        items = _items.ToDictionary(x => x.Item1, x => new Item(x.Item1, x.Item2, x.Item3));
    }
}
