using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endgame : MonoBehaviour
{
    private bool success;
    public Sprite openDoor;
    public GameObject tiger;
    private bool opened;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        success = gameObject.GetComponent<ItemMatch>().success;
        if (success)
        {
            Debug.Log("to room");
            SceneManager.LoadScene("menu",LoadSceneMode.Single);
        }
    }
}
