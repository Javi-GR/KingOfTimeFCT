using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
   public TimeManager tm;
   public GameObject parentUi;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            parentUi.SetActive(true);
            tm.StopTime();
            
        }
        if(Input.GetKeyDown(KeyCode.Escape) )
        {
            parentUi.SetActive(false);
            tm.stopped = false;
        }
    }
}
