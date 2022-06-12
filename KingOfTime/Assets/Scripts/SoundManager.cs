using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip rewindTime, slowdownTime, jump, fire, death, keyLeft, floatingIsland, explosion, hurt;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        rewindTime = Resources.Load<AudioClip>("rewindtime");
        slowdownTime = Resources.Load<AudioClip>("slowdowntime");
        jump = Resources.Load<AudioClip>("jump");
        /*
        fire = Resources.Load<AudioClip>("");
       
        keyLeft = Resources.Load<AudioClip>("");*/
        explosion = Resources.Load<AudioClip>("enemyexplosion");
        hurt = Resources.Load<AudioClip>("mangrunt");
        floatingIsland = Resources.Load<AudioClip>("movingisland");
        death = Resources.Load<AudioClip>("death");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
            
    }
}
