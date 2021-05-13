using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneBase : MonoBehaviour
{
    protected Rigidbody rb;
    protected float startDrag, startAngDrag;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb)
        {
            startDrag = rb.drag;
            startAngDrag = rb.angularDrag;
        }
    }
    void FixedUpdate()
    {
        if (!rb)
        {
            Debug.LogWarning("There is no Rigidbody attached", gameObject);
            return;
        }
        HandlePhysics();
    }

    protected virtual void HandlePhysics() { }
}
