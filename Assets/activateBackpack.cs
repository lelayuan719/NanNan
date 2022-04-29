using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBackpack : MonoBehaviour
{
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if(Canvas != null)
        {
            Canvas.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
