using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    public float lookSpeed = 3f;

    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Vector2 mousePosition = Input.mousePosition;
        mousePosition.y -= 15f;
        Ray ray = cam.ScreenPointToRay(mousePosition);
        float hit = 0f;

        if (playerPlane.Raycast(ray, out hit))
        {
            // do a look
            Vector3 target = ray.GetPoint(hit);
            Quaternion targetRotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
        }
    }
}
