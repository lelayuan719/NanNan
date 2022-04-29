using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI charName;
    public TextAsset inkJSON;
    private Story story;
    private string sentence;
    public string[] character;
    private int index;
    public float typingSpeed;
    private AudioSource source;
    public PlayerController playerController;
    public GameObject npc;
    private bool spacePressed = false;
    public Coroutine typer;
    private GameObject curChar;
    private Animator curAnim;


    void Start()
    {
        ExecuteAfterTime(1);
        source = GetComponent<AudioSource>();
        story = new Story(inkJSON.text);
        sentence = story.Continue();
        typer = null;
    }

    public void TriggerDialog()
    {
        playerController.playerCanMove = false;
        playerController.speed = 0;
        curChar = GameObject.Find(character[0]);
        curAnim = curChar.GetComponent<Animator>();
        curAnim.SetBool("isTalking",true);
        typer = StartCoroutine(Type());
    }

    void Update(){
        if(textDisplay.text == sentence
           && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))){
            NextSentence();
        } else if (npc.GetComponent<NPC>().dialogStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))){
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
            curAnim.SetBool("isTalking",false);
            sentence = story.Continue();
            index++;
            curChar = GameObject.Find(character[index]);
            curAnim = curChar.GetComponent<Animator>();
            curAnim.SetBool("isTalking",true);
            textDisplay.text = "";
            typer = StartCoroutine(Type());
        } else{
            curAnim.SetBool("isTalking",false);
            textDisplay.text = "";
            textDisplay.enabled = false;
            source.enabled = false;
            playerController.playerCanMove = true;
            playerController.speed = 100;
            SpriteRenderer npcspr = npc.GetComponent<SpriteRenderer>();
            npcspr.enabled = false;
            foreach (Renderer r in npc.GetComponentsInChildren(typeof(Renderer)))
            {
                r.enabled = false;
            }
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
     yield return new WaitForSeconds(time);
    }
}
