using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeExpander : Item
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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHud = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHud>();
        playerHP = player.GetComponent<Health>();
        playerXP = player.GetComponent<PlayerExperience>();
    }

    public void UpdateLifeExpander()
    {
        if (!activated)
        {
            activated = true;
            playerHP.maxHealth += itemTier[itemLevel].extraLife;
            playerHud.UpdateLifeBar();
            playerXP.CloseUpgradeScreen(this);
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

            playerXP.CloseUpgradeScreen(this);
        }
        
      
    }
}
