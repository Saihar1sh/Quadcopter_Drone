using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEngine : MonoBehaviour, IEngine
{
    [SerializeField]
    private float maxPower = 4f, rotationSpeed = 1000f;
    private float RotationSpeed;
    //Interface Methods
    public void InitEngine()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateEngine(Rigidbody rb, InputsController inputs)
    {

        Vector3 engineForce = Vector3.zero;
        Vector3 upVector = transform.up;
        upVector.x = 0;
        upVector.z = 0;
        float difference = upVector.magnitude;
        float finalDiff = difference * Physics.gravity.magnitude;

        engineForce = Vector3.up * ((rb.mass * Physics.gravity.magnitude + finalDiff) + (inputs.Throttle * maxPower)) / 4f;
        rb.AddRelativeForce(engineForce, ForceMode.Force);
    }
    public void RotateBlades(DroneMovementController droneMovement, InputsController inputs)
    {
        if (droneMovement.IsGrounded && !inputs.IsInput)                                //is on ground and we don't input
            RotationSpeed = Mathf.Lerp(rotationSpeed, 0, Time.deltaTime * 2f);
        else if (droneMovement.IsGrounded && inputs.IsInput)                            //is on ground and we input
            RotationSpeed += 10;
        else if (!droneMovement.IsGrounded && inputs.IsInput)                           //not on ground and we input
        {
            if (rotationSpeed <= 1000000)
                rotationSpeed = 1000;
            RotationSpeed += inputs.Throttle * rotationSpeed;
        }
        else if (!droneMovement.IsGrounded && !inputs.IsInput)                           //not on ground and we don't input
        {
            RotationSpeed += 100f;
        }

        transform.localRotation = Quaternion.Euler(-90f, RotationSpeed, 0);
    }

}
