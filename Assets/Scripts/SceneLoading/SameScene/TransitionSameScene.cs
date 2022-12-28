using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionSameScene : MonoBehaviour
{
    [SerializeField] protected bool unfreeze = true;
    [SerializeField] protected UnityEvent onTransition;

    public virtual void Transition()
    {
        
    }
}
