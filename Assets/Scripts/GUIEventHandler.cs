using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIEventHandler : MonoBehaviour {


	public static void GUIEventHandlerUpdate ()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

                ItemInfo.CloseInfo();
        }
        else
        {
            UpdateItemInfo(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject);
        }
    }

    private static void UpdateItemInfo(GameObject gameObj)
    {
        if (gameObj == null) return;
       
        if(gameObj.GetComponent<PlayerInventorySlot>() != null && gameObj.GetComponent<PlayerInventorySlot>().Item != null)
        {
            ItemInfo.UpdateInfo(gameObj.GetComponent<PlayerInventorySlot>().Item, gameObj.GetComponent<PlayerInventorySlot>(), null);
        }else
        if (gameObj.GetComponent<PlayerEquipmentSlot>() != null && gameObj.GetComponent<PlayerEquipmentSlot>().Item != null)
        {
            ItemInfo.UpdateInfo(gameObj.GetComponent<PlayerEquipmentSlot>().Item, null, gameObj.GetComponent<PlayerEquipmentSlot>());
        }
        else if (gameObj.GetComponent<PlayerInventorySlot>() != null && gameObj.GetComponent<PlayerInventorySlot>().Item == null)
        {
            ItemInfo.CloseInfo();
        }
        else if (gameObj.GetComponent<PlayerEquipmentSlot>() != null && gameObj.GetComponent<PlayerEquipmentSlot>().Item == null)
        {
            ItemInfo.CloseInfo();
        }
    }


    public static void SetSelectedGUIObject(GameObject gameObj)
    {
        EventSystem.current.SetSelectedGameObject(gameObj);
    }


    public static GameObject GetSelectedGUIObject()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            return null;
        }
    }


}
