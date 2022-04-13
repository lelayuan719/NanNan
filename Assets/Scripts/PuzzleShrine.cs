using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleShrine : MonoBehaviour
{
    private bool success;
    public GameObject amulet;
    
    // Start is called before the first frame update
    void Start()
    {
        amulet.SetActive(false);
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
            amulet.SetActive(true);
        }
    }

}
