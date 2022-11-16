using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButtonNotification : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject notificationDot;

    public void OnPointerEnter(PointerEventData eventData)
    {
        notificationDot.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.GM.inventoryUI.RefreshNotification();
    }
}
