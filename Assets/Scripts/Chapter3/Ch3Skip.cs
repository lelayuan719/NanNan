using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] GameObject unlockSecondFloorCam;
    [SerializeField] GameObject[] unlockSecondFloorDisable;

    // Start is called before the first frame update
    void Start()
    {
        int skipI = (int)skipAfter;

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
            GameManager.GM.player.transform.position = skipLiFetchDest.position;
            GameManager.GM.cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            skipLiFetchCam.SetActive(true);
        }
        // Second floor
        if (skipI >= 3)
        {
            GameManager.GM.inventory.GiveItem("stairKey");
            GameManager.GM.player.transform.position = unlockSecondFloorDest.position;
            GameManager.GM.cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            unlockSecondFloorCam.SetActive(true);
            foreach (var obj in unlockSecondFloorDisable) obj.SetActive(false);
        }
    }

    enum Skips
    {
        Nothing,
        Flashbacks,
        LiFetch,
        UnlockSecondFloor,
    }
}
