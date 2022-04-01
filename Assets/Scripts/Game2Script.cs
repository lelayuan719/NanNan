using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game2Script : MonoBehaviour
{
    public bool isSolved;
    [SerializeField] private Transform emptySpace = null;
    private Camera cam;
    [SerializeField] private Tiles2Script[] tiles;
    private int emptySpaceIndex = 4;
    [SerializeField] private GlowTilesScript[] glowTiles;

    // Start is called before the first frame update
    void Start()
    {
        isSolved = false;
        cam = Camera.main;
        Shuffle();
    }

    public IEnumerator Pause(int i){
        yield return new WaitForSeconds(0.2f);
        glowTiles[i].Change(true);
    }

    public IEnumerator Remove(int i){
        yield return new WaitForSeconds(0.2f);
        glowTiles[i].Change(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit){
                //Debug.Log(hit.transform.name);
                if (Vector2.Distance(a: emptySpace.position, b: hit.transform.position) < 6){
                    // Debug.Log("IN!");
                    Vector2 lastEmptyPosition = emptySpace.position;
                    // Debug.Log(lastEmptyPosition);
                    // Debug.Log(hit.transform);
                    Tiles2Script currTile = hit.transform.GetComponent<Tiles2Script>();
                    // GlowTilesScript gTile = hit.transform.GetComponent<GlowTilesScript>();
                    // Debug.Log(currTile);
                    emptySpace.position = currTile.destPosition;
                    currTile.destPosition = lastEmptyPosition;
                    // gTile.destPosition = lastEmptyPosition;
                }
            }
        }
        int correctTiles = 0;
        foreach (var a in tiles){
            if (a != null){
                string objectName = a.name;
                if (!a.finalPosition){
                    if (objectName == "Tile (0)"){
                        StartCoroutine(Remove(0));
                        StopCoroutine(Remove(0));
                        // glowTiles[0].Change(false);
                    }
                    else if (objectName == "Tile (1)"){
                        StartCoroutine(Remove(1));
                        StopCoroutine(Remove(1));
                        // glowTiles[1].Change(false);
                    }
                    else if (objectName == "Tile (2)"){
                        StartCoroutine(Remove(2));
                        StopCoroutine(Remove(2));
                        // glowTiles[2].Change(false);
                    }
                    else if (objectName == "Tile (3)"){
                        StartCoroutine(Remove(3));
                        StopCoroutine(Remove(3));
                        // glowTiles[3].Change(false);
                    }
                }
                else {
                    if (objectName == "Tile (0)"){
                        StartCoroutine(Pause(0));
                        StopCoroutine(Pause(0));
                        // glowTiles[0].Change(true);
                    }
                    else if (objectName == "Tile (1)"){
                        StartCoroutine(Pause(1));
                        StopCoroutine(Pause(1));
                        // glowTiles[1].Change(true);
                    }
                    else if (objectName == "Tile (2)"){
                        StartCoroutine(Pause(2));
                        StopCoroutine(Pause(2));
                        // glowTiles[2].Change(true);
                    }
                    else if (objectName == "Tile (3)"){
                        StartCoroutine(Pause(3));
                        StopCoroutine(Pause(3));
                        // glowTiles[3].Change(true);
                    }
                    correctTiles++;
                }
            }
        }
        if (correctTiles != tiles.Length - 1){
            StartCoroutine(Remove(4));
            StopCoroutine(Remove(4));
            // dglowTiles[4].Change(false);
        }
        else {
            StartCoroutine(Pause(4));
            StopCoroutine(Pause(4));
            // glowTiles[4].Change(true);
            Debug.Log(message: "You Won!");
            isSolved = true;
        }
    }
    public void Shuffle() {
        if (emptySpaceIndex != 4){
            var tilePos15 = tiles[4].destPosition;
            tiles[4].destPosition = emptySpace.position;
            emptySpace.position = tilePos15;
            tiles[emptySpaceIndex] = tiles[4];
            tiles[4] = null;
            emptySpaceIndex = 4; 
        }
        int invertion;
        do {
            for (int i = 0; i < 4; i++){
                if (tiles[i] != null){
                    var prevPosition = tiles[i].destPosition;
                    int randomIndex = Random.Range(0, 3);
                    tiles[i].destPosition = tiles[randomIndex].destPosition;
                    // glowTiles[i].destPosition = tiles[randomIndex].destPosition;
                    tiles[randomIndex].destPosition = prevPosition;
                    // glowTiles[randomIndex].destPosition = prevPosition;
                    var tile = tiles[i];
                    // var gTile = glowTiles[i];
                    tiles[i] = tiles[randomIndex];
                    // glowTiles[i] = glowTiles[randomIndex];
                    tiles[randomIndex] = tile;
                    // glowTiles[randomIndex] = gTile;
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
}
