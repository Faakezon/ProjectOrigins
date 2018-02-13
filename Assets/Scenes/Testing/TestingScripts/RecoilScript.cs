using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilScript : MonoBehaviour {

    public static RecoilScript instance = null;

    public float shake = 0;
    public float shakeAmount = 0.01f;
    float decreaseFactor = 100.0f;

    Vector3 startVector;
    Quaternion startQuaternion;


    public float RPM;
    public float fireDelay;
    public float nextShoot;
    public bool allowFire;

    public ParticleSystem ps;
    public GameObject Bullet;

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

        fireDelay = RPM / 60.0f;
        allowFire = true;

        startVector = transform.localPosition;
        startQuaternion = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (shake > 0)
        {
            transform.localRotation = Quaternion.Euler(Random.insideUnitSphere * shakeAmount * 100) * startQuaternion;
            transform.localPosition = Random.insideUnitSphere * shakeAmount + startVector;
            shake -= Time.deltaTime * decreaseFactor;

            if (allowFire)
            {
                StartCoroutine("FireParticles");
            }
        }
        else
        {
            shake = 0.0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startVector, Time.deltaTime * 0.5f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, startQuaternion, Time.deltaTime * 0.5f);

        }



    }

    IEnumerator FireParticles()
    {
        Debug.Log("FireparticlesBeforeTrue");
        allowFire = false;
        Instantiate(Bullet, ps.gameObject.transform.position, ps.gameObject.transform.rotation);
        ps.Play();
        yield return new WaitForSeconds(1 / (RPM / 60));
        allowFire = true;
        Debug.Log("FireparticlesAfterTrue");
    }


}
