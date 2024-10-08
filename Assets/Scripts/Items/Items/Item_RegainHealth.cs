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
    
    private PlayerExperience playerXP;

    private SavedStats savedStats;

    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        playerHP = player.GetComponent<Health>();
        playerXP = player.GetComponent<PlayerExperience>();

        timer = reloadTimeOrdinary;
    }
    private void Update()
    {
        if (startItem)
        {
            startItem = false;
            UpdateRegainHealth(true);
        }
        if (activated)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (playerHP.health < playerHP.maxHealth)
            {
                playerHP.health += itemTier[itemLevel].regainHealth;
                PlayerHud.playerHud.UpdateLifeBar();
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
                PlayerHud.playerHud.UpdateLifeBar();
                timer = reloadTimeOrdinary;
            }
        }

        
        
    }

    public void UpdateRegainHealth(bool _isStartItem)
    {
        if (!activated)
        {
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            activated = true;
            timer = itemTier[itemLevel].reloadTime;
        } 
        else
        {
            itemLevel++;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            
        }

        playerXP.CloseUpgradeScreen(this, _isStartItem);
        
    }
}
