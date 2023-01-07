using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickMonologueTrigger : MonoBehaviour
{
    void OnMouseDown()
    {
        ActivateDialog();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ActivateDialog();
    }
    
    void ActivateDialog()
    {
        GetComponent<Dialog>().TriggerDialog();
    }
}
