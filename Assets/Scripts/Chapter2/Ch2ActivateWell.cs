using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch2ActivateWell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        Activate();
    }

    public void Activate()
    {
        print("Activated well");
    }
}
