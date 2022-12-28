using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlashbackTrigger : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] CinemachineVirtualCamera oldCamera;
    [SerializeField] CinemachineVirtualCamera newCamera;
    [SerializeField] GameObject[] disableMe;
    [SerializeField] GameObject[] enableMe;
    [SerializeField] UnityEvent onTriggered;
    [SerializeField] UnityEvent onReturn;

    private Vector3 returnPos;

    void Start()
    {

    }

    public void StartFlashback()
    {
        // Teleport
        returnPos = GameManager.GM.player.transform.position;
        GameManager.GM.player.transform.position = destination.position;

        // Activate and deactivate
        foreach (var obj in disableMe) obj.SetActive(false);
        foreach (var obj in enableMe) obj.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
        oldCamera.gameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);

        // Set cursor to null
        CursorManager.SetCursor(null);

        // Start events
        onTriggered.Invoke();
    }

    public void EndFlashback()
    {
        // Teleport
        GameManager.GM.player.transform.position = returnPos;

        // Activate and deactivate
        foreach (var obj in disableMe) obj.SetActive(true);
        foreach (var obj in enableMe) obj.SetActive(false);
        newCamera.gameObject.SetActive(false);
        oldCamera.gameObject.SetActive(true);

        // Start events
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
