using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.45f;
    public Transform player;
    private bool dead = false;
    public int nKeys = 0;
    public Text fadeText;

    // Update is called once per frame
    void Update()
    {
        if(player.position.y < -120){
            dead = true;
            LoadSameLevel(dead);
        }
    }
    void FixedUpdate()
    {
        if(nKeys == 3)
        {
            LoadNextLevel();
            Debug.Log("You finished the level!");
        }
    }
    public void LoadSameLevel(bool died)
    {
        if(died)
        {
            //Change Text of fade
            fadeText.text = "Try Again";
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }else{
            fadeText.text = "";
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }
        
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
