using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : MonoBehaviour
{
    public Texture2D texture;
    public Inventory inv;


    // Start is called before the first frame update
    void OnMouseDown(){
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        inv.GiveItem(gameObject.name);
        gameObject.SetActive(false);
    }

    void OnMouseOver(){
        Cursor.SetCursor(texture,Vector2.zero,CursorMode.Auto);
    }
    void OnMouseExit(){
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }
}
