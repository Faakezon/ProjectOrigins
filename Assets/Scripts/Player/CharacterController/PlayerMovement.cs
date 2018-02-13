using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance = null;

    //Public Variables
    public float MaxSpeed = 20;
    public bool AimAble = true;

    //Input variables
    private float horizontal;
    private float vertical;
    private float jumpInput;
    private float rightHorizontal;
    private float rightVertical;

    

    Rigidbody rigid;
    Animator anim;

    public Transform shoulderTransform;
    public Transform rightShoulder;
    public Vector3 lookPos; //AimObj.position
    public Transform AimObj;
    GameObject rsp; //Right shoulder position helper.

    Transform spine;

    Quaternion rightDir;
    Quaternion leftDir;

    void Awake () {
        spine = GameObject.Find("Spine").GetComponent<Transform>();
        SetupSingleton();


        rigid = GetComponent<Rigidbody>();
        SetupAnimator();

        rsp = new GameObject();
        rsp.name = transform.root.name + " Right Shoulder IK Helper";

        rightDir = transform.rotation;
        leftDir = rightDir * Quaternion.Euler(0,180,0);

    }

    void SetupSingleton()
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

    void Update () {

        HandleInput();
        HandleMovement();
        HandleRotation();
        HandleAimingPos();
        HandleAnimations();
        HandleShoulder();

        Shoot(PlayerInput.instance.rightTrigger); //Here for now.
    }

    private void HandleInput()
    {
        horizontal = PlayerInput.instance.horizontal;
        vertical = PlayerInput.instance.vertical;
        jumpInput = PlayerInput.instance.jumpInput;

        rightHorizontal = PlayerInput.instance.rightHorizontal;
        rightVertical = -PlayerInput.instance.rightVertical;
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
        float animValue = horizontal;

        if (lookPos.x < transform.position.x)
        {
            animValue = -animValue;
        }
        anim.SetFloat("Movement", animValue, 0.1f, Time.deltaTime);
    }

    private void HandleAimingPos()
    {
        if (AimAble)
        {
            lookPos = AimObj.transform.position;
            Debug.DrawLine(transform.GetChild(2).GetChild(2).position, lookPos);
        }
    }

    private void HandleRotation()
    {
        Vector3 directionToLook = lookPos - spine.transform.position;
        directionToLook.y = 0;
        directionToLook.z = spine.transform.position.z;
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
        spine.transform.rotation = Quaternion.Slerp(spine.transform.rotation, targetRotation, Time.deltaTime * 15);

        if(rightHorizontal > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rightDir, Time.deltaTime * 15);
        }else if(rightHorizontal < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, leftDir, Time.deltaTime * 15);
        }
    }

    private void HandleMovement()
    {
        Vector3 movement = new Vector3(horizontal, 0.0f, 0.0f);
        transform.position += Vector3.right * movement.x * MaxSpeed * Time.deltaTime; //MaxSpeed == 5 works pretty well with anims.
    }


    private void SetupAnimator()
    {
        anim = GetComponent<Animator>();
    }


    private void Shoot(float triggerInput) //Here for now.
    {
        if(triggerInput <= 0)
        {
            anim.SetBool("Shooting", false);
            return;
        }
        else
        {
            RecoilScript.instance.shake = 1.0f;
            anim.SetBool("Shooting", true);
        }
    }



}



