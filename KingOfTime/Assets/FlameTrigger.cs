using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrigger : MonoBehaviour
{
    private HealthScript playerHealth;
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SoundManager.PlaySound("burn");
            playerHealth.currentHealth -= 5f;
            playerHealth.TakeDamage();
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}
