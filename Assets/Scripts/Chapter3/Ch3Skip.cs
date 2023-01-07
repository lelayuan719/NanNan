using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ch3Skip : MonoBehaviour
{
    enum Skip
    {
        Nothing,
        Flashbacks,
        LiFetch,
        UnlockSecondFloor,
        StartHideAndSeek,
        EndHideAndSeek,
    }
    [SerializeField] Skip skipAfter;

    [Foldout("Skip Flashback")] [SerializeField] FlashbackTrigger finalFlashback;

    [Foldout("Li Skip")] [SerializeField] Transform skipLiFetchDest;
    [Foldout("Li Skip")] [SerializeField] GameObject skipLiFetchCam;
    [Foldout("Li Skip")] [SerializeField] GameObject[] liSkipDisable;
    [Foldout("Li Skip")] [SerializeField] UnityEvent onLiSkip;

    [Foldout("Unlock Second Floor")] [SerializeField] Transform unlockSecondFloorDest;
    [Foldout("Unlock Second Floor")] [SerializeField] GameObject hallwayCam;
    [Foldout("Unlock Second Floor")] [SerializeField] GameObject[] unlockSecondFloorDisable;

    [Foldout("Start Hide and Seek")] [SerializeField] Transform startHideSeekDest;
    [Foldout("Start Hide and Seek")] [SerializeField] DoorTeleport2 stairDoor;
    [Foldout("Start Hide and Seek")] [SerializeField] GameObject principal;
    [Foldout("Start Hide and Seek")] [SerializeField] Transform principalDest;
    [Foldout("Start Hide and Seek")] [SerializeField] Ch2ActivateWell confinerChange;
    [Foldout("Start Hide and Seek")] [SerializeField] GameObject[] startHideSeekDisable;
    [Foldout("Start Hide and Seek")] [SerializeField] UnityEvent onStartHideSeek;

    //[Foldout("End Hide and Seek")] [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        int skipI = (int)skipAfter;

        GameObject player = GameManager.GM.player;

        // Flashbacks
        if (skipI >= 1)
        {
            finalFlashback.onReturn.Invoke();

            // Notify the player of skipping
            print("To stop skipping, change Debug Skip > Ch 3 Skip > Skip After to \"Nothing\".");
        }
        // Li Fetch
        if (skipI >= 2)
        {
            StartCoroutine(StopStartDialog());
            GameManager.GM.inventory.GiveItem("waterCup");
            GameManager.GM.inventory.GiveItem("mathBook");
            player.transform.position = skipLiFetchDest.position;
            GameManager.GM.ChangeActiveCam(skipLiFetchCam);
            foreach (var obj in liSkipDisable) obj.SetActive(false);
            onLiSkip.Invoke();
        }
        // Second floor
        if (skipI >= 3)
        {
            GameManager.GM.inventory.RemoveItem("waterCup");
            GameManager.GM.inventory.RemoveItem("mathBook");
            GameManager.GM.inventory.GiveItem("stairKey");
            player.transform.position = unlockSecondFloorDest.position;
            GameManager.GM.ChangeActiveCam(hallwayCam);
            foreach (var obj in unlockSecondFloorDisable) obj.SetActive(false);
        }
        // Starting hide and seek
        if (skipI >= 4)
        {
            GameManager.GM.inventory.RemoveItem("stairKey");
            principal.transform.position = principalDest.position;
            player.transform.position = startHideSeekDest.position;
            player.GetComponent<HidingController>().SetCanHide(true);
            player.GetComponent<NoteFragmentHandler>().SpawnNotes();
            player.GetComponent<NoteFragmentHandler>().CollectNote();
            player.GetComponent<PlayerController>().SetInstantDoors(true);
            principal.SetActive(true);
            principal.GetComponentInChildren<PrincipalHideAndSeek>().StartSeeking();
            foreach (var obj in startHideSeekDisable) obj.SetActive(false);
            onStartHideSeek.Invoke();
        }
        // Finishing hide and seek
        if (skipI >= 5)
        {
            var fragHandler = player.GetComponent<NoteFragmentHandler>();
            fragHandler.collectedFragments = NoteFragmentHandler.MAX_FRAGMENTS;
            fragHandler.DestroySceneNotes();
            fragHandler.CompleteNote();
        }
    }

    IEnumerator StopStartDialog()
    {
        yield return new WaitForEndOfFrame();
        GameManager.GM.dialogManager.StopDialog();
    }
}