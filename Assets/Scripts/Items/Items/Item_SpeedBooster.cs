using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_SpeedBooster : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float speed;
        public float acceleration;
        public float glide;

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

    public void UpgradeSpeed()
    {
        if (!activated)
        {
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            activated = true;
            playerSpeed.maxSpeed = itemTier[itemLevel].speed;
            playerSpeed.acceleration = itemTier[itemLevel].acceleration;
            playerSpeed.glideFactor = itemTier[itemLevel].glide;
            playerXP.CloseUpgradeScreen(this);
        }
        else
        {
            itemLevel++;
            playerSpeed.maxSpeed = itemTier[itemLevel].speed;
            playerSpeed.acceleration = itemTier[itemLevel].acceleration;
            playerSpeed.glideFactor = itemTier[itemLevel].glide;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            playerXP.CloseUpgradeScreen(this);
        }

    }
}
