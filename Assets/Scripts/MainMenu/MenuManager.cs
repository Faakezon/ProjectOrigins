using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance = null;

    private GameObject characterScreen;
    private GameObject createCharacterScreen;
    private GameObject loadCharacterScreen;

    private PlayerData playerData;

    private EventSystem es;

    private bool gameDataExist;

    public GameObject CharacterScreen
    {
        get
        {
            return characterScreen;
        }

        set
        {
            characterScreen = value;
        }
    }

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

        CharacterScreen = GameObject.Find("CharacterScreen");
        createCharacterScreen = CharacterScreen.transform.GetChild(0).gameObject;
        loadCharacterScreen = CharacterScreen.transform.GetChild(1).gameObject;

        playerData = new PlayerData();
        es = FindObjectOfType<EventSystem>();
	}
	

    bool LoadGameData()
    {
        return DataController.instance.LoadGameData();
    }

    public void ClearGameData()
    {
        Camera.main.GetComponent<Animator>().SetBool("CharacterScreen", false);
        Camera.main.GetComponent<Animator>().SetBool("PlanetExplorerState", false);
        createCharacterScreen.SetActive(false);
        loadCharacterScreen.SetActive(false);
        DataController.instance.ClearGameData();
        GameStateHandler.SetNewGameState(GameStateHandler.GameState.MainMenu);
        ReticuleBehaviour.instance.Reticle.position = ReticuleBehaviour.instance.startPos;
    }

    public void RunCharacterScreen()
    {
        Camera.main.GetComponent<Animator>().SetBool("CharacterScreen", true);

        CharacterScreen.transform.GetChild(0).gameObject.GetComponentInChildren<InputField>().text = "";
        gameDataExist = LoadGameData();

        //Load Game Data
        if (gameDataExist) //There was a character already.
        {
            SetupExistingCharacterScreen();

        }
        else //No character available, create one.
        {
            SetupCreateNewCharacterScreen();
        }
    }

    public void SetSelectionForGamepad(GameObject SelectionObj)
    {
        es.SetSelectedGameObject(SelectionObj);   
    }

    void SetupExistingCharacterScreen()
    {
        createCharacterScreen.SetActive(false);
        loadCharacterScreen.SetActive(true);

        loadCharacterScreen.transform.GetChild(1).GetComponent<Text>().text = DataController.instance.GetPlayerData().name;
    }
    
    void SetupCreateNewCharacterScreen()
    {
        createCharacterScreen.SetActive(true); //Activate CreateCharacterScreen
        loadCharacterScreen.SetActive(false); //Deactivate LoadCharacterScreen
    }

    public void CreateNewCharacter()
    {
        playerData.name = createCharacterScreen.GetComponentInChildren<InputField>().text;
        DataController.instance.SaveGameData(playerData);
        gameDataExist = LoadGameData();

        if (gameDataExist) //There was a character already.
        {
            SetupExistingCharacterScreen();

        }
        else //No character available, create one.
        {
            SetupCreateNewCharacterScreen();
        }

    }


    public void LogIn()
    {
        SceneHandler.instance.LoadScene("PlanetExplorer");
    }

}
