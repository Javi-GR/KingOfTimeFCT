using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SentinelEnemy : MonoBehaviour
{
    Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health = 10f;

    public Blip blip;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange) WatchPlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    
    private void WatchPlayer()
    {
        
        transform.LookAt(player);

    }
    //Attack function of the shooting enemy
    private void AttackPlayer()
    {
        //Enemy looks at player to shoot
        Vector3 rotationOffset = new Vector3(player.position.x, player.position.y, player.position.z);
        transform.LookAt(player);

        //Checks if not already attacking
        if (!alreadyAttacked)
        {
            
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            StartCoroutine(destroyBullet(bullet));

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    //Resets the attack of the enemy so it doesnt shoot constantly
    private void ResetAttack()
    {
        timeBetweenAttacks = Random.Range(0.6f, 1f);
        alreadyAttacked = false;
    }
    //Function of the enemy to deal damage to him
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    //Function that destroys the enemy object 
    private void DestroyEnemy()
    {
        Destroy(gameObject);
        blip.DestroyedEnemy();
    }
    //Funciton to see the attack range and sight range in gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    //Destroys the projectile of the enmy after 0.4s
    private IEnumerator destroyBullet(GameObject bullet){

        yield return new WaitForSeconds(0.4f);
        if(bullet!=null){
            Destroy(bullet);
        }
        
    }
}