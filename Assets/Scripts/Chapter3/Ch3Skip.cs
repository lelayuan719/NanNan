using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3Skip : MonoBehaviour
{
    [SerializeField] bool skipFlashbacks;
    [SerializeField] bool skipLiFetch;

    [SerializeField] FlashbackTrigger finalFlashback;

    [Header("Li Skip")]
    [SerializeField] Transform skipLiFetchDest;
    [SerializeField] GameObject skipLiFetchCam;

    // Start is called before the first frame update
    void Start()
    {
        if (skipFlashbacks)
        {
            finalFlashback.onReturn.Invoke();
        }

        if (skipLiFetch)
        {
            GameManager.GM.inventory.GiveItem("waterCup");
            GameManager.GM.inventory.GiveItem("mathBook");
            GameManager.GM.player.transform.position = skipLiFetchDest.position;
            GameManager.GM.cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            skipLiFetchCam.SetActive(true);
        }
    }
}
