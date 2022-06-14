using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeysData 
{   
    public float[] keyPosition;
    public int numberOfKeys;
    public KeysData(KeyLeft keyLeft)
    {
        if(keyLeft.keyPosition != null)
        {
            keyPosition[0] = keyLeft.keyPosition.transform.position.x;
            keyPosition[1] = keyLeft.keyPosition.transform.position.y;
            keyPosition[2] = keyLeft.keyPosition.transform.position.z;
            numberOfKeys = keyLeft.levelManager.nKeys;
        }
        
    } 
}
