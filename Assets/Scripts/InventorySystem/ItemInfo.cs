using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour {

    /// <summary>
    /// Triggered by each slot to display information about the item in the slot.
    /// </summary>


    private static GameObject itemInfo;

    private static Image itemIcon;
    private static Text itemName;
    private static Text itemDesc;
    private static Text itemType;

    private static RectTransform aBtn;
    private static RectTransform xBtn;

    private void Start()
    {
        itemInfo = transform.GetChild(0).gameObject;

        itemIcon = itemInfo.transform.Find("ItemIcon").GetComponent<Image>();
        itemName = itemInfo.transform.Find("ItemName").GetComponent<Text>();
        itemDesc = itemInfo.transform.Find("BackgroundImageDesc").transform.Find("ItemDescription").GetComponent<Text>();
        itemType = itemInfo.transform.Find("ItemType").GetComponent<Text>();

        aBtn = itemInfo.transform.Find("A-Btn").GetComponent<RectTransform>();
        xBtn = itemInfo.transform.Find("X-Btn").GetComponent<RectTransform>();

        itemInfo.SetActive(false);
    }

    public static void OpenInfo(InventoryItem item, PlayerInventorySlot inventory)
    {
        if (item == null) return;

        itemInfo.SetActive(true);

        itemIcon.sprite = item.itemIcon;
        itemName.text = item.itemName;
        itemDesc.text = item.itemDescription;
        itemType.text = item.itemType.ToString();

        //Activate Buttons - Can dismantle and equip these items.
        aBtn.gameObject.SetActive(true);
        xBtn.gameObject.SetActive(true);

        //USE A STATIC POSITION INSTEAD IF INVENTORY OR EQUIPMENT IS OPEN
        InventoryPos(inventory);

    }
    public static void OpenInfo(InventoryItem item, PlayerEquipmentSlot equipment)
    {
        if (item == null) return;

        itemInfo.SetActive(true);

        itemIcon.sprite = item.itemIcon;
        itemName.text = item.itemName;
        itemDesc.text = item.itemDescription;
        itemType.text = item.itemType.ToString();

        //Deactivate Buttons - Cannot dismantle or unequip equipped weapons. Need one in every equipslot.
        aBtn.gameObject.SetActive(false);
        xBtn.gameObject.SetActive(false);

        //USE A STATIC POSITION INSTEAD IF INVENTORY OR EQUIPMENT IS OPEN
        EquipmentPos(equipment);


    }

    public static void UpdateInfo(InventoryItem item, PlayerInventorySlot inventory, PlayerEquipmentSlot equipment)
    {
        if (item == null) return;

        itemIcon.sprite = item.itemIcon;
        itemName.text = item.itemName;
        itemDesc.text = item.itemDescription;
        itemType.text = item.itemType.ToString();

        if (inventory != null)
            InventoryPos(inventory);

        if (equipment != null)
            EquipmentPos(equipment);
    }

    public static void CloseInfo()
    {
        itemIcon.sprite = null;
        itemName.text = null;
        itemDesc.text = null;
        itemType.text = null;
        if(itemInfo)
            itemInfo.SetActive(false);
    }

    public static bool GetItemInfoActive()
    {
        if (itemInfo.activeSelf) { return true; }
        else
        {
            return false;
        }
    }


    private static void InventoryPos(PlayerInventorySlot inventory)
    {
        Vector3 vector = Camera.main.WorldToScreenPoint(CameraPointer.instance.reticlePosition.position);

        if (vector.x < Screen.width / 2) //LEFT SIDE OF THE SCREEN
        {
            itemInfo.transform.localPosition = new Vector3(
            inventory.transform.position.x + (itemInfo.GetComponent<RectTransform>().rect.width / 2),
            inventory.transform.position.y + (itemInfo.GetComponent<RectTransform>().rect.height / 2),
            0);

            itemInfo.transform.localPosition += CameraPointer.instance.reticlePosition.localPosition * 10;
        }
        else //RIGHT SIDE OF THE SCREEN
        {
            itemInfo.transform.localPosition = new Vector3(
                inventory.transform.position.x - (itemInfo.GetComponent<RectTransform>().rect.width / 2),
                inventory.transform.position.y + (itemInfo.GetComponent<RectTransform>().rect.height / 2),
                0);

            itemInfo.transform.localPosition += CameraPointer.instance.reticlePosition.localPosition * 10;
        }
    }

    private static void EquipmentPos(PlayerEquipmentSlot equipment)
    {
        Vector3 vector = Camera.main.WorldToScreenPoint(CameraPointer.instance.reticlePosition.position);

        if (vector.x < Screen.width / 2) //LEFT SIDE OF THE SCREEN
        {
            itemInfo.transform.localPosition = new Vector3(
                equipment.transform.position.x + (itemInfo.GetComponent<RectTransform>().rect.width / 2),
                equipment.transform.position.y + (itemInfo.GetComponent<RectTransform>().rect.height / 2),
                0);
            itemInfo.transform.localPosition += CameraPointer.instance.reticlePosition.localPosition * 10;
        }
        else //RIGHT SIDE OF THE SCREEN
        {
            itemInfo.transform.localPosition = new Vector3(
                equipment.transform.position.x - (itemInfo.GetComponent<RectTransform>().rect.width / 2),
                equipment.transform.position.y + (itemInfo.GetComponent<RectTransform>().rect.height / 2),
                0);
            itemInfo.transform.localPosition += CameraPointer.instance.reticlePosition.localPosition * 10;
        }
    }


}
