using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReticuleBehaviour : MonoBehaviour {

    private float reticuleSpeed = 0.1f;
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal") * reticuleSpeed, Input.GetAxis("Vertical") * reticuleSpeed, 0);
        transform.Translate(movementVector);

    }





}







