using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour {

    public static InputHandler instance = null;

    public enum GameState
    {
        MainMenu, CharacterScreen, PlanetExplorer, Planet
    }

    public static GameState currentState;


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

        //SetCurrentState(GameState.MainMenu);
        SetStateAwake();

        

    }

    void SetStateAwake()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
                SetCurrentState(GameState.MainMenu);
                break;
            case "PlanetExplorer":
                SetCurrentState(GameState.PlanetExplorer);
                break;
        }
    }

    // Update is called once per frame
    void Update () {

        //Debug.Log(currentState);

        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuController();
                MainCameraRef.instance.SetCameraStaticPos();
                break;
            case GameState.CharacterScreen:
                CharacterScreen();
                MainCameraRef.instance.SetCameraStaticPos();
                break;
            case GameState.PlanetExplorer:
                PlanetExplorerController();
                MainCameraRef.instance.SetCameraStaticPos();
                break;
            case GameState.Planet:
                PlanetController();
                MainCameraRef.instance.SetCameraStaticPos();
                break;
            default:
                break;
        }



    }

    public void SetCurrentState(GameState gameState)
    {
        currentState = gameState;
    }

    void MainMenuController()
    {
        if (Input.GetButtonDown("Start") || Input.GetButtonDown("XboxA"))
        {
            Debug.Log("START");
            SetCurrentState(GameState.CharacterScreen);
            MenuManager.instance.RunCharacterScreen();

        }

       
    }

    void CharacterScreen()
    {
        //Just a reset to MainLogoScreen if user deletes character.
        if(MenuManager.currentState == MenuManager.MenuState.LogoScreen)
            SetCurrentState(GameState.MainMenu);

        if (SceneHandler.instance.GetCurrentScene().name == "PlanetExplorer")
            SetCurrentState(GameState.PlanetExplorer);
    }

    void PlanetExplorerController()
    {
        if (Input.GetButtonDown("Start"))
        {
            SceneHandler.instance.ToggleInventory();
            MainCameraRef.instance.GlitchAttack();

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
