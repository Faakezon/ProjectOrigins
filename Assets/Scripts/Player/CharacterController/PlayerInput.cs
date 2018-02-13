using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance = null;

    //Axis
    public float horizontal;
    public float vertical;
    
    public float rightHorizontal;
    public float rightVertical;

    public float rightTrigger;
    //Buttons
    public float jumpInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Update is for buttons.
        jumpInput = Input.GetAxis("XboxY");
    }
    

    void FixedUpdate()
    {   
        //FixedUpdate is for axis.
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        rightHorizontal = Input.GetAxis("RightStickHorizontal");
        rightVertical = -Input.GetAxis("RightStickVertical");

        rightTrigger = Input.GetAxis("RightTrigger");



    }
}

