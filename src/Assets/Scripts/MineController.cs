using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    [Header("Missile Attributes")]
    public float explosionRadius = 8f;
    public float explosionPower = 2500f;
    public LayerMask affectedLayers = 436224;

    [Header("Timer Details")]
    public float lifetime = 20f;
    public Color originalColor;
    public Color flashColor;
    private Material mineMaterial;
    private float flashDuration = 0.2f;
    private int numberOfFlashes = 5;
    private float originalLife;
    private bool flashing = false;

    private bool triggered = false;
    private Rigidbody rb;
    [HideInInspector]
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mineMaterial = GetComponent<MeshRenderer>().material;
        originalLife = lifetime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Missile")
        {
            triggered = true;
        }
    }

    void Update()
    {
        // Check lifetime
        if (lifetime <= originalLife * flashDuration && !flashing)
        {
            StartCoroutine(FlashColor());
        }

        // Explode if triggered
        if (triggered)
        {
            // Collect the current position
            Vector3 explosionPos = transform.position;
            explosionPos.y += 0.5f;

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
                    obj_rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 2f);
                }
            }

            // Tell the spawner that we are destroyed
            spawner.SendMessage("ObjectDestroyed", gameObject);

            // Set gameobject to inactive just incase destroy takes too long
            gameObject.SetActive(false);

            // set the object for destruction
            Destroy(gameObject);
        }

        lifetime -= Time.deltaTime;
    }

    private Collider[] GetObjectsInRange()
    {
        // Get all the objects within a range that are part of the layermask
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius, affectedLayers);

        // Return all the objects
        return objectsInRange;
    }

    public void ConfigureMine(GameObject obj)
    {
        spawner = obj;
    }

    private IEnumerator FlashColor()
    {
        flashing = true;
        int flashes = 0;
        while (flashes < numberOfFlashes)
        {
            mineMaterial.SetColor("_color", flashColor);
            yield return new WaitForSeconds(flashDuration);
            mineMaterial.SetColor("_color", originalColor);
            yield return new WaitForSeconds(flashDuration);
            flashes++;
        }
        triggered = true;
    }
}
