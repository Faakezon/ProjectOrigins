using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

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

        rigid = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        SetupAnimator();

        rsp = new GameObject();
        rsp.name = transform.root.name + " Right Shoulder IK Helper";

        rightDir = transform.rotation;
        leftDir = rightDir * Quaternion.Euler(0,180,0);
        
    }


    void Update () {
        HandleMovement();
        HandleRotation();
        HandleAimingPos();
        HandleMovementAnimation();
        HandleShoulder();

        Shoot(PlayerInput.instance.rightTrigger); //Here for now.
    }

    private void FixedUpdate()
    {
        anim.SetFloat("XVel1", Mathf.Abs(rigid.velocity.x + 1));
        //isLanding = IsLanding();
        HandleJump();
        if(!anim.GetBool("isGrounded"))
            SetIsLanding(IsLanding());
    }


    private void HandleShoulder()
    {
        shoulderTransform.LookAt(lookPos);

        Vector3 rightShoulderPos = rightShoulder.TransformPoint(Vector3.zero);
        rsp.transform.position = rightShoulderPos;
        rsp.transform.parent = transform;

        shoulderTransform.position = rsp.transform.position;
    }

    private void HandleMovementAnimation()
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
            TriggerJump();
        }
    }



    private bool IsLanding()
    {
        Ray ray = new Ray(this.capCol.transform.position, Vector3.down);
        RaycastHit hit;

        //Debug.Log(rigid.velocity.y + Physics.gravity.y);

        if (Physics.Raycast(ray, out hit, 15))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
        }
        if (Vector3.Distance(this.capCol.transform.position, hit.point) < 1.1f && (rigid.velocity.y + Physics.gravity.y) <= -9.81f)
        {
            return true;
        }
        else return false;
        
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides with the ground layer, the player has landed (set to false) and is grounded (set to true).
        if(collision.collider.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //Debug.Log("LANDED");
            ResetJumpTrigger();
            SetIsGrounded(true);
            SetIsLanding(false);
        }
    }



    private void Shoot(float triggerInput) //Here for now.
    {
        if(triggerInput <= 0)
        {
            SetIsShooting(false);
            return;
        }
        else
        {
            RecoilScript.instance.shake = 1.0f;
            SetIsShooting(true);
        }
    }

    public void JumpAniLeaveGround()
    {
        Debug.Log("JumpAniLeaveGround");
        rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
        SetIsGrounded(false);
        SetIsLanding(false);
    }

    private void SetupAnimator()
    {
        anim = GetComponent<Animator>();
    }

    private void SetIsLanding(bool landing)
    {
        anim.SetBool("isLanding", landing);
    }

    private void SetIsGrounded(bool grounded)
    {
        anim.SetBool("isGrounded", grounded);
    }

    private void SetIsShooting(bool shooting)
    {
        anim.SetBool("Shooting", shooting);
    }

    private void TriggerJump()
    {
        anim.SetTrigger("Jump");
    }

    private void ResetJumpTrigger()
    {
        anim.ResetTrigger("Jump");
    }
}



