using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class EndlessRunnerController : MonoBehaviour
{
    public static EndlessRunnerController Instance;
    public static float scrollSpeed = 5;
    public static char[] letters = "RESURRECTION".ToCharArray();

    public bool running = true;
    [Tooltip("Skips most of the level, only needing to complete one QTE")]
    public bool skipMost;
    [SerializeField] float spawnDelay = 4.0f;

    [SerializeField] GameObject QTEPrefab;
    [SerializeField] Transform QTESpawn;
    [SerializeField] TextMeshProUGUI letterDisplay;
    [SerializeField] Animator anim;

    List<int> unusedLetterI;
    List<int> usedLetterI;

    int lostLetterCount = 2;

    private void Awake()
    {
        Instance = this;

        ResetScene();
    }

    private void Start()
    {
        InvokeRepeating("SpawnQTE", spawnDelay, spawnDelay);
        anim.SetBool("isWalking", true);

        // DEBUG skip most of the level
        if (skipMost)
        {
            for (int i = 1; i < letters.Length; i++)
            {
                int letterI = unusedLetterI.RandomElement();
                CollectLetter(letterI);
            }
        }
    }

    void ResetItemLists()
    {
        unusedLetterI = Enumerable.Range(0, letters.Length).ToList();
        usedLetterI = new List<int>();
    }

    void ResetScene()
    {
        ResetItemLists();
    }

    void SpawnQTE()
    {
        if (unusedLetterI.Count == 0) return;

        // Get letter
        int letterI = unusedLetterI.RandomElement();
        string letter = letters[letterI].ToString();

        // Spawn object
        GameObject qte = Instantiate(QTEPrefab, QTESpawn.position, Quaternion.identity);
        qte.GetComponent<QTE>().Init(letterI, letter);
    }

    public void CollectLetter(int letterI)
    {
        CollectLetterList(letterI);
        RegenerateDisplay();

        // Test for completing
        if (unusedLetterI.Count == 0)
        {
            Win();
        }
    }

    void CollectLetterList(int letterI)
    {
        unusedLetterI.Remove(letterI);
        usedLetterI.Add(letterI);
    }

    void RemoveLetterList(int letterI)
    {
        usedLetterI.Remove(letterI);
        unusedLetterI.Add(letterI);
    }

    public void MissQTE()
    {
        // Missing removes 2 letters, so we lose if not enough
        if (usedLetterI.Count < lostLetterCount)
        {
            GetCaught();
            return;
        }

        // Otherwise, lose two letters normally
        for (int i = 0; i < lostLetterCount; i++)
        {
            int letterI = usedLetterI[^1];
            RemoveLetterList(letterI);
        }
        RegenerateDisplay();
    }

    // Win the game
    void Win()
    {
        running = false;
        anim.SetBool("isWalking", false);
        CancelInvoke();
        letterDisplay.GetComponent<TextFader>().FadeOut(1f);

        print("Won");
    }

    // Getting caught means you lose unless you have the amulet
    void GetCaught()
    {
        Item item;
        if ((item = GameManager.GM.inventory.CheckForItem("amulet")) != null)
        {
            GameManager.GM.inventory.RemoveItem(item);
            ResetItemLists();
            print("Has amulet, got extra life");
        }
        else
        {
            Lose();
        }
        RegenerateDisplay();
    }

    // Get caught and lose
    void Lose()
    {
        ResetScene();
        GetComponent<SimpleTransitionSameScene>().Transition();
        anim.SetBool("isWalking", true);
        print("Lost");
    }

    // Render all collected letters at top
    void RegenerateDisplay()
    {
        List<char> usedLetterList = usedLetterI.ConvertAll(i => letters[i]);
        letterDisplay.text = string.Concat(usedLetterList);
    }
}
