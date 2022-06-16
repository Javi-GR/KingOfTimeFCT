using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeFall : MonoBehaviour
{
    public GameObject bridge;
    public GameObject walls;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(walls.activeSelf)
            {
                return;
            }
            if(bridge.activeSelf && !walls.activeSelf)
            {
                SoundManager.PlaySound("fallingwood");
                bridge.SetActive(false);
                walls.SetActive(true);
            }
        }
    }
}
