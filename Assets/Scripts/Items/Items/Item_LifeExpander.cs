using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_LifeExpander : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public int extraLife = 10;

    }
    public ItemTier[] itemTier;

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
    }
    private void Update()
    {
        if (startItem)
        {
            startItem = false;
            UpdateLifeExpander(true);
        }
    }

    public void UpdateLifeExpander(bool _isStartItem)
    {
        if (!activated)
        {
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            activated = true;
            playerHP.maxHealth += itemTier[itemLevel].extraLife;
            playerHud.UpdateLifeBar();
            
        }
        else
        {
            itemLevel++;
            playerHP.maxHealth += itemTier[itemLevel].extraLife;
            playerHud.UpdateLifeBar();

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            
        }
        playerXP.CloseUpgradeScreen(this,_isStartItem);   
    }
}
