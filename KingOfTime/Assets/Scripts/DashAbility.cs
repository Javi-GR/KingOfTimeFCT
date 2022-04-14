using System.Collections;
using System.Collections.Generic;
using CreatingCharacters.Player;
using UnityEngine;

namespace CreatingCharacters.Abilities
{
    [RequireComponent(typeof(KairosMovementController))]
    public class DashAbility : Ability
    {
        [SerializeField] private float dashForce;
        [SerializeField] private float dashDuration;

        private KairosMovementController playerMovementController;

        private void Awake(){
            playerMovementController = GetComponent<KairosMovementController>();
        }

        private void Update(){
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Cast());
            }
        }
        public override IEnumerator Cast()
        {
            playerMovementController.AddForce(Camera.main.transform.forward, dashForce);

            yield return new WaitForSeconds(dashDuration);
            playerMovementController.ResetImpact();
        }
    }
}

