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
    [SerializeField] Transform destination;
    [SerializeField] UnityEvent onComplete;

    Coroutine cr;
    SpriteRenderer sr;
    Rigidbody2D rb;
    bool isFloating = true; // Floating bodies move to both x and y

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
    }

    public void Move()
    {
        cr = StartCoroutine(MoveCR());
    }

    public void StopMidway()
    {
        StopCoroutine(cr);
    }

    IEnumerator MoveCR()
    {
        Vector3 startPos = transform.position;
        float startTime = Time.time;
        float directionToMove = Mathf.Sign(destination.position.x - transform.position.x);

        // Check for already there
        if ((Vector2)destination.position == (Vector2)transform.position)
        {
            if (onComplete != null) onComplete.Invoke();
            yield break;
        }

        // Get direction and flip sprite if necessary
        float distance = (destination.position - transform.position).magnitude;
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
            do
            {
                elapsedTime = Time.time - startTime;
                transform.position = Vector3.Lerp(startPos, destination.position, elapsedTime / newTime);
                yield return new WaitForEndOfFrame();
            }
            while (elapsedTime < newTime);
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

            while (Mathf.Sign(destination.position.x - transform.position.x) == directionToMove)
            {
                rb.velocity = new Vector2(directionToMove * newSpeed, rb.velocity.y);
                yield return new WaitForFixedUpdate();
            }
            rb.velocity = Vector2.zero;
        }

        // Clean up
        Vector2 destPos = destination.position;
        if (!isFloating)
            destPos.y = transform.position.y;
        transform.position = destPos;

        // Reset and do callback
        if (onComplete != null) onComplete.Invoke();
    }
}
