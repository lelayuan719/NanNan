using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : MonoBehaviour
{
    public Texture2D texture;
    public Inventory inv;


    // Start is called before the first frame update
    void OnMouseDown(){
        inv.GiveItem(gameObject.name);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameObject.SetActive(false);
    }

    void OnMouseOver(){
    }
    void OnMouseExit(){
    }
}
