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
    GameObject floatingIsland;
    private bool dead = false;
    public int nKeys = 0;
    public Text fadeText;
    private Vector3 startPosition;
    private Vector3 point;
    private float lerpTimeLM = 4;
    private float currentLerpTimeLM = 0;

    // Update is called once per frame
    void Update()
    {
        if(player.position.y < -190 && !dead){
            dead = true;
            SoundManager.PlaySound("death");
            LoadSameLevel(dead);
        }
        if(dead && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(nKeys == 3 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(floatingIsland.transform.position == startPosition)
            {
               SoundManager.PlaySound("movingisland");
            }
            player.transform.parent = floatingIsland.transform;
            LoadNextLevel();
            currentLerpTimeLM += Time.deltaTime;
            if(currentLerpTimeLM >= lerpTimeLM)
            {
                currentLerpTimeLM = lerpTimeLM;
            }
            float percentage = currentLerpTimeLM/lerpTimeLM;
            floatingIsland.transform.position = Vector3.Lerp(startPosition, point, percentage);
            
        }
        if(nKeys == 3 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            LoadNextLevel(); 
        }
    }
    
    void Start()
    {
        floatingIsland = GameObject.FindGameObjectWithTag("FloatingIsland");
        if(floatingIsland != null)
        {
            point = floatingIsland.transform.position + Vector3.up * 500f;
            startPosition = floatingIsland.transform.position;
        }
        
    }
   
    public void LoadSameLevel(bool died)
    {
        if(died)
        {
            dead = true;
            transition.SetTrigger("Start");
            fadeText.text = "Press F to pay respect [f]";
        }else{
            fadeText.text = "";
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }
        
    }
    public void LoadNextLevel()
    {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
   private IEnumerator EndingLevel()
   {
       LoadNextLevel();
       yield return new WaitForSeconds(1f);
   }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        if(dead){
            yield return new WaitForFixedUpdate();
        }else{
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(levelIndex);
        }
        
        
    }
    public void  QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        LoadLevel(0);
    }
    
}
