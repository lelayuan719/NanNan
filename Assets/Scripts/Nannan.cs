using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nannan : MonoBehaviour
{
    public int score = 0;
    public float speed = 5.0f;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("left");
            transform.Translate(-speed * Time.deltaTime, 0, 0);

            
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            
        }
        if (score == 4)
        {
            Destroy(door);
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Keys")
        {
            score++;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Walls")
        {
            
        }


    }
}
