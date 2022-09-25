using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerMatch : MonoBehaviour
{
    private bool matched;
    public GameObject amulet;
    public GameObject tiger;
    private bool updated;
    // Start is called before the first frame update
    void Start()
    {
        matched = gameObject.GetComponent<ItemMatch>().success;
        updated = false;
    }

    // Update is called once per frame
    void Update()
    {
        matched = gameObject.GetComponent<ItemMatch>().success;
        if (matched && !updated)
        {
            updated = true;
            amulet.SetActive(true);
            tiger.SetActive(true);
        }
    }
}
