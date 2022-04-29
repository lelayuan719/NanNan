using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialog dialog;
    public bool dialogStarted = false;
    private GameObject target;
    public float maxDist;

    void Start () 
    {
        target = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player") && !dialogStarted){
            dialog.TriggerDialog();
            dialogStarted = true;
        }
    }

    private void OnMouseDown(){
        if (!dialogStarted && Vector2.Distance(transform.position, target.transform.position) < maxDist){
            dialog.TriggerDialog();
            dialogStarted = true;
        }
    }
}
