using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissileController : MonoBehaviour
{
    [Header("Missile Attributes")]
    [HideInInspector]
    public float moveSpeed = 15f;
    public float explosionRadius = 8f;
    public float explosionPower = 2500f;
    public LayerMask layersToExplode = 32768;

    private bool triggered = false;
    private Rigidbody rb;
    [HideInInspector]
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            triggered = true;
        }
    }

    void Update()
    {
        // Explode if triggered
        if (triggered)
        {
            // Collect the current position
            Vector3 explosionPos = transform.position;
            explosionPos.y -= 0.2f;

            // Get all object in range that are port of the layermask
            Collider[] objects = GetObjectsInRange();

            // Go through each returned collider to do explosion
            foreach (Collider obj in objects)
            {
                // Get the rb attached to the object
                Rigidbody obj_rb = obj.GetComponent<Rigidbody>();

                // Check to make sure we got a body
                if (obj_rb != null)
                {
                    // Apply the explosion
                    obj_rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 1f);
                }
            }

            // Tell the spawner that we are destroyed
            spawner.SendMessage("ObjectDestroyed", gameObject);

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