using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{

    Animator anim;
    private Vector3 AimObj;
    Vector3 IK_lookPos;
    Vector3 targetPos;

    PlayerMovement pm;

    public float lerpRate = 15;
    public float updateLookPosThreshold = 2;
    public float lookWeight = 1;
    public float bodyWeight = .9f;
    public float headWeight = 1;
    public float clampWeight = 1;

    public float rightHandWeight = 1;
    public float leftHandWeight = 1;

    private Transform rightHandTarget;
    private Transform rightElbowTarget;
    private Transform leftHandTarget;
    private Transform leftElbowTarget;


    void Start()
    {
        anim = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();

        AimObj = GameObject.Find("AimObj").transform.position;

        rightHandTarget = GameObject.Find("RightHandTarget").transform;
        rightElbowTarget = GameObject.Find("RightElbowTarget").transform;
        leftHandTarget = GameObject.Find("LeftHandTarget").transform;
        leftElbowTarget = GameObject.Find("LeftElbowTarget").transform;
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        IK_Handling();        
    }

    void IK_Handling()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);

        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);

        anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rightHandWeight);
        anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, leftHandWeight);

        anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowTarget.position);
        anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowTarget.position);

        //this.lookPos = pm.lookPos;
        //lookPos.z = transform.position.z;

        float distanceFromPlayer = Vector3.Distance(AimObj, transform.position);

        if (distanceFromPlayer > updateLookPosThreshold)
        {
            targetPos = AimObj;
        }

        IK_lookPos = Vector3.Slerp(IK_lookPos, targetPos, Time.deltaTime * lerpRate);

        anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight, headWeight, clampWeight);
        anim.SetLookAtPosition(IK_lookPos);

    }





}
