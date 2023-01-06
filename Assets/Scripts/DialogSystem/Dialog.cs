using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Ink.Runtime;

public class Dialog : MonoBehaviour
{
    public TextAsset inkJSON;
    public GameObject npc;
    public GameObject background;
    [SerializeField] private bool freezesCharacter = true;
    [SerializeField] private bool canRepeat = false;
    [SerializeField] private bool autoContinue = false;
    public float typingSpeed = 0.05f;

    public Coroutine typer;
    public UnityEvent onStart;
    public UnityEvent onComplete;
    [SerializeField] private ChoiceAction[] _choiceActions;
    public Dictionary<string, ChoiceHandler> choiceActions;
    [SerializeField] private EventDictionary[] _midwayActions;
    public Dictionary<string, UnityEvent> midwayActions;

    public MyStory story;
    [HideInInspector] public bool running = false;
    [HideInInspector] public bool choosing;
    [HideInInspector] public bool completed;

    private DialogManager dialogManager;
    private string sentence;
    private int index;
    private bool spacePressed = false;
    private float autoContinueTime = 2.0f;
    private GameObject curChar;
    private Animator curAnim;
    private UnityAction onChoose;

    void Awake()
    {
        choiceActions = _choiceActions.ToDictionary(x => x.name, x => x.choiceHandler);
        midwayActions = _midwayActions.ToDictionary(x => x.name, x => x.action);
        onChoose += MakeChoice;
        completed = false;
    }

    void Start()
    {
        dialogManager = GameManager.GM.dialogManager;
        typer = null;
    }

    IEnumerator TriggerDialogCoroutine()
    {
        // Stop double clicking
        yield return new WaitForEndOfFrame();
        
        running = true;
        dialogManager.dialogActive = true;
        dialogManager.textDisplay.enabled = true;
        if (freezesCharacter) dialogManager.FreezeCharacter();
        if (background) background.SetActive(true);

        story = new MyStory(inkJSON.text);
        NextSentence();
        onStart.Invoke();
    }

    public void TriggerDialog()
    {
        // If can't repeat and already completed, do nothing
        if (!canRepeat && completed) return;

        // If component is disabled, do nothing
        if (!enabled) return;

        // If dialog is already playing, do nothing
        dialogManager = GameManager.GM.dialogManager;
        if (dialogManager.dialogActive) return;

        // Otherwise, start the dialog
        StartCoroutine(TriggerDialogCoroutine());
    }

    void Update(){
        if (running)
        {
            if (dialogManager.textDisplay.text == sentence
                && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
                && !choosing)
            {
                // Stops auto-continuing the next line
                if (autoContinue) CancelInvoke();

                NextSentence();
            }
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                StopCoroutine(typer);

                // Stops auto-continuing the next line
                if (autoContinue) CancelInvoke();

                dialogManager.textDisplay.text = sentence;
            }
            // DEBUG skip
            else if (Input.GetKeyDown("."))
            {
                story.ContinueMaximally();
                foreach (var midwayAction in midwayActions.Values)
                {
                    midwayAction.Invoke();
                }
                NextSentence();
            }
        }
    }

    IEnumerator Type(){
        if (!spacePressed){
            int i = 0;
            char letter;
            while (i < sentence.Length)
            {
                letter = sentence[i];
                dialogManager.textDisplay.text += letter;
                i++;
                // Immediately apply rich text tags
                if ((letter == '<') && (i < sentence.Length))
                {
                    do
                    {
                        letter = sentence[i];
                        dialogManager.textDisplay.text += letter;
                        i++;
                    } while ((letter != '>') && (i < sentence.Length));

                    letter = sentence[i];
                    dialogManager.textDisplay.text += letter;
                    i++;
                }
                yield return new WaitForSeconds(typingSpeed);
            }

            // If we're done typing, start the next line soon
            if (autoContinue) Invoke(nameof(NextSentence), autoContinueTime);
        }
    }
    
    private void NextSentence(){
        dialogManager.nextDialogSound.Play();

        if(story.canContinue)
        {
            sentence = story.Continue();
            index++;

            // Animation
            if (curAnim) curAnim.SetBool("isTalking", false);
            if (dialogManager.aliases.ContainsKey(story.currentCharacter))
            {
                curChar = dialogManager.aliases[story.currentCharacter];
                curAnim = curChar.GetComponent<Animator>();
                curAnim.SetBool("isTalking", true);
            }

            // Midway events
            HandleMidwayEvents();

            // Choices
            SetupChoices();

            // Starting typing
            dialogManager.textDisplay.text = "";
            typer = StartCoroutine(Type());
        }
        // Finish dialog
        else
        {
            StopDialog();
        }
    }

    void SetupChoices()
    {
        if (story.canChoose)
        {
            choosing = true;
            string choiceName = story.currentTags[0];
            var actions = choiceActions[choiceName];
            List<string> choices = story.GetChoices();

            dialogManager.SetupChoices(choices, actions, onChoose, story);
        }
        else
        {
            dialogManager.RemoveChoices();
        }
    }

    void HandleMidwayEvents()
    {
        if (story.currentTags.Count > 0)
        {
            string eventName = story.currentTags[0];
            print(eventName);
            if (midwayActions.TryGetValue(eventName, out UnityEvent actions))
                actions.Invoke();
        }
    }

    void MakeChoice()
    {
        choosing = false;
        StopCoroutine(typer);
        NextSentence();
    }

    public void StopDialog()
    {
        if (!running) return;

        completed = true;
        running = false;
        dialogManager.dialogActive = false;

        if (background) background.SetActive(false);
        if (dialogManager.aliases.ContainsKey(story.currentCharacter))
        {
            curAnim.SetBool("isTalking", false);
        }

        dialogManager.textDisplay.text = "";
        dialogManager.textDisplay.enabled = false;
        dialogManager.RemoveChoices();

        if (freezesCharacter) dialogManager.UnfreezeCharacter();

        onComplete.Invoke();
        StopCoroutine(typer);
        if (autoContinue) CancelInvoke(); // Cancels auto continue invokes. This is unrelated to the previous lines!
        if (!canRepeat) enabled = false;
    }

    private void OnDisable()
    {
        if (running) StopDialog();
    }
}

[System.Serializable]
public class ChoiceAction
{
    public string name;
    public ChoiceHandler choiceHandler;
}

[System.Serializable]
public class EventDictionary
{
    public string name;
    public UnityEvent action;
}