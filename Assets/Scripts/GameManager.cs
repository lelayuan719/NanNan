using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Instance
    public static GameManager GM;

    // References
    public GameObject inventoryObj;
    public Inventory inventory;
    public UIInventory inventoryUI;
    public DialogManager dialogManager;
    public GameObject cam;
    public GameObject player;

    [HideInInspector] public SceneLoader sceneLoader;
    [HideInInspector] public CinemachineBrain cine;

    // Variables
    public bool gameIsRunning = true;
    public List<string> disableInvScenes;

    // Game state
    public bool gaveAmulet;

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
        sceneLoader = GetComponent<SceneLoader>();
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
        if (scene.name.StartsWith("Prologue") || scene.name.StartsWith("menu")) {
            inventoryObj.SetActive(false);
        }
        else
        {
            if (disableInvScenes.Contains(scene.name))
                inventoryObj.SetActive(false);
            else
                inventoryObj.SetActive(true);
        }
        CursorManager.SetCursor(null);
    }

    // Activates the inventory for the first time
    public void ActivateInventory()
    {
        inventoryObj.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        sceneLoader.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName, SceneTransitionSettings[] transitionSequence)
    {
        sceneLoader.LoadScene(sceneName, transitionSequence);
    }

    public void ChangeActiveCam(GameObject newCam)
    {
        cine.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
        newCam.gameObject.SetActive(true);
    }

    public void GiveAmulet(bool didGive)
    {
        gaveAmulet = didGive;

        if (didGive)
        {
            inventory.RemoveItem("amulet");
            print("Gave amulet. Oh no.");
        }
        else
            print("Didn't give amulet.");
    }
}