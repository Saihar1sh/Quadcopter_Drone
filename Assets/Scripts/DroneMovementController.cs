using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(InputsController))]
public class DroneMovementController : DroneBase
{
    private float yaw;
    [SerializeField]
    private float pitchPower = 30f, rollPower = 30f, yawPower = 4f, smothness = 2f, groundCheckDist = 1f;
    private float finalPitch, finalYaw, finalroll;

    private bool isGrounded;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform groundCheck;

    private InputsController inputSystem;
    private List<IEngine> engines = new List<IEngine>();

    public bool IsGrounded { get => isGrounded; }

    private void Start()
    {
        inputSystem = GetComponent<InputsController>();
        engines = GetComponentsInChildren<IEngine>().ToList<IEngine>();
    }
    private void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position, -transform.up, groundCheckDist, whatIsGround);
    }
    protected override void HandlePhysics()
    {
        HandleEngines();
        HandleControls();
    }

    protected virtual void HandleEngines()
    {
        //rb.AddForce(Vector3.up * (rb.mass * Physics.gravity.magnitude));
        foreach (IEngine engine in engines)
        {
            engine.UpdateEngine(rb, inputSystem);
            engine.RotateBlades(this, inputSystem);
        }
    }
    protected virtual void HandleControls()
    {
        float pitch = -inputSystem.Cyclic.y * pitchPower;
        float roll = inputSystem.Cyclic.x * rollPower;
        yaw += inputSystem.YawInput * yawPower;

        finalPitch = Mathf.Lerp(finalPitch, pitch, Time.deltaTime * smothness);
        finalroll = Mathf.Lerp(finalroll, roll, Time.deltaTime * smothness);
        finalYaw = Mathf.Lerp(finalYaw, yaw, Time.deltaTime * smothness);

        Quaternion rotation = Quaternion.Euler(finalPitch, finalYaw, finalroll);
        rb.MoveRotation(rotation);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDist, groundCheck.position.z));
    }
}
