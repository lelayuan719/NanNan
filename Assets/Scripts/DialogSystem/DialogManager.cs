using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI charName;
    public GameObject player;
    public bool dialogActive = false;
    [SerializeField] private Alias[] _aliases;

    [HideInInspector] public AudioSource nextDialogSound;
    public Dictionary<string, GameObject> aliases;

    void Awake()
    {
        aliases = _aliases.ToDictionary(x => x.name, x => x.obj);
        nextDialogSound = GetComponent<AudioSource>();
        GameManager.GM.dialogManager = this;
    }
}

[System.Serializable]
public class Alias
{
    public string name;
    public GameObject obj;
}