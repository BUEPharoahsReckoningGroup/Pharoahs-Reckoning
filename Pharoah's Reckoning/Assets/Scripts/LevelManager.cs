using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject CurrentCheckpoint;
    public float respawnDelay = 2f; // Delay in seconds before respawning

    // Respawn the player
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerCoroutine());
    }

    private IEnumerator RespawnPlayerCoroutine()
    {
        // Reference to the PlayerController and Animator
        PlayerController player = FindObjectOfType<PlayerController>();
        Animator playerAnimator = player.GetComponent<Animator>();

        // Trigger the death animation
        playerAnimator.SetTrigger("Death");

        // Disable player controls during death
        player.enabled = false;

        // Wait for the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // Reset the player position to the checkpoint
        player.transform.position = CurrentCheckpoint.transform.position;

        // Reset the player's animation state
        playerAnimator.ResetTrigger("Death");
        playerAnimator.SetTrigger("Respawn");

        // Re-enable player controls
        player.enabled = true;
    }
}
