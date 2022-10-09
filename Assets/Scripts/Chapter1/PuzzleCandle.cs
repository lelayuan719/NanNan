using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleCandle : MonoBehaviour
{
    public GameObject darkness;
    public UnityEngine.Rendering.Universal.Light2D candleLight;
    private bool updated = false;
    private bool isFlickering = false;
    private float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*if (updated && !isFlickering){
            StartCoroutine(CandleEffect());
        }*/
    }

    public void Activate()
    {
        updated = true;
        darkness.SetActive(false);
        candleLight.enabled = true;
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
