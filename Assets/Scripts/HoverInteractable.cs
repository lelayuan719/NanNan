using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverInteractable : MonoBehaviour
{
    private Color startcolor;
    private SpriteRenderer spriteRenderer;
    public Texture2D newCursor;
    [SerializeField] private bool canPickUp = false;
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

        if (canPickUp)
        {
            GameManager.GM.inventoryUI.inventoryButtonHelper.Open();
        }
    }

    void OnMouseDown()
    {
        if (canPickUp)
        {
            GameManager.GM.inventoryUI.inventoryButtonHelper.Close();
        }
    }

    void OnMouseExit()
    {
        print("exit");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        if (canPickUp)
        {
            GameManager.GM.inventoryUI.inventoryButtonHelper.Close();
        }
    }

}
