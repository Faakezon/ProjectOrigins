using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

using UnityEngine.SceneManagement;

public class PostProcessingFader : MonoBehaviour {

    private PostProcessingProfile mainMenuProfile;
    private BloomModel.Settings bloom;

    private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        mainMenuProfile = GetComponent<PostProcessingBehaviour>().profile;
        bloom = mainMenuProfile.bloom.settings;

        bloom.lensDirt.intensity = 0;
        mainMenuProfile.bloom.settings = bloom;

        if (SceneManager.GetActiveScene().name != "MainMenu")
            return;

        ps = GameObject.Find("Particle System").GetComponent<ParticleSystem>();


    }
	
	// Update is called once per frame
	void Update () {
        switch (MenuManager.currentState)
        {
            case MenuManager.MenuState.LogoScreen:
                FadeIn();
                break;
            case MenuManager.MenuState.CharacterScreen:
                FadeOut();
                break;
            default:
                break;
        }
    }

    void FadeIn()
    {
        if (bloom.lensDirt.intensity < 50)
        {
            bloom.lensDirt.intensity += 1 * Time.deltaTime;
            mainMenuProfile.bloom.settings = bloom;
        }
        //EnlargeParticleSize();
    }

    void EnlargeParticleSize()
    {
        if (ps == null) return;

        if (ps.startSize < 12)
        {
            ps.startSize += 0.5f * Time.deltaTime;
        }
    }

    void FadeOut()
    {
        if(bloom.lensDirt.intensity > 1)
        {
            bloom.lensDirt.intensity -= 10 * Time.deltaTime;
            mainMenuProfile.bloom.settings = bloom;
        }
        
    }
}
