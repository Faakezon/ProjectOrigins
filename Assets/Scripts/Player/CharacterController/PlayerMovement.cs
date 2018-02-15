using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Static instance.
    public static PlayerMovement instance = null;

    //Public Variables
    public float MaxSpeed = 20;
    public bool AimAble = true;

    //Private Variables
    private bool isLanding;
    private bool isGrounded = true;
    private bool jumpButtonPressed = false;

    //Private Components
    Rigidbody rigid;
    Animator anim;
    CapsuleCollider capCol;

    //Helper
    public Transform shoulderTransform;
    public Transform rightShoulder;
    public Vector3 lookPos; //AimObj.position
    public Transform AimObj;
    GameObject rsp; //Right shoulder position helper.
    Transform spine;

    //Direction
    Quaternion rightDir;
    Quaternion leftDir;


    void Awake () {
        spine = GameObject.Find("Spine").GetComponent<Transform>();
        SetupSingleton();


        rigid = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
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
        HandleMovement();
        HandleRotation();
        HandleAimingPos();
        HandleAnimations();
        HandleShoulder();

        Shoot(PlayerInput.instance.rightTrigger); //Here for now.
    }

    private void FixedUpdate()
    {
        anim.SetFloat("XVel1", Mathf.Abs(rigid.velocity.x + 1));
        isLanding = IsLanding();
        HandleJump();
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
        float animValue = PlayerInput.instance.horizontal;

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

        if(PlayerInput.instance.rightHorizontal > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rightDir, Time.deltaTime * 15);
        }else if(PlayerInput.instance.rightHorizontal < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, leftDir, Time.deltaTime * 15);
        }
    }

    private void HandleMovement()
    {
        Vector3 movement = new Vector3(PlayerInput.instance.horizontal, 0.0f, 0.0f);
        //rigid.MovePosition(rigid.position + (Vector3.right * movement.x * MaxSpeed * Time.deltaTime)); //MaxSpeed == 5 works pretty well with anims.
        rigid.velocity = new Vector2(movement.x * MaxSpeed, rigid.velocity.y);
    }

    private void HandleJump()
    {
        
        if(isGrounded && PlayerInput.instance.jumpInput == 1)
        {
            anim.SetTrigger("Jump");
        }
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isLanding", IsLanding());

        //if(PlayerInput.instance.jumpInput != 0 && inAir == false)
        //{
        //    rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
        //}

    }

    private bool IsLanding()
    {
        if (isGrounded && !isLanding)
            return false;


        Ray ray = new Ray(this.capCol.transform.position, Vector3.down);
        RaycastHit hit;

        //Debug.Log(rigid.velocity.y + Physics.gravity.y);
        
        if (Physics.Raycast(ray, out hit, 15))
        {
            isLanding = false;
        }
        if (Vector3.Distance(this.capCol.transform.position, hit.point) < 1.1f && (rigid.velocity.y + Physics.gravity.y) <= -9.81f)
        {
            //anim.ResetTrigger("Jump");
            isLanding = true;
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        return isLanding;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("LANDED");
            anim.ResetTrigger("Jump");
            isGrounded = true;
            isLanding = false;
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isLanding", isLanding);
        }
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


    public void JumpAniLeaveGround()
    {
        Debug.Log("JumpAniLeaveGround");
        isGrounded = false;
        isLanding = false;
        rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isLanding", isLanding);
    }

}



