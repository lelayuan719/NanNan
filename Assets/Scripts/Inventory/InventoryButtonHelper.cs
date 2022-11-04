using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonHelper : MonoBehaviour
{
    public Sprite closedImage;
    public Sprite openImage;
    private Image currImage;

    // Start is called before the first frame update
    void Start()
    {
        currImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        currImage.sprite = openImage;
    }

    public void Close()
    {
        currImage.sprite = closedImage;
    }
}
