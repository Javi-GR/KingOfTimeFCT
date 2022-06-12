using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.03f;
    public float slowdownLength = 3.5f;
    public bool slowed = false;
    public bool stopped = false;
    public bool inConversation= false;
    private Vector3 normalVector = Vector3.up;

    Rigidbody rb;
    PlayerMovement pm;

    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void Update()
    {
       if(stopped)
        {
            return;
        }
        if(slowed)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime += 0.002f;
            Time.fixedDeltaTime = Mathf.Clamp(Time.timeScale, 0f, 0.02f);
            if(Time.timeScale == 1f && Time.fixedDeltaTime == 0.02f && !inConversation)
            {
                slowed = false;
                if(pm.grounded)
                {
                    return;
                }else{
                    TimeImpulse();
                }
                
            }
            if(Time.timeScale == 1f && Time.fixedDeltaTime == 0.02f && inConversation)
            {
                slowed = false;
                inConversation = false;
            }
        }
        

        
    }
    public void TimeImpulse()
    {
        Vector3 velocity = rb.velocity;
            rb.AddForce(Vector2.up * 300f * 1.5f);
            rb.AddForce(normalVector * 300f * 0.5f);
            if (rb.velocity.y < 0.5f)
            {
                rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
            }
            else if (rb.velocity.y > 0f)
            {
                rb.velocity = new Vector3(velocity.x, velocity.y / 2f, velocity.z);
            }
    }
    
    public void DoSlowMotion ()
    {
        slowed = true;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
    public void StopTime()
    {
        stopped = true;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
    }

}
