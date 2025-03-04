using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLinesController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float radiusDecreaseFactor = 100f;
    [SerializeField] float speedIncreaseFactor;
    [SerializeField] float startingRadius = 48f;

    // -----------------------------------------------------------------------------------------

    ParticleSystem particleSystem;
    ParticleSystem.ShapeModule shape;
    float baseSpeed;

    void Start() 
    {
        baseSpeed = playerController.moveSpeed;

        particleSystem = GetComponent<ParticleSystem>();
        shape = particleSystem.shape;
        shape.radius = startingRadius;
    }
    void Update()
    {
        float currentSpeed = playerController.moveSpeed;

        shape.radius = startingRadius - (currentSpeed - baseSpeed) * radiusDecreaseFactor;

    }
}
