using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ExperienceBooster : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float experienceBoost;

    }
    public ItemTier[] itemTier;

    private PlayerExperience playerXP;
    private PlayerMovment playerSpeed;

    private SavedStats savedStats;

    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
    }

    private void Update()
    {
        if (startItem)
        {
            startItem = false;
            UpgradeXPBoost(true);
        }
    }

    public void UpgradeXPBoost(bool _isStartItem)
    {
        if (!activated)
        {
            activated = true;
            playerXP.experienceBoost = itemTier[itemLevel].experienceBoost;
           
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }
        }
        else
        {
            itemLevel++;
            playerXP.experienceBoost = itemTier[itemLevel].experienceBoost;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            
        }

        playerXP.CloseUpgradeScreen(this,_isStartItem);
    }
}
