using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRepresentation : MonoBehaviour {

    private InventoryItem item;

    public InventoryItem Item {
        get { return item; }
    }

    private bool mouseOver = false;
    private SpriteRenderer spriteRenderer;


    public void Init(InventoryItem item)
    {
        if(item != null)
        {
            this.item = item;
            GameObject.Instantiate(item.itemObject, this.transform.position, Quaternion.identity, this.transform);
        }
    }

    private void OnGUI()
    {
        //JUST FOR DEBUGGING PURPOSES
        
        if (mouseOver)
        {
            //ItemInfo.OpenInfo(item);
        }
        else
        {
            ItemInfo.CloseInfo();
        }
        
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
