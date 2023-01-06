using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFragmentHandler : MonoBehaviour
{
    public const int MAX_FRAGMENTS = 7;
    public int collectedFragments = 0;

    [SerializeField] GameObject notePrefab;
    [SerializeField] List<GameObject> noteLocations;

    public void SpawnNotes()
    {
        // Get which room has two notes in it
        int doubleRoom = Random.Range(0, noteLocations.Count);

        // Iterate through rooms
        for (int roomI = 0; roomI < noteLocations.Count; roomI++)
        {
            int[] allNoteI;

            // Get which rooms have a note in
            if (roomI == doubleRoom)
            {
                allNoteI = new int[] { 0, 1 };
            }
            else
            {
                allNoteI = new int[] { Random.Range(0, 2) };
            }

            // Spawn notes
            foreach (int noteI in allNoteI)
            {
                Transform noteLocation = noteLocations[roomI].transform.GetChild(noteI);
                SpawnNoteAt(noteLocation);
            }
        }
    }

    void SpawnNoteAt(Transform location)
    {
        GameObject note = Instantiate(notePrefab, location);
        note.transform.localPosition = Vector3.zero;
    }

    public void CollectNote()
    {
        collectedFragments++;
        Debug.LogFormat("Collected {0} / {1} fragments", collectedFragments, MAX_FRAGMENTS);

        if (collectedFragments == 1)
        {
            GameManager.GM.inventory.GiveItem("noteFragments");
        }

        Item noteItem = GameManager.GM.inventory.CheckForItem("noteFragments");
        noteItem.displayName = string.Format("Note Fragments ({0} / {1})", collectedFragments, MAX_FRAGMENTS);

        if (collectedFragments == 2)
        {
            noteItem.description = "Fragments of a suicide note.";
        }
    }
}
