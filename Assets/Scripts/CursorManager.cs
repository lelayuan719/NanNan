using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager CM;

    public List<Texture2D> cursors;
    public List<Vector2> cursorHotspots;

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

        // Set hotspots to middle
        foreach (var tex in cursors)
        {
            Vector2 hotspot = new Vector2(tex.width / 2, tex.height / 2);
            cursorHotspots.Add(hotspot);
        }
        // Set hotspot of default to top left
        cursorHotspots[0] = Vector2.zero;
    }

    public static void SetCursor(CursorTypes? cursor)
    {
        if (cursor != null) Cursor.SetCursor(CM.cursors[(int)cursor], CM.cursorHotspots[(int)cursor], CursorMode.Auto);
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