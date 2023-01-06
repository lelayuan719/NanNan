using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteFragmentPickup : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onPickup;

    private void Start()
    {

    }

    void OnMouseDown()
    {
        Collect();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Collect();
    }

    public void Collect()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        if (TryGetComponent(out SpriteRenderer render))
            render.enabled = false;
        else if (TryGetComponent(out Image image))
            image.enabled = false;

        if (TryGetComponent(out Collider2D collider))
            collider.enabled = false;

        onPickup.Invoke();
        GameManager.GM.player.GetComponent<NoteFragmentHandler>().CollectNote();
    }
}
