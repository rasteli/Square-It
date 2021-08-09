using UnityEngine;

public class Controller : MonoBehaviour
{
    public Texture2D cursor;

    private void Start()
    {
        AudioManager.Instance.Play("Song");
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
}
