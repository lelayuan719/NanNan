using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOpener : MonoBehaviour {

    public GameObject Canvas;
    public GameObject Button;

    public void Start(){
        Canvas.SetActive(false);
    }

    public void openCanvas()
    {
        if(Canvas != null)
        {
            Canvas.SetActive(true);
            Button.SetActive(false);
        }
    }
   
}