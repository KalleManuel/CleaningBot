using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ForceField : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float range;
        public float dmg;
        public float healing;
        public float hurtInterval;
        public float onDuration;
        public float offDuration;

    }
    public ItemTier[] itemTier;

    [Header("Force Field Details")]

    public List<GameObject> enemiesInRange;
    public List<GameObject> humansInRange;

    public bool dealDmg, isOn, isUp, isOff;
    public float dmgTimer, wait;

    private PlayerExperience playerXP;

    private SavedStats savedStats;

    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();

        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();

        
        transform.localScale = new Vector3(0, 0, 1);
        isOn = true;
        isUp = false;
        isOff = false;
        wait = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (isOn)
            {
                if (transform.localScale.x < itemTier[itemLevel].range)
                {
                    float temp = transform.localScale.x;
                    temp += Time.deltaTime;
                    transform.localScale = new Vector3(temp, temp, 1);
                }
                else
                {
                    transform.localScale = new Vector3(itemTier[itemLevel].range, itemTier[itemLevel].range, 1);
                    isOn = false;
                    isUp = true;
                }
            }

            if (isUp)
            {
                if (wait < itemTier[itemLevel].onDuration)
                {
                    wait += Time.deltaTime;

                }
                else
                {
                    isUp = false;
                    isOff = true;
                    wait = 0;
                }
            }

            if (isOff)
                {
                    if (transform.localScale.x > 0)
                    {
                        float temp2 = transform.localScale.x;
                        temp2 -= Time.deltaTime;
                        transform.localScale = new Vector3(temp2, temp2, 1);
                    }
                    else if(transform.localScale.x != 0)
                {
                    transform.localScale = new Vector3(0, 0, 1);
                }
                
                else if (wait < (itemTier[itemLevel].offDuration))
                {
                        
                        wait += Time.deltaTime;
                }
                else
                {
                    isOff = false;
                    isOn = true;
                    wait = 0;
                }
            }

            if (dealDmg)
            {
                if (dmgTimer < itemTier[itemLevel].hurtInterval)
                {
                    dmgTimer += Time.deltaTime;

                }
                else
                {
                    HurtEnemies();
                    HealHumans();
                    dmgTimer = 0;
                }
            }
        }

    }

    public void HurtEnemies()
    {
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            Debug.Log("Damage to " + enemiesInRange[i]);

            enemiesInRange[i].GetComponent<EnemyHealth>().health -= itemTier[itemLevel].dmg;
            enemiesInRange[i].GetComponent<EnemyHealth>().positionOfInflictor = this.gameObject.transform.position;
            enemiesInRange[i].GetComponent<VisualDamage>().visualDmg = true;
        }
    }
    public void HealHumans()
    {
        for (int i = 0; i < humansInRange.Count; i++)
        {
            Health humanHealth = humansInRange[i].GetComponent<Health>();
            if (humanHealth.health < humanHealth.maxHealth)
            {
                humanHealth.health += itemTier[itemLevel].healing;
                // any visual Feedback? Blinking green?
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(collision.gameObject);
        }

        if (collision.gameObject.tag == "BodySoul")
        {
            humansInRange.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Remove(collision.gameObject);
        }

        if (collision.gameObject.tag == "BodySoul")
        {
            humansInRange.Remove(collision.gameObject);
        }
    }

    public void UpgradeForcefield()
    {
        if (!activated)
        {
            activated = true;
            dealDmg = true;

            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            playerXP.CloseUpgradeScreen(this);
        }
        else
        {
            itemLevel++;
            isOn = true;
            isUp = false;
            isOff = false;
            wait = 0;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            playerXP.CloseUpgradeScreen(this);
        }               
    }
}
