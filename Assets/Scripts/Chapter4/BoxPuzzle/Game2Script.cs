using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game2Script : MonoBehaviour
{
    public bool isSolved;
    [SerializeField] private Transform emptySpace = null;
    [SerializeField] public Tiles2Script[] tiles;
    private int emptySpaceIndex = 4;
    [SerializeField] private GlowTilesScript[] glowTiles;
    [HideInInspector] public List<Vector3> slotPositions = new List<Vector3>();
    [HideInInspector] public bool canPlay = false;
    private int[] disallowedSlots = { 2, 3, 0, 1, -1 }; // Which slot the tiles can't move into. Ex. index 0 can't move into index 2 because it's opposite
    private string[] tokenTypes = new string[] { "Rat", "Hedgehog", "Fox", "Snake" };
    public int emptySpaceLoc = 4;
    public TokenTracker tokenTracker;
    public GameObject mainPuzzle;
    public GameObject rewardBox;
    public GameObject minimap;

    void Awake()
    {
        isSolved = false;

        InitTiles();
    }

    // Get tile positions
    void InitTiles()
    {
        for (int loc = 0; loc < (tiles.Length - 1); loc++)
        {
            slotPositions.Add(tiles[loc].transform.position);
            tiles[loc].correctLoc = loc;
            tiles[loc].currLoc = loc;
        }
        slotPositions.Add(emptySpace.position);
    }

    void CheckStartSolved()
    {
        foreach (var tile in tiles)
        {
            // Something is solved
            if (tile != null)
            {
                if (tile.currLoc == tile.correctLoc)
                    StartGlow(tile.currLoc);
            }
        }

        CheckSolved();
    }

    // Check if we can play the game
    private void OnEnable()
    {
        // Disable minimap
        minimap.SetActive(false);

        // Remove tokens from inventory
        foreach (string tokenType in tokenTypes)
        {
            string tokenName = "token" + tokenType;
            GameManager.GM.inventory.RemoveItem(tokenName);
        }

        // Check to see if we can play the game
        if (!canPlay && (tokenTracker.tokens == 4))
        {
            canPlay = true;
            StartGame();
        }
    }

    // Starts once all tiles have been collected
    void StartGame()
    {
        Shuffle();
        CheckStartSolved();
    }

    public void StartGlow(int loc){
        glowTiles[loc].Change(true);
    }

    public void StopGlow(int loc){
        glowTiles[loc].Change(false);
    }

    public void SwapTile(int swapLoc)
    {
        if ((disallowedSlots[swapLoc] != emptySpaceLoc) && !isSolved)
        {
            StartCoroutine(tiles[swapLoc].MoveTo(emptySpaceLoc));
            emptySpaceLoc = swapLoc;
        }
    }

    public void CheckSolved()
    {
        int correct = 0;
        foreach (var tile in tiles)
        {
            // Something is solved
            if (tile != null) 
            {
                if (tile.currLoc == tile.correctLoc)
                    correct++;
            }
        }

        if (correct == 4)
        {
            Debug.Log(message: "You Won!");
            glowTiles[4].Change(true);
            isSolved = true;
            Invoke("AtSolved", 2);
        }
    }

    public void Shuffle() {
        if (emptySpaceIndex != 4){
            var tilePos15 = tiles[4].transform.position;
            tiles[4].transform.position = emptySpace.position;
            emptySpace.position = tilePos15;
            tiles[emptySpaceIndex] = tiles[4];
            tiles[4] = null;
            emptySpaceIndex = 4; 
        }
        int invertion;
        do {
            for (int i = 0; i < 4; i++){
                if (tiles[i] != null){
                    var prevPosition = tiles[i].transform.position;
                    int prevLoc = tiles[i].currLoc;
                    int randomIndex = Random.Range(0, 3);

                    tiles[i].transform.position = tiles[randomIndex].transform.position;
                    tiles[randomIndex].transform.position = prevPosition;

                    tiles[i].currLoc = tiles[randomIndex].currLoc;
                    tiles[randomIndex].currLoc = prevLoc;

                    var tempTile = tiles[i];
                    tiles[i] = tiles[randomIndex];
                    tiles[randomIndex] = tempTile;
                }
            }
            invertion = GetInversions();
            Debug.Log(message: "Puzzle Shuffled");
        } while (invertion % 2 == 1);
    }

    public int findIndex(Tiles2Script tileScr){
        for (int i = 0; i < tiles.Length; i++){
            if (tiles[i] != null){
                if (tiles[i] == tileScr){
                    return i;
                }
            }
        }
        return -1;
    }

    int GetInversions(){
        int sum = 0;
        for (int i = 0; i < tiles.Length; i++){
            int thisInvertion = 0;
            for (int j = i; j < tiles.Length; j++){
                if (tiles[j] != null){
                    if (tiles[i].tileNum > tiles[j].tileNum){
                        thisInvertion++;
                    }
                }
            }
            sum += thisInvertion;
        }
        return sum;
    }

    void AtSolved()
    {
        rewardBox.SetActive(true);
        mainPuzzle.SetActive(false);
    }
}
