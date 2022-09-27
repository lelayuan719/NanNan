using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public Inventory inventory;
    public UIInventory inventoryUI;

    public bool gameIsRunning = true;

    // Runs before a scene gets loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadGM()
    {
        GameObject GM = Instantiate(Resources.Load("Prefabs/GameManager")) as GameObject;
        DontDestroyOnLoad(GM);
    }

    private void Awake()
    {
        //Application.targetFrameRate = 30;

        if (GM == null)
        {
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // It is save to remove listeners even if they
        // didn't exist so far.
        // This makes sure it is added only once
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // Add the listener to be called when a scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        inventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsRunning) { return; }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Disables inventory in prologue
        // If we add prologue scene after Grandma this will need to be changed
        if (scene.name.StartsWith("Prologue")) {
            transform.Find("Inventory").gameObject.SetActive(false);
        }
    }

    // Activates the inventory for the first time
    public void ActivateInventory()
    {
        transform.Find("Inventory").gameObject.SetActive(true);
    }
}