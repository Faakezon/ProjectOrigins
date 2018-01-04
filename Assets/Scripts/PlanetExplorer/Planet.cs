using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public string Name;
    public int LevelRequirement = 0;

    private void Start()
    {
        Name = this.gameObject.name;
        
    }


}
