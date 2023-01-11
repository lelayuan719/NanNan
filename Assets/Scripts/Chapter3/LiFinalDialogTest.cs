using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiFinalDialogTest : MonoBehaviour
{
    [SerializeField] GameObject cantLeaveObject;

    bool gotSticker;
    bool gotNecklace;
    Dialog finalDialog;

    private void Awake()
    {
        finalDialog = GetComponent<Dialog>();
    }

    public void GetSticker()
    {
        gotSticker = true;
        // If we haven't go the necklace yet, lock the player in until they get it
        if (!gotNecklace)
        {
            cantLeaveObject.SetActive(true);
        }
        CountUp();
    }

    public void GetNecklace()
    {
        gotNecklace = true;
        CountUp();
    }

    void CountUp()
    {
        if (gotSticker && gotNecklace)
        {
            finalDialog.TriggerDialog();
            cantLeaveObject.SetActive(false);
        }
    }
}
