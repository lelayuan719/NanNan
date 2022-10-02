using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalButton : MonoBehaviour
{
    public GameObject otherButton1;
    public GameObject otherButton2;
    public GameObject otherPanel1;
    public GameObject otherPanel2;
    public GameObject panel;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void openPanel()
    {
        if(otherPanel1.activeSelf){
            otherPanel1.SetActive(false);
        } else if(otherPanel2.activeSelf){
            otherPanel2.SetActive(false);
        }

        panel.SetActive(true);
        
    }
}
