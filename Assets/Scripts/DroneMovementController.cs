using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovementController : MonoBehaviour
{
    private float throttle;

    public float speed = 10f, rotationSpeed = 0, groundCheckDist = 5f;

    private bool isGrounded = false;

    private Vector3 idleVelocity;

    [SerializeField]
    private LayerMask whatIsGround;

    private Rigidbody droneRb;

    [SerializeField]
    private Transform rightRotar, leftRotar, frontRotar, rearRotar, groundCheck;

    [SerializeField]
    private InputSystem inputSystem;

    private void Awake()
    {
        droneRb = GetComponent<Rigidbody>();
    }
    private void Start()
    {

    }
    void Update()
    {
        throttle += CalculateThrottle();
        RotarBladesRotation();
        if (inputSystem.IsInput)
        {
            ApplyThrust();
            droneRb.drag = 0;

        }

        if (inputSystem.HasVerticalInput)
            idleVelocity = droneRb.velocity;

        RotationSpeedCheck();

    }

    private void RotationSpeedCheck()
    {
        if (rotationSpeed >= 10000)
        {
            if (inputSystem.IsInput)
                rotationSpeed = 1000;
            else
            {
                droneRb.velocity = idleVelocity;
                //droneRb.drag = 50;
                rotationSpeed += 10;
            }

        }
    }

    private float CalculateThrottle()
    {
        float throttleVal = inputSystem.Vertical * speed * Time.deltaTime;
        throttleVal = Mathf.Clamp(throttleVal, -2.5f, 5f);
        Debug.Log("throttle: " + throttleVal);
        return throttleVal;
    }

    private void ApplyThrust()
    {
        droneRb.AddForceAtPosition(transform.up * throttle, rightRotar.position);
        droneRb.AddForceAtPosition(transform.up * throttle, leftRotar.position);
        droneRb.AddForceAtPosition(transform.up * throttle, frontRotar.position);
        droneRb.AddForceAtPosition(transform.up * throttle, rearRotar.position);
    }
    private void RotarBladesRotation()
    {
        rotationSpeed += throttle * rotationSpeed;

        rightRotar.rotation = Quaternion.Euler(-90f, rotationSpeed, 0);
        leftRotar.rotation = Quaternion.Euler(-90f, rotationSpeed, 0);
        frontRotar.rotation = Quaternion.Euler(-90f, rotationSpeed, 90f);
        rearRotar.rotation = Quaternion.Euler(-90f, rotationSpeed, 90f);
        //Debug.Log("Rotation speed: " + rotationSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("ghsdv");
            isGrounded = true;
        }
    }
}
