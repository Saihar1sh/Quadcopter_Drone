using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private bool isInput, hasVerticalInput;

    private float vertical, horizontal;

    public bool IsInput { get { return isInput; } }
    public bool HasVerticalInput { get { return hasVerticalInput; } }

    public float Vertical { get { return vertical; } }
    public float Horizontal { get { return horizontal; } }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputValues();
        //Debug.Log("h : " + horizontal + " v : " + vertical);
    }

    private void InputValues()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.anyKey)
            isInput = true;
        else
            isInput = false;

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            hasVerticalInput = true;
        }
    }

}
