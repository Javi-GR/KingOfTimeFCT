using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLeft : MonoBehaviour
{
    
    public LevelManager levelManager;
    public PickUp pickUp;
    public GameObject keyPosition;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == keyPosition.tag)
        {
            SoundManager.PlaySound("keyleft");
            keyPosition.transform.position = transform.position + Vector3.up * 2;
            pickUp.DropObject();
            Destroy(keyPosition.GetComponent<Rigidbody>());
            levelManager.nKeys++;
            Debug.Log("Number of keys "+levelManager.nKeys);
        }
    }
    public void SaveKeyPosition()
    {
        SaveSystem.SaveKeyPosition(this);
    }
    public void LoadKeyPosition()
    {
        Debug.Log("Loaded Key position");
        KeysData data = SaveSystem.LoadKeyPosition();
        Vector3 position;
        position.x = data.keyPosition[0];
        position.y = data.keyPosition[1];
        position.z = data.keyPosition[2];
        keyPosition.transform.position = position;
        Destroy(keyPosition.GetComponent<Rigidbody>());
        levelManager.nKeys = data.numberOfKeys;

    }
}
