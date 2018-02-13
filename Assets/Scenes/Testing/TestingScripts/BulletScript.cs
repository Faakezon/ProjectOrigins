using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    float movementSpeed = 50;
    bool hit = false;
	
	// Update is called once per frame
	void FixedUpdate () {
        if(hit == false)
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

        Destroy(this.gameObject, 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        hit = true;   
    }

}
