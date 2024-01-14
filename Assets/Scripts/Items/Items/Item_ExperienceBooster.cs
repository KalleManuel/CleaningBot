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

    public void UpgradeXPBoost()
    {
        if (!activated)
        {
            activated = true;
            playerXP.experienceBoost = itemTier[itemLevel].experienceBoost;
           
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            playerXP.CloseUpgradeScreen(this);
        }
        else
        {
            itemLevel++;
            playerXP.experienceBoost = itemTier[itemLevel].experienceBoost;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            playerXP.CloseUpgradeScreen(this);
        }

    }
}
