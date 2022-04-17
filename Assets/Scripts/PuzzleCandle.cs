using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PuzzleCandle : MonoBehaviour
{
    private GameObject candle;
    private bool match;
    public GameObject darkness;
    public GameObject DialogNext;
    public GameObject Dialog1;
    public GameObject Dialog2;
    public Light2D candleLight;
    private bool updated = false;
    private bool isFlickering = false;
    private float timeDelay;
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
        if (match && !updated)
        {
            updated = true;
            darkness.SetActive(false);
            Dialog1.GetComponent<Dialog2>().enabled = false;
            Dialog2.GetComponent<Dialog2>().enabled = true;
            DialogNext.SetActive(true);
            candleLight.enabled = true;
        }
        /*if (updated && !isFlickering){
            StartCoroutine(CandleEffect());
        }*/
    }

    IEnumerator CandleEffect(){
        isFlickering = true;
        candleLight.intensity -= 1;
        timeDelay = Random.Range(0.01f,0.2f);
        yield return new WaitForSeconds(timeDelay);
        candleLight.intensity += 1;
        timeDelay = Random.Range(0.01f,0.2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = true;
    }
}
