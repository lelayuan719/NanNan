using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HidingPlace : MonoBehaviour
{
    [SerializeField] float canHideDistance = 0.3f;
    [SerializeField] LightFader[] externalLights;

    float hideTime = 1f;
    float emergeTime = 0.5f;
    float playerHeight;

    HidingController playerHide;
    LightFader thisFader;

    private void Start()
    {
        StartCoroutine(AfterStart());
    }

    IEnumerator AfterStart()
    {
        yield return new WaitForEndOfFrame();
        playerHide = GameManager.GM.player.GetComponent<HidingController>();
        thisFader = GameManager.GM.player.GetComponentInChildren<LightFader>();
        playerHeight = GameManager.GM.player.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // If we are close enough and press space, we hide
        if (Input.GetKeyDown(KeyCode.Space)
            && playerHide.canHide
            && Mathf.Abs(transform.position.y - GameManager.GM.player.transform.position.y) < playerHeight
            && Mathf.Abs(transform.position.x - GameManager.GM.player.transform.position.x) < canHideDistance
            )
        {
            ToggleHide();
        }
    }

    public void ToggleHide()
    {
        if (!playerHide.hiding)
            Hide();
        else
            Emerge();
    }

    public void Hide()
    {
        thisFader.FadeIn(hideTime);
        foreach (var externalFader in externalLights) externalFader.FadeOut(hideTime);
        playerHide.Hide();
    }

    public void Emerge()
    {
        thisFader.FadeOut(emergeTime);
        foreach (var externalFader in externalLights) externalFader.FadeIn(emergeTime);
        playerHide.Emerge();
    }
}
