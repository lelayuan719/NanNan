using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMatch: MonoBehaviour
{
    public UIItem collectedItem;
    public Item item;
    public GameObject interactionItem;
    public string itemCheck;
    public bool success;
    // Start is called before the first frame update
    void Start()
    {
        collectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        item = collectedItem.item;
        print(item);
        success = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        //collectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        item = collectedItem.item;
        if (collectedItem == null)
        {
            print("no item selected");
        }
        if (collectedItem.item == null)
         {
             print("whattt");
         }
         else if(item.title == itemCheck)
        {
            success = true;
            print(itemCheck + " match successful");
        } else
        {
            print("wrong item match");
        }
       /* if(collectedItem.item.title == itemCheck)
         {
             print("matches used on candle");
         }
         */
    }

}
