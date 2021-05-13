using System;
using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!rb)
        {
            Debug.LogWarning("There is no Rigidbody attached", gameObject);
            return;
        }
        HandlePhysics();
    }

    protected virtual void HandlePhysics()
    {

    }
}
