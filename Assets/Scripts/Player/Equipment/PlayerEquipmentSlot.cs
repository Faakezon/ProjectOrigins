using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipmentSlot : MonoBehaviour {

    private GameObject equipmentPlaceHolder;

    private bool occupied;
    public bool Occupied
    {
        get { return occupied; }
        set { occupied = value; }
    }

    private InventoryItem item;
    public InventoryItem Item
    {
        get { return item; }
        set
        {
            if (value != null && value.itemName != "")
            {
                value.slotname = "";
                //Debug.Log("Item SLOT: " + value.Slot);
                item = value;
                Image.sprite = item.itemIcon;
                occupied = true;
                //Instantiate(item.itemObject, equipmentPlaceHolder.transform.position, equipmentPlaceHolder.transform.rotation, equipmentPlaceHolder.transform);

            }
            //else
            //{
            //    item = null;
            //    Image.sprite = null;
            //    occupied = false;
            //}

        }
    }

    private Image image;
    public Image Image
    {
        get { return image; }
    }


    public void Wake(string message)
    {
        //Debug.Log(message + " " + gameObject.name);
        image = transform.GetChild(1).GetComponent<Image>();
        InitEquipmentSlot();
        Reset();
    }
    

    private void InitEquipmentSlot()
    {
        switch (gameObject.name)
        {
            case "EquipmentSlotHead":
                //Debug.Log(gameObject.name);
                break;
            case "EquipmentSlotTorso":
                //Debug.Log(gameObject.name);
                break;
            case "EquipmentSlotPants":
                //Debug.Log(gameObject.name);
                break;
            case "EquipmentSlotBoots":
                //Debug.Log(gameObject.name);
                break;

            case "EquipmentSlotWeapon":
                //Debug.Log(gameObject.name);
                //equipmentPlaceHolder = GameObject.FindGameObjectWithTag("WeaponHand"); //Instantiate weapon on this position as a child.
                break;

        }
    }


    public void OnMouseEnter()
    {
        //Debug.Log("Reticle Over: " + this.name);
        ItemInfo.OpenInfo(item, this);

    }
    public void OnMouseExit()
    {
        //Debug.Log("Reticle Exit: " + this.name);
        ItemInfo.CloseInfo();
    }
    public void OnMouseOver()
    {
        //Debug.Log("Reticle Hover: " + this.name);
        //ItemInfo.UpdateInfo(item, null, this);

    }
    // Custom reticle events
    void OnReticleEnter()
    {
        Invoke("OnMouseEnter", 0);
    }
    void OnReticleExit()
    {
        Invoke("OnMouseExit", 0);
    }
    void OnReticleHover()
    {
        Invoke("OnMouseOver", 0);
    }


    public void Reset()
    {
        image.sprite = null;
        occupied = false;
        item = null;

        //if(equipmentPlaceHolder.transform.GetChild(0).gameObject != null)
        //{
        //    Destroy(equipmentPlaceHolder.transform.GetChild(0).gameObject);
        //}
    }
}
