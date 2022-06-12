using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseGun : MonoBehaviour
{
    public GameObject explosion;
    public Camera cam;
    public float impactForce = 60f;
    public float range = 100f;
    public float damage = 10f;
    public ParticleSystem gunEffect;


    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Mouse0))
       {
           Shoot();
       }
 
    }
    void Shoot()
    {
        gunEffect.Play();
        RaycastHit hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);
            SentinelEnemy target =  hitInfo.transform.GetComponent<SentinelEnemy>();
            Enemy targetEnemy =  hitInfo.transform.GetComponent<Enemy>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
             if(targetEnemy != null)
            {
                targetEnemy.TakeDamage(damage);
            }
            if(hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
            }
            GameObject impact =  Instantiate(explosion, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impact, 1.5f);

        }
    }
}
