using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    [SerializeField] float canHideDistance = 0.5f;

    HidingController playerHide;

    private void Start()
    {
        StartCoroutine(AfterStart());
    }

    IEnumerator AfterStart()
    {
        yield return new WaitForEndOfFrame();
        playerHide = GameManager.GM.player.GetComponent<HidingController>();
    }

    void Update()
    {
        // If we are close enough and press space, we hide
        if (Input.GetKeyDown(KeyCode.Space)
            && playerHide.canHide
            && (Vector3.Distance(transform.position, playerHide.gameObject.transform.position) < canHideDistance))
        {
            playerHide.ToggleHide();
        }
    }
}
