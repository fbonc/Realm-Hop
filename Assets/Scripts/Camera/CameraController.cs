using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] PlayerController playerController; // for speed

    [Header("Distance")]
    [SerializeField] Vector3 baseOffset = new Vector3(2.2f, 1, 0);
    [SerializeField] float maxOffset = 5.5f;
    [SerializeField] float speedDistanceFactor = 0.75f;
    [SerializeField] float horizontalSmoothSpeed = 0.125f;
    [SerializeField] float verticalSmoothSpeed = 0.0125f;
    

    [Header("Rotation")]
    [SerializeField] float smoothPitchSpeed = 0.05f;
    [SerializeField] Vector3 lookOffset = new Vector3(0, 0.35f, 0);

    [Header("FOV")]
    [SerializeField] float startingFieldOfView = 60.0f;

    [SerializeField] float maxFOV = 80.0f;
    [SerializeField] float FOVIncreaseRate = 60f;
    [SerializeField] float FOVChangespeed = 10f;

    // -----------------------------------------------------------------------------------------

    float baseSpeed;


    void Start() {
        baseSpeed = playerController.moveSpeed;
    }

    void LateUpdate()
    {
        float currentSpeed = playerController.moveSpeed; 
        float speedRatio = currentSpeed/baseSpeed - 1;

        float targetFieldOfView = startingFieldOfView + speedRatio * FOVIncreaseRate;
        Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, targetFieldOfView, FOVChangespeed * Time.deltaTime);

        if (Camera.main.fieldOfView > maxFOV) {
            Camera.main.fieldOfView = maxFOV;
        }
        
        Vector3 dynamicOffset = baseOffset;
        dynamicOffset.x = baseOffset.x + (speedRatio * speedDistanceFactor);
        if (dynamicOffset.x > maxOffset) {
            dynamicOffset.x = maxOffset;
        }
        

        Vector3 targetPosition = player.position + player.rotation * dynamicOffset;
        
        Vector3 currentPos = transform.position;
        float smoothX = Mathf.Lerp(currentPos.x, targetPosition.x, horizontalSmoothSpeed);
        float smoothZ = Mathf.Lerp(currentPos.z, targetPosition.z, horizontalSmoothSpeed);
        float smoothY = Mathf.Lerp(currentPos.y, targetPosition.y, verticalSmoothSpeed);
        transform.position = new Vector3(smoothX, smoothY, smoothZ);
        
        Vector3 lookTarget = player.position + lookOffset;
        Quaternion targetRotation = Quaternion.LookRotation(lookTarget - transform.position);
        
        Vector3 currentEuler = transform.rotation.eulerAngles;
        Vector3 targetEuler = targetRotation.eulerAngles;
        
        float smoothedPitch = Mathf.LerpAngle(currentEuler.x, targetEuler.x, smoothPitchSpeed);
        
        Quaternion newRotation = Quaternion.Euler(smoothedPitch, targetEuler.y, targetEuler.z);
        transform.rotation = newRotation;

    }
}
