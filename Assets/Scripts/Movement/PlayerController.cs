using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("General Movement")]
    [SerializeField] float jumpHeight = 1.5f;
    public float moveSpeed = 0.08f;

    [Header("Rotation")]
    public float rotateSpeed = 150f;
    public float maxDelta = 100f;
    public float minRotationSpeedMultiplier = 0.5f;
    public float maxRotationSpeedMultiplier = 2.0f;

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
    // For controlling rotation speed dynamically
    int controllingFingerId = -1;
    float controllingFingerInitialX = 0f;
    float rotationSpeedMultiplier = 1f; // 1 means no change


    // FORWARDS MOVEMENT
    bool isRotating;
    CharacterController controller;
    float standardSpeed;
    float speedIncrease;

    // JUMPING
    Vector3 move;
    private float gravityValue = 9.81f;
    private float verticalVelocity;

    // MISC
    private GameObject respawn;
    private GameObject camera;

    public void SetRushMode() {
        if (rushMode == false) {
            rushMode = true;
        } else {
            rushMode = false;
        }
    }

    public void Respawn() {

        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;
        
        transform.position = respawn.transform.position;
        transform.rotation = respawn.transform.rotation;

        cc.enabled = true;

        camera.transform.position = respawn.transform.position;
        camera.transform.rotation = respawn.transform.rotation;

        moveSpeed = standardSpeed;
 
}

    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;
        rotationPriority = RotationPriority.None;
        halfScreenwidth = Screen.width / 2;

        controller = GetComponent<CharacterController>();
        standardSpeed = moveSpeed;
        speedIncrease = rushSpeedUpRate;

        respawn = GameObject.FindWithTag("Respawn");
        camera = GameObject.FindWithTag("MainCamera");
    }


    void Update()
    {
        
        GetTouchInput();


        if (rotationPriority == RotationPriority.Right && rightFingerId != -1)
        {
            float effectiveRotateSpeed = rotateSpeed * rotationSpeedMultiplier;
            transform.Rotate(Vector3.up * effectiveRotateSpeed * Time.deltaTime);
            isRotating = true;
        }

        else if (rotationPriority == RotationPriority.Left && leftFingerId != -1)
        {
            float effectiveRotateSpeed = rotateSpeed * rotationSpeedMultiplier;
            transform.Rotate(Vector3.up * -effectiveRotateSpeed * Time.deltaTime);
            isRotating = true;
        }

        else
        {
            isRotating = false;
            rotationSpeedMultiplier = 1f;
            controllingFingerId = -1;
        }

        
        verticalVelocity -= gravityValue * Time.deltaTime;

        if (controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravityValue);

            if (!isRotating)
            {
                moveSpeed -= slowDownRate * Time.deltaTime;

                if(moveSpeed < standardSpeed)
                {
                    moveSpeed = standardSpeed;
                }
            }

            if (moveSpeed > maxSpeed && !rushMode)
            {
                moveSpeed = maxSpeed;
            }
        }

        if (isRotating){ 
            if(!rushMode)
            {
                moveSpeed += speedUpRate * Time.deltaTime;
            } else
            {
                if (moveSpeed < rushSpeedThreshold1){
                    speedIncrease = rushSpeedUpRate;
                } else if (moveSpeed < rushSpeedThreshold2) {
                    speedIncrease = rushSpeedUpRate / accelerationSlowDownFactor;
                } else if (moveSpeed < rushSpeedThreshold3) {
                    speedIncrease = rushSpeedUpRate / Mathf.Pow(accelerationSlowDownFactor, 2);
                } else {
                    speedIncrease = rushSpeedUpRate / Mathf.Pow(accelerationSlowDownFactor, 3.75f);
                }
                moveSpeed += speedIncrease * Time.deltaTime;
            }
        }
            

        Debug.Log("Movement speed: " + moveSpeed + "\nspeedIncrease: " + speedIncrease);


        // MOVE FORWARDS
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        controller.Move(forward * moveSpeed * Time.deltaTime);

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

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(t.fingerId))
            {
                continue;
            }

            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x < halfScreenwidth && leftFingerId == -1)
                    {
                        leftFingerId = t.fingerId;
                        rotationPriority = RotationPriority.Left;
                        controllingFingerId = t.fingerId;
                        controllingFingerInitialX = t.position.x;
                        Debug.Log("Left finger pressed. Priority: Left");
                    }
                    else if (t.position.x > halfScreenwidth && rightFingerId == -1)
                    {
                        rightFingerId = t.fingerId;
                        rotationPriority = RotationPriority.Right;
                        controllingFingerId = t.fingerId;
                        controllingFingerInitialX = t.position.x;
                        Debug.Log("Right finger pressed. Priority: Right");
                    }
                    break;

                case TouchPhase.Moved:
                    if (t.fingerId == controllingFingerId)
                    {
                        float delta = t.position.x - controllingFingerInitialX;
                        float deltaAdjusted = rotationPriority == RotationPriority.Left ? -delta : delta;
                        rotationSpeedMultiplier = Mathf.Clamp(1 + (deltaAdjusted / maxDelta), minRotationSpeedMultiplier, maxRotationSpeedMultiplier);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (t.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                        rotationPriority = (rightFingerId != -1) ? RotationPriority.Right : RotationPriority.None;
                        Debug.Log("Left finger released. New priority: " + rotationPriority);
                        if (t.fingerId == controllingFingerId)
                        {
                            controllingFingerId = -1;
                            rotationSpeedMultiplier = 1f;
                        }
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                        rotationPriority = (leftFingerId != -1) ? RotationPriority.Left : RotationPriority.None;
                        Debug.Log("Right finger released. New priority: " + rotationPriority);
                        if (t.fingerId == controllingFingerId)
                        {
                            controllingFingerId = -1;
                            rotationSpeedMultiplier = 1f;
                        }
                    }
                    break;
            }
        }
    }
}
