using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySlot : MonoBehaviour {

    private bool occupied;
    public bool Occupied {
        get { return occupied; }
        set { occupied = value; }
    }

    private InventoryItem item;
    public InventoryItem Item {
        get { return item; }
        set {
            if(value != null && value.itemName != "")
            {
                value.slotname = this.name;
                //Debug.Log("Item SLOT: " + value.Slot);
                item = value;
                Image.sprite = item.itemIcon;
                occupied = true;

                if (item.isStackable) {
                    stackableAmountDisplay.enabled = true;
                }
                else
                {
                    stackableAmountDisplay.enabled = false;
                }


            }
            
        } }


    private Image image;
    public Image Image {
        get { return image; } }


    private int stack;
    public int Stack {
        get { return stack; }
        set {
            if (item != null && item.isStackable)
            {
                if(value <= CONSTANTS.MAX_INVENTORY_STACK && value > 0) {
                    stack = value;
                    stackableAmountDisplay.enabled = true;
                    stackableAmountDisplay.text = stack.ToString();
                }
                
            }
            else if(item != null && !item.isStackable)
            {
                stack = 0;
                stackableAmountDisplay.enabled = false;
            }
        }
    }

    private Text stackableAmountDisplay;

    private void Awake()
    {
        stackableAmountDisplay = transform.GetChild(2).GetComponent<Text>();
        image = transform.GetChild(1).GetComponent<Image>();
    }


    public void OnMouseEnter()
    {
        //Debug.Log("Reticle Over: " + this.name);
        ItemInfo.OpenInfo(item, this);
        PlayerInventory.instance.CheckInput(this, Item);
    }
    public void OnMouseExit()
    {
        //Debug.Log("Reticle Exit: " + this.name);
        ItemInfo.CloseInfo();
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
        stack = 0;
        item = null;
    }


}
