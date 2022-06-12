using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{

    //https://youtu.be/5nWRrkaFpic?list=LL&t=599
    public float currentHealth = 40.0f;
    [SerializeField]
    private float maxHealth = 40.0f;
    [SerializeField]
    private int regenRate = 20;
    private bool canRegen = false;
    [SerializeField]
    private float healCooldown = 3.0f;
    [SerializeField]
    private float maxHealCooldown =3.0f;
    [SerializeField]
    private bool startCooldown = false;
    [SerializeField]
    private Image splatterImage = null;
    [SerializeField]
    private Image hurtImage = null;
    [SerializeField]
    private float hurtTimer = 0.1f;

    
    void UpdateHealth()
    {
        Color splatterAlpha = splatterImage.color;
        splatterAlpha.a = 1 - (currentHealth / maxHealth);
        splatterImage.color = splatterAlpha;

    }
    IEnumerator HurtFlash()
    {
        hurtImage.enabled = true;
        SoundManager.PlaySound("mangrunt");
        yield return new WaitForSeconds(hurtTimer);
        hurtImage.enabled = false;
    }
    public void TakeDamage()
    {
        if(currentHealth >= 0)
        {
            canRegen = false;
            StartCoroutine(HurtFlash());
            UpdateHealth();
            healCooldown = maxHealCooldown;
            startCooldown = true;
        }else{
            LevelManager lv = FindObjectOfType<LevelManager>();
            lv.LoadSameLevel(true);
        }
    }
    private void Update()
    {
        if(startCooldown)
        {
            healCooldown -= Time.deltaTime;
            if(healCooldown <= 0)
            {
                canRegen = true;
                startCooldown = false;
            }
        }
        if(canRegen)
        {
            if(currentHealth <= maxHealth - 0.01)
            {
                currentHealth += Time.deltaTime * regenRate;
                UpdateHealth();
            }
            else{
                currentHealth = maxHealth;
                healCooldown = maxHealCooldown;
                canRegen = false;
            }
        }
    }
}
