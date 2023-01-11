using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onPickup;

    private Inventory inv;
    Dialog pickupDialog;

    private void Start()
    {
        inv = GameManager.GM.inventory;
        TryGetComponent(out pickupDialog);
    }

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
        
        // Disable image
        if (TryGetComponent(out SpriteRenderer render))
            render.enabled = false;
        else if (TryGetComponent(out Image image))
            image.enabled = false;

        // Disable collider
        if (TryGetComponent(out Collider2D collider))
            collider.enabled = false;

        // Start dialog
        if (pickupDialog != null) pickupDialog.TriggerDialog();

        onPickup.Invoke();
    }
}
