using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{

    public static PlayerEquipment instance = null;


    //Slots
    private PlayerEquipmentSlot weapon, head, torso, boots;
    private GameObject weaponObj;

    //Properties
    public PlayerEquipmentSlot Weapon { get { return weapon; } set { weapon = value; } }
    public PlayerEquipmentSlot Head { get { return head; } set { head = value; } }
    public PlayerEquipmentSlot Torso { get { return torso; } set { torso = value; } }
    public PlayerEquipmentSlot Boots { get { return boots; } set { boots = value; } }

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
    public bool InstantiateEquipment()
    {
        foreach (PlayerEquipmentSlot slot in FindObjectsOfType<PlayerEquipmentSlot>())
        {
            switch (slot.name)
            {
                case "EquipmentSlotWeapon":
                    weapon = slot;
                    weapon.Wake("WEAPON WAKE");
                    break;
                case "EquipmentSlotHead":
                    head = slot;
                    head.Wake("HEAD WAKE");
                    break;
                case "EquipmentSlotTorso":
                    torso = slot;
                    torso.Wake("TORSO WAKE");
                    break;
                case "EquipmentSlotBoots":
                    boots = slot;
                    boots.Wake("BOOTS WAKE");
                    break;
                default:
                    break;
            }
        }
        return true;

    }
    private void AddBeginnerEquipment()
    {
        Debug.Log("Beginner Equipment Added");
        EquipWeapon(ObjectFactory.CreateWeaponItemInInventory());
        EquipArmor(ObjectFactory.CreateHeadItemInInventory());
        EquipArmor(ObjectFactory.CreateTorsoItemInInventory());
        EquipArmor(ObjectFactory.CreateBootsItemInInventory());
    } // NEEDS TO BE RECONFIGURED IF ITEM DB IS CHANGED!!

    public void CheckInput(PlayerEquipmentSlot slot, InventoryItem item)
    {
        if (item != null)
        {
            if (Input.GetMouseButtonDown(1)) //RightClick
            {
                //Debug.Log("RightClicked on: " + item.itemName + " in your equipment.");
                if (ItemInfo.GetItemInfoActive())
                {
                    ItemInfo.CloseInfo();
                }
                //if (PlayerBehaviour.Player._PlayerInventory.AddItem(item))
                //{
                //    slot.Reset();
                //}
            }

        }
    }


    public void EquipWeapon(InventoryItem item)
    {
        if (item.itemType == InventoryItem.ItemType.Weapon)
        {
            Weapon.Item = item;
        }
    }
    public void EquipArmor(InventoryItem item)
    {
        switch (item.armorType)
        {
            case InventoryItem.ArmorType.Head:
                Debug.Log("Equip Head: " + item.itemName);
                Head.Item = item;
                break;
            case InventoryItem.ArmorType.Torso:
                Debug.Log("Equip Torso: " + item.itemName);
                Torso.Item = item;
                break;
            case InventoryItem.ArmorType.Boots:
                Debug.Log("Equip Boots: " + item.itemName);
                Boots.Item = item;
                break;
            default:
                break;
        }
    }

    public void UnEquipWeapon()
    {
        if(weapon.Item != null && PlayerInventory.instance.FreeSlotInInventory())
        {
            PlayerInventory.instance.AddItem(weapon.Item);
            weapon.GetComponent<PlayerEquipmentSlot>().Reset();
        }
    }


    public bool SaveEquipmentData()
    {
        PlayerData pData = new PlayerData();
        pData.newlyCreatedCharacter = false;
        pData.weaponData = weapon.Item;
        pData.headData = head.Item;
        pData.torsoData = torso.Item;
        pData.bootsData = boots.Item;
        DataController.instance.SaveEquipmentData(pData);
        return true;

    }
    public bool LoadEquipmentData(PlayerData savedData)
    {
        if (savedData != null)
        {
            Debug.Log("LoadEquipmentData");
            PlayerData loadedData = savedData;
            EquipWeapon(loadedData.weaponData);
            EquipArmor(loadedData.headData);
            EquipArmor(loadedData.torsoData);
            EquipArmor(loadedData.bootsData);

            if (loadedData.newlyCreatedCharacter)
                AddBeginnerEquipment();

            return true;
        }
        else
        {
            return false;
        }
    }


}
