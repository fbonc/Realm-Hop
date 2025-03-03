using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistance : MonoBehaviour
{
    [Tooltip("Player transform to follow.")]
    public Transform player;
    
    [Tooltip("Reference to the player's controller for reading speed.")]
    public PlayerController playerController;
    
    [Tooltip("Base offset relative to the player.")]
    public Vector3 baseOffset = new Vector3(0, 5, -10);
    
    [Tooltip("Extra distance to add per unit of player speed.")]
    public float speedDistanceFactor = 0.5f;
    
    [Tooltip("How quickly the camera moves to the target position.")]
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // Get the current speed from the player's controller (via a public getter or public field).
        float currentSpeed = playerController.moveSpeed; 
        
        // Calculate dynamic offset: adjust the z value based on speed.
        Vector3 dynamicOffset = baseOffset;
        dynamicOffset.z = baseOffset.z - (currentSpeed * speedDistanceFactor);
        
        // Multiply the offset by the player's rotation so that the camera remains behind the player.
        Vector3 targetPosition = player.position + player.rotation * dynamicOffset;
        
        // Smoothly interpolate the camera's position to the target position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        // Have the camera look at the player.
        transform.LookAt(player);
    }
}
