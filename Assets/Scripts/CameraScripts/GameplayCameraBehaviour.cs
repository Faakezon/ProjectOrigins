using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCameraBehaviour : MonoBehaviour {

    //Editor Variable
    public float XAimOffset = 2;
    public float YAimOffset = 1;
    public float MovementOffset = 2;

    //Target
    public Transform _target;

    //Offset
    private Vector3 offsetVector;
    private Vector3 aimOffset;
    private Vector3 movementOffset;


    // Use this for initialization
    public void SetStartOffsetVectorPosition (Vector3 myNewPos, Transform target) {
        _target = target;
        offsetVector = myNewPos - _target.position;
    }

    private void Update() {
        aimOffset = new Vector3(XAimOffset * PlayerInput.instance.rightHorizontal, YAimOffset * PlayerInput.instance.rightVertical, 0);
        movementOffset = new Vector3(PlayerInput.instance.horizontal * MovementOffset, 0, 0);
    }

    // Update is called once per frame
    void LateUpdate () {
        if(_target != null)
            transform.position = Vector3.Lerp(transform.position, _target.position + offsetVector + aimOffset + movementOffset, Time.deltaTime * 5);
	}
}
