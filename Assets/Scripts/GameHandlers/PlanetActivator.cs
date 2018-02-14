using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetActivator : MonoBehaviour {

    //Camera Variables to start with
    public Transform CameraPosRot;
    public Transform CameraTarget;


	void Awake () {
        //Debug.Log("PlanetActivator: " + SceneHandler.instance.GetCurrentScene().name + " activated.");
        SetGameStateHandler();
        SetCameraStartPosRotForScene();
        ActivateGameplayCamera();
        ActivatePlayerInput();
    }

    void SetGameStateHandler()
    {
        GameStateHandler.SetNewGameState(GameStateHandler.GameState.Planet);
    }

    void SetCameraStartPosRotForScene()
    {
        MainCameraRef.instance.transform.position = CameraPosRot.position;
        MainCameraRef.instance.transform.rotation = CameraPosRot.rotation;
    }

    void ActivateGameplayCamera()
    {
        //Camera.main.GetComponent<GameplayCameraBehaviour>().enabled = true;
        Camera.main.GetComponent<GameplayCameraBehaviour>().SetStartOffsetVectorPosition(CameraPosRot.position, CameraTarget);
    }
    void ActivatePlayerInput()
    {
        PlayerInput.instance.UsePlayerInput = true;
    }

}
