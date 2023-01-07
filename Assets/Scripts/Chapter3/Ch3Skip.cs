using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ch3Skip : MonoBehaviour
{
    [SerializeField] Skips skipAfter;

    [Header("Flashback Skip")]
    [SerializeField] FlashbackTrigger finalFlashback;

    [Header("Li Skip")]
    [SerializeField] Transform skipLiFetchDest;
    [SerializeField] GameObject skipLiFetchCam;
    [SerializeField] GameObject[] liSkipDisable;
    [SerializeField] UnityEvent onLiSkip;

    [Header("Unlock Second Floor")]
    [SerializeField] Transform unlockSecondFloorDest;
    [SerializeField] GameObject hallwayCam;
    [SerializeField] GameObject[] unlockSecondFloorDisable;

    [Header("Start Hide and Seek")]
    [SerializeField] Transform startHideSeekDest;
    [SerializeField] DoorTeleport2 stairDoor;
    [SerializeField] GameObject principal;
    [SerializeField] Transform principalDest;
    [SerializeField] Ch2ActivateWell confinerChange;
    [SerializeField] GameObject[] startHideSeekDisable;
    [SerializeField] UnityEvent onStartHideSeek;

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
    }

    enum Skips
    {
        Nothing,
        Flashbacks,
        LiFetch,
        UnlockSecondFloor,
        StartHideAndSeek,
    }
}