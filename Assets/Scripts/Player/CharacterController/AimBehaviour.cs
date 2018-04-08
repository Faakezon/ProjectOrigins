using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBehaviour : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    public float deadzone = 0.25f;
    public float rotationSpeed = 4f;

    Vector3 lookDirection;

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
        Rotate();
    }

    void HandleInput()
    {
        horizontal = PlayerInput.instance.rightHorizontal;
        vertical = -PlayerInput.instance.rightVertical;

        Vector2 stickInput = new Vector3(horizontal, -vertical, 0);

        if (stickInput.magnitude < deadzone)
        {
            stickInput = Vector2.zero;
        }

        lookDirection = stickInput.normalized;
        //Debug.Log( Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg);



    }

    void Rotate()
    {
        //Vector3 skipRot = lookDirection * Mathf.Rad2Deg;
        //if (skipRot.x > -0.5f && skipRot.x < 0.5f)
        //{
        //    PlayerMovement.instance.AimAble = false;
        //}
        //else
        //{
        //    PlayerMovement.instance.AimAble = true;
        //}


        if (Quaternion.LookRotation(lookDirection) != new Quaternion(0, 0, 0, 1))
        {
            Quaternion newRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection, Vector3.up), rotationSpeed * Time.deltaTime);
            transform.rotation = newRot;
        }
        
    }

    
}
