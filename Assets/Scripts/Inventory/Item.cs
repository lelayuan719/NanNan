using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string title;
    public string displayName;
    public string description;
    public Sprite icon;

    public Item(string title, string displayName, string description){
        this.title = title;
        this.displayName = displayName;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + title);
    }

    public Item(Item item){
        this.title = item.title;
        this.displayName = item.displayName;
        this.description = item.description;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.title);
    }

}
