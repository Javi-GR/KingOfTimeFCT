using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;
     private bool isLocked = false;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingDirection;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
    private void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
     public void Lock(bool shouldLock)
        {
            isLocked = shouldLock;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 90f, 10f * Time.deltaTime);

            if(!shouldLock)
            {
                smoothedVelocity = new Vector2();
                currentLookingDirection = new Vector2(player.eulerAngles.y, -transform.eulerAngles.x);
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, 10f * Time.deltaTime);
            }
        }
}
