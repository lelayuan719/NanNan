using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueTrigger : MonoBehaviour
{
    bool read = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateDialog();
        }
    }

    void ActivateDialog()
    {
        if (read == false)
        {
            GetComponent<Dialog>().TriggerDialog();
            read = true;
        }
    }
}
