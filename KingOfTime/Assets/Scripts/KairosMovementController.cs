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
        private bool isWallrunning = false;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        protected override void Update()
        {
            if(!isWallrunning)
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
                if(jumpCount == 0)
                {
                    if(characterController.isGrounded)
                    {
                        ResetImpactY();
                        AddForce(Vector3.up, jumpForce);
                        jumpCount = 1;
                    }
                }
                else if(jumpCount == 1)
                {
                    ResetImpactY();
                    AddForce(Vector3.up, jumpForce);
                    jumpCount = 2;
                }
                CheckForWall();
            }
            
        }

        private void CheckForWall()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.right, out hit, wallRaycatsDistance))
            {
                StartCoroutine(WallrunRight(hit.collider));
            }
            if(Physics.Raycast(transform.position, -transform.right, out hit, wallRaycatsDistance))
            {
                StartCoroutine(WallrunLeft(hit.collider));
            }
        }

        

        private IEnumerator WallrunLeft(Collider wallrunColl)
        {
            isWallrunning = true;
            RaycastHit hit;
            while(Input.GetKey(KeyCode.W) && Physics.Raycast(transform.position, -transform.right, out hit, wallRaycatsDistance))
            {
                characterController.Move(new Vector3(climbSpeed * Time.deltaTime, 0.5f * Time.deltaTime, 0f));
                yield return null;
            }
            ResetImpactY();

            AddForce(Vector3.up, edgeUpforce);
            isWallrunning = false;
        }
        private IEnumerator WallrunRight(Collider wallrunColl)
        {
            isWallrunning = true;
            RaycastHit hit;
            while(Input.GetKey(KeyCode.W) && Physics.Raycast(transform.position, -transform.right, out hit, wallRaycatsDistance))
            {
                AddForce(Vector3.up, edgeUpforce);
                characterController.Move(new Vector3(climbSpeed * Time.deltaTime, 0.5f * Time.deltaTime, 0f));
                yield return null;
            }
            ResetImpactY();

            AddForce(Vector3.up, edgeUpforce);
            isWallrunning = false;
        }
    }   
}

