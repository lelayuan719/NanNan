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

    [SerializeField] Flashbacks flashbacks;
    [SerializeField] LiFetchSkip liFetchSkip;
    [SerializeField] UnlockSecondFloor unlockSecondFloor;
    [SerializeField] StartHideSeek startHideAndSeek;

    [System.Serializable]
    public struct Flashbacks
    {
        public FlashbackTrigger finalFlashback;
    }
    [System.Serializable]
    public struct LiFetchSkip
    {
        public Transform playerDest;
        public GameObject cam;
        public GameObject[] disableme;
        public UnityEvent onStart;
    }
    [System.Serializable]
    public struct UnlockSecondFloor
    {
        public Transform playerDest;
        public GameObject hallwayCam;
        public GameObject[] disableme;
    }
    [System.Serializable]
    public struct StartHideSeek
    {
        public Transform playerDest;
        public DoorTeleport2 stairDoor;
        public GameObject principal;
        public Transform principalDest;
        public Ch2ActivateWell confinerChange;
        public GameObject[] disableme;
        public UnityEvent onStart;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameManager.GM.player;

        // Flashbacks
        if (skipAfter >= Skip.Flashbacks)
        {
            flashbacks.finalFlashback.onReturn.Invoke();

            // Notify the player of skipping
            print("To stop skipping, change Debug Skip > Ch 3 Skip > Skip After to \"Nothing\".");
        }
        // Li Fetch
        if (skipAfter >= Skip.LiFetch)
        {
            StartCoroutine(StopStartDialog());
            GameManager.GM.inventory.GiveItem("waterCup");
            GameManager.GM.inventory.GiveItem("mathBook");
            player.transform.position = liFetchSkip.playerDest.position;
            GameManager.GM.ChangeActiveCam(liFetchSkip.cam);
            foreach (var obj in liFetchSkip.disableme) obj.SetActive(false);
            liFetchSkip.onStart.Invoke();
        }
        // Second floor
        if (skipAfter >= Skip.UnlockSecondFloor)
        {
            GameManager.GM.inventory.RemoveItem("waterCup");
            GameManager.GM.inventory.RemoveItem("mathBook");
            GameManager.GM.inventory.GiveItem("stairKey");
            player.transform.position = unlockSecondFloor.playerDest.position;
            GameManager.GM.ChangeActiveCam(unlockSecondFloor.hallwayCam);
            foreach (var obj in unlockSecondFloor.disableme) obj.SetActive(false);
        }
        // Starting hide and seek
        if (skipAfter >= Skip.StartHideAndSeek)
        {
            GameManager.GM.inventory.RemoveItem("stairKey");
            startHideAndSeek.principal.transform.position = startHideAndSeek.principalDest.position;
            player.transform.position = startHideAndSeek.playerDest.position;
            player.GetComponent<HidingController>().SetCanHide(true);
            player.GetComponent<NoteFragmentHandler>().SpawnNotes();
            player.GetComponent<NoteFragmentHandler>().CollectNote();
            player.GetComponent<PlayerController>().SetInstantDoors(true);
            startHideAndSeek.principal.SetActive(true);
            startHideAndSeek.principal.GetComponentInChildren<PrincipalHideAndSeek>().StartSeeking();
            foreach (var obj in startHideAndSeek.disableme) obj.SetActive(false);
            startHideAndSeek.onStart.Invoke();
        }
        // Finishing hide and seek
        if (skipAfter >= Skip.EndHideAndSeek)
        {
            var fragHandler = player.GetComponent<NoteFragmentHandler>();
            fragHandler.collectedFragments = NoteFragmentHandler.MAX_FRAGMENTS;
            fragHandler.DestroySceneNotes();
            fragHandler.CompleteNote();
        }
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 1);
    }

    IEnumerator StopStartDialog()
    {
        yield return new WaitForEndOfFrame();
        GameManager.GM.dialogManager.StopDialog();
    }
}