using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    float movementSpeed = 50;
    bool hit = false;

    Vector3 hitPos;

    ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) {
            hitPos = hit.point;
        }
    }

    // Update is called once per frame
    void Update () {

        if(hitPos != Vector3.zero) //We have a physical target.
        {
            MoveToTarget(hitPos);
            IsHit(hitPos);
        }
        else //No physical target.
        {
            //Debug.Log("NoTarget");
            MoveToTarget();
        }

        Destroy(this.gameObject, 5);
    }

    private void MoveToTarget() //No param, no decided target.
    {
        if (hit == false)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
    }
    private void MoveToTarget(Vector3 targetPos) //Target as param, will stop at target hit pos.
    {
        if (hit == false && hitPos != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * movementSpeed);
        }
    }

    private bool IsHit(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, hitPos) <= 0.1f)
        {
            ps.Play();
            if (ps.particleCount == 0)
                Destroy(this.gameObject, 0.2f);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        hit = true;   
    }

}
