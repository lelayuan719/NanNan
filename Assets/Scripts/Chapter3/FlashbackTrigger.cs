using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlashbackTrigger : MonoBehaviour
{
    public Transform destination;
    public GameObject[] disableMe;
    public GameObject[] enableMe;
    public UnityEvent onTriggered;
    public UnityEvent onReturn;

    private Vector3 returnPos;

    void Start()
    {

    }

    public void StartFlashback()
    {
        // Teleport
        returnPos = GameManager.GM.player.transform.position;
        GameManager.GM.player.GetComponent<PlayerController>().TeleportTo(destination);

        // Activate and deactivate
        foreach (var obj in disableMe) obj.SetActive(false);
        foreach (var obj in enableMe) obj.SetActive(true);
        GetComponent<Collider2D>().enabled = false;

        // Set cursor to null
        CursorManager.SetCursor(null);

        // Start events
        onTriggered.Invoke();
    }

    public void EndFlashback()
    {
        foreach (var obj in disableMe) obj.SetActive(true);
        foreach (var obj in enableMe) obj.SetActive(false);

        GameManager.GM.player.GetComponent<PlayerController>().TeleportTo(returnPos);
        onReturn.Invoke();
    }

    void OnMouseDown()
    {
        GetComponent<SimpleTransitionSameScene>().Transition();
    }

    void Update()
    {
        
    }
}
