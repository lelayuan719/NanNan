using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public GameObject notificationDot;
    private Image spriteImage;
    private UIItem selectedItem;
    private UIInventory inventoryUI;
    private Tooltip tooltip;
    private Vector2 startDragPos;
    public int slot;
    public bool seen = true;

    bool _read = true;
    public bool read
    {
        get
        {
            return _read;
        }
        set
        {
            _read = value;
            if (notificationDot) notificationDot.SetActive(!_read);
        }
    }

    public string Name { get; } = "Some Name";

    private void Awake(){
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItem = GameManager.GM.inventoryUI.selectedItem;
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
        seen = true;
    }

    void Start()
    {
        inventoryUI = GameManager.GM.inventoryUI;
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null){
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
        } 
        else {
            spriteImage.color = Color.clear;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startDragPos = eventData.position;
        inventoryUI.SwapItems(slot);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (eventData.position != startDragPos)
        {
            // Swapping
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (var result in results)
            {
                if (result.gameObject.TryGetComponent(out UIItem uiItem))
                {
                    if (uiItem.slot != inventoryUI.mostRecentSlot)
                    {
                        inventoryUI.SwapItems(uiItem.slot);
                        return;
                    }
                }
            }
            
            // Checking for item matches
            Vector2 point = Camera.main.ScreenToWorldPoint(eventData.position);
            Collider2D[] hits = Physics2D.OverlapPointAll(point);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out ItemMatch match))
                {
                    match.CheckItemMatch();
                }
            }

            // Finally, swap back to most recent slot
            inventoryUI.SwapItems(slot);
        }
    }

    // Following
    public void StartFollowing()
    {
        inventoryUI.tooltip.gameObject.SetActive(false);
        inventoryUI.mostRecentSlot = slot;
        inventoryUI.selectedItem = this;
    }

    public void StopFollowing()
    {
        transform.localPosition = Vector2.zero;
        inventoryUI.selectedItem = null;
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(this.item != null){
            tooltip.GenerateTooltip(this.item);
            read = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        tooltip.gameObject.SetActive(false);
    }
}
