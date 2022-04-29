using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PuzzleDoor : MonoBehaviour
{
    private bool success;
    public Sprite openDoor;
    public GameObject tiger;
    private bool opened;
    // Start is called before the first frame update
    void Start()
    {
        tiger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        success = gameObject.GetComponent<ItemMatch>().success;
        if(success && !opened){
            gameObject.GetComponent<SpriteRenderer>().sprite = openDoor;
        }
    }
    private void OnMouseDown()
    {
        success = gameObject.GetComponent<ItemMatch>().success;
        if (success)
        {
            Debug.Log("to room");
            SceneManager.LoadScene("chapter_1_room",LoadSceneMode.Single);
        }
    }
   
}
