using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : MonoBehaviour
{
    public Dialog2 dialog;
    private bool dialogStarted = false;

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player") && !dialogStarted){
            dialog.TriggerDialog();
            dialogStarted = true;
        }
    }
}
