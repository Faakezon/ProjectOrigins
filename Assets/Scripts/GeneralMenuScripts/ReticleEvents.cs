using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleEvents : MonoBehaviour {

    // Custom reticle events
    void OnReticleEnter()
    {
        //Debug.Log("Entering over " + this.transform.parent.name);
        MenuManager.instance.SetSelectionForGamepad(this.transform.parent.gameObject);
    }
    void OnReticleExit()
    {
        //Debug.Log("Exiting over " + this.transform.parent.name);
        MenuManager.instance.SetSelectionForGamepad(null);
    }
    void OnReticleHover()
    {
        //Debug.Log("Hovering over " + this.transform.parent.name);
    }
}
