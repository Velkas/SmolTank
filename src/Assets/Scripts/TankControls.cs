using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls : MonoBehaviour
{
    [Header("Player Attributes")]
    public int playerNumber = 1;
    public bool tankControl = true;
    public const string tankType = "Player";
    private TankAttributes attributes;

    [Header("Missile Attributes")]
    public GameObject missile;
    public Transform missileSpawn;
    private float fireTimer = 0;
    [HideInInspector]
    public List<GameObject> missiles = new List<GameObject>();

    [Header("Mine Attributes")]
    public GameObject mine;
    public Transform mineSpawn;
    [HideInInspector]
    public List<GameObject> mines = new List<GameObject>();

    private string movementAxisName;
    private string turnAxisName;
    private Rigidbody rb;
    private float movementInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attributes = new TankAttributes().GetTankAttributes(tankType);

        if (attributes == null)
        {
            Debug.LogError("Unable to find tank attributes for tank type: " + tankType.ToLowerInvariant());
        }

        // The axes names are based on player number.
        movementAxisName = "Vertical" + playerNumber;
        turnAxisName = "Horizontal" + playerNumber;
    }

    void Update()
    {
        // Store the value of both input axes.
        movementInput = Input.GetAxis(movementAxisName);
        turnInput = Input.GetAxis(turnAxisName);

        if (Input.GetButtonDown("Fire1") && fireTimer <= 0 && missiles.Count < (int)attributes.bulletLimit)
        {
            missiles.Add(Shoot());
            fireTimer = 1f / (int)attributes.fireRate;
        }

        if (Input.GetButtonDown("Fire2") && mines.Count < (int)attributes.mineLimit)
        {
            mines.Add(LayMine());
        }

        fireTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }

    private void Move()
    {
        if (tankControl)
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * movementInput * (int)attributes.movement * Time.fixedDeltaTime;
            // Apply this movement to the rigidbody's position.
            rb.MovePosition(transform.position + movement);
        }
        else
        {
            // directional move
        }
    }

    private void Turn()
    {
        if (tankControl)
        {
            // Determine the number of degrees to be turned based on the input, speed and time between frames.
            float turn = turnInput * ((int)attributes.movement * 10) * Time.fixedDeltaTime;

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            rb.MoveRotation(transform.rotation * turnRotation);
        }
        else
        {
            // directional rotation
        }
    }

    private GameObject Shoot()
    {
        // Shoot a missile
        GameObject missileInstance = Instantiate(missile, missileSpawn.position, missileSpawn.rotation);
        MissileController controller = missileInstance.GetComponent<MissileController>();
        controller.moveSpeed = (float)attributes.bulletSpeed;
        controller.spawner = gameObject;
        return missileInstance;
    }

    private GameObject LayMine()
    {
        // Lay a mine
        GameObject mineInstance = Instantiate(mine, mineSpawn.position, mineSpawn.rotation);
        mineInstance.SendMessage("ConfigureMine", gameObject);
        return mineInstance;
    }

    public void ObjectDestroyed(GameObject obj)
    {
        if (missiles.Exists(o => o == obj))
        {
            // Missile was destroyed, remove it so a new one can be spawned
            missiles.Remove(obj);
        }
        else if (mines.Exists(o => o == obj))
        {
            // Mine was destroyed, remove it so a new one can be spawned
            mines.Remove(obj);
        }
        else
        {
            // Error, couldn't find the object
            Debug.LogError(gameObject.name + " does not contain GameObject: " + obj.name);
        }
    }
}