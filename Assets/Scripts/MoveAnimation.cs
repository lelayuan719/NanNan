using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveAnimation : MonoBehaviour
{
    [SerializeField] float moveTime = -1; // -1 means speed-based
    [SerializeField] float moveSpeed = -1; // -1 means time-based
    [SerializeField] bool flipSprite;
    [SerializeField] Transform destination;
    [SerializeField] UnityEvent onComplete;

    Coroutine cr;
    SpriteRenderer sr;
    Rigidbody2D rb;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        if (moveSpeed != -1)
        {
            rb = GetComponent<Rigidbody2D>();
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

        // Check for already there
        if ((Vector2)destination.position == (Vector2)transform.position)
        {
            if (onComplete != null) onComplete.Invoke();
            yield break;
        }

        // Get direction and flip sprite if necessary
        float directionToMove = Mathf.Sign(destination.position.x - transform.position.x);
        if (flipSprite)
        {
            if (directionToMove < 0)
                sr.flipX = true;
            else
                sr.flipX = false;
        }

        // Time based movement
        if (moveTime != -1)
        {
            // Move while we are still on the same side
            while ((Time.time - startTime) < moveTime)
            {
                float t = (Time.time - startTime) / moveTime;
                transform.position = Vector3.Lerp(startPos, destination.position, t);
                yield return new WaitForFixedUpdate();
            }
        }
        // Speed based movement
        if (moveSpeed != -1)
        {
            // Move while we are still on the same side
            while (Mathf.Sign(destination.position.x - transform.position.x) == directionToMove)
            {
                rb.velocity = new Vector2(directionToMove * moveSpeed, rb.velocity.y);
                yield return new WaitForFixedUpdate();
            }
            rb.velocity = Vector2.zero;
        }

        transform.position = destination.position;

        // Reset and do callback
        if (onComplete != null) onComplete.Invoke();
    }
}
