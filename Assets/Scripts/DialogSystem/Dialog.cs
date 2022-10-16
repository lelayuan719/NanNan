using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Ink.Runtime;
using System;

public class Dialog : MonoBehaviour
{
    public TextAsset inkJSON;
    public GameObject npc;
    public GameObject background;

    public float typingSpeed = 0.05f;
    public Coroutine typer;
    public UnityEvent onComplete;

    public MyStory story;
    [HideInInspector] public bool completed;

    private DialogManager dialogManager;
    private string sentence;
    private int index;
    private bool spacePressed = false;
    private GameObject curChar;
    private Animator curAnim;

    void Start()
    {
        ExecuteAfterTime(1);

        dialogManager = GameManager.GM.dialogManager;
        story = new MyStory(inkJSON.text);
        sentence = story.Continue();
        typer = null;
        completed = false;
    }

    public void TriggerDialog()
    {
        dialogManager.textDisplay.enabled = true;
        dialogManager.player.GetComponent<PlayerController>().playerCanMove = false;
        dialogManager.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        dialogManager.player.GetComponent<Animator>().SetBool("isWalking", false);
        if (background) background.SetActive(true);

        if (dialogManager.aliases.ContainsKey(story.currentCharacter))
        {
            curChar = dialogManager.aliases[story.currentCharacter];
            if (curChar.TryGetComponent<Animator>(out curAnim));
            curAnim.SetBool("isTalking",true);
        }
        typer = StartCoroutine(Type());
    }

    void Update(){
        if(dialogManager.textDisplay.text == sentence
           && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))){
            NextSentence();
        } else if (npc.GetComponent<NPC>().dialogStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))){
            StopCoroutine(typer);
            dialogManager.textDisplay.text = sentence;
        }
    }

    IEnumerator Type(){
        if (!spacePressed){
           foreach(char letter in sentence){
                dialogManager.textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
    
    private void NextSentence(){
        dialogManager.nextDialogSound.Play();

        if(story.canContinue)
        {
            if (curAnim) curAnim.SetBool("isTalking",false);
            sentence = story.Continue();
            index++;

            if (dialogManager.aliases.ContainsKey(story.currentCharacter))
            {
                curChar = dialogManager.aliases[story.currentCharacter];
                curAnim = curChar.GetComponent<Animator>();
                curAnim.SetBool("isTalking", true);
            }

            dialogManager.textDisplay.text = "";
            typer = StartCoroutine(Type());
        } 
        else
        {
            completed = true;

            if (background) background.SetActive(false);
            if (dialogManager.aliases.ContainsKey(story.currentCharacter))
            {
                curAnim.SetBool("isTalking", false);
            }

            dialogManager.textDisplay.text = "";
            dialogManager.textDisplay.enabled = false;

            dialogManager.player.GetComponent<PlayerController>().playerCanMove = true;

            onComplete.Invoke();
            enabled = false;
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}