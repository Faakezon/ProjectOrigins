using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{
    Animator anim;
    private Transform AimObj;
    Vector3 IK_lookPos;
    Vector3 targetPos;

    public float lerpRate = 15;
    
    public float lookWeight = 1;
    public float bodyWeight = .9f;
    public float headWeight = 1;
    public float clampWeight = 1;

    public float rightHandWeight = 1;
    public float leftHandWeight = 1;

    private Transform rightHandTarget, rightHand;
    private Transform rightElbowTarget, rightElbow;
    private Transform leftHandTarget, leftHand;
    private Transform leftElbowTarget, leftElbow;


    void Start()
    {
        anim = GetComponent<Animator>();
        

        AimObj = GameObject.Find("AimObj").transform;

        rightHand = GameObject.Find("RightHandTarget").transform;
        rightElbow = GameObject.Find("RightElbowTarget").transform;
        leftHand = GameObject.Find("LeftHandTarget").transform;
        leftElbow = GameObject.Find("LeftElbowTarget").transform;
    }

    private void Update()
    {
        targetPos = AimObj.position;
        rightHandTarget = rightHand;
        rightElbowTarget = rightElbow;
        leftHandTarget = leftHand;
        leftElbowTarget = leftElbow;
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


        IK_lookPos = Vector3.Slerp(IK_lookPos, targetPos, Time.deltaTime * lerpRate);

        anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight, headWeight, clampWeight);
        anim.SetLookAtPosition(targetPos);
        
        

    }





}
