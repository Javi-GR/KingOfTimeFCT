using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public RewindTime rewindTime;

    // Start is called before the first frame update
    void Start()
    {
        textCooldownS.gameObject.SetActive(false);
        imageCooldownS.fillAmount = 0.0f;
        textCooldownR.gameObject.SetActive(false);
        imageCooldownR.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && rewindTime.canCollectRecallData == true)
        {
            UseSpellStopTime();
        }
        if(Input.GetKeyDown(KeyCode.E) && timeManager.slowed == false)
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
            isCooldownRewind = true;
            textCooldownR.gameObject.SetActive(true);
            cooldownTimerR = cooldownTimeR;
            StartCoroutine(rewindTime.Recall());
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
            isCooldownStopTime = true;
            textCooldownS.gameObject.SetActive(true);
            cooldownTimerS = cooldownTimeS;
            timeManager.DoSlowMotion();
        }
    }
}
