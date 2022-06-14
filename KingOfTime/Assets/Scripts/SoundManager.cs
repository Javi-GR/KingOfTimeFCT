using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip rewindTime, burn, laserHit, slowdownTime, steam,  
    jump, shot, death, deathbyrobot, robotDeath, hitmarker, keyLeft,pickedUpKey, floatingIsland, explosion, hurt;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        rewindTime = Resources.Load<AudioClip>("rewindtime");
        slowdownTime = Resources.Load<AudioClip>("slowdowntime");
        jump = Resources.Load<AudioClip>("jump");
        shot = Resources.Load<AudioClip>("shot");
        burn = Resources.Load<AudioClip>("burn");
        robotDeath = Resources.Load<AudioClip>("robotdeath");
        steam = Resources.Load<AudioClip>("steam");
        laserHit = Resources.Load<AudioClip>("laserhit");
        deathbyrobot = Resources.Load<AudioClip>("deathbyrobot");
        hitmarker = Resources.Load<AudioClip>("hitmarker"); 
        keyLeft = Resources.Load<AudioClip>("keyleft");
        pickedUpKey = Resources.Load<AudioClip>("keypickedup");
        explosion = Resources.Load<AudioClip>("enemyexplosion");
        hurt = Resources.Load<AudioClip>("mangrunt");
        floatingIsland = Resources.Load<AudioClip>("movingisland");
        death = Resources.Load<AudioClip>("death");

        audioSrc = GetComponent<AudioSource>();
    }

 
    public static void PlaySound(string clip)
    {
        
        switch(clip)
        {
            case "slowdowntime":
                audioSrc.PlayOneShot(slowdownTime);
                break;
            case "rewindtime":
                audioSrc.PlayOneShot(rewindTime);
                break;
            case "jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "movingisland":
                audioSrc.PlayOneShot(floatingIsland);
                break;
            case "death":
                audioSrc.PlayOneShot(death);
                break;
            case "mangrunt":
                audioSrc.PlayOneShot(hurt);
                break;
            case "enemyexplosion":
                audioSrc.PlayOneShot(explosion);
                break;
            case "keypickedup":
                audioSrc.PlayOneShot(pickedUpKey);
                break;
            case "keyleft":
                audioSrc.PlayOneShot(keyLeft);
                break;
            case "hitmarker":
                audioSrc.PlayOneShot(hitmarker);
                break;
            case "deathbyrobot":
                audioSrc.PlayOneShot(deathbyrobot);
                break;
            case "shot":
                audioSrc.PlayOneShot(shot);
                break;
            case "burn":
                audioSrc.PlayOneShot(burn);
                break;
            case "laserhit":
                audioSrc.PlayOneShot(laserHit);
                break;
            case "steam":
                audioSrc.PlayOneShot(steam);
                break;
            case "robotdeath":
                audioSrc.PlayOneShot(robotDeath);
                break;
        }
            
    }
    public static void StopSound()
    {
        audioSrc.Stop();
    }
}
