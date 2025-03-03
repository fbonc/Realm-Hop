using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLandTrigger : MonoBehaviour
{
    public Transform groundCheck;
    public float groundDistanceThreshold = 0.35F;
    private float trueGroundDistanceThreshold;
    public LayerMask groundLayer;
    private Animator animator;
    private Vector3 oldPosition;
    private bool landAnimationDone = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        oldPosition = transform.position;    
    }

    void FixedUpdate()
    {
        Debug.DrawRay(groundCheck.position, Vector3.down * 20, Color.red);
        RaycastHit debugHit;
        Physics.Raycast(groundCheck.position, Vector3.down, out debugHit, 20, groundLayer);
        //Debug.Log("Distance to ground: " + debugHit.distance);


        Vector3 velocity = transform.position - oldPosition;
        float verticalVelocity = velocity.y / Time.deltaTime;
        oldPosition = transform.position;

        // Debug.Log("Current vertical speed: " + verticalVelocity);

        trueGroundDistanceThreshold = 0.40f * groundDistanceThreshold * verticalVelocity * -1;
        Debug.Log("trueGroundDistancethreshold: " + trueGroundDistanceThreshold);

        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, trueGroundDistanceThreshold, groundLayer) && verticalVelocity < 0 && !landAnimationDone)
        {
            landAnimationDone = true;
            animator.SetTrigger("Land");
        }

        if (verticalVelocity > 0) {
            landAnimationDone = false;
        }

    }
}
