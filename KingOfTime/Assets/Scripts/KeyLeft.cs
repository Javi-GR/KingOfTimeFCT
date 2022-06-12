using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLeft : MonoBehaviour
{
    [SerializeField]
    public string triggerColor;
    public LevelManager levelManager;
    public PickUp pickUp;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == triggerColor)
        {
            other.gameObject.transform.position = transform.position + Vector3.up * 2;
            pickUp.DropObject();
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            levelManager.nKeys++;
        }
    }
}
