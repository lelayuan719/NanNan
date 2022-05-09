using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWarlockBox : MonoBehaviour
{
    private bool success;
    public GameObject amulet;
    private bool check;
    public GameObject panel;
    public GameObject paused;

    // Start is called before the first frame update
    void Start()
    {
        amulet.SetActive(false);
        success = GameObject.Find("Main Camera").GetComponent<LockCorrectScript>().isSolved;
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        success = GameObject.Find("Main Camera").GetComponent<LockCorrectScript>().isSolved;
        if (success && check == false)
        {
            
            if(amulet.activeSelf == false && check == false)
            {
                print("got key " + check);
                check = true;
                amulet.SetActive(true);
                paused.SetActive(true);
                panel.SetActive(false);
                
            }
          
        }

    }
    private void OnMouseDown()
    {
       /* success = GameObject.Find("Main Camera").GetComponent<LockCorrectScript>().isSolved;
        if (success)
        {
            print("got key");
            amulet.SetActive(true);
        }*/
    }
}
