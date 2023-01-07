using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Floor2Chase : MonoBehaviour
{
    [SerializeField] Transform door;

    public void StartChase()
    {
        UnityEvent onComplete = new UnityEvent();
        onComplete.AddListener(delegate { door.GetComponent<SimpleTransitionSameScene>().Transition(); });
        GameManager.GM.player.GetComponent<PlayerController>().MoveTo(door, onComplete, true);
    }
}
