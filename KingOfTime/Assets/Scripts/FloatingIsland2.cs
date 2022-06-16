using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIsland2 : MonoBehaviour
{
    private Vector3 startPos;
    private bool enteredIsland = false;
    private Vector3 point1V;
    [SerializeField]
    private float distance;
    [SerializeField]
    private bool up;
    private float lerpTime = 3;
    private float currentLerpTime = 0;
    private bool arrived = false;
    [SerializeField]
    private bool elevator;
    public GameObject player;
    void Start()
    {
        startPos = this.transform.position;
        if(up)
        {
            point1V = this.transform.position + Vector3.up * distance;
        }
        else{
            point1V = this.transform.position+ Vector3.down * distance;
        }
    }
    void Update()
    {
        if(enteredIsland)
        {
            if(transform.position == startPos)
                SoundManager.PlaySound("movingisland");
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float percentage = currentLerpTime/lerpTime;
            transform.position = Vector3.Lerp(startPos, point1V, percentage);
            if(this.transform.position == point1V )
            {
                arrived = true;
            }
            if(arrived && elevator)
            {
                SoundManager.StopSound();
                player.transform.parent = null;
                enteredIsland = false;
                currentLerpTime = 0;
                if(up)
                {
                    up = false;
                }
                else
                {
                    up = true;
                }
                point1V = startPos;
                startPos = this.transform.position;
                arrived = false;
            }
            if(arrived && !elevator)
            {
                SoundManager.StopSound();
                player.transform.parent = null;
                enteredIsland = false;
                currentLerpTime = 0;
                this.GetComponent<BoxCollider>().enabled = false;
            }    
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player){
            player.transform.parent = transform;
            enteredIsland = true;
        }
        
    }
    
    

    
}
