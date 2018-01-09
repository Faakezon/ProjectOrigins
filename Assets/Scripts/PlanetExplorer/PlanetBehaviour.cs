using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetBehaviour : MonoBehaviour {

    private Planet planet;
    private Behaviour halo;

    private void Start()
    {
        planet = GetComponent<Planet>();
        halo = (Behaviour)GetComponent("Halo");
        
    }

    private void OnMouseEnter()
    {
        PlanetSelector.instance.Set_PlanetInfo(planet.name, planet.LevelRequirement);
        halo.enabled = true;
       
    }

    private void OnMouseExit()
    {
        PlanetSelector.instance.Hide_PlanetInfo();
        halo.enabled = false;
    }

    // Custom reticle events
    void OnReticleEnter()
    {
        //Debug.Log("Entering over " + this.transform.parent.name); 
        Invoke("OnMouseEnter", 0);
    }
    void OnReticleExit()
    {
        //Debug.Log("Exiting over " + this.transform.parent.name); 
        Invoke("OnMouseExit", 0);
    }
    void OnReticleHover()
    {
        //Debug.Log("Hovering over " + this.transform.parent.name);
    }


}
