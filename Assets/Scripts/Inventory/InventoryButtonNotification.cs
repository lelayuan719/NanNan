using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButtonNotification : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject notification;
    [SerializeField] Sprite closedSprite;
    [SerializeField] Sprite openSprite;

    private Image currentImage;

    void Start()
    {
        currentImage = notification.GetComponent<Image>();
    }

    // Hovering over
    public void OnPointerEnter(PointerEventData eventData)
    {
        currentImage.sprite = openSprite;
    }

    // Not hovering over
    public void OnPointerExit(PointerEventData eventData)
    {
        currentImage.sprite = closedSprite;
        GameManager.GM.inventoryUI.RefreshNotification();
    }
}
