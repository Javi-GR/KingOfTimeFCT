using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
   public Dialogue dialogue;
   public GameObject tutorial;

   public void TriggerDialogue ()
   {
       tutorial.SetActive(true);
       FindObjectOfType<TutorialDialogue>().StartConversation(dialogue);
       Destroy(this.GetComponent<BoxCollider>());
   }
   private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag == "Player")
       {
           TriggerDialogue();
       }
   }
}
