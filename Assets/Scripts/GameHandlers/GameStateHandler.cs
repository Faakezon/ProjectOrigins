using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour{

    public enum GameState
    {
        MainMenu, CharacterScreen, PlanetExplorer, Planet
    }

    public static GameState currentGameState;


    /// <summary>
    /// Set when game starts by SceneHandler.
    /// Should always be MainMenu in final game version.
    /// </summary>
    public static void SetStateAwake()
    {
        switch (SceneHandler.instance.GetCurrentScene().name)
        {
            case "MainMenu":
                SetNewGameState(GameState.MainMenu);
                break;
            case "PlanetExplorer":
                SetNewGameState(GameState.PlanetExplorer);
                break;
        }
    }


    public static void SetNewGameState(GameState newGameState)
    {
        currentGameState = newGameState;
    }
}
