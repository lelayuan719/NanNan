using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TokenTracker : MonoBehaviour
{
    [HideInInspector] public int tokens;
    public UnityEvent onCollectAll;

    public void AddToken()
    {
        tokens++;
        if (tokens == 4)
        {
            onCollectAll.Invoke();
        }
    }
}
