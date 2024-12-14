using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int lives = 3;

    public int totalArtifacts = 0;

    private float flickerTime = 0f;
    private float flickerDuration = 0.1f;
    private SpriteRenderer spriteRenderer;
    public bool isImmune = false;
    public float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (isImmune)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;
            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
                spriteRenderer.enabled = true;
            }
        }
    }

    void SpriteFlicker()
    {
        if (flickerTime < flickerDuration)
        {
            flickerTime += Time.deltaTime;
        }
        else if (flickerTime >= flickerDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            flickerTime = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isImmune || isDead) return;

        health -= damage;
        if (health < 0) health = 0;

        if (health == 0)
        {
            if (lives > 0)
            {
                StartCoroutine(HandleRespawn());
            }
            else
            {
                StartCoroutine(HandleGameOver());
            }
        }

        Debug.Log("Player Health: " + health);
        Debug.Log("Player Lives: " + lives);

        PlayHitReaction();
    }

    void PlayHitReaction()
    {
        isImmune = true;
        immunityTime = 0f;
    }

    IEnumerator HandleRespawn()
    {
        isDead = true; // Prevent further actions
        animator.SetTrigger("Death"); // Trigger the death animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for the animation to finish

        FindObjectOfType<LevelManager>().RespawnPlayer(); // Call the respawn method
        health = 100; // Reset health
        lives--; // Deduct a life
        isDead = false; // Allow actions again
    }

    IEnumerator HandleGameOver()
    {
        isDead = true; // Prevent further actions
        animator.SetTrigger("Death"); // Trigger the death animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for the animation to finish

        Debug.Log("Game Over"); // Log the game over state
        Destroy(gameObject); // Destroy the player
    }

   public void CollectedArtifact(int artifactValue)
    {
        this.totalArtifacts += artifactValue;
        Debug.Log("Total Artifacts Collected: " + this.totalArtifacts);
    }
}
