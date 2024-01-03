using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : Item
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

    private void Start()
    {
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        shieldFeedback = GetComponent<SpriteRenderer>();
        shieldFeedback.enabled = false;
        timer = itemTier[itemLevel].ontime;
    }
    private void Update()
    {
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

    public void UpgradeShield()
    {
        if (!activated)
        {
            shieldOn = true;
            playerHealth.shield = true;
            playerHealth.shieldAmount = itemTier[itemLevel].sheild;
            shieldFeedback.enabled = true;
            activated = true;
            playerXP.CloseUpgradeScreen(this);
        }
        else
        {
            itemLevel++;
            playerHealth.shieldAmount = itemTier[itemLevel].sheild;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            playerXP.CloseUpgradeScreen(this);
        }

    }
}
