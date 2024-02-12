using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int damageAmount = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
