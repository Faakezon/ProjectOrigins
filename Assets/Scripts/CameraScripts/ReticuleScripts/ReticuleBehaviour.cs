using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReticuleBehaviour : MonoBehaviour {

    public static ReticuleBehaviour instance = null;

    private Transform reticle;
    private float reticuleSpeed = 10.0f;
    private GameObject hitObject = null;

    public Vector2 startPos;

    public Transform Reticle
    {
        get
        {
            return reticle;
        }

    }

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
        reticle = this.transform;
        startPos = this.transform.position;
    }


    void Update () {
        ReticuleMovement();
        ReticuleHitDetection();
        
    }


    void ReticuleMovement()
    {
        Vector3 pos = Camera.main.ScreenToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToScreenPoint(pos);

        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal") * reticuleSpeed, Input.GetAxis("Vertical") * reticuleSpeed, 0);
        transform.Translate(movementVector);
    }

/// <summary>
/// Works for both overlay UI elements and world positioned elements. 
/// </summary>
    void ReticuleHitDetection()
    {
        // Raycast variables
        RaycastHit hit;
        Vector3 reticuleWorldPos = Camera.main.ScreenToWorldPoint(reticle.position);
        Debug.Log(reticuleWorldPos);
        if (!SceneHandler.instance.IsSceneActive("InventoryScreen"))
        {
            // Raycast SCREENPOS
            if (Physics.Raycast(Camera.main.transform.position, reticle.position - Camera.main.transform.position, out hit))
            {

                if (hit.transform.gameObject != null)
                {
                    hitObject = hit.transform.gameObject;
                    hitObject.SendMessage("OnReticleEnter", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleEnter"

                }
                else
                {
                    hitObject.SendMessage("OnReticleHover", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleHover"
                }

            }else         
            // Raycast WORLDPOS
            if (Physics.Raycast(Camera.main.transform.position, reticuleWorldPos - Camera.main.transform.position, out hit))
            {
                Debug.DrawRay(Camera.main.transform.position, reticuleWorldPos - Camera.main.transform.position);
                if (hit.transform.gameObject != null)
                {
                    hitObject = hit.transform.gameObject;
                    hitObject.SendMessage("OnReticleEnter", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleEnter"

                }
                else
                {
                    //hitObject.SendMessage("OnReticleHover", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleHover"
                }

            }
            else
            {
                if (hitObject != null)
                {
                    hitObject.SendMessage("OnReticleExit", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleExit"
                }
                hitObject = null;
            }
        }
    }


    

}







