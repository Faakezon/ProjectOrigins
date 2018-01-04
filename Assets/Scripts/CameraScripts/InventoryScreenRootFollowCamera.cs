using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreenRootFollowCamera : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        //Always follow camera pos in X Y 
        this.transform.position = new Vector3(MainCameraRef.instance.gameObject.transform.position.x, MainCameraRef.instance.gameObject.transform.position.y, this.transform.position.z);
	}
}
