using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CreatingCharacters.Player{

    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
      
        [SerializeField] protected float movementSpeed = 20f;
        [SerializeField] protected float jumpForce = 25f;
        [SerializeField] protected float mass = 2f;
        [SerializeField] protected float damping = 5f;

        [SerializeField] protected float edgeUpforce = 0.1f;
        [SerializeField] protected float wallRaycatsDistance;
        [SerializeField] protected float climbSpeed;
        private int jumpCount = 0;
        private bool isWallrunning = false;


        protected CharacterController characterController;

        protected float velocityY;
        protected Vector3 currentImpact;
        private readonly float gravity = Physics.gravity.y;
        private void Awake()
        {
          characterController = GetComponent<CharacterController>();

        }

        protected virtual void Update()
        {
            
            if(characterController.isGrounded)
            {
                jumpCount = 0;
            }
            Move();
            Jump();
            //CheckForWall();
            
            
        }
        protected virtual void Move(){
            Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

            movementInput = transform.TransformDirection(movementInput);

            if(characterController.isGrounded && velocityY < 0f)
            {
                velocityY = 0f;
            }

            velocityY += gravity * Time.deltaTime;

            Vector3 velocity = movementInput * movementSpeed + Vector3.up * velocityY;

            if(currentImpact.magnitude > 0.2f){
                velocity += currentImpact;
            }

            characterController.Move(velocity * Time.deltaTime);

            currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, damping * Time.deltaTime);
        }

        protected virtual void Jump(){
           if(Input.GetKeyDown(KeyCode.Space) && jumpCount == 0)
           {
                AddForce(Vector3.up, jumpForce);
                jumpCount =jumpCount +1;
           }
           else if(Input.GetKeyDown(KeyCode.Space) && jumpCount == 1)
           {
                AddForce(Vector3.up, jumpForce);
                jumpCount =jumpCount +1;
           }
           else{
               
               Debug.Log("Has alcanzado todos los saltos");
           }
        }

        
        
        public void AddForce(Vector3 direction, float magnitude){

            currentImpact += direction.normalized * magnitude / mass;

        }
       

        public void ResetImpact()
        {
            currentImpact = Vector3.zero;
            velocityY = 0f;
        }
        
        public void ResetImpactY()
        {
            currentImpact.y = 0f;
            velocityY = 0f;
        }

        /* private void CheckForWall()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.right, out hit, wallRaycatsDistance))
            {
                StartCoroutine(WallrunRight(hit.collider));
            }
            else if(Physics.Raycast(transform.position, -transform.right, out hit, wallRaycatsDistance))
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
                characterController.Move(new Vector3(climbSpeed * Time.deltaTime, 0.1f * Time.deltaTime, 0f));
                yield return null;
            }
            ResetImpactY();

            AddForce(Vector3.left, edgeUpforce);
            isWallrunning = false;
        }
        private IEnumerator WallrunRight(Collider wallrunColl)
        {
            isWallrunning = true;
            RaycastHit hit;
            while(Input.GetKey(KeyCode.W))
            {
               if(Physics.Raycast(transform.position, transform.right, out hit, wallRaycatsDistance)){
                    characterController.Move(new Vector3(climbSpeed * Time.deltaTime, 0.1f * Time.deltaTime, 0f));
                    yield return null;
               }
               if(Input.GetKeyDown(KeyCode.Space)){
                   AddForce(Vector3.right, edgeUpforce);
               }
               
            }
            ResetImpactY();
            isWallrunning = false;
        }
*/
        
    }
}

