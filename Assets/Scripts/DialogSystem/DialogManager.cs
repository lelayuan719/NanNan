using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI charName;
    public GameObject buttonObject;
    public GameObject player;
    public GameObject choiceButtonPrefab;
    public bool dialogActive = false;
    [SerializeField] private Alias[] _aliases;

    private List<GameObject> choiceButtons = new List<GameObject>();
    [HideInInspector] public AudioSource nextDialogSound;
    public Dictionary<string, GameObject> aliases;

    void Awake()
    {
        aliases = _aliases.ToDictionary(x => x.name, x => x.obj);
        nextDialogSound = GetComponent<AudioSource>();
        GameManager.GM.dialogManager = this;
    }

    public void FreezeCharacter()
    {
        player.GetComponent<GenericController>().playerCanMove = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Animator>().SetBool("isWalking", false);
    }

    public void UnfreezeCharacter()
    {
        player.GetComponent<GenericController>().playerCanMove = true;
    }

    public void SetupChoices(List<string> choices, ChoiceHandler actions, UnityAction onChoose, MyStory story)
    {
        buttonObject.SetActive(true);

        // Instantiate choices
        for (int i = 0; i < choices.Count; i++)
        {
            GameObject choiceObject = Instantiate(choiceButtonPrefab, buttonObject.transform);

            Button choiceButton = choiceObject.GetComponent<Button>();
            (choiceButton.targetGraphic as TextMeshProUGUI).text = choices[i];
            
            int j = i; // Very necessary to make the delegate closure work!
            choiceButton.onClick.AddListener(delegate { actions.MakeChoice(j); story.ChooseChoiceIndex(j); });
            choiceButton.onClick.AddListener(onChoose);

            choiceButtons.Add(choiceObject);
        }
    }

    public void RemoveChoices()
    {
        foreach (var button in choiceButtons)
        {
            Destroy(button);
        }

        choiceButtons = new List<GameObject>();
        buttonObject.SetActive(false);
    }
}

[System.Serializable]
public class Alias
{
    public string name;
    public GameObject obj;
}