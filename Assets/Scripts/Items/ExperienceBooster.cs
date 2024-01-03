using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBooster : Item
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

    private void Start()
    {
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
    }

    public void UpgradeXPBoost()
    {
        if (!activated)
        {
            activated = true;
            playerXP.experienceBoost = itemTier[itemLevel].experienceBoost;
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
