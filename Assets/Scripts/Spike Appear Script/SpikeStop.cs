using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeStop : MonoBehaviour
{
    [SerializeField] private GameObject spikes; // Reference to the spike GameObject
    [SerializeField] private Transform targetPoint; // The point the spike should move to
    [SerializeField] private float speed = 5f; // Speed of movement

    private bool isActivated = false; // Flag to check if movement is triggered

    private void OnTriggerEnter2D(Collider2D other) // 2D collision detection
    {
        if (other.CompareTag("Player")) // Ensure it's the player triggering it
        {
            isActivated = true; // Set the flag to start the movement
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            // Move the spikes towards the target point
            spikes.transform.position = Vector2.MoveTowards(spikes.transform.position, targetPoint.position, speed * Time.deltaTime);

            // Stop moving when it reaches the target
            if (Vector2.Distance(spikes.transform.position, targetPoint.position) < 0.01f)
            {
                spikes.transform.position = targetPoint.position; // Snap to the exact target position
                isActivated = false; // Stop movement
            }
        }
    }
}
