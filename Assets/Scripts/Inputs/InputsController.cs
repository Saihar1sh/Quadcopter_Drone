using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputsController : MonoBehaviour
{
    private bool isInput;

    private Vector2 cyclic;
    private float yawInput, throttle;


    //properties -- read only
    public bool IsInput { get { return isInput; } }

    public Vector2 Cyclic { get => cyclic; }
    public float YawInput { get => yawInput; }
    public float Throttle { get => throttle; }

    void Update()
    {

        if (cyclic != Vector2.zero || yawInput != 0 || throttle != 0)
            isInput = true;
        else
            isInput = false;
    }

    private void OnCyclic(InputValue input)
    {
        cyclic = input.Get<Vector2>();
    }
    private void OnYaw(InputValue input)
    {
        yawInput = input.Get<float>();
    }
    private void OnThrottle(InputValue input)
    {
        throttle = input.Get<float>();
    }
}
