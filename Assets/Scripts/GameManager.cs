using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

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
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!G.gameIsRunning) { return; }
    }
}