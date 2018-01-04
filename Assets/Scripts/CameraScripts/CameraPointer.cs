﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CameraPointer : MonoBehaviour
{
    public static CameraPointer instance = null;

    private GameObject hitObject = null;
    public Transform reticlePosition;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Replace with reticle screen position
        

        // Raycast variables
        RaycastHit hit;
        

        // Raycast
        if (Physics.Raycast(Camera.main.transform.position, reticlePosition.position - Camera.main.transform.position, out hit))
        {
            
            if (hit.transform.gameObject != null)
            {
                hitObject = hit.transform.gameObject;
                //Debug.Log("RAYHIT: " + hitObject.name);
                hitObject.SendMessage("OnReticleEnter", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleEnter"

            }
            else
            {
                hitObject.SendMessage("OnReticleHover", SendMessageOptions.DontRequireReceiver); // Trigger "OnReticleHover"
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