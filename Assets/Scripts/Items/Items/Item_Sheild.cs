using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Sheild : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float sheild;
        public float ontime;
        public float offtime;
        
    }
    public ItemTier[] itemTier;

    private PlayerExperience playerXP;
    private PlayerMovment playerSpeed;
    private Health playerHealth;
    SpriteRenderer shieldFeedback;

    public bool shieldOn, shieldOff;
    private float timer;

    private SavedStats savedStats;

    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        shieldFeedback = GetComponent<SpriteRenderer>();
        shieldFeedback.enabled = false;
        timer = itemTier[itemLevel].ontime;
    }
    private void Update()
    {
        if (startItem)
        {
            startItem = false;
            UpgradeShield(true);
        }
        if (activated)
        {
            if (shieldOn)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    shieldOn = false;
                    playerHealth.shield = false;
                    timer = itemTier[itemLevel].offtime;
                    shieldFeedback.enabled = false;
                    shieldOff = true;
                }

            } else if (shieldOff)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    shieldOff = false;
                    playerHealth.shield = true;
                    timer = itemTier[itemLevel].ontime;
                    shieldFeedback.enabled = true;
                    shieldOn = true;
                }
            }
        }
       
    }

    public void UpgradeShield(bool _isStartItem)
    {
        if (!activated)
        {
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            shieldOn = true;
            playerHealth.shield = true;
            playerHealth.shieldAmount = itemTier[itemLevel].sheild;
            shieldFeedback.enabled = true;
            activated = true;
            
        }
        else
        {
            itemLevel++;
            playerHealth.shieldAmount = itemTier[itemLevel].sheild;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            
        }
        playerXP.CloseUpgradeScreen(this, _isStartItem);
    }
}
