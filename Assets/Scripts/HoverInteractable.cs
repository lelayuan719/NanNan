using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverInteractable : MonoBehaviour
{
    private Color startcolor;
    private SpriteRenderer spriteRenderer;
    public Texture2D newCursor;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        print("hovering");
        Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        print("exit");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
