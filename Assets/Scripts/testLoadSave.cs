using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoadSave : MonoBehaviour
{
    private GameObject NanNan;
    // Start is called before the first frame update
    void Start()
    {
        NanNan = GameObject.Find("character");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        NanNan.GetComponent<PlayerSave>().level = SaveSystem.LoadPlayer().level;
        print("new level post save:" + NanNan.GetComponent<PlayerSave>().level);
    }
}
