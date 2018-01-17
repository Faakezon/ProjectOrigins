using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerRepresentation : MonoBehaviour {

    public static DisplayPlayerRepresentation instance = null;

    private Transform playerRepresentationHolder;
    private GameObject representation;

    private float horizontalSpeed = 5.0f;

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

       
        playerRepresentationHolder = transform.GetChild(0);
    }

    // Use this for initialization
    public void SetupPlayerRepresentation()
    {
        if (representation != null)
            Clear();

        representation = Instantiate(Resources.Load("PlayerRepresentation"), playerRepresentationHolder.position, Quaternion.identity, playerRepresentationHolder.transform) as GameObject;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (representation != null)
        {
            float h = horizontalSpeed * -Input.GetAxis("RightStickHorizontal");
            representation.transform.Rotate(0, h, 0);
        }
    }

    public void Clear()
    {
        Destroy(representation);
    }
}
