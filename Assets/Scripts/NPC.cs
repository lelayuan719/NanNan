using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialog dialog;
    private bool dialogStarted = false;

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player") && !dialogStarted){
            dialog.TriggerDialog();
            dialogStarted = true;
        }
    }
}
