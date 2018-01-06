using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class MainCameraRef : MonoBehaviour {

    public static MainCameraRef instance = null;

    private Animator animator;
    private PostProcessingBehaviour ppb;

    private Kino.AnalogGlitch glitch;

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
        glitch = instance.GetComponent<Kino.AnalogGlitch>();
    }

    public void SetCameraStaticPos()
    {
        switch (InputHandler.currentState)  
        {
            case InputHandler.GameState.MainMenu:
                ResizeGlitchEffect();
                break;
            case InputHandler.GameState.CharacterScreen:
                StopGlitchEffects();
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
    void Update()
    {
        if (InputHandler.currentState == InputHandler.GameState.PlanetExplorer || InputHandler.currentState == InputHandler.GameState.Planet)
        {
            if(InputHandler.currentState == InputHandler.GameState.PlanetExplorer)
                glitch.scanLineJitter = 0.2f;
            else
            {
                glitch.scanLineJitter = 0.0f;
            }

            if (glitch.colorDrift > 0)
            {
                glitch.colorDrift -= (1.0f * Time.deltaTime);
            }
        }
    }
    public void SetReticuleActive(bool active)
    {
        Debug.Log("Reticule Active: " + active);
        reticuleBehaviour.GetComponent<RectTransform>().gameObject.SetActive(active);
    }

    private void ResizeGlitchEffect()
    {
        if(glitch.scanLineJitter > 0.3)
        {
            glitch.scanLineJitter -= 0.3f * Time.deltaTime;
        }
        else
        {
            glitch.scanLineJitter = 0.3f;
        }
        if(glitch.colorDrift > 0.05)
        {
            glitch.colorDrift -= 0.3f * Time.deltaTime;
        }
        else
        {
            glitch.colorDrift = 0.05f;
        }
    }
    private void StopGlitchEffects()
    {
        if (glitch.scanLineJitter > 0)
        {
            glitch.scanLineJitter -= (0.4f * Time.deltaTime);
        }
        if (glitch.colorDrift > 0)
        {
            glitch.colorDrift -= (0.4f * Time.deltaTime);
        }
    }

    public void GlitchAttack()
    {
        Debug.Log("Derp");
        glitch.colorDrift = 1.0f;
        glitch.colorDrift = 0.5f;
        
    }

}
