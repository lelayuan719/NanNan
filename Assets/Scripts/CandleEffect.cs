using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleEffect : MonoBehaviour
{
    public ItemMatch match;
    private bool updated = false;
    public GameObject darkness;
    public GameObject DialogNext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(match.success && !updated){
            updated = true;
            darkness.SetActive(false);
            DialogNext.SetActive(true);
        }
    }
}
