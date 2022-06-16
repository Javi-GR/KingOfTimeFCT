using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit : MonoBehaviour
{
    public HealthScript health;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SoundManager.PlaySound("laserhit");
            health.currentHealth -= 5.0f;
            health.TakeDamage();
        }
    }
}
