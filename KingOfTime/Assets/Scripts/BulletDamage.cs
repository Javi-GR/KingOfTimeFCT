using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField]
    private float enemyDamage = 10f;
    [SerializeField]
    private GameObject explosiveParticle = null;
    private HealthScript playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
    }

    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            explosiveParticle.SetActive(true);
            playerHealth.currentHealth -= enemyDamage;
            playerHealth.TakeDamage();
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
