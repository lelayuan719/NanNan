using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowTilesScript : MonoBehaviour
{
    public Vector3 destPosition;
    private Vector3 corrPosition;
    private SpriteRenderer spr;
    public int tileNum; 
    public bool finalPosition;
    
    void Awake()
    {
        destPosition = transform.position;
        corrPosition = transform.position;
        spr = GetComponent<SpriteRenderer>();
        Change(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(a: transform.position, b: destPosition, t: 0.1f);
        /*if (destPosition == corrPosition){
            //spr.color = Color.green;
            Change(true);
        }
        else {
            //spr.color = Color.white;
            Change(false);
        }*/
        // g.renderer.material.color.a = 1.0f;
    }
    public void Change(bool correct){
        Color objectColor = this.GetComponent<Renderer>().material.color;
        if (correct){
            this.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        }
        else {
            this.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        }
    }
}
