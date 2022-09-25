using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLight : MonoBehaviour
{
    public Transform player;
    public Transform lightObj;
    public GameObject light;
    private bool inDark = false;
    [Range(0,5)]
    public float smoothFactor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (inDark){
            Follow();
        }
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

    void Follow()
    {
        Vector3 playerPosition = player.position;
        
        Vector3 smoothedPosition = 
            Vector3.Lerp(transform.position,
            playerPosition,smoothFactor*Time.fixedDeltaTime);
        lightObj.position = smoothedPosition;
    }
}
