using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hover_interactable : MonoBehaviour
{
    private Color startcolor;
    private SpriteRenderer spriteRenderer;
    public Material mainSprite;
    public Material hoverSprite;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        mainSprite = gameObject.GetComponent<SpriteRenderer>().material;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeSprite(Material material)
    {
        gameObject.GetComponent<SpriteRenderer>().material = material;
    }

    void OnMouseEnter()
    {
        print("hovering");
        ChangeSprite(hoverSprite);
    }

    void OnMouseExit()
    {
        ChangeSprite(mainSprite);
    }

}
