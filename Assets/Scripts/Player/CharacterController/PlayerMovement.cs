using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Public Variables
    public float MaxSpeed = 20;
    public float Acceleration = 64;
    public float JumpSpeed = 8;
    public float JumpDuration = 150;


    //Input variables
    private float horizontal;
    private float vertical;
    private float jumpInput;

    //Internal variables
    private bool onGround;
    private float jumpDuration;
    private bool jumpKeyDown = false;
    private bool canJump = false;
    private float movement_Anim;

    Rigidbody rigid;
    Animator anim;
    LayerMask layerMask;
    Transform modelTransform;

    public Transform shoulderTransform;
    public Transform rightShoulder;
    public Vector3 lookPos;
    public Transform AimObj;
    GameObject rsp; //Right shoulder position helper.


    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        SetupAnimator();

        layerMask = ~(1 << 8);

        rsp = new GameObject();
        rsp.name = transform.root.name + " Right Shoulder IK Helper";
    }


    // Update is called once per frame
    void FixedUpdate () {

        HandleInput();
        UpdateRigidbodyValues();
        HandleMovement();
        HandleRotation();
        HandleAimingPos();
        HandleAnimations();
        HandleShoulder();

        Shoot(Input.GetAxis("RightTrigger"));
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Fire1");
    }


    private void HandleShoulder()
    {
        shoulderTransform.LookAt(lookPos);

        Vector3 rightShoulderPos = rightShoulder.TransformPoint(Vector3.zero);
        rsp.transform.position = rightShoulderPos;
        rsp.transform.parent = transform;

        shoulderTransform.position = rsp.transform.position;
    }

    private void HandleAnimations()
    {
        anim.SetBool("inAir", !onGround);
        float animValue = horizontal;

        if(lookPos.x < transform.position.x)
        {
            Debug.Log("Derp");
            animValue = -animValue;
        }
        //anim.SetFloat("Movement", animValue);
        anim.SetFloat("Movement", animValue, 0.1f, Time.deltaTime);
    }

    private void HandleAimingPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookPos = lookP;
        }

        lookPos = AimObj.transform.position;
        Debug.DrawLine(transform.GetChild(2).GetChild(2).position, lookPos);

    }

    private void HandleRotation()
    {
        Vector3 directionToLook = lookPos - transform.position;
        directionToLook.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);
    }

    private void HandleMovement()
    {
        onGround = isOnGround();

        if(horizontal < -0.1f)
        {
            if(rigid.velocity.x > -this.MaxSpeed)
            {
                rigid.AddForce(new Vector3(-this.Acceleration, 0, 0));
            }
            else
            {
                rigid.velocity = new Vector3(-this.MaxSpeed, rigid.velocity.y, 0);
            }
        }else if(horizontal > 0.1f)
        {
            if(rigid.velocity.x < this.MaxSpeed)
            {
                rigid.AddForce(new Vector3(this.Acceleration, 0, 0));
            }
            else
            {
                rigid.velocity = new Vector3(this.MaxSpeed, rigid.velocity.y, 0);
            }
        }



        if(jumpInput > 0.1f)
        {
            if (!jumpKeyDown)
            {
                jumpKeyDown = true;
                if(onGround)
                {
                    rigid.velocity = new Vector3(rigid.velocity.y, this.JumpSpeed, 0);
                    jumpDuration = 0.0f;
                }

            }else if (canJump)
            {
                jumpDuration += Time.deltaTime;
                if(jumpDuration < this.JumpDuration / 1000)
                {
                    rigid.velocity = new Vector3(rigid.velocity.x, this.JumpSpeed, 0);
                }
            }
        }
        else
        {
            jumpKeyDown = false;
        }

    }

    private bool isOnGround()
    {
        bool retVal = false;
        float lengthToSearch = 1.5f;

        Vector3 lineStart = transform.position + Vector3.up;
        Vector3 vectorToSearch = -Vector3.up;

        RaycastHit hit;

        if(Physics.Raycast(lineStart, vectorToSearch, out hit, lengthToSearch, layerMask))
        {
            retVal = true;
        }
        return retVal;
    }

    private void UpdateRigidbodyValues()
    {
        if (onGround)
        {
            rigid.drag = 4;
        }
        else
        {
            rigid.drag = 0;
        }
    }



    private void SetupAnimator()
    {
        anim = GetComponent<Animator>();

        //foreach(var childAnimator in GetComponentsInChildren<Animator>())
        //{
        //    if(childAnimator != anim)
        //    {
        //        anim.avatar = childAnimator.avatar;
        //        modelTransform = childAnimator.transform;
        //        Destroy(childAnimator);
        //        break;
        //    }
        //}
    }


    private void Shoot(float triggerInput)
    {
        if(triggerInput <= 0)
        {
            anim.SetBool("Shooting", false);
            return;
        }
        else
        {
            anim.SetBool("Shooting", true);
        }
    }

}
