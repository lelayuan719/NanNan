using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour, IPointerClickHandler
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
        Collect();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Collect();
    }

    public void Collect()
    {
        inv.GiveItem(gameObject.name);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        // gameObject.SetActive(false);
        if (TryGetComponent(out SpriteRenderer render))
            render.enabled = false;
        else if (TryGetComponent(out Image image))
            image.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        onPickup.Invoke();
    }
    

}
