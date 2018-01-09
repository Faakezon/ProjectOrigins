using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetActivator : MonoBehaviour {

	void Start () {
        GameStateHandler.SetNewGameState(GameStateHandler.GameState.Planet);
    }
	
}
