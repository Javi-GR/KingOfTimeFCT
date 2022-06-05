using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLeft : MonoBehaviour
{
    [SerializeField]
    public string triggerColor;
    public LevelManager levelManager;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == triggerColor)
        {
            other.gameObject.transform.position = transform.position + Vector3.up * 2;
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.isStatic = true;
            levelManager.nKeys++;
        }
    }
}
