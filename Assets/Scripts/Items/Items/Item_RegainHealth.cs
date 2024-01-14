using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_RegainHealth : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float reloadTime = 1.5f;
        public float regainHealth = 0.05f;
    }
    public ItemTier[] itemTier;

    public float timer;
    public float reloadTimeOrdinary = 10;


    private GameObject player;
    private Health playerHP;
    private PlayerHud playerHud;
    private PlayerExperience playerXP;

    private SavedStats savedStats;

    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHud = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHud>();
        playerHP = player.GetComponent<Health>();
        playerXP = player.GetComponent<PlayerExperience>();

        timer = reloadTimeOrdinary;
    }
    private void Update()
    {
        if (activated)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (playerHP.health < playerHP.maxHealth)
            {
                playerHP.health += itemTier[itemLevel].regainHealth;
                playerHud.UpdateLifeBar();
                timer = itemTier[itemLevel].reloadTime;
            }
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (playerHP.health < playerHP.maxHealth)
            {
                playerHP.health += itemTier[itemLevel].regainHealth;
                playerHud.UpdateLifeBar();
                timer = reloadTimeOrdinary;
            }
        }

        
        
    }

    public void UpdateRegainHealth()
    {
        if (!activated)
        {
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            activated = true;
            timer = itemTier[itemLevel].reloadTime;
            playerXP.CloseUpgradeScreen(this);
        } 
        else
        {
            itemLevel++;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            playerXP.CloseUpgradeScreen(this);
        }

        
        
    }
}
