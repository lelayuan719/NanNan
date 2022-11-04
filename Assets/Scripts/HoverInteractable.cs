using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverInteractable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public CursorTypes newCursor = CursorTypes.Question;
    [SerializeField] private bool canPickUp = false;

    // Start is called before the first frame update
    void Start()
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
        CursorManager.SetCursor(newCursor);

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
        CursorManager.SetCursor(null);

        if (canPickUp)
        {
            GameManager.GM.inventoryUI.inventoryButtonHelper.Close();
        }
    }
}
