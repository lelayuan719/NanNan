using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public GameObject light;
    private bool inDark = false;
    [Range(0,5)]
    public float smoothFactor;

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "darkness"){
            light.SetActive(true);
            inDark = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "darkness"){
            light.SetActive(false);
            inDark = false;
        }
    }
}
