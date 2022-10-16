using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTooLong : MonoBehaviour
{
    [SerializeField] private float waitTime; // seconds
    private Dialog waitTooLongDialog;
    private bool completed;

    private void Start()
    {
        waitTooLongDialog = GetComponent<Dialog>();
    }

    void Update()
    {
        waitTime -= Time.deltaTime;
        if (!completed && (waitTime <= 0) && !GameManager.GM.dialogManager.dialogActive)
        {
            waitTooLongDialog.TriggerDialog();
            completed = true;
        }
    }
}
