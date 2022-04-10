using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseFollow : MonoBehaviour
{
    private bool mouseCentered;
    private void Start()
    {
        mouseCentered = false;
        transform.position = new Vector3(0, 0, -10);
    }
    private void Update()
    {
        //&& Input.mousePosition.x < (Screen.width / 2 + 20) && Input.mousePosition.y < (Screen.height / 2 + 20) && Input.mousePosition.y > (Screen.height / 2 - 20)
        if (Input.mousePosition.x > ((Screen.width) * 0.40) && Input.mousePosition.x < ((Screen.width) * 0.60) && Input.mousePosition.y > ((Screen.height) * 0.40) && Input.mousePosition.y < ((Screen.height) * 0.60))
        {
            print("inbounds");
            mouseCentered = true;
        }
        if (mouseCentered)
        {
            float speed = 5f;
            //print(Input.GetAxisRaw("Mouse Y") + " " + Input.GetAxisRaw("Mouse X"));
            transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
        }
     }

}
