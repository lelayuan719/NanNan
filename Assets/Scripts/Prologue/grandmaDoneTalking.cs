using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaDoneTalking : MonoBehaviour
{
    // Start is called before the first frame update
    public bool done;
    public bool updated;
    public GameObject dialog;
    public GameObject backpack;

    void Start()
    {
        updated = false;
        done = dialog.GetComponent<Dialog>().completed;
    }

    // Update is called once per frame
    void Update()
    {
        done = dialog.GetComponent<Dialog>().completed;
        if (done && !updated){
            updated = true;
            backpack.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
