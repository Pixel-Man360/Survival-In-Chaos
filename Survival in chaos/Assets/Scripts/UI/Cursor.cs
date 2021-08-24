using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _cursorRend;
    void OnEnable()
    {
        Reload.OnReloadPressed += ReloadPressed;
        PlayerShooting.OnShoot += CursorBlink;
    }

    void OnDisable()
    {
        Reload.OnReloadPressed -= ReloadPressed;
        PlayerShooting.OnShoot -= CursorBlink;
    }

    void ReloadPressed()
    {
        _cursorRend.color = new Color(_cursorRend.color.r, _cursorRend.color.g, _cursorRend.color.b, 0);
    }

    void CursorBlink()
    {
        Vector3 newPosition = Utility.GetMousePosition();

        transform.position = new Vector3(newPosition.x, newPosition.y, 0f);
        StartCoroutine(BlinkColor());
    }

    IEnumerator BlinkColor()
    {
        _cursorRend.color = new Color(_cursorRend.color.r, _cursorRend.color.g, _cursorRend.color.b, 255);

        yield return new WaitForSeconds(0.5f);
        
        _cursorRend.color = new Color(_cursorRend.color.r, _cursorRend.color.g, _cursorRend.color.b, 0);

    }
}
