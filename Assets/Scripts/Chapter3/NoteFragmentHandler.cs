using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFragmentHandler : MonoBehaviour
{
    public const int MAX_FRAGMENTS = 7;
    public int collectedFragments = 0;

    public void CollectNote()
    {
        collectedFragments++;
        Debug.LogFormat("Collected {0} / {1} fragments", collectedFragments, MAX_FRAGMENTS);

        if (collectedFragments == 1)
        {
            GameManager.GM.inventory.GiveItem("noteFragment");
        }
    }
}
