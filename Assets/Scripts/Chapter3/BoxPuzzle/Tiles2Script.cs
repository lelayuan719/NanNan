using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tiles2Script : MonoBehaviour, IPointerDownHandler
{
    public Game2Script puzzle;
    public int correctLoc;
    public int currLoc;
    public int tileNum;
    public float travelTime = 0.05f;
    private bool moving = false;

    public IEnumerator MoveTo(int loc)
    {
        moving = true;
        puzzle.StopGlow(currLoc);
        puzzle.tiles[currLoc] = null;

        Vector2 startPos = transform.position;
        Vector3 destPos = puzzle.slotPositions[loc];

        float startTime = Time.time;
        float elapsedTime;
        // Move towards the destination
        do
        {
            elapsedTime = Time.time - startTime;
            var t = elapsedTime / travelTime;
            transform.position = Vector3.Lerp(startPos, destPos, t);
            yield return new WaitForEndOfFrame();
        } while (elapsedTime < travelTime);
        transform.position = destPos;
        
        currLoc = loc;
        puzzle.tiles[loc] = this;

        // Check if we're correct
        if (loc == correctLoc)
        {
            puzzle.StartGlow(loc);
            puzzle.CheckSolved();
        }
        moving = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!moving && puzzle.canPlay)
        {
            puzzle.SwapTile(currLoc);
        }
    }
}
