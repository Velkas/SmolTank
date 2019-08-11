using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInteraction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
