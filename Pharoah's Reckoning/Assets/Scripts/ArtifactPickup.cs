using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    public int artifactValue = 1; // Set the artifact value to 1 for all artifacts

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().CollectedArtifact(artifactValue);
            Destroy(this.gameObject);
        }
    }
}
