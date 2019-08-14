using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInteraction : MonoBehaviour
{
    public Texture2D cursorTexture;

    private bool Paused = false;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        //Cursor.visible = Paused;
        Cursor.lockState = Paused ? CursorLockMode.None : CursorLockMode.Confined;

        if (Input.GetKeyDown("escape") || Input.GetKeyDown("p"))
        {
            Paused = !Paused;
            Time.timeScale = Paused ? 0 : 1;
        }
    }
}
