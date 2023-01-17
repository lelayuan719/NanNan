using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveAnimation))]
public abstract class CyclingObjectTemplate : MonoBehaviour
{
    public bool cycleOnClick = true;

    protected List<Transform> otherDestinations;

    protected List<Vector2> positions;
    protected int positionI = 0;
    protected MoveAnimation moveAnim;

    protected virtual void Awake()
    {
        // Create list of possible positions
        positions = otherDestinations.ConvertAll<Vector2>(x => x.position);
        positions.Insert(0, transform.position);

        moveAnim = GetComponent<MoveAnimation>();
        moveAnim.onComplete.AddListener(new UnityEngine.Events.UnityAction(StopMoving));
    }

    private void OnMouseDown()
    {
        if (cycleOnClick) CyclePos();
    }

    // Toggles the position between the set destinations
    public virtual void CyclePos()
    {
        // Update index and destination
        positionI++;
        positionI %= positions.Count;
        Vector2 destPos = positions[positionI];

        // Disable interactions
        GetComponent<Collider2D>().enabled = false;

        // Start move animation
        moveAnim.ChangeDest(destPos);
        moveAnim.Move();
    }

    public void RestorePos()
    {
        positionI = 0;
        transform.position = positions[0];
    }

    protected virtual void StopMoving()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
