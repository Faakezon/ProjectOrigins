using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGlitchVFX : MonoBehaviour
{

    public static CameraGlitchVFX instance = null;

    private Kino.AnalogGlitch glitch;

    private bool glitchAttackActive = false;

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
        DontDestroyOnLoad(gameObject);
        glitch = Camera.main.GetComponent<Kino.AnalogGlitch>();
    }

    private void Update()
    {
        switch (GameStateHandler.currentGameState)
        {
            case GameStateHandler.GameState.MainMenu:
                ResizeGlitchEffect();
                break;
            case GameStateHandler.GameState.CharacterScreen:
                ResizeGlitchEffect();
                break;
            case GameStateHandler.GameState.PlanetExplorer:
                CameraGlitchVFX.instance.glitch.scanLineJitter = 0.05f;
                CameraGlitchVFX.instance.glitch.colorDrift = 0.0f;
                break;
            case GameStateHandler.GameState.Planet:
                break;
            default:
                break;
        }

        if (glitchAttackActive)
        {
            if (CameraGlitchVFX.instance.glitch.colorDrift > 0)
            {
                CameraGlitchVFX.instance.glitch.colorDrift -= (0.8f * Time.deltaTime);
            }
            else
            {
                glitchAttackActive = false;
            }
        }
    }

    public void ResizeGlitchEffect()
    {
        if (CameraGlitchVFX.instance.glitch.scanLineJitter > 0.3)
        {
            CameraGlitchVFX.instance.glitch.scanLineJitter -= 0.3f * Time.deltaTime;
        }
        else
        {
            CameraGlitchVFX.instance.glitch.scanLineJitter = 0.3f;
        }
        if (CameraGlitchVFX.instance.glitch.colorDrift > 0.05)
        {
            CameraGlitchVFX.instance.glitch.colorDrift -= 0.3f * Time.deltaTime;
        }
        else
        {
            CameraGlitchVFX.instance.glitch.colorDrift = 0.05f;
        }
    }
    public void StopGlitchEffect()
    {
        if (CameraGlitchVFX.instance.glitch.scanLineJitter > 0)
        {
            CameraGlitchVFX.instance.glitch.scanLineJitter -= (0.4f * Time.deltaTime);
        }
        if (CameraGlitchVFX.instance.glitch.colorDrift > 0)
        {
            CameraGlitchVFX.instance.glitch.colorDrift -= (0.4f * Time.deltaTime);
        }
    }

    public void GlitchAttack()
    {
        Debug.Log("Derp");
        CameraGlitchVFX.instance.glitch.colorDrift = 0.5f;
        glitchAttackActive = true;
    }
    
}
