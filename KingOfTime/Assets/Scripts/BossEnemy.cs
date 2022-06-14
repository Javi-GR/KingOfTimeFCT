using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject player;
    Rigidbody playerRb;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health = 100f;

    public Blip blip;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform shootingPoint;
    public Transform explosionPoint;
    public ParticleSystem shootingParticles;
    public ParticleSystem dustExplosionParticles;
    public ParticleSystem explosionParticles;
    public BossEnemyHEalth bossHealthUI;
    private bool exploded = false;
    private bool dead = false;
    public GameObject redKey;

    //States
    public float sightRange, attackRange, closeRange, explosionForce;
    public bool playerInSightRange, playerInAttackRange, playerInCloseRange;

    private void Awake()
    {
        playerRb = player.GetComponent<Rigidbody>();
        bossHealthUI.SetMaxHealth(health);
    }

    private void Update()
    {
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInCloseRange = Physics.CheckSphere(transform.position, closeRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange && !dead) WatchPlayer();
        if (playerInAttackRange && playerInSightRange&& !dead) AttackPlayer();
        if (playerInAttackRange && playerInSightRange && playerInCloseRange && !dead) Explosion();
    }

    
    private void WatchPlayer()
    {
        
        transform.LookAt(player.transform);

    }
    private void Explosion()
    {
        if(playerRb != null && !exploded)
        {
            exploded = true;
            StartCoroutine(ResetExplosion());
            dustExplosionParticles.Play();
            Vector3 direction = player.transform.position - explosionPoint.position;
            playerRb.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
            playerRb.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }
    private IEnumerator ResetExplosion()
    {
        SoundManager.PlaySound("steam");
        yield return new WaitForSeconds(0.5f);
        exploded = false;
    }
    private IEnumerator DeadBoss()
    {
        yield return new WaitForSeconds(2f);
        SoundManager.PlaySound("keyleft");
        Instantiate(redKey, transform.position, Quaternion.identity);
        blip.DestroyedEnemy();
        Destroy(gameObject);
    }
    
    //Attack function of the shooting enemy
    private void AttackPlayer()
    {
        //Enemy looks at player to shoot
        Vector3 rotationOffset = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.LookAt(rotationOffset);

        //Checks if not already attacking
        if (!alreadyAttacked)
        {
            shootingParticles.Play();
            GameObject bullet = Instantiate(projectile, shootingPoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            SoundManager.PlaySound("enemyexplosion");
            rb.AddForce(shootingPoint.forward * 30f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    //Resets the attack of the enemy so it doesnt shoot constantly
    private void ResetAttack()
    {
        timeBetweenAttacks = Random.Range(0.7f, 2f);
        alreadyAttacked = false;
    }
    //Function of the enemy to deal damage to him
    public void TakeDamage(float damage)
    {
        health -= damage;
        bossHealthUI.SetHealth(health);
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    //Function that destroys the enemy object 
    private void DestroyEnemy()
    {
        dead = true;
        SoundManager.PlaySound("robotdeath");
        explosionParticles.Play();
        StartCoroutine(DeadBoss());
    }
    //Funciton to see the attack range and sight range in gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, closeRange);
    }
    //Destroys the projectile of the enmy after 0.4s
    
}
