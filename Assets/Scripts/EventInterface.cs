using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventInterface : MonoBehaviour
{
    public UnityEvent onLoaded;

    private void Start()
    {
        StartCoroutine(AfterStart());
    }

    IEnumerator AfterStart()
    {
        yield return new WaitForEndOfFrame();
        onLoaded.Invoke();
    }

    public void GiveItem(string name)
    {
        GameManager.GM.inventory.GiveItem(name);
    }

    public void RemoveItem(string name)
    {
        GameManager.GM.inventory.RemoveItem(name);
    }

    public void ChangeScene(string name)
    {
        GameManager.GM.LoadScene(name);
    }
}