using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats instance = null;

    //Stats at creation, Not Changeable
    private string _name = "MyName";
    private float _speed = 3.5f;

    private int _health = 100;
    

    public string Name { get { return _name; } }
    public float Speed { get { return _speed; } set { _speed = value; } }

    public int Health { get { return _health; } set { _health = value; } }


    public void RecoverHealth(InventoryItem item)
    {
        Debug.Log("The requested feature is not implemented.");
    }


    private void Awake()
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
}
