using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveAnimation : MonoBehaviour
{
    [SerializeField] float moveTime;
    [SerializeField] Transform destination;
    [SerializeField] UnityEvent onComplete;

    public void Move()
    {
        StartCoroutine(MoveCR());
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

        // Move while we are still on the same side
        while ((Time.time - startTime) < moveTime)
        {
            float t = (Time.time - startTime) / moveTime;
            transform.position = Vector3.Lerp(startPos, destination.position, t);
            yield return new WaitForFixedUpdate();
        }
        transform.position = destination.position;

        // Reset and do callback
        if (onComplete != null) onComplete.Invoke();
    }
}
