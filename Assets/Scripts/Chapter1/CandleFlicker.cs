using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandleFlicker : MonoBehaviour
{
    private bool isFlickering = false;
    private Light2D candleLight;
    private float timeDelay;

    private void Awake()
    {
        candleLight = GetComponent<Light2D>();
    }

    void Update()
    {
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
