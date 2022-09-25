using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelOpener : MonoBehaviour {

    public GameObject Panel;

    public void Start(){
        Panel.SetActive(false);
    }

    public void openPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
    }
   
}
