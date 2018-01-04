using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour {

    /// <summary>
    /// WILL HANDLE ALL PREFABS TO INSTANTIATE AS GAMEOBJECTS IN THE FUTURE.
    /// </summary>

    protected static ObjectFactory instance;

    //JUST FOR PARENTING OF GUI
    public GameObject GUIParent;


    //GAME OBJECTS
    public GameObject ItemRepresentation;

    // Use this for initialization
    void Start () {
        instance = this;
	}


    /// <summary>
    /// For debugging purposes...
    /// </summary>
    /// <returns></returns>
    public static InventoryItem CreateWeaponItemInInventory()
    {
        if (ItemDatabase.itemDatabase.itemList[0] != null)
        {
            return new InventoryItem(ItemDatabase.itemDatabase.itemList[0]);
        }
        else
        {
            return null;
        }
    }
    public static InventoryItem CreateBetterWeaponItemInInventory()
    {
        if (ItemDatabase.itemDatabase.itemList[4] != null)
        {
            return new InventoryItem(ItemDatabase.itemDatabase.itemList[4]);
        }
        else
        {
            return null;
        }
    }
    public static InventoryItem CreateHeadItemInInventory()
    {
        if (ItemDatabase.itemDatabase.itemList[1] != null)
        {
            return new InventoryItem(ItemDatabase.itemDatabase.itemList[1]);
        }
        else
        {
            return null;
        }
    }
    public static InventoryItem CreateTorsoItemInInventory()
    {
        if (ItemDatabase.itemDatabase.itemList[2] != null)
        {
            return new InventoryItem(ItemDatabase.itemDatabase.itemList[2]);
        }
        else
        {
            return null;
        }
    }
    public static InventoryItem CreateBootsItemInInventory()
    {
        if (ItemDatabase.itemDatabase.itemList[3] != null)
        {
            return new InventoryItem(ItemDatabase.itemDatabase.itemList[3]);
        }
        else
        {
            return null;
        }
    }



    /// <summary>
    /// Spawn the Item passed as parameter.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="SpawnAt"></param>
    /// <returns></returns>
    public static ItemRepresentation CreateItem(InventoryItem item, Vector3 SpawnAt)
    {
        ItemRepresentation go = Instantiate(ObjectFactory.instance.ItemRepresentation, SpawnAt, Quaternion.identity).GetComponent<ItemRepresentation>();
        go.Init(item);
        return go;
    }
    public static ItemRepresentation CreateItem(InventoryItem item, Vector3 SpawnAt, Vector3 forceVector)
    {
        ItemRepresentation go = Instantiate(ObjectFactory.instance.ItemRepresentation, SpawnAt, Quaternion.identity).GetComponent<ItemRepresentation>();
        go.Init(item);
        go.GetComponent<Rigidbody>().AddForce(forceVector, ForceMode.Impulse);

        return go;
    }

    /// <summary>
    /// CREATE AN ITEM BASED ON PASSED IN ID PARAMETER
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="SpawnAt"></param>
    /// <returns></returns>
    public static ItemRepresentation CreateItemFromID(int ID, Vector3 SpawnAt)
    {
        if (ItemDatabase.itemDatabase.itemList[ID] != null)
        {
            ItemRepresentation go = Instantiate(ObjectFactory.instance.ItemRepresentation, SpawnAt, Quaternion.identity).GetComponent<ItemRepresentation>();
            go.Init(ItemDatabase.itemDatabase.itemList[ID]);
            return go;
        }
        else
        {
            return null;
        }
    }

    public static ItemRepresentation CreateTorch(Vector3 SpawnAt)
    {
        ItemRepresentation go = Instantiate(ObjectFactory.instance.ItemRepresentation, SpawnAt, Quaternion.identity).GetComponent<ItemRepresentation>();
        go.Init(ItemDatabase.itemDatabase.itemList[0]);
        return go;
    }

    public static ItemRepresentation CreatePear(Vector3 SpawnAt)
    {
        ItemRepresentation go = Instantiate(ObjectFactory.instance.ItemRepresentation, SpawnAt, Quaternion.identity).GetComponent<ItemRepresentation>();
        go.Init(ItemDatabase.itemDatabase.itemList[1]);
        return go;
    }

    public static ItemRepresentation CreateAxe(Vector3 SpawnAt)
    {
        ItemRepresentation go = Instantiate(ObjectFactory.instance.ItemRepresentation, SpawnAt, Quaternion.identity).GetComponent<ItemRepresentation>();
        go.Init(ItemDatabase.itemDatabase.itemList[2]);
        return go;
    }
}
