using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;
    bool slowed = false;

    private void Update()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        if(Time.timeScale == 1f){
            Time.fixedDeltaTime += 0.002f;
            Time.fixedDeltaTime = Mathf.Clamp(Time.timeScale, 0f, 0.02f);
        }
        
    }
    
    
    public void DoSlowMotion ()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

}
