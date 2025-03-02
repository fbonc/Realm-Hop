using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLandTrigger : MonoBehaviour
{
    public float maxDistance = 10.0F;
    public Transform groundCheck;
    public float groundDistanceThreshold = 0.5F;
    public LayerMask groundLayer;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        Debug.DrawRay(groundCheck.position, Vector3.down * maxDistance, Color.red);

        if (Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hit, groundDistanceThreshold, groundLayer))
        {
            animator.SetTrigger("Land");
            Debug.Log("Distance to ground: " + hit.distance);
        }
        else
        {
            // If no ground is detected within maxDistance
            Debug.Log("No ground detected within " + maxDistance + " units.");
        }
        
    }
}
