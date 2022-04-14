using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Player;

namespace CreatingCharacters.Abilities
{
    public class KairosMovementController : PlayerMovementController
    {
        [SerializeField] protected float edgeUpforce;
        [SerializeField] protected float wallRaycatsDistance;
        [SerializeField] protected float climbSpeed;
        private int jumpCount = 0;
        private bool isClimbing = false;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        protected override void Update()
        {
            if(!isClimbing)
            {  
                base.Update();
            }
            if(characterController.isGrounded)
            {
                jumpCount=0;
            }
        }

        protected override void Jump()
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.forward, out hit, wallRaycatsDistance))
                {
                    if(hit.collider.GetComponent<Climbable>() != null)
                    {
                        StartCoroutine(Climb(hit.collider));
                        return;
                    }
                }
                if(jumpCount == 0)
                {
                        ResetImpactY();
                        AddForce(Vector3.up, jumpForce);

                    if(characterController.isGrounded)
                    {
                        jumpCount = 1;
                    }
                    else{
                        jumpCount = 2;
                    }
                }
                else if(jumpCount == 1)
                {
                    ResetImpactY();
                    AddForce(Vector3.up, jumpForce);
                    jumpCount = 2;
                }
                
            }
        }

        private IEnumerator Climb(Collider climbableCollider)
        {
            isClimbing = true;
            while(Input.GetKey(KeyCode.Space))
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.right, out hit, wallRaycatsDistance))
                {
                    if(hit.collider == climbableCollider)
                    {
                        characterController.Move(new Vector3(0f, climbSpeed * Time.deltaTime, 0f));
                        yield return null;
                    }
                }
                if(Physics.Raycast(transform.position, -transform.right, out hit, wallRaycatsDistance))
                {
                    if(hit.collider == climbableCollider)
                    {
                        characterController.Move(new Vector3(climbSpeed * Time.deltaTime, 0f, 0f));
                        yield return null;
                    }
                }
            }
            ResetImpactY();

            AddForce(Vector3.up, edgeUpforce);
            isClimbing = false;
        }
    }   
}

