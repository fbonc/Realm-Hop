using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    int leftFingerId, rightFingerId;
    float halfScreenwidth;
    [SerializeField] float rotateSpeed = 20f;



    [SerializeField] float jumpHeight = 10f;
    public Vector3 jump;
    public bool isGrounded;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;

        halfScreenwidth = Screen.width / 2;


        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);

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

        }

        if (leftFingerId != -1) {

            transform.Rotate(Vector3.up * -1 * rotateSpeed * Time.deltaTime);
            
        }


        if (isGrounded){
            rb.AddForce(jump * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }




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
                        Debug.Log("Stopped tracking left finger");
                    }

                    else if (t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                        Debug.Log("Stopped tracking right finger");
                    }
                    
                    break;

            

            }




        }


        
    }
}
