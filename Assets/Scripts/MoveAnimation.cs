using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveAnimation : MonoBehaviour
{
    enum MoveType
    {
        SpeedBased,
        TimeBased,
    }
    [SerializeField] MoveType typeOfMove;
    [ShowIf("IsTimeBased")] [SerializeField] float moveTime = 0;
    [HideIf("IsTimeBased")] [SerializeField] float moveSpeed = 0;
    [SerializeField] bool flipSprite;
    [Tooltip("Updates the destination's location while moving, instead of on awake.")] [SerializeField] bool trackDestination;
    [SerializeField] public Transform destination;
    public UnityEvent onComplete;

    Coroutine cr;
    SpriteRenderer sr;
    Rigidbody2D rb;
    bool isFloating = true; // Floating bodies move to both x and y
    Vector2 destVec;

    bool IsTimeBased() => typeOfMove == MoveType.TimeBased;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        // Rigid body and seeing if we should float the movement
        rb = GetComponent<Rigidbody2D>();
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            isFloating = false;
        }

        destVec = destination.position;
    }

    public void Move()
    {
        cr = StartCoroutine(MoveCR());
    }

    public void StopMidway()
    {
        StopCoroutine(cr);
    }

    public void ChangeDest(Vector2 newDest)
    {
        destVec = newDest;
    }

    public void ChangeDest(Transform newDest)
    {
        destVec = newDest.position;
    }

    IEnumerator MoveCR()
    {
        Vector3 startPos = transform.position;
        if (trackDestination) destVec = destination.position;
        float startTime = Time.time;
        float directionToMove = Mathf.Sign(destVec.x - transform.position.x);

        // Check for already there
        if (destVec == (Vector2)transform.position)
        {
            if (onComplete != null) onComplete.Invoke();
            yield break;
        }

        // Get direction and flip sprite if necessary
        float distance = Vector2.Distance(destVec, transform.position);
        if (flipSprite)
        {
            if (directionToMove < 0)
                sr.flipX = true;
            else
                sr.flipX = false;
        }

        // Floating lerps to position
        if (isFloating)
        {
            // Speed-based calculates new move time
            float newTime;
            if (typeOfMove == MoveType.SpeedBased)
            {
                newTime = distance / moveSpeed;
            }
            else
            {
                newTime = moveTime;
            }

            // Move
            float elapsedTime;
            while ((elapsedTime = Time.time - startTime) < newTime)
            {
                if (trackDestination) destVec = destination.position;
                transform.position = Vector3.Lerp(startPos, destVec, elapsedTime / newTime);
                yield return new WaitForEndOfFrame();
            }
        }

        // Grounded applies rigidbody velocity
        else
        {
            // Time-based calculates new velocity
            float newSpeed;
            if (typeOfMove == MoveType.TimeBased)
            {
                newSpeed = distance / moveTime;
            }
            else
            {
                newSpeed = moveSpeed;
            }

            while (Mathf.Sign(destVec.x - transform.position.x) == directionToMove)
            {
                if (trackDestination)
                {
                    destVec = destination.position;
                    directionToMove = Mathf.Sign(destVec.x - transform.position.x);
                }
                rb.velocity = new Vector2(directionToMove * newSpeed, rb.velocity.y);
                yield return new WaitForFixedUpdate();
            }
            rb.velocity = Vector2.zero;
        }

        // Clean up
        Vector2 newVec = destVec;
        if (!isFloating)
            newVec.y = transform.position.y;
        transform.position = newVec;

        // Reset and do callback
        if (onComplete != null) onComplete.Invoke();
    }
}
