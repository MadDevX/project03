using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CursorScript
{
    public static Texture2D defaultCursor;
    public static Texture2D pickupCursor;

    public static void SetPickup()
    {
        Cursor.SetCursor(pickupCursor, Vector2.zero, CursorMode.Auto);
    }

    public static void SetDefault()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
