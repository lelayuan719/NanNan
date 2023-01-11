using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class NoteFragmentHandler : MonoBehaviour
{
    public const int MAX_FRAGMENTS = 7;
    public int collectedFragments = 0;

    [SerializeField] GameObject notePrefab;
    [SerializeField] List<Transform> noteLocations;
    [SerializeField] UnityEvent onCompleteNote;

    List<GameObject> spawnedNotes = new List<GameObject>();

    public void SpawnNotes()
    {
        // Get which room has two notes in it
        int doubleRoom = Random.Range(0, noteLocations.Count);

        // Iterate through rooms
        for (int roomI = 0; roomI < noteLocations.Count; roomI++)
        {
            int[] spawnNoteI;

            // Get which rooms have a note in
            if (roomI == doubleRoom)
            {
                spawnNoteI = new int[] { 0, 1 };
            }
            else
            {
                spawnNoteI = new int[] { Random.Range(0, 2) };
            }

            // Enable movable objects
            foreach (var collider in noteLocations[roomI].GetComponentsInChildren<Collider2D>())
            {
                collider.enabled = true;
                collider.gameObject.transform.Translate(0, 0, -1);
            }

            // Spawn notes
            foreach (int noteI in spawnNoteI)
            {
                Transform noteLocation = noteLocations[roomI].GetChild(noteI);
                SpawnNoteAt(noteLocation);
            }
        }
    }

    // On game over, returns everything to where it was
    public void ResetNotes()
    {
        // Inventory
        GameManager.GM.inventory.RemoveItem("noteFragments");
        collectedFragments = 0;
        CollectNote();

        // Respawn notes
        DestroySceneNotes();
        SpawnNotes();
    }

    public void DestroySceneNotes()
    {
        foreach (var note in spawnedNotes) Destroy(note);
        spawnedNotes = new List<GameObject>();
    }

    void SpawnNoteAt(Transform location)
    {
        // Spawn note
        GameObject note = Instantiate(notePrefab, location);
        note.transform.localPosition = Vector3.zero;
        spawnedNotes.Add(note);
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
        // TEMP
        else if (collectedFragments == MAX_FRAGMENTS)
        {
            CompleteNote();
        }
    }

    public void CompleteNote()
    {
        GameManager.GM.inventory.RemoveItem("noteFragments");
        GameManager.GM.inventory.GiveItem("noteComplete");
        onCompleteNote.Invoke();
    }
}
