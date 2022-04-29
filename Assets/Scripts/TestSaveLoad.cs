using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveLoad : MonoBehaviour
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
        if (Input.GetKeyDown("space"))
        {
            print("saving");
            SaveSystem.SavePlayer(NanNan.GetComponent<PlayerSave>());
            NanNan.GetComponent<PlayerSave>().level -= 1;
            print("pre load level deduction" + NanNan.GetComponent<PlayerSave>().level);

        }
    }
    private void OnMouseDown()
    {
        print("adding level");
        NanNan.GetComponent<PlayerSave>().level += 1;
        print("level: " + NanNan.GetComponent<PlayerSave>().level);
    }
   
}
