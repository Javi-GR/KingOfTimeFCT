using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50F;
    public void TakeDamage(float amount)
    {
       
        health -= amount;
        Debug.Log(health+ " health left");
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
