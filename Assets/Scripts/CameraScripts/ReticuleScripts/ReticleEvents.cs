using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleEvents : MonoBehaviour {

    // Custom reticle events
    public void OnReticleEnter()
    {
        //Debug.Log("Entering over " + this.transform.parent.name + " , POSITION: " + this.transform.position);
        MenuManager.instance.SetSelectionForGamepad(this.transform.parent.gameObject);
    }
    public void OnReticleExit()
    {
        //Debug.Log("Exiting over " + this.transform.parent.name);
        MenuManager.instance.SetSelectionForGamepad(null);
    }
    void OnReticleHover()
    {
        //Debug.Log("Hovering over " + this.transform.parent.name);
    }
}
