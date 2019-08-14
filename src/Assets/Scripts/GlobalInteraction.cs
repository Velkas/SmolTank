using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInteraction : MonoBehaviour
{
    public bool Paused = false;

    void Update()
    {
        Cursor.visible = Paused;
        Cursor.lockState = Paused ? CursorLockMode.None : CursorLockMode.Confined;

        if (Input.GetKeyDown("escape") || Input.GetKeyDown("p"))
        {
            Paused = !Paused;
            Time.timeScale = Paused ? 0 : 1;
        }
    }
}
