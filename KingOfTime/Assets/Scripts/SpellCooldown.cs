using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpellCooldown : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldownR;
    [SerializeField]
    private Text textCooldownR;
    [SerializeField]
    private Image imageCooldownS;
    [SerializeField]
    private Text textCooldownS;

    //Cooldown timer
    private bool isCooldownRewind = false;
    private bool isCooldownStopTime = false;
    private float cooldownTimeS = 10.0f;
    private float cooldownTimerS = 0.0f;
    private float cooldownTimeR = 10.0f;
    private float cooldownTimerR = 0.0f;
    public TimeManager timeManager;
    RewindTime rewindTime;

    // Start is called before the first frame update
    void Start()
    {
        textCooldownS.gameObject.SetActive(false);
        imageCooldownS.fillAmount = 0.0f;
        textCooldownR.gameObject.SetActive(false);
        imageCooldownR.fillAmount = 0.0f;
        rewindTime = GameObject.FindGameObjectWithTag("Player").GetComponent<RewindTime>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && rewindTime == null)
        {
            UseSpellStopTime();
        }
        if(Input.GetKeyDown(KeyCode.Q) && rewindTime.canCollectRecallData == true && rewindTime != null)
        {
            
            UseSpellStopTime();
        }
        if(Input.GetKeyDown(KeyCode.E) && timeManager.slowed == false && SceneManager.GetActiveScene().buildIndex == 1)
        {
            UseSpellRewind();
        }
        if(isCooldownStopTime)
        {
            ApplyCooldownStopTime();
        }
        if(isCooldownRewind)
        {
            ApplyCooldownRewind();
        }
    }
    void ApplyCooldownStopTime()
    {
        //Subtracts time since last called
        cooldownTimerS -= Time.deltaTime;

        if(cooldownTimerS < 0.0f)
        {
            isCooldownStopTime = false;
            textCooldownS.gameObject.SetActive(false);
            imageCooldownS.fillAmount = 0.0f;
        }
        else
        {
            textCooldownS.text = Mathf.RoundToInt(cooldownTimerS).ToString();
            imageCooldownS.fillAmount = cooldownTimerS / cooldownTimeS;
        }
    }
    void ApplyCooldownRewind()
    {
        //Subtracts time since last called
        cooldownTimerR -= Time.deltaTime;

        if(cooldownTimerR < 0.0f)
        {
            isCooldownRewind = false;
            textCooldownR.gameObject.SetActive(false);
            imageCooldownR.fillAmount = 0.0f;
        }
        else
        {
            textCooldownR.text = Mathf.RoundToInt(cooldownTimerR).ToString();
            imageCooldownR.fillAmount = cooldownTimerR / cooldownTimeR;
        }
    }
    public void UseSpellRewind()
    {
        if(isCooldownRewind)
        {
            Debug.Log("Spell rewind in use");
        }
        else
        {
            SoundManager.PlaySound("rewindtime");
            isCooldownRewind = true;
            textCooldownR.gameObject.SetActive(true);
            cooldownTimerR = cooldownTimeR;
            rewindTime.CallRecall();
        }
    }
    public void UseSpellStopTime()
    {
        if(isCooldownStopTime)
        {
            Debug.Log("Spell slow down in use");
        }
        else
        {
            SoundManager.PlaySound("slowdowntime");
            isCooldownStopTime = true;
            textCooldownS.gameObject.SetActive(true);
            cooldownTimerS = cooldownTimeS;
            timeManager.DoSlowMotion();
        }
    }
}
