using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager CM;

    public List<Texture2D> cursors;

    private void Awake()
    {
        if (CM == null)
        {
            CM = this;
        }
        else if (CM != this)
        {
            Destroy(gameObject);
        }
    }

    public static void SetCursor(CursorTypes? cursor)
    {
        if (cursor != null) Cursor.SetCursor(CM.cursors[(int)cursor], Vector2.zero, CursorMode.Auto);
        else Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}

public enum CursorTypes
{
    Default,
    Question,
    Door,
    Dialog,
    Hand,
    Fist,
}