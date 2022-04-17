using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class Dialog2 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject background;
    public TextAsset inkJSON;
    private Story story;
    private string sentence;
    private int index;
    public float typingSpeed;
    private AudioSource source;
    public PlayerControllerC2 playerController;
    public GameObject npc;
    private bool spacePressed = false;
    private Coroutine typer;
    public bool disappear;


    void Start()
    {
        source = GetComponent<AudioSource>();
        story = new Story(inkJSON.text);
        sentence = story.Continue();
        typer = null;
    }

    public void TriggerDialog()
    {
        playerController.playerCanMove = false;
        playerController.speed = 0;
        background.SetActive(true);
        textDisplay.enabled = true;
        typer = StartCoroutine(Type());
    }

    void Update(){
        if(textDisplay.text == sentence
           && Input.GetKeyDown(KeyCode.Space)){
            NextSentence();
        } else if (Input.GetKeyDown(KeyCode.Space)){
            StopCoroutine(typer);
            textDisplay.text = sentence;
        }
    }

    IEnumerator Type(){
        if (!spacePressed){
           foreach(char letter in sentence){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            }
        } 
        
    }
    
    public void NextSentence(){
        source.Play();

        if(story.canContinue){
            sentence = story.Continue();
            index++;
            textDisplay.text = "";
            typer = StartCoroutine(Type());
        } else{
            textDisplay.text = "";
            textDisplay.enabled = false;
            background.SetActive(false);
            playerController.playerCanMove = true;
            playerController.speed = 4;
            if(disappear){
                SpriteRenderer npcspr = npc.GetComponent<SpriteRenderer>();
                npcspr.enabled = false;
                foreach (Renderer r in npc.GetComponentsInChildren(typeof(Renderer)))
                {
                    r.enabled = false;
                }
            }
            
        }
    }
}

