using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogue : MonoBehaviour
{
    private Queue<string> sentences;
    public GameObject parentDisplay;
    public Text tutorialText;
    public Image imageTutorial;
    public bool conversationEnded;
    public TimeManager timeManager;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartConversation(Dialogue dialogue)
    {
        
        sentences.Clear();
        timeManager.StopTime();
        

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            DisplayNextSentence();
        }
        if(conversationEnded){
            conversationEnded = false;
            timeManager.stopped = false;
            timeManager.slowed = true;
            timeManager.inConversation = true;
        }
        
    }

    public void DisplayNextSentence ()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            parentDisplay.SetActive(false);
            return;
        }

        string sentence = sentences.Dequeue();
        tutorialText.text = sentence;
        Debug.Log(sentence);
    }
    void EndDialogue()
    {
        conversationEnded = true;
        Debug.Log("End of conversation");
    }
}
