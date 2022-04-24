using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreatingCharacters.Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField]private float lookSensitivity ;
        [SerializeField]private float lookSmoothing ;
        float xRotation = 0f;

        private bool isLocked = false;
        private Transform playerTransform;
        private Vector2 currentLookingDirection;

        private void Awake()
        {
            playerTransform = transform.root;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Lock(bool shouldLock)
        {
            isLocked = shouldLock;

            if(!shouldLock)
            {
                currentLookingDirection = new Vector2(playerTransform.eulerAngles.y, -transform.eulerAngles.x);
                Camera.main.fieldOfView = 60f;
            }
        }
        

        private void Update()
        {
            if(isLocked){ return; }
            
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerTransform.Rotate(Vector3.up * mouseX);
        }

        
    }
}

