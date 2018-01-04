using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Just a class to instantiate the database of items in the game.
/// </summary>


public class ItemDatabase : MonoBehaviour {
    public static InventoryItemList itemDatabase;

    void Start () {
        //WeaponList at the moment (Contains other items as well)
        itemDatabase = (InventoryItemList)Resources.Load("ItemDatabase/InventoryItemList"); 
    }
}
