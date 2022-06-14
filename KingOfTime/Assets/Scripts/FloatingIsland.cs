using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIsland : MonoBehaviour
{
    private Vector3 startPos;
    private bool enteredIsland = false;
    private bool point1Checked = false;
    private Vector3 point1V;
    private Vector3 point2V;
    private float distance = 125f;
    private float lerpTime = 3;
    private float lerpTime1 = 6;
    private float currentLerpTime = 0;
    private float currentLerpTime1 = 0;
    private bool arrived = false;
    public GameObject player;
    void Start()
    {
        startPos = this.transform.position;
        point1V = this.transform.position + Vector3.up * distance;
        point2V = point1V + Vector3.right * distance;
    }
    void Update()
    {
        if(enteredIsland && !point1Checked)
        {
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float percentage = currentLerpTime/lerpTime;
            transform.position = Vector3.Lerp(startPos, point1V, percentage);
            if(transform.position == point1V)
                point1Checked = true;
        }
        if(point1Checked)
        {
            currentLerpTime1+=Time.deltaTime;
            if(currentLerpTime1 >= lerpTime1)
            {
                currentLerpTime1 = lerpTime1;
            }
            float percentage1 = currentLerpTime1/lerpTime1;
            transform.position = Vector3.Lerp(transform.position, point2V, percentage1);
            if(transform.position == point2V)
                player.transform.parent = null;
                arrived = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player && !arrived){
            player.transform.parent = transform;
            enteredIsland = true;
            SoundManager.PlaySound("movingisland");
        }
        
    }
    

    
}
