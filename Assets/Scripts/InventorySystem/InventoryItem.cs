using UnityEngine;

[System.Serializable]
public class InventoryItem {

    public string itemName;
    public int itemID;

    public string slotname;

    public string itemDescription;

    public Sprite itemIcon;
    public GameObject itemObject;

    public bool isUnique = false;
    public bool isIndestructible = false;
    public bool isQuestItem = false;
    public bool isStackable = false;
    public bool destroyOnUse = false;
    public float encumbranceValue = 0;

    public ItemType itemType;
    public ArmorType armorType;

    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        Material,
        QuestItem
    }
    

    public enum ArmorType
    {
        Head,
        Torso,
        Boots
    }

    //CONSUMEABLE ITEM STATS
    public int HPRecovery;

    public InventoryItem() { }

    public InventoryItem(InventoryItem item)
    {
        itemName = item.itemName;
        itemID = item.itemID;
        slotname = item.slotname;
        itemDescription = item.itemDescription;
        itemIcon = item.itemIcon;
        itemObject = item.itemObject;
        isUnique = item.isUnique;
        isIndestructible = item.isIndestructible;
        isQuestItem = item.isQuestItem;
        isStackable = item.isStackable;
        destroyOnUse = item.destroyOnUse;
        encumbranceValue = item.encumbranceValue;
        itemType = item.itemType;
        armorType = item.armorType;

        HPRecovery = item.HPRecovery;


    }
}
