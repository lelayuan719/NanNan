using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowTilesScript : MonoBehaviour
{
    private Image render;
    public int tileNum; 
    public bool finalPosition;
    
    void Awake()
    {
        render = GetComponent<Image>();
        Change(false);
    }

    public void Change(bool correct){
        render.enabled = correct;
    }
}
