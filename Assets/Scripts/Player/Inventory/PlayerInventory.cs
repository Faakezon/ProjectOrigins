using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance = null;

    public List<PlayerInventorySlot> inventoryWeaponSlots = new List<PlayerInventorySlot>();
    public List<PlayerInventorySlot> inventoryHeadSlots = new List<PlayerInventorySlot>();
    public List<PlayerInventorySlot> inventoryTorsoSlots = new List<PlayerInventorySlot>();
    public List<PlayerInventorySlot> inventoryBootsSlots = new List<PlayerInventorySlot>();


    private List<InventoryItem> itemList = new List<InventoryItem>();

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



    /// <summary>
    /// DEBUGGING PURPOSES ONLY
    /// </summary>
    private void Update()
    {
        if (SceneMng.instance.IsSceneActive("InventoryScreen"))
            {
            //DEBUG UPDATE, TO BE REMOVED
            //GET ITEMS
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AddItem(ObjectFactory.CreateWeaponItemInInventory());
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                AddItem(ObjectFactory.CreateBetterWeaponItemInInventory());
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                AddItem(ObjectFactory.CreateHeadItemInInventory());
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddItem(ObjectFactory.CreateTorsoItemInInventory());
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                AddItem(ObjectFactory.CreateBootsItemInInventory());
            }



            //EQUIP ITEMS
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (inventoryWeaponSlots[0].Item != null)
                {
                    UseItem(inventoryWeaponSlots[0], inventoryWeaponSlots[0].Item);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (inventoryHeadSlots[0].Item != null)
                {
                    UseItem(inventoryHeadSlots[0], inventoryHeadSlots[0].Item);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (inventoryTorsoSlots[0].Item != null)
                {
                    UseItem(inventoryTorsoSlots[0], inventoryTorsoSlots[0].Item);
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (inventoryBootsSlots[0].Item != null)
                {
                    UseItem(inventoryBootsSlots[0], inventoryBootsSlots[0].Item);
                }
            }

            //UNEQUIP ITEMS
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PlayerEquipment.instance.UnEquipWeapon();
            }

            //CLEAR ALL ITEMS
            if (Input.GetKeyDown(KeyCode.M))
            {
                foreach (PlayerInventorySlot slot in inventoryWeaponSlots)
                {
                    if (slot.Item != null)
                    {
                        RemoveItem(slot, slot.Item);
                    }
                }
            }
        }
    }

    public bool InstantiateInventory()
    {
        ClearLists();

        if (SceneMng.instance.GetCurrentScenes()[1].name == "InventoryScreen")
        {
            Debug.Log("Instantiate Inventory");
            if (
                CreateNewInventoryWeaponSlots() &&
                CreateNewInventoryHeadSlots() &&
                CreateNewInventoryTorsoSlots() &&
                CreateNewInventoryBootsSlots()
                )
            {
                Debug.Log("All Inventory Slots Created");
                //Debug.Log("AMOUNT OF WEAPON SLOTS: " + inventoryWeaponSlots.Count);
                return true;
            }
        }
        return false;
    }

    private bool CreateNewInventoryWeaponSlots()
    {    
        foreach (GameObject slot in GameObject.FindGameObjectsWithTag("ItemSlotWeapon"))
        {
            //Debug.Log(slot);
            inventoryWeaponSlots.Add(slot.GetComponent<PlayerInventorySlot>());
        }

        if (inventoryWeaponSlots != null){
            inventoryWeaponSlots = inventoryWeaponSlots.OrderBy(w => w.name).ToList();
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private bool CreateNewInventoryHeadSlots()
    {
        foreach (GameObject slot in GameObject.FindGameObjectsWithTag("ItemSlotHead"))
        {
            inventoryHeadSlots.Add(slot.GetComponent<PlayerInventorySlot>());
        }
        if (inventoryHeadSlots != null){
            inventoryHeadSlots = inventoryHeadSlots.OrderBy(w => w.name).ToList();
            return true;
        }
        else{
            return false;
        }
    }
    private bool CreateNewInventoryTorsoSlots()
    {
        foreach (GameObject slot in GameObject.FindGameObjectsWithTag("ItemSlotTorso"))
        {
            inventoryTorsoSlots.Add(slot.GetComponent<PlayerInventorySlot>());
        }
        if (inventoryTorsoSlots != null){
            inventoryTorsoSlots = inventoryTorsoSlots.OrderBy(w => w.name).ToList();
            return true;
        }
        else{
            return false;
        }
    }
    private bool CreateNewInventoryBootsSlots()
    {
        foreach (GameObject slot in GameObject.FindGameObjectsWithTag("ItemSlotBoots"))
        {
            inventoryBootsSlots.Add(slot.GetComponent<PlayerInventorySlot>());
        }
        if (inventoryBootsSlots != null){
            inventoryBootsSlots = inventoryBootsSlots.OrderBy(w => w.name).ToList();
            return true;
        }
        else{
            return false;
        }

    }


    /// <summary>
    /// Check Input if we should use the item.
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="item"></param>
    public void CheckInput(PlayerInventorySlot slot, InventoryItem item)
    {
        if (item != null)
        {
            if (Input.GetButtonDown("XboxA")) 
            {
                //USE ITEM / EQUIP ITEM 
                if (ItemInfo.GetItemInfoActive())
                {
                    ItemInfo.CloseInfo();
                }
                UseItem(slot, item);
            }
            if (Input.GetButtonDown("XboxX")) 
            {
                //USE ITEM / EQUIP ITEM 
                if (ItemInfo.GetItemInfoActive())
                {
                    ItemInfo.CloseInfo();
                }
                RemoveItem(slot, item); //SHOULD BE CHANGED TO DISMANTLE METHOD AND GET SOMETHING FOR DISMANTLING ITEM.
            }
        }
    }


    public bool SaveInventoryData()
    {
        PlayerData pData = new PlayerData();
        pData.inventoryList = itemList;
        pData.newlyCreatedCharacter = false; //Save that the player have gotten its beginner equipment
        DataController.instance.SaveInventoryData(pData);

        if (pData.inventoryList == DataController.instance.GetPlayerData().inventoryList)
            return true;
        else
        {
            return false;
        }
    }
    public bool LoadInventoryData(PlayerData savedData)
    {
        if (savedData == null)
            return false;

        Debug.Log("Load Inventory Data");
        PlayerData loadedData = savedData;

        foreach (InventoryItem item in loadedData.inventoryList)
        {
            AddItem(item);
        }
        return true;
    }

    public void ClearLists()
    {
        itemList.Clear();
        inventoryWeaponSlots.Clear();
        inventoryHeadSlots.Clear();
        inventoryTorsoSlots.Clear();
        inventoryBootsSlots.Clear();
    }

    public bool FreeSlotInInventory()
    {
        for (int i = 0; i < inventoryWeaponSlots.Count; i++) //Check all slots
        {
            if (!inventoryWeaponSlots[i].Occupied) //Not Occupied...
            {
                return true;
            }
        }
        return false;
    }

    public bool AddItem(InventoryItem item)
    {
        if(item != null && item.slotname == "") 
        {
            switch (item.itemType)
            {
                case InventoryItem.ItemType.Weapon:
                    AddWeapon(item);
                    break;
                case InventoryItem.ItemType.Armor:
                    switch (item.armorType)
                    {
                        case InventoryItem.ArmorType.Head:
                            AddHead(item);
                            break;
                        case InventoryItem.ArmorType.Torso:
                            AddTorso(item);
                            break;
                        case InventoryItem.ArmorType.Boots:
                            AddBoots(item);
                            break;
                        default:
                            break;
                    }
                    break;


                case InventoryItem.ItemType.Consumable:
                    break;
                case InventoryItem.ItemType.Material:
                    break;
                case InventoryItem.ItemType.QuestItem:
                    break;
                default:
                    break;
            }
        }//IF THE ITEM IS FOUND
        else if (item != null && item.slotname != "") 
        {
            Debug.Log("Loaded Inventory Added");
            switch (item.itemType)
            {
                case InventoryItem.ItemType.Weapon:
                    AddWeapon(item, item.slotname);
                    break;
                case InventoryItem.ItemType.Armor:
                    switch (item.armorType)
                    {
                        case InventoryItem.ArmorType.Head:
                            AddHead(item, item.slotname);
                            break;
                        case InventoryItem.ArmorType.Torso:
                            AddTorso(item, item.slotname);
                            break;
                        case InventoryItem.ArmorType.Boots:
                            AddBoots(item, item.slotname);
                            break;
                        default:
                            break;
                    }
                    break;


                case InventoryItem.ItemType.Consumable:
                    break;
                case InventoryItem.ItemType.Material:
                    break;
                case InventoryItem.ItemType.QuestItem:
                    break;
                default:
                    break;
            }
        }//IF ITEM IS LOADED

        return false;
    }
    //public bool AddItem(InventoryItem item, PlayerInventorySlot slot)
    //{
    //    switch (item.itemType)
    //    {
    //        case InventoryItem.ItemType.Weapon:
    //            AddWeapon(item);
    //            break;
    //        case InventoryItem.ItemType.Armor:
    //            switch (item.armorType)
    //            {
    //                case InventoryItem.ArmorType.Head:
    //                    AddHead(item);
    //                    break;
    //                case InventoryItem.ArmorType.Torso:
    //                    AddTorso(item);
    //                    break;
    //                case InventoryItem.ArmorType.Boots:
    //                    AddBoots(item);
    //                    break;
    //                default:
    //                    break;
    //            }
    //            break;


    //        case InventoryItem.ItemType.Consumable:
    //            break;
    //        case InventoryItem.ItemType.Material:
    //            break;
    //        case InventoryItem.ItemType.QuestItem:
    //            break;
    //        default:
    //            break;
    //    }
    //    return false;
    //}

    private bool AddWeapon(InventoryItem item)
    {
        for (int i = 0; i < inventoryWeaponSlots.Count; i++) //Check all slots
        {
            if (!inventoryWeaponSlots[i].Occupied) //Not Occupied...
            {
                inventoryWeaponSlots[i].Item = item;
                inventoryWeaponSlots[i].Occupied = true;
                itemList.Add(item);
                Debug.Log("Item Added: " + item.itemName);

                return true;
            }
            else
            {
                continue;
            }
        }
        return false;
    }
    private bool AddWeapon(InventoryItem item, string slotName)
    {
        PlayerInventorySlot theSlot = null;
        foreach (PlayerInventorySlot slot in inventoryWeaponSlots)
        {
            if (slot.name == slotName)
            {
                theSlot = slot;
                break;
            }
        }
        Debug.Log("The Slot: " + theSlot);
        if (theSlot == null)
            return false;

        Debug.Log("The slot: " + theSlot.name + ", Item SlotName: " + item.slotname);

        theSlot.Item = item;
        theSlot.Occupied = true;
        itemList.Add(item);
        Debug.Log("Item Added: " + item.itemName);
        return true;
    }
    private bool AddHead(InventoryItem item)
    {
        for (int i = 0; i < inventoryHeadSlots.Count; i++) //Check all slots
        {
            if (!inventoryHeadSlots[i].Occupied) //Not Occupied...
            {
                inventoryHeadSlots[i].Item = item;
                inventoryHeadSlots[i].Occupied = true;
                itemList.Add(item);
                Debug.Log("Item Added: " + item.itemName);

                return true;
            }
            else
            {
                continue;
            }
        }
        return false;
    }
    private bool AddHead(InventoryItem item, string slotName)
    {
        PlayerInventorySlot theSlot = null;
        foreach (PlayerInventorySlot slot in inventoryHeadSlots)
        {
            if (slot.name == slotName)
            {
                theSlot = slot;
                break;
            }
        }
        Debug.Log("The Slot: " + theSlot);
        if (theSlot == null)
            return false;

        theSlot.Item = item;
        theSlot.Occupied = true;
        itemList.Add(item);
        Debug.Log("Item Added: " + item.itemName);
        return true;
    }
    private bool AddTorso(InventoryItem item)
    {
        for (int i = 0; i < inventoryTorsoSlots.Count; i++) //Check all slots
        {
            if (!inventoryTorsoSlots[i].Occupied) //Not Occupied...
            {
                inventoryTorsoSlots[i].Item = item;
                inventoryTorsoSlots[i].Occupied = true;
                itemList.Add(item);
                Debug.Log("Item Added: " + item.itemName);

                return true;
            }
            else
            {
                continue;
            }
        }
        return false;
    }
    private bool AddTorso(InventoryItem item, string slotName)
    {
        PlayerInventorySlot theSlot = null;
        foreach (PlayerInventorySlot slot in inventoryTorsoSlots)
        {
            if (slot.name == slotName)
            {
                theSlot = slot;
                break;
            }
        }
        Debug.Log("The Slot: " + theSlot);
        if (theSlot == null)
            return false;

        theSlot.Item = item;
        theSlot.Occupied = true;
        itemList.Add(item);
        Debug.Log("Item Added: " + item.itemName);
        return true;
    }
    private bool AddBoots(InventoryItem item)
    {
        for (int i = 0; i < inventoryBootsSlots.Count; i++) //Check all slots
        {
            if (!inventoryBootsSlots[i].Occupied) //Not Occupied...
            {
                inventoryBootsSlots[i].Item = item;
                inventoryBootsSlots[i].Occupied = true;
                itemList.Add(item);
                Debug.Log("Item Added: " + item.itemName);

                return true;
            }
            else
            {
                continue;
            }
        }
        return false;
    }
    private bool AddBoots(InventoryItem item, string slotName)
    {
        PlayerInventorySlot theSlot = null;
        foreach (PlayerInventorySlot slot in inventoryBootsSlots)
        {
            if (slot.name == slotName)
            {
                theSlot = slot;
                break;
            }
        }
        Debug.Log("The Slot: " + theSlot);
        if (theSlot == null)
            return false;

        theSlot.Item = item;
        theSlot.Occupied = true;
        itemList.Add(item);
        Debug.Log("Item Added: " + item.itemName);
        return true;
    }



    public void UseItem(PlayerInventorySlot slot, InventoryItem item)
    {
        
        switch (item.itemType)
        {
            case InventoryItem.ItemType.Weapon: //EQUIP WEAPON
                EquipWeapon(item, slot);
                break;
            case InventoryItem.ItemType.Armor: //EQUIP ARMOR
                switch (item.armorType)
                {
                    case InventoryItem.ArmorType.Head:
                        Debug.Log("Head Equipped");
                        EquipHead(item, slot);
                        break;
                    case InventoryItem.ArmorType.Torso:
                        Debug.Log("Torso Equipped");
                        EquipTorso(item, slot);
                        break;
                    case InventoryItem.ArmorType.Boots:
                        Debug.Log("Boots Equipped");
                        EquipBoots(item, slot);
                        break;
                    default:
                        break;
                }
                //RemoveItem(slot, item);
                break;
            default:
                break;
        }

        
    }

    public void RemoveItem(PlayerInventorySlot slot, InventoryItem item)
    {
        itemList.Remove(item);
        slot.Reset();
    }

    /// <summary>
    /// EQUIP
    /// </summary>
    ///First are only for beginner equipment
    private void EquipWeapon(InventoryItem item)
    {
        PlayerEquipment.instance.EquipWeapon(item);
    }
    private void EquipHead(InventoryItem item)
    {
        PlayerEquipment.instance.EquipArmor(item);
    }
    private void EquipTorso(InventoryItem item)
    {
        PlayerEquipment.instance.EquipArmor(item);
    }
    private void EquipBoots(InventoryItem item)
    {
        PlayerEquipment.instance.EquipArmor(item);
    }

    /// <summary>
    /// These are used regularly.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="slot"></param>
    private void EquipWeapon(InventoryItem item, PlayerInventorySlot slot)
    {
        if (PlayerEquipment.instance.Weapon != null)
        {
            if (PlayerEquipment.instance.Weapon.Occupied) //IF WE ALREADY HAVE A WEAPON EQUIPPED!
            {
                //Temporarily hold the item that is equipped.
                InventoryItem tempItem = PlayerEquipment.instance.Weapon.Item;
                //Reset the slot that held equipped item.
                PlayerEquipment.instance.Weapon.Reset();
                //Equip the new item.
                PlayerEquipment.instance.EquipWeapon(item);
                //Remove the newly equipped item from the inventory itemlist.
                itemList.Remove(item);
                //Place the former equipped item in the slot that was freed up.
                slot.Item = tempItem;
                //Add that item to the inventory itemlist.
                itemList.Add(tempItem);
                return; //All is done. No need to RemoveItem() since we just switched places with it.
            }
        }
        else
        {
            Debug.Log("Weapon Slot Could Not Be Found");
        }
    }
    private void EquipHead(InventoryItem item, PlayerInventorySlot slot)
    {
        if (PlayerEquipment.instance.Head != null)
        {
            if (PlayerEquipment.instance.Head.Occupied) //IF WE ALREADY HAVE A WEAPON EQUIPPED!
            {
                //Temporarily hold the item that is equipped.
                InventoryItem tempItem = PlayerEquipment.instance.Head.Item;
                //Reset the slot that held equipped item.
                PlayerEquipment.instance.Head.Reset();
                //Equip the new item.
                PlayerEquipment.instance.EquipArmor(item);
                //Remove the newly equipped item from the inventory itemlist.
                itemList.Remove(item);
                //Place the former equipped item in the slot that was freed up.
                slot.Item = tempItem;
                //Add that item to the inventory itemlist.
                itemList.Add(tempItem);
                return; //All is done. No need to RemoveItem() since we just switched places with it.
            }
        }
        else
        {
            Debug.Log("Head Slot Could Not Be Found");
        }
    }
    private void EquipTorso(InventoryItem item, PlayerInventorySlot slot)
    {
        if (PlayerEquipment.instance.Torso != null)
        {
            if (PlayerEquipment.instance.Torso.Occupied) //IF WE ALREADY HAVE A WEAPON EQUIPPED!
            {
                //Temporarily hold the item that is equipped.
                InventoryItem tempItem = PlayerEquipment.instance.Torso.Item;
                //Reset the slot that held equipped item.
                PlayerEquipment.instance.Torso.Reset();
                //Equip the new item.
                PlayerEquipment.instance.EquipArmor(item);
                //Remove the newly equipped item from the inventory itemlist.
                itemList.Remove(item);
                //Place the former equipped item in the slot that was freed up.
                slot.Item = tempItem;
                //Add that item to the inventory itemlist.
                itemList.Add(tempItem);
                return; //All is done. No need to RemoveItem() since we just switched places with it.
            }
        }
        else
        {
            Debug.Log("Torso Slot Could Not Be Found");
        }
    }
    private void EquipBoots(InventoryItem item, PlayerInventorySlot slot)
    {
        if (PlayerEquipment.instance.Boots != null)
        {
            if (PlayerEquipment.instance.Boots.Occupied) //IF WE ALREADY HAVE A WEAPON EQUIPPED!
            {
                //Temporarily hold the item that is equipped.
                InventoryItem tempItem = PlayerEquipment.instance.Boots.Item;
                //Reset the slot that held equipped item.
                PlayerEquipment.instance.Boots.Reset();
                //Equip the new item.
                PlayerEquipment.instance.EquipArmor(item);
                //Remove the newly equipped item from the inventory itemlist.
                itemList.Remove(item);
                //Place the former equipped item in the slot that was freed up.
                slot.Item = tempItem;
                //Add that item to the inventory itemlist.
                itemList.Add(tempItem);
                return; //All is done. No need to RemoveItem() since we just switched places with it.
            }
        }
        else
        {
            Debug.Log("Boots Slot Could Not Be Found");
        }
    }

}

