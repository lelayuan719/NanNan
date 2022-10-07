using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    public Texture2D texture;
    private Inventory inv;

    public UnityEvent onPickup;

    private void Start()
    {
        inv = GameManager.GM.inventory;
    }

    // Start is called before the first frame update
    void OnMouseDown(){
        inv.GiveItem(gameObject.name);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameObject.SetActive(false);

        onPickup.Invoke();
    }

    void OnMouseOver(){
    }
    void OnMouseExit(){
    }
}
