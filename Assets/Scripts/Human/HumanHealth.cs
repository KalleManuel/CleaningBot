using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHealth : MonoBehaviour
{
    [SerializeField]
   private string characterName;

    public float humanHealth = 30;
    public float humanMaxHealth = 30;
    public float playerSheild = 0;
    public float regainHealth = 0.05f;
    

    private float currentHealth;

    [SerializeField]
    private GameObject lifeBar;

    public bool dealDamage;

    public float damage;
    public float damageTimer;
    private float timerStartValue;
    public float healing;

    public bool timeToRegainHealth;
    public bool inCR;

    [SerializeField]
    public GameObject theHumanBody;

    private PlayerCompanions playerCompanions;
    public string healthBarCode;


    // Start is called before the first frame update
    void Start()
    {
        lifeBar = GameObject.FindGameObjectWithTag(healthBarCode);
        playerCompanions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCompanions>();

        dealDamage = false;
        timerStartValue = 0.5f;
        damageTimer = timerStartValue;
        UpdateLifeBar();
        timeToRegainHealth = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (humanHealth < humanMaxHealth)
        {
            if (timeToRegainHealth)
            {
                timeToRegainHealth = false;
                StartCoroutine(RegainHealth(1));
            }
        }
        
        if (dealDamage)
        {
            if (damageTimer > 0)
            {
                damageTimer -= Time.deltaTime;
            }
            else
            {
                humanHealth -= damage - playerSheild;
                VisualDamage visualDmg = GetComponent<VisualDamage>();
                visualDmg.visualDmg = true;
                damageTimer = timerStartValue;
                UpdateLifeBar();
                
            }
        }
        else damageTimer = timerStartValue;

        if (humanHealth <= 0)
        {
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            damage = collision.gameObject.GetComponent<EnemyDamage>().dealDamage;
            dealDamage = true;

        }
       

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            dealDamage = false;
            damage = 0;

        }
    }

    public void Dead()
    {
        string deathMessage = characterName + " is dead";

        StartCoroutine(PlayerHud.playerHud.SendMessageToHUD(deathMessage,3,false));
        PlayerHud.playerHud.UpdateHumansInGame();
        playerCompanions.UpdateHumanCompanions();

        Destroy(theHumanBody);

        
    }

    public void UpdateLifeBar()
    {
        currentHealth = humanHealth / humanMaxHealth;
        lifeBar.transform.localScale = new Vector3(1f, currentHealth, 1f);
    }

    private IEnumerator RegainHealth(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        humanHealth += regainHealth;
 

        if (humanHealth > humanMaxHealth)
        {
            humanHealth = humanMaxHealth;
        }

        UpdateLifeBar();

        timeToRegainHealth = true;
    }
}
