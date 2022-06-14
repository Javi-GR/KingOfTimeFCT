using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField]
    private float enemyDamage = 10f;
    private GameObject fireParticle;
    
    private Vector3 firePosition;
    private HealthScript playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        fireParticle = GameObject.FindGameObjectWithTag("Fire");

    }

    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerHealth.currentHealth -= enemyDamage;
            playerHealth.TakeDamage();
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(this.gameObject);
        }
        if(other.CompareTag("EnemyGround"))
        {
            firePosition = this.transform.position;
            StartCoroutine(SpreadFire());
            
            
        }
    }
    IEnumerator SpreadFire()
    {
        yield return new WaitForSeconds(.3f);
        Instantiate(fireParticle, firePosition, Quaternion.identity);
        Destroy(this.gameObject);
       
    }
}
