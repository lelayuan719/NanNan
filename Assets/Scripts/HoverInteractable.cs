using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverInteractable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Color startcolor;
    private SpriteRenderer spriteRenderer;
    public Texture2D newCursor;
    [SerializeField] private bool canPickUp = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Mouse entering
    void OnMouseEnter()
    {
        Hover();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover();
    }

    void Hover()
    {
        print("hovering");
        Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.Auto);

        if (canPickUp)
        {
            GameManager.GM.inventoryUI.inventoryButtonHelper.Open();
        }
    }

    // Mouse clicking
    void OnMouseDown()
    {
        Click();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }
    
    void Click()
    {
        if (canPickUp)
        {
            GameManager.GM.inventoryUI.inventoryButtonHelper.Close();
        }
    }

    // Mouse leaving
    void OnMouseExit()
    {
        ExitHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExitHover();
    }

    void ExitHover()
    {
        print("exit");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        if (canPickUp)
        {
            GameManager.GM.inventoryUI.inventoryButtonHelper.Close();
        }
    }
}
