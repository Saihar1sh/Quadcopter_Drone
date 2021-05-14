using UnityEngine;

public class DroneEngine : MonoBehaviour, IEngine
{
    [SerializeField]
    private float maxPower = 4f, rotationSpeed = 1000f;
    private float RotationSpeed;

    [SerializeField]
    private UIManager UI;

    private void Start()
    {
        UI.SetThrottleMaxValue();
    }

    //Interface Methods
    public void InitEngine() { }

    public void UpdateEngine(Rigidbody rb, InputsController inputs)
    {

        Vector3 engineForce = Vector3.zero;
        Vector3 upVector = transform.up;
        upVector.x = 0;
        upVector.z = 0;
        float difference = upVector.magnitude;
        float finalDiff = difference * Physics.gravity.magnitude;

        //engineforce = upDirection * ((mass* gravity) + thrust)) / 4 rotar blades --- for distribution as we assign force to each of them seperately
        engineForce = Vector3.up * ((rb.mass * Physics.gravity.magnitude + finalDiff) + (inputs.Throttle * maxPower)) / 4f;
        rb.AddRelativeForce(engineForce, ForceMode.Force);

        Debug.Log("force" + engineForce, gameObject);
        UI.SetThrottleValue(engineForce.y);
    }
    public void RotateBlades(DroneMovementController droneMovement, InputsController inputs)
    {
        if (droneMovement.IsGrounded && !inputs.IsInput)                                //is on ground and we don't input
        {
            RotationSpeed = Mathf.Lerp(rotationSpeed, 0, Time.deltaTime * 2f);
        }

        else if (droneMovement.IsGrounded && inputs.IsInput)                            //is on ground and we input
            RotationSpeed += 10f;

        else if (!droneMovement.IsGrounded && inputs.IsInput)                           //not on ground and we input
        {
            if (rotationSpeed <= 1000000)                    //capping the rotation speed
                rotationSpeed = 1000;
            float _throttle;
            if (inputs.Throttle == 0)                                                   //while not applying thrust
                _throttle = 1;
            else
                _throttle = Mathf.Abs(inputs.Throttle);                                 //for blades to rotate in single direction

            RotationSpeed += _throttle * rotationSpeed;
        }

        else if (!droneMovement.IsGrounded && !inputs.IsInput)                           //not on ground and we don't input
        {
            RotationSpeed += 100f;
        }

        transform.localRotation = Quaternion.Euler(-90f, RotationSpeed, 0);
    }

}
