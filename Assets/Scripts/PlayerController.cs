using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("General Movement")]
    public float rotateSpeed = 150f;
    [SerializeField] float jumpHeight = 1.5f;
    public float moveSpeed = 0.08f;

    [Header("Classic Movement")]
    [SerializeField] float speedUpRate = 0.015f;
    [SerializeField] float slowDownRate = 0.045f;
    [SerializeField] float maxSpeed = 0.155f;


    [Header("Rush Movement")]
    public bool rushMode = false;
    [SerializeField] float rushSpeedUpRate = 0.03f;
    [SerializeField] float rushSpeedThreshold1 = 0.15f;
    [SerializeField] float rushSpeedThreshold2 = 0.20f;
    [SerializeField] float rushSpeedThreshold3 = 0.25f;
    [SerializeField] float accelerationSlowDownFactor;

    
    // -----------------------------------------------------------------------------------------

    // TOUCH INPUT
    int leftFingerId, rightFingerId;
    float halfScreenwidth;
    enum RotationPriority { None, Left, Right }
    RotationPriority rotationPriority;

    // FORWARDS MOVEMENT
    bool isRotating;
    CharacterController controller;
    float standardSpeed;
    float speedIncrease;

    // JUMPING
    Vector3 move;
    private float gravityValue = 9.81f;
    private float verticalVelocity;


    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;
        rotationPriority = RotationPriority.None;
        halfScreenwidth = Screen.width / 2;

        controller = GetComponent<CharacterController>();
        standardSpeed = moveSpeed;
        speedIncrease = rushSpeedUpRate;
    }


    void Update()
    {
        
        GetTouchInput();

        if (rotationPriority == RotationPriority.Right && rightFingerId != -1) {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            isRotating = true;
        }
        else if (rotationPriority == RotationPriority.Left && leftFingerId != -1) {
            transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
            isRotating = true;
        }
        else {
            isRotating = false;
        }

        
        verticalVelocity -= gravityValue * Time.deltaTime;

        if (controller.isGrounded){
            verticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravityValue);

            if (!isRotating){
                if(!rushMode) {
                    moveSpeed -= slowDownRate;
                    if(moveSpeed < standardSpeed) {
                        moveSpeed = standardSpeed;
                    }
                }
                
            } else {
                if(!rushMode){
                    moveSpeed += speedUpRate;
                    if(moveSpeed > maxSpeed) {
                        moveSpeed = maxSpeed;
                    }
                } else {
                    if (moveSpeed < rushSpeedThreshold1) {
                        speedIncrease = rushSpeedUpRate;
                    } else if (moveSpeed < rushSpeedThreshold2) {
                        speedIncrease = rushSpeedUpRate / accelerationSlowDownFactor;
                    } else if (moveSpeed < rushSpeedThreshold3) {
                        speedIncrease = rushSpeedUpRate / Mathf.Pow(accelerationSlowDownFactor, 2);
                    } else {
                        speedIncrease = rushSpeedUpRate / Mathf.Pow(accelerationSlowDownFactor, 4.60f);
                    }
                    moveSpeed += speedIncrease;
                }
            }
 
        }

        Debug.Log("Movement speed: " + moveSpeed + "\nspeedIncrease: " + speedIncrease);


        // MOVE FORWARDS
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        controller.Move(forward * moveSpeed);

        move = new Vector3(0, 0, 0);
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);

        //Debug.Log($"movespeed: {moveSpeed}, standardSpeed: {standardSpeed}");

    }


    void GetTouchInput() {

        if (Input.touchCount == 0)
        {
            leftFingerId = -1;
            rightFingerId = -1;
            rotationPriority = RotationPriority.None;
            return;
        }

        for (int i = 0; i < Input.touchCount; i++) {

            Touch t = Input.GetTouch(i);
            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x < halfScreenwidth && leftFingerId == -1) {
                        leftFingerId = t.fingerId;
                        rotationPriority = RotationPriority.Left;
                        Debug.Log("Left finger pressed. Priority: Left");
                    }
                    
                    else if (t.position.x > halfScreenwidth && rightFingerId == -1) {
                        rightFingerId = t.fingerId;
                        rotationPriority = RotationPriority.Right;
                        Debug.Log("Right finger pressed. Priority: Right");
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId) {
                        leftFingerId = -1;
                        rotationPriority = (rightFingerId != -1) ? RotationPriority.Right : RotationPriority.None;
                        Debug.Log("Left finger released. New priority: " + rotationPriority);
                    }

                    else if (t.fingerId == rightFingerId) {
                        rightFingerId = -1;
                        rotationPriority = (leftFingerId != -1) ? RotationPriority.Left : RotationPriority.None;
                        Debug.Log("Right finger released. New priority: " + rotationPriority);
                    }
                    break;

            }

        }
        
    }
}
