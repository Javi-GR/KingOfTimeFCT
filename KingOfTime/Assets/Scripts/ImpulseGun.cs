using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseGun : MonoBehaviour
{
    public GameObject explosion;
    public Camera cam;
    public float impactForce = 120f;
    public float range = 100f;
    public float damage = 10f;
    public ParticleSystem gunEffect;
    public PauseMenuScript pauseMenu;


    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Mouse0) && !pauseMenu.GameIsPaused)
       {
           Shoot();
       }
       else
       {
         return;
       }
 
    }
    void Shoot()
    {
        gunEffect.Play();
        SoundManager.PlaySound("shot");
        RaycastHit hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red, 3f);
            Debug.Log(hitInfo.transform.name);
            BossEnemy target =  hitInfo.transform.GetComponent<BossEnemy>();
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
