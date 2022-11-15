using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBackpack : MonoBehaviour
{
    bool collected = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnMouseDown()
    {
        Collect();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }

    void Collect()
    {
        if (collected == false)
        {
            GameManager.GM.ActivateInventory();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            GetComponent<Dialog>().TriggerDialog();
            GetComponent<SpriteRenderer>().enabled=false;
            collected = true;
        }
        // gameObject.SetActive(false);
    }
}
