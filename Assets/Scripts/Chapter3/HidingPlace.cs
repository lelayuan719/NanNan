using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HidingPlace : MonoBehaviour
{
    [SerializeField] float canHideDistance = 0.3f;
    [SerializeField] bool hidesBehind = true;
    [SerializeField] LightFader[] externalLights;

    float hideTime = 1f;
    float emergeTime = 0.5f;
    float playerHeight;

    HidingController playerHide;
    LightFader thisFader;
    ChangeSortingLayer[] behindSprites = new ChangeSortingLayer[] { };

    private void Start()
    {
        if (hidesBehind)
        {
            behindSprites = GetComponentsInChildren<ChangeSortingLayer>();
        }
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
        // Fade in/out lights
        thisFader.FadeIn(hideTime);
        foreach (var externalFader in externalLights) externalFader.FadeOut(hideTime);

        // Change sprite order to put player behind things
        if (hidesBehind)
        {
            foreach (var behindSprite in behindSprites) behindSprite.PutInFrontPlayer();
        }
        else
        {
            GameManager.GM.player.GetComponent<ChangeSortingLayer>().ChangeLayer("Default");
        }

        playerHide.Hide();
    }

    public void Emerge()
    {
        // Restore lights
        thisFader.FadeOut(emergeTime);
        foreach (var externalFader in externalLights) externalFader.FadeIn(emergeTime);

        // Restore sprite order
        if (hidesBehind)
        {
            foreach (var behindSprite in behindSprites) behindSprite.PutBehindPlayer();
        }
        else
        {
            GameManager.GM.player.GetComponent<ChangeSortingLayer>().ChangeLayer("Player");
        }

        playerHide.Emerge();
    }
}
