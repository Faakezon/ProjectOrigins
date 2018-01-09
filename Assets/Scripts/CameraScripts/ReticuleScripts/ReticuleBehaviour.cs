using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReticuleBehaviour : MonoBehaviour {

    public static ReticuleBehaviour instance = null;

    private Transform reticle;
    private float reticuleSpeed = 10.0f;

    private GameObject hitObjectScreenPos = null;
    private GameObject hitObjectWorldPos = null;

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
        ReticleHitDetectionUpdate();
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

    void ReticleHitDetectionUpdate()
    {
        ReticuleHitDetection();
        ReticuleHitDetectionWorldPos();
    }

    /// <summary>
    /// Different raycasts for screen positioned elements and world positioned elements. 
    /// </summary>
    void ReticuleHitDetection()
    {
        // Raycast variables
        RaycastHit hit;
        Vector3 reticuleWorldPos = Camera.main.ScreenToWorldPoint(reticle.position);

        // Raycast SCREENPOS
        if (Physics.Raycast(Camera.main.transform.position, reticle.position - Camera.main.transform.position, out hit))
        {

            if (hit.transform.gameObject != null)
            {
                hitObjectScreenPos = hit.transform.gameObject;
                hitObjectScreenPos.SendMessage("OnReticleEnter", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleEnter"

            }
            else
            {
                hitObjectScreenPos.SendMessage("OnReticleHover", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleHover"
            }
        }
        else
        {
            if (hitObjectScreenPos != null)
            {
                hitObjectScreenPos.SendMessage("OnReticleExit", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleExit"
            }
            hitObjectScreenPos = null;
        }
    }
    void ReticuleHitDetectionWorldPos()
    {
        if (!SceneHandler.instance.IsSceneActive("InventoryScreen"))
        {
            Ray ray = Camera.main.ScreenPointToRay(reticle.position);
            RaycastHit hit;

            // Raycast
            if (Physics.Raycast(ray, out hit))
            {
                if (hitObjectWorldPos != hit.transform.gameObject)
                {
                    if (hitObjectWorldPos != null)
                    {
                        hitObjectWorldPos.SendMessage("OnReticleExit"); // Trigger "OnReticleExit"
                    }
                    hitObjectWorldPos = hit.transform.gameObject;
                    hitObjectWorldPos.SendMessage("OnReticleEnter"); // Trigger "OnReticleEnter"
                }
                else
                {
                    hitObjectWorldPos.SendMessage("OnReticleHover"); // Trigger "OnReticleHover"
                }
            }
            else
            {
                if (hitObjectWorldPos != null)
                {
                    hitObjectWorldPos.SendMessage("OnReticleExit"); // Trigger "OnReticleExit"
                }
                hitObjectWorldPos = null;
            }
        }
    }



}







