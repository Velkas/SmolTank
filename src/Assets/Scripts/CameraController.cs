using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float zoomSpeed = 20f;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    private Camera cam;

    private float startOrtho = 24f;
    private float minOrtho = 15f;
    private float maxOrtho = 30f;
    private float targetOrtho;
    private Vector3 cameraOrigin;

    void Start()
    {
        player = GetComponent<Transform>();
        cam = Camera.main;
        startOrtho = cam.orthographicSize;
        targetOrtho = startOrtho;
        offset = cam.transform.position;
    }

    void LateUpdate()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // follow player if zoomed
        if (cam.orthographicSize < maxOrtho * 0.85f)
        {
            // move to player within a bounds
            Vector3 targetPos = player.position + offset;
            Vector3 smoothTarget = Vector3.Lerp(cam.transform.position, targetPos, (smoothSpeed / 3f) * Time.fixedDeltaTime);
            cam.transform.position = smoothTarget;
        }
        else
        {
            Vector3 smoothTarget = Vector3.Lerp(cam.transform.position, offset, (smoothSpeed / 3f) * Time.fixedDeltaTime);
            cam.transform.position = smoothTarget;
        }
    }
}
