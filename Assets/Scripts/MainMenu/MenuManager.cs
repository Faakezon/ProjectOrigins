using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance = null;


    public enum MenuState
    {
        LogoScreen, CharacterScreen
    }

    public static MenuState currentState;

    private GameObject characterScreen;
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



        currentState = MenuState.LogoScreen;
        CharacterScreen = GameObject.Find("CharacterScreen");
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
        currentState = MenuState.LogoScreen;
        DataController.instance.ClearGameData();
    }

    public void RunCharacterScreen()
    {
        Camera.main.GetComponent<Animator>().SetBool("CharacterScreen", true);
        currentState = MenuState.CharacterScreen;
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
        CharacterScreen.transform.GetChild(0).gameObject.SetActive(false);
        CharacterScreen.transform.GetChild(1).gameObject.SetActive(true);

        CharacterScreen.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = DataController.instance.GetPlayerData().name;
    }
    
    void SetupCreateNewCharacterScreen()
    {
        CharacterScreen.transform.GetChild(0).gameObject.SetActive(true);
        CharacterScreen.transform.GetChild(1).gameObject.SetActive(false);



    }

    public void CreateNewCharacter()
    {
        playerData.name = CharacterScreen.transform.GetChild(0).gameObject.GetComponentInChildren<InputField>().text;
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
        SceneMng.instance.LoadScene("PlanetExplorer");
    }

}
