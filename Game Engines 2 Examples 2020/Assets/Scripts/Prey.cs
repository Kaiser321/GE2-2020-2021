using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour
{
    public bool attack = false;
    public Transform predator;
    public Transform muzzle;
    public int fireRate = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Predator")
        {
            predator = other.transform;
            GetComponent<BigBoid>().pathFollowingEnabled = false;
            GetComponent<BigBoid>().pursueEnabled = true;
            GetComponent<BigBoid>().pursueTarget = predator.GetComponent<BigBoid>();
            StartCoroutine(ShootingCoroutine());
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Shoot only if target is in POV of 60 degree arc.
        float angle  = Vector3.Angle(transform.forward, predator.position  - transform.position);
        attack = angle < 60;
    }

    // OnTriggerExit gets triggered too soon.
    //void OnTriggerExit(Collider other)
    //{
    //    GetComponent<BigBoid>().pathFollowingEnabled = true;
    //    GetComponent<BigBoid>().pursueEnabled = false;
    //    GetComponent<BigBoid>().pursueTarget = null;
    //    attack = false;
    //    StopCoroutine(ShootingCoroutine());
    //}

    void Shoot()
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.position = muzzle.position;
        bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        bullet.transform.rotation = transform.rotation;
        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1000);
    }

    System.Collections.IEnumerator ShootingCoroutine()
    {
        while (true)
        {
            if(attack)
            {
                Shoot();
            }
            yield return new WaitForSeconds(1.0f / fireRate);
        }
    }
}
