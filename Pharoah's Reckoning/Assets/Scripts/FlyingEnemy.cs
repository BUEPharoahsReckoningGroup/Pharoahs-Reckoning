using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyController
{
    public float HorizontalSpeed; // Speed of horizontal movement
    public float VerticalSpeed;   // Speed of sine wave oscillation
    public float Amplitude;       // Height of oscillation

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; // Store the initial position
    }

    void FixedUpdate()
    {
        // Horizontal Movement
        float direction = isFacingRight ? 1 : -1;
        initialPosition.x += HorizontalSpeed * direction * Time.deltaTime;

        // Vertical Oscillation
        float oscillation = Mathf.Sin(Time.time * VerticalSpeed) * Amplitude;
        transform.position = new Vector3(initialPosition.x, initialPosition.y + oscillation, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wall" || collider.tag == "Enemy")
        {
            Flip(); // Use Flip method from EnemyController
        }
        else if (collider.tag == "Player")
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
        }
    }
}
