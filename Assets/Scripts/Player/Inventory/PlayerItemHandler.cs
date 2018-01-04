using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour {

    private static bool _active = true;

    private static Timer _timer;

    public static void SetPickupCollider(bool active)
    {
        _active = active;
        if(_active == false)
            PickupPause(500);
    }

    private static void PickupPause(float time)
    {
        _timer = new Timer(time);
        _timer.Elapsed += new ElapsedEventHandler(onTimer);
        _timer.AutoReset = true;
        _timer.Start();
    }
    private static void onTimer(object source, ElapsedEventArgs e)
    {
        SetPickupCollider(true);
        _timer.Close();
    }

  

    private void OnTriggerEnter(Collider other)
    {
        if(_active)
            Pickup(other);
    }




    private void Pickup(Collider other)
    {
        if (other.tag == "ItemRepresentation")
        {
            ItemRepresentation tempItemRep = other.GetComponent<ItemRepresentation>();

            ItemInfo.CloseInfo();

            //if (PlayerBehaviour.Player._PlayerInventory.AddItem(tempItemRep.Item))
            //{
            //    //Debug.Log("Returned True and now Destroy");
            //    Destroy(other.gameObject);
            //}
            //else
            //{
            //    other.GetComponent<Rigidbody>().AddForce(PlayerBehaviour.Player.transform.forward * 5, ForceMode.Impulse);
            //}
        }
    }
}
