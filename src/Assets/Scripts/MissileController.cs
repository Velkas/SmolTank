using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float explosionRadius = 10f;
    public float explosionPower = 5f;
    public LayerMask layersToExplode;

    private bool triggered = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }

    void Update()
    {
        // Explode if triggered
        if (triggered)
        {
            // Collect the current position
            Vector3 explosionPos = transform.position;

            // Get all object in range that are port of the layermask
            Collider[] objects = GetObjectsInRange();

            // Go through each returned collider to do explosion
            foreach (Collider obj in objects)
            {
                // Get the rb attached to the object
                Rigidbody obj_rb = GetComponent<Rigidbody>();

                // Check to make sure we got a body
                if (obj_rb != null)
                {
                    // log the body for debugging
                    Debug.Log("Exploding " + obj.name);

                    // Apply the explosion
                    obj_rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 3f);
                }
            }

            // Set gameobject to inactive just incase destroy takes too long
            gameObject.SetActive(false);

            // set the object for destruction
            Destroy(gameObject);
        }
    }

    private Collider[] GetObjectsInRange()
    {
        // Get all the objects within a range that are part of the layermask
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius, layersToExplode);

        // Return all the objects
        return objectsInRange;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
        // Apply this movement to the rigidbody's position.
        rb.MovePosition(transform.position + movement);
    }
}
