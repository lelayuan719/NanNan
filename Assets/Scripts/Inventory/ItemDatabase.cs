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
            ("brokenAmulet", "Broken Amulet", "A shattered magical amulet"),
            ("matches", "Matches", "Matches"),
            ("shard", "Shard", "A sharp shard"),
            ("key", "Key", "A strange key"),
            ("fruit", "Fruit", "A tasty fruit"),
            ("tokenRat", "Rat Token", "An embossed rat token"),
            ("tokenHedgehog", "Hedgehog Token", "An embossed hedgehog token"),
            ("tokenFox", "Fox Token", "An embossed fox token"),
            ("tokenSnake", "Snake Token", "An embossed snake token"),
            ("waterCup", "Water Cup", "A cup of water."),
            ("mathBook", "Math Textbook", "A worn out textbook, put through years of use."),
            ("stairKey", "Stairwell Key", "A shabby copper key."),
            ("emptyCup", "Empty Cup", "An empty paper cup."),
            ("noteFragments", "Note Fragments", "A fragment of a suicide note."),
            ("noteComplete", "Suicide Note", "Xiaoying's suicide note."),
            ("instrumentNecklace", "Strange Object", "A ritual instrument with a necklace tightly wrapped around it."),
        };

        items = _items.ToDictionary(x => x.Item1, x => new Item(x.Item1, x.Item2, x.Item3));
    }
}