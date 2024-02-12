using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    float rotationSpeed = 100f;

    public GameObject cannonballPrefab; // Reference to the cannonball prefab
    public float minForce = 10f; // Minimum force to launch the cannonball
    public float maxForce = 30f; // Maximum force to launch the cannonball
    public float chargeRate = 5f; // Rate at which the charge increases
    public Transform firePoint; // Point where the cannonball launches from
    public float cannonballLifetime = 0.001f;
    public int damageAmount = 2;

    private float currentForce; // Current force of the cannon shot
    private bool isCharging = false; // Flag to track whether charging is in progress

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.forward * Input.GetAxisRaw("Vertical") * rotationSpeed * Time.deltaTime);

        // Start charging when holding down spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentForce = minForce;
        }

        // Charge up the shot while spacebar is held down
        if (isCharging)
        {
            currentForce += chargeRate * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, minForce, maxForce);
        }

        // Release spacebar to fire the cannonball
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Fire();
        }
    }

    // Function to fire the cannonball
    void Fire()
    {
        // Instantiate the cannonball at the firePoint position
        GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();

        // Add force to the cannonball
        rb.AddForce(firePoint.right * currentForce, ForceMode2D.Impulse);

        Destroy(cannonball, cannonballLifetime);

        // Reset charging parameters
        isCharging = false;
        currentForce = minForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected!");
        // Check if the collided object is a ship
        if (collision.gameObject.CompareTag("Ship"))
        {
            Debug.Log("Ship Hit!");
            // Apply damage to the ship
            HealthSystem shipHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (shipHealth != null)
            {
                shipHealth.TakeDamage(damageAmount);
                Debug.Log("Ship Health: " + shipHealth.currentHealth);
            }

            // Destroy the cannonball on collision
            Destroy(gameObject);
        }
    }
}