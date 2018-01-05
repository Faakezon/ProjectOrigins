using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour {

    public static SceneHandler instance = null;
    //public Slider loadingBar;
    private AsyncOperation async;
    private string _nextScene;

    private PlayerData pData; 

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

    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "LoadingScreen")
        {
            //loadingBar = FindObjectOfType<Slider>();
            Debug.Log("LoadingScreen");
            LoadAsync(_nextScene);
        }

    }

    public Scene GetCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }
    public Scene[] GetCurrentScenes()
    {
        Scene[] scenes = new Scene[SceneManager.sceneCount]; 
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            scenes[i] = SceneManager.GetSceneAt(i);
        }

        return scenes;
    }
    public bool IsSceneActive(string sceneName)
    {
        Scene[] scenes = new Scene[SceneManager.sceneCount];
        scenes = GetCurrentScenes();
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(scenes[i].name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    public void ToggleInventory()
    {
        if (SceneHandler.instance.IsSceneActive("InventoryScreen"))
        {
            //SceneManager.LoadSceneAsync("PlanetExplorer", LoadSceneMode.Additive); //REMOVE
            StartCoroutine("UnloadInventoryScreen");
        }
        else
        {
            SceneManager.LoadSceneAsync("InventoryScreen", LoadSceneMode.Additive);
            StartCoroutine("LoadInventoryScreen");
        }
    }


    public void LoadScene(string nextScene)
    {
        _nextScene = nextScene;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void LoadAsync(string level)
    {
        StartCoroutine(LoadLevelWithBar(level));
    }

    private bool GetSavedPlayerData()
    {
        pData = DataController.instance.GetSavedPlayerData();
        if (pData != null)
            return true;
        else
        {
            return false;
        }
    }

    IEnumerator LoadInventoryScreen()
    {
        Debug.Log("Waiting for inventory screen to load...");
        yield return new WaitUntil(() => SceneManager.GetSceneByName("InventoryScreen").isLoaded);
        Debug.Log("Inventory screen was loaded!");

        //Instantiate Equipment and Inventory 
        yield return new WaitUntil(() => PlayerEquipment.instance.InstantiateEquipment());
        yield return new WaitUntil(() => PlayerInventory.instance.InstantiateInventory());

        //Fetch saved data.
        yield return new WaitUntil(() => GetSavedPlayerData());
        Debug.Log("Beginner: " + pData.newlyCreatedCharacter);
        //Load saved data into Equipment and Inventory
        yield return new WaitUntil(() => PlayerEquipment.instance.LoadEquipmentData(pData));
        yield return new WaitUntil(() => PlayerInventory.instance.LoadInventoryData(pData));

        //SceneManager.UnloadSceneAsync("PlanetExplorer"); //REMOVE
    }

    IEnumerator UnloadInventoryScreen()
    {
        Debug.Log("Wait until Inventory and Equipment is saved...");
        yield return new WaitUntil(() => PlayerInventory.instance.SaveInventoryData());
        yield return new WaitUntil(() => PlayerEquipment.instance.SaveEquipmentData());
        Debug.Log("Inventory and Equipment Saved!");
        SceneManager.UnloadSceneAsync("InventoryScreen");
    }

    IEnumerator LoadLevelWithBar(string level)
    {
        async = SceneManager.LoadSceneAsync(level);
        while (!async.isDone)
        {
            //Debug.Log(async.progress);
            //loadingBar.value = async.progress;
            yield return null;
        }
    }

}
