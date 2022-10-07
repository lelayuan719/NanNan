using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBear : MonoBehaviour
{
    public GameObject DialogNext;
    public GameObject Dialog1;
    public GameObject Dialog2;
    public GameObject scene1;
    public GameObject scene2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Activate()
    {
        Dialog1.GetComponent<Dialog2>().enabled = false;
        Dialog2.GetComponent<Dialog2>().enabled = true;
        scene1.SetActive(false);
        scene2.SetActive(true);
        DialogNext.SetActive(true);
    }
}
