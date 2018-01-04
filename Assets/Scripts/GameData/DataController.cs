using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class DataController : MonoBehaviour {

    public GameData gameData;
    private string gameDataProjectFilePath = "/StreamingAssets/data.json";

    private string gameDataFileName = "data.json";

    private PlayerData allPlayerData;

    public static DataController instance = null;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        
    }


    public bool LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            // Retrieve the allPlayerData property of loadedData
            allPlayerData = loadedData.allPlayerData;

            Debug.Log("Game Data Loaded");

            if (allPlayerData.name != "")
            {
                LoadPlayerProgress();
                return true;
            }
            else
            {
                Debug.Log("No player found.");
                return false;
            }

        }
        else
        {
            Debug.LogError("Cannot load game data!");
            return false;
        }
    }

    public void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);

        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }

    public void SaveGameData(PlayerData playerData)
    {
        gameData.allPlayerData = playerData;
        string dataAsJson = JsonUtility.ToJson(gameData);

        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }



    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    private void LoadPlayerProgress()
    {
        Debug.Log("Player Progress Loaded");
        // Create a new PlayerProgress object
        //playerProgress = new PlayerProgress();

        // If PlayerPrefs contains a key called "highestScore", set the value of playerProgress.highestScore using the value associated with that key
        //if (PlayerPrefs.HasKey("highestScore"))
        //{
        //    playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        //}
    }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    private void SavePlayerProgress()
    {
        // Save the value playerProgress.highestScore to PlayerPrefs, with a key of "highestScore"
        //PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }

    public PlayerData GetPlayerData()
    {
        return allPlayerData;
    }

    public PlayerData GetSavedPlayerData()
    {
        LoadGameData();
        return allPlayerData;
    }

    public void SaveInventoryData(PlayerData pData)
    {
        Debug.Log("Saving Inventory Data in Data Controller");
        PlayerData apData = allPlayerData;
        apData.inventoryList.Clear();
        SaveGameData(apData);
        apData.inventoryList = pData.inventoryList;
        SaveGameData(apData);
    }
    public void SaveEquipmentData(PlayerData pData)
    {
        PlayerData apData = allPlayerData;
        apData.newlyCreatedCharacter = pData.newlyCreatedCharacter;
        apData.weaponData = pData.weaponData;
        apData.headData = pData.headData;
        apData.torsoData = pData.torsoData;
        apData.bootsData = pData.bootsData;
        SaveGameData(apData);
    }
    public void ClearGameData()
    {
        allPlayerData = new PlayerData();
        SaveGameData(allPlayerData);
        LoadGameData();
    }

}
