using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSelector : MonoBehaviour {

    private GameObject PlanetInfoBox;
    private Text PlanetInfoBox_Name;
    private Text PlanetInfoBox_LevelReq;

    public static PlanetSelector instance = null;


    void Start () {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


        PlanetInfoBox = GameObject.Find("PlanetInfoBox");
        PlanetInfoBox_Name = PlanetInfoBox.transform.GetChild(2).GetComponent<Text>();
        PlanetInfoBox_LevelReq = PlanetInfoBox.transform.GetChild(3).GetComponent<Text>();

        PlanetInfoBox.SetActive(false);
    }

    private void Update()
    {
        if (PlanetInfoBox.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneHandler.instance.LoadScene(PlanetInfoBox_Name.text);
                GameStateHandler.SetNewGameState(GameStateHandler.GameState.Planet);
            }
            if (Input.GetButtonDown("XboxA"))
            {
                Debug.Log("SelectedPlanet: " + PlanetInfoBox_Name.text);
                SceneHandler.instance.LoadScene(PlanetInfoBox_Name.text);
                GameStateHandler.SetNewGameState(GameStateHandler.GameState.Planet);
            }
        }
    }

    public void Set_PlanetInfo(string name, int levelRequirement)
    {
        //Debug.Log(name + " " + levelRequirement);
        PlanetInfoBox.SetActive(true);
        PlanetInfoBox_Name.text = name;
        PlanetInfoBox_LevelReq.text = levelRequirement.ToString();

        
    }
    
    public void Hide_PlanetInfo()
    {
        PlanetInfoBox.SetActive(false);
    }
}
