using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCandle : MonoBehaviour
{
    private GameObject candle;
    private bool match;
    public GameObject darkness;
    // Start is called before the first frame update
    void Start()
    {
        candle = GameObject.Find("candle");
        match = candle.GetComponent<ItemMatch>().success;
    }

    // Update is called once per frame
    void Update()
    {
        match = candle.GetComponent<ItemMatch>().success;
        if (match == true)
        {
            darkness.SetActive(false);
        }
    }
}
