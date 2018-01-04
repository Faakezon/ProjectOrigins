using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    public GameObject itemRep;

    private Vector3 up = new Vector3(0, 10, 0);

	void Update () {
        Debugging();
	}

    private void Debugging()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                ObjectFactory.CreateTorch(hit.point + up);
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                ObjectFactory.CreatePear(hit.point + up);
            }

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                ObjectFactory.CreateAxe(hit.point + up);
            }

        }
    }
}
