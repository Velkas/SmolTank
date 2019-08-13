using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls : MonoBehaviour
{
    [Header("Player Attributes")]
    public int playerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float moveSpeed = 12f;                 // How fast the tank moves forward and back.
    public float turnSpeed = 180f;            // How fast the tank turns in degrees per second.

    [Header("Missile Attributes")]
    public GameObject missile;
    public Transform missileSpawn;
    public float fireRate = 0.25f;
    private float fireCounter = 0;

    private string movementAxisName;          // The name of the input axis for moving forward and back.
    private string turnAxisName;              // The name of the input axis for turning.
    private Rigidbody rb;              // Reference used to move the tank.
    private float movementInput;         // The current value of the movement input.
    private float turnInput;             // The current value of the turn input.
    //private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // The axes names are based on player number.
        movementAxisName = "Vertical" + playerNumber;
        turnAxisName = "Horizontal" + playerNumber;
    }

    private void Update()
    {
        // Store the value of both input axes.
        movementInput = Input.GetAxis(movementAxisName);
        turnInput = Input.GetAxis(turnAxisName);

        if (Input.GetButtonDown("Fire1") && fireCounter <= 0)
        {
            Shoot();
            fireCounter = 1f * fireRate;
        }

        fireCounter -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }

    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * movementInput * moveSpeed * Time.deltaTime;
        // Apply this movement to the rigidbody's position.
        rb.MovePosition(transform.position + movement);
    }

    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = turnInput * turnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        rb.MoveRotation(transform.rotation * turnRotation);
    }

    private void Shoot()
    {
        GameObject spawnedMissile = Instantiate(missile, missileSpawn.position, missileSpawn.rotation);
    }
}