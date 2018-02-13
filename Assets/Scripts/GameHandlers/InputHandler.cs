using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour {

    public static InputHandler instance = null;


    // Use this for initialization
    void Awake () {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update () {

        switch (GameStateHandler.currentGameState)
        {
            case GameStateHandler.GameState.MainMenu:
                MainMenuController();
                MainCameraRef.instance.SetCameraStaticPos();
                break;
            case GameStateHandler.GameState.CharacterScreen:
                CharacterScreen();
                MainCameraRef.instance.SetCameraStaticPos();
                break;
            case GameStateHandler.GameState.PlanetExplorer:
                PlanetExplorerController();
                MainCameraRef.instance.SetCameraStaticPos();
                break;
            case GameStateHandler.GameState.Planet:
                PlanetController();
                MainCameraRef.instance.SetCameraStaticPos();
                break;
            default:
                break;
        }
    }

    

    void MainMenuController()
    {
        if (Input.GetButtonDown("Start"))
        {
            GameStateHandler.SetNewGameState(GameStateHandler.GameState.CharacterScreen);
            MenuManager.instance.RunCharacterScreen();
        }
    }

    void CharacterScreen()
    {
        //Just a reset to MainLogoScreen if user deletes character.
        //if(MenuManager.currentState == MenuManager.MenuState.LogoScreen)
        //    GameStateHandler.SetNewGameState(GameStateHandler.GameState.MainMenu);

        if (SceneHandler.instance.GetCurrentScene().name == "PlanetExplorer")
            GameStateHandler.SetNewGameState(GameStateHandler.GameState.PlanetExplorer);
    }

    void PlanetExplorerController()
    {
        if (Input.GetButtonDown("Start"))
        {
            SceneHandler.instance.ToggleInventory();
            CameraGlitchVFX.instance.GlitchAttack();

        }
    }

    void PlanetController()
    {
        
        if (Input.GetButtonDown("Start"))
        {
            SceneHandler.instance.ToggleInventory();
        }
        if (SceneHandler.instance.IsSceneActive("InventoryScreen"))
        {
            MainCameraRef.instance.SetReticuleActive(true);
        }
        else
        {
            MainCameraRef.instance.SetReticuleActive(false);
        }
    }

}
