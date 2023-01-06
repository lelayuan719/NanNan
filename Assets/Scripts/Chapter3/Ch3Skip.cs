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
            GameManager.GM.cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            skipLiFetchCam.SetActive(true);
        }
        // Second floor
        if (skipI >= 3)
        {
            GameManager.GM.inventory.RemoveItem("waterCup");
            GameManager.GM.inventory.RemoveItem("mathBook");
            GameManager.GM.inventory.GiveItem("stairKey");
            player.transform.position = unlockSecondFloorDest.position;
            GameManager.GM.cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            hallwayCam.SetActive(true);
            foreach (var obj in unlockSecondFloorDisable) obj.SetActive(false);
        }
        // Starting hide and seek
        if (skipI >= 4)
        {
            GameManager.GM.inventory.RemoveItem("stairKey");
            GameManager.GM.inventory.RemoveItem("noteFragment");
            principal.transform.position = principalDest.position;
            player.transform.position = startHideSeekDest.position;
            GameManager.GM.cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            hallwayCam.SetActive(true);
            player.GetComponent<HidingController>().SetCanHide(true);
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
