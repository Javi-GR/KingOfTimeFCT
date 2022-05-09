using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseGun : MonoBehaviour
{
    public GameObject explosion;
    public Camera cam;

    public TimeManager timeManager;
    

    // Update is called once per frame
    void Update()
    {
       if(Input.GetButtonDown("Fire1")){
           Shoot();
       }
 
    }
    void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo))
        {
            Instantiate(explosion, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            timeManager.DoSlowMotion();
        }
    }
}
