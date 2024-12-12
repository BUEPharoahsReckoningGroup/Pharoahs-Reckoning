using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Get the direction the player is facing
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player.GetComponent<SpriteRenderer>().flipX) // Player is facing left
        {
            speed = -Mathf.Abs(speed); // Bullet moves to the left
        }
        else // Player is facing right
        {
            speed = Mathf.Abs(speed); // Bullet moves to the right
        }

        // Set the bullet's velocity
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
       
       Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
   void OnTriggerEnter2D(Collider2D other)
{
    // Destroy the bullet when it hits a wall
    if (other.CompareTag("Wall"))
    {
        Destroy(this.gameObject); // Destroy this specific bullet
    }

    // Destroy both the bullet and the enemy when they collide
    if (other.CompareTag("Enemy"))
    {
        Destroy(this.gameObject); // Destroy this specific bullet
        Destroy(other.gameObject); // Destroy the enemy
    }
}

}