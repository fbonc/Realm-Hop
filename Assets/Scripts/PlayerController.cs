using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    // CAMERA
    [SerializeField] GameObject camera;
    int leftFingerId, rightFingerId;
    float halfScreenwidth;
    [SerializeField] float rotateSpeed = 20f;
    bool isRotating;

    CharacterController controller;

    // FORWARDS MOVEMENT

    [SerializeField] float moveSpeed;
    [SerializeField] float speedUpRate;

    float standardSpeed;

    // JUMPING
    [SerializeField] float jumpHeight = 1f;
    Vector3 move;
    private float gravityValue = 9.81f;
    private float verticalVelocity;







    // Start is called before the first frame update
    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;

        halfScreenwidth = Screen.width / 2;

        controller = GetComponent<CharacterController>();
        
        standardSpeed = moveSpeed;

    }


    // Update is called once per frame
    void Update()
    {
        
        GetTouchInput();

        if (rightFingerId != -1) {

            transform.Rotate(Vector3.up * 1 * rotateSpeed * Time.deltaTime);
            isRotating = true;

        }

        if (leftFingerId != -1) {

            transform.Rotate(Vector3.up * -1 * rotateSpeed * Time.deltaTime);
            isRotating = true;
            
        }


        
        verticalVelocity -= gravityValue * Time.deltaTime;

        if (controller.isGrounded){
            moveSpeed = standardSpeed;
            verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            // rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }


        if (isRotating){
            moveSpeed += speedUpRate;

        }


        // MOVE FORWARDS
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        controller.Move(forward * moveSpeed);

        move = new Vector3(0, 0, 0);
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);


        //Debug.Log($"movespeed: {moveSpeed}, standardSpeed: {standardSpeed}");


    }


    void GetTouchInput() {

        for (int i = 0; i < Input.touchCount; i++) {

            Touch t = Input.GetTouch(i);

            switch (t.phase) {
                case TouchPhase.Began:
                    if (t.position.x < halfScreenwidth && leftFingerId == -1)
                    {
                        leftFingerId = t.fingerId;
                        Debug.Log("Tracking left finger");
                    }
                    else if (t.position.x > halfScreenwidth && rightFingerId == -1)
                    {
                        rightFingerId = t.fingerId;
                        Debug.Log("Tracking right finger");
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                        isRotating = false;
                        Debug.Log("Stopped tracking left finger");
                    }

                    else if (t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                        isRotating = false;
                        Debug.Log("Stopped tracking right finger");
                    }
                    
                    break;

            

            }




        }


        
    }
}
