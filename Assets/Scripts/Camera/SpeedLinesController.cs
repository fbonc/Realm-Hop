using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class SpeedLinesController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float radiusDecreaseFactor = 74f;
    [SerializeField] float startingRadius = 47f;
    [SerializeField] float minRadius = 35f;

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
    void FixedUpdate()
    {
        float currentSpeed = playerController.moveSpeed;
        shape.radius = startingRadius - ((currentSpeed/baseSpeed) - 1) * radiusDecreaseFactor;

        if (shape.radius < minRadius) {
            shape.radius = minRadius;
        }

    }
}
