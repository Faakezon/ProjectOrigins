using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLightActivator : MonoBehaviour {

    private LightShafts lightShaft;

	// Use this for initialization
	void Start () {
        lightShaft = GetComponent<LightShafts>();
        lightShaft.m_Cameras[0] = MainCameraRef.instance.GetComponent<Camera>();
	}
}
