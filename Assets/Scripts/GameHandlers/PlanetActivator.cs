using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetActivator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InputHandler.instance.SetCurrentState(InputHandler.GameState.Planet);
    }
	
}
