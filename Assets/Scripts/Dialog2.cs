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
    public string[] character;
    private Story story;
    private string sentence;
    private int index;
    public float typingSpeed;
    private AudioSource source;
    public PlayerController playerController;
    public GameObject npc;
    private bool spacePressed = false;
    private Coroutine typer;
    public bool disappear;
    /*private GameObject curChar;
    private Animator curAnim;*/


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
        playerController.GetComponent<Animator>().SetBool("isWalking", false);
        background.SetActive(true);
        textDisplay.enabled = true;
        /*curChar = GameObject.Find(character[0]);
        curAnim = curChar.GetComponent<Animator>();
        curAnim.SetBool("isTalking",true);*/
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
            //curAnim.SetBool("isTalking",false);
            sentence = story.Continue();
            index++;
            //character list must match names of character GameObjects
            /*curChar = GameObject.Find(character[index]);
            curAnim = curChar.GetComponent<Animator>();
            curAnim.SetBool("isTalking",true);*/
            textDisplay.text = "";
            typer = StartCoroutine(Type());
        } else{
            //curAnim.SetBool("isTalking",false);
            textDisplay.text = "";
            textDisplay.enabled = false;
            background.SetActive(false);
            playerController.playerCanMove = true;
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

