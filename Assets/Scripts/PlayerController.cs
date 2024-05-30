using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{


    int leftFingerId, rightFingerId;
    float halfScreenwidth;
    [SerializeField] float rotateSpeed = 20f;
    bool isRotating;

    Rigidbody rb;

    [SerializeField] float jumpHeight = 10f;
    bool isGrounded;

    [SerializeField] float moveSpeed;
    [SerializeField] float speedUpRate;



    float standardSpeed;
    [SerializeField] GameObject camera;


    // Start is called before the first frame update
    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;

        halfScreenwidth = Screen.width / 2;

        rb = GetComponent<Rigidbody>();
        
        standardSpeed = moveSpeed;

    }

    void OnCollisionStay(){
        isGrounded = true;
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


        if (isGrounded){
            moveSpeed = standardSpeed;
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }


        if (isRotating){
            moveSpeed += speedUpRate;

        }

        // Drifting mode
        // rb.AddForce(camera.transform.forward * moveSpeed);

        Debug.Log($"movespeed: {moveSpeed}, standardSpeed: {standardSpeed}");


    }

    void FixedUpdate(){

        Vector3 forward = new Vector3(camera.transform.forward.x, 0.0f, camera.transform.forward.z);
        this.transform.Translate(forward * 0.01f * moveSpeed, Space.World);

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
