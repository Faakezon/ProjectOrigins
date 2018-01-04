using System.Collections.Generic;

[System.Serializable]
public class PlayerData {

    public bool newlyCreatedCharacter = true; //To instantiate beginner weapons and armor.

    public string name;

    public List<InventoryItem> inventoryList = new List<InventoryItem>();

    public InventoryItem weaponData;
    public InventoryItem headData;
    public InventoryItem torsoData;
    public InventoryItem bootsData;

}
