using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    private bool success;
    public GameObject tiger;
    // Start is called before the first frame update
    void Start()
    {
        tiger.SetActive(false);
        success = gameObject.GetComponent<ItemMatch>().success;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnMouseDown()
    {
        success = gameObject.GetComponent<ItemMatch>().success;
        if (success)
        {
            //print("tiger doll trigger");
            tiger.SetActive(true);
        }
    }
   
}
