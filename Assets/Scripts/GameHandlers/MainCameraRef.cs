using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class MainCameraRef : MonoBehaviour {

    public static MainCameraRef instance = null;

    private Animator animator;
    private PostProcessingBehaviour ppb;
    


    public PostProcessingProfile[] ppbProfile;
    private ReticuleBehaviour reticuleBehaviour;

    // Use this for initialization
    void Awake () {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        animator = instance.GetComponent<Animator>();
        ppb = instance.GetComponent<PostProcessingBehaviour>();
        reticuleBehaviour = instance.GetComponentInChildren<ReticuleBehaviour>();
    }

    public void SetCameraStaticPos()
    {
        switch (InputHandler.currentState)  
        {
            case InputHandler.GameState.MainMenu:
                break;
            case InputHandler.GameState.CharacterScreen:
                break;
            case InputHandler.GameState.PlanetExplorer:
                animator.SetBool("CharacterScreen", false);
                this.transform.position = new Vector3(0, 0, -10);
                this.transform.LookAt(new Vector3(0, 0, 0));
                //ppb.profile = ppbProfile[1];
                break;
            case InputHandler.GameState.Planet:
                
                break;
            default:
                break;
        }

    }

    public void SetReticuleActive(bool active)
    {
        Debug.Log("Reticule Active: " + active);
        reticuleBehaviour.GetComponent<RectTransform>().gameObject.SetActive(active);
    }

}
