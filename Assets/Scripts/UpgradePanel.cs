using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    private SavedStats savedStats;

    [Header("HEALTH")]
    //public int healthUpgradeCost = 200;
    public TextMeshProUGUI healthCostText;
    public Image[] healthLevelIndicators;
    public Button healthButton;

    [Header("SPEED")]
    //public int speedUpgradeCost = 200;
    public TextMeshProUGUI speedCostText;
    public Image[] speedLevelIndicators;
    public Button speedButton;

    [Header("XP")]
    //public int xPUpgradeCost = 1000;
    public TextMeshProUGUI xPCostText;
    public Image[] xPLevelIndicators;
    public Button xPButton;

    [Header("DMG")]
    //public int dMGUpgradeCost = 1000;
    public TextMeshProUGUI dMGCostText;
    public Image[] dMGLevelIndicators;
    public Button dMGButton;


    [Header("REVIVE")]
    //public int reviveUpgradeCost = 1000;
    public TextMeshProUGUI reviveCostText;
    public Image [] reviveLevelIndicators;
    public Button reviveButton;

    [Header("PLUS ONE ITEM")]
    //public int plusOneItemUpgradeCost = 1000;
    public TextMeshProUGUI plusOneItemUpgradeCostText;
    public Image[] plusOneItemLevelIndicators;
    public Button plusOneItemButton;

    [Header("START AT LEVEL 2")]
    //public int level2BoostCost = 750;
    public TextMeshProUGUI level2BoostCostText;
    public Image[] level2BoostLevelIndicators;
    public Button level2BoostButton;

    [Header("GENERAL")]
    public Sprite lit;
    public Sprite unLit;
    public TextMeshProUGUI warningText;

    // Start is called before the first frame update
    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        warningText.text = "";

        //set Level Indicators for Upgrades 
        SetIndicators();
       
    }

    public void SetIndicators()
    {
        UpdatePrice();

        CheckLevelIndicators(reviveLevelIndicators, savedStats.amountRevives, savedStats.maxAmountRevive,reviveButton,reviveCostText);
        CheckLevelIndicators(plusOneItemLevelIndicators, savedStats.amountExtraStartItems, savedStats.maxAmountExtraStartItems, plusOneItemButton,plusOneItemUpgradeCostText);
        CheckLevelIndicators(level2BoostLevelIndicators, savedStats.level2Boost, savedStats.maxAmountLevel2Boost, level2BoostButton, level2BoostCostText);
        CheckLevelIndicators(healthLevelIndicators, savedStats.healthLevel, savedStats.healthMaxLevel, healthButton, healthCostText);
        CheckLevelIndicators(speedLevelIndicators, savedStats.speedLevel, savedStats.speedMaxLevel, speedButton, speedCostText);
        CheckLevelIndicators(xPLevelIndicators, savedStats.xPLevel, savedStats.xPMaxLevel, xPButton, xPCostText);
        CheckLevelIndicators(dMGLevelIndicators, savedStats.dMGLevel, savedStats.dMGMaxLevel, dMGButton, dMGCostText);
    }

    public void UpdatePrice()
    {
      
        reviveCostText.text = "" + savedStats.reviveUpgradeCost;
        plusOneItemUpgradeCostText.text = "" + savedStats.plus1ItemUpgradeCost;
        level2BoostCostText.text = "" + savedStats.level2ItemUpgradeCost;
        healthCostText.text = "" + savedStats.healthUpgradeCost;
        speedCostText.text = "" + savedStats.speedUpgradeCost;
        xPCostText.text = "" + savedStats.xPUpgradeCost;
        dMGCostText.text = "" + savedStats.dMGUpgradeCost;
    } 
    public void CheckLevelIndicators(Image[] indicatorsToCheck, int level, int maxLevel, Button button, TextMeshProUGUI priceTag)
    {
        int tempLevel = level;

        for (int i = 0; i < maxLevel; i++)
        {
            if (tempLevel > 0)
            {
                indicatorsToCheck[i].sprite = lit;
                tempLevel--;
            }
            else
            {
                indicatorsToCheck[i].sprite = unLit;
            }
        }

        if (level == maxLevel)
        {
            button.interactable = false;
            priceTag.text = "Fully Upgraded";
        }
    }

    public void ResetTextElement()
    {
        warningText.text = "";
    }

    public void BuyRevive()
    {

        if (savedStats.coins >= savedStats.reviveUpgradeCost)
        {
   
            savedStats.amountRevives++;
            savedStats.coins -= savedStats.reviveUpgradeCost;

            float tempIncrementedCost = savedStats.reviveUpgradeCost * 2.4f;
            savedStats.reviveUpgradeCost = (int)tempIncrementedCost;

            CheckLevelIndicators(reviveLevelIndicators, savedStats.amountRevives, savedStats.maxAmountRevive,reviveButton,reviveCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
            UpdatePrice();
        }
        else warningText.text = "You can't afford this item!";
    }

    public void BuyOneExtraItem()
    {

        if (savedStats.coins >= savedStats.plus1ItemUpgradeCost)
        {
           
            savedStats.amountExtraStartItems++;
            savedStats.coins -= savedStats.plus1ItemUpgradeCost;
      
            CheckLevelIndicators(plusOneItemLevelIndicators, savedStats.amountExtraStartItems, savedStats.maxAmountExtraStartItems,plusOneItemButton,plusOneItemUpgradeCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
            UpdatePrice();
        }
        else warningText.text = "You can't afford this item!";
    }

    public void BuyLevel2Boost()
    {

        if (savedStats.coins >= savedStats.level2ItemUpgradeCost)
        {
            savedStats.level2Boost++;
            savedStats.coins -= savedStats.level2ItemUpgradeCost;
            CheckLevelIndicators(level2BoostLevelIndicators, savedStats.level2Boost, savedStats.maxAmountLevel2Boost, level2BoostButton, level2BoostCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
            UpdatePrice();
        }
        else warningText.text = "You can't afford this item!";
    }

    public void BuyHealthBoost()
    {
        if (savedStats.coins >= savedStats.healthUpgradeCost)
        {
            savedStats.healthLevel++;
            savedStats.coins -= savedStats.healthUpgradeCost;

            savedStats.extraHealth *= 1.2f;

            float tempIncrementedCost = savedStats.healthUpgradeCost * 1.5f;
            savedStats.healthUpgradeCost = (int)tempIncrementedCost;

            CheckLevelIndicators(healthLevelIndicators, savedStats.healthLevel, savedStats.healthMaxLevel, healthButton, healthCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
            UpdatePrice();
        }
        else warningText.text = "You can't afford this item!";
    }
    public void BuySpeedBoost()
    {
        if (savedStats.coins >= savedStats.speedUpgradeCost)
        {
            savedStats.speedLevel++;
            savedStats.coins -= savedStats.speedUpgradeCost;

            savedStats.extraSpeed *= 1.1f;
            savedStats.extraMaxSpeed *= 1.1f;

            float tempIncrementedCost = savedStats.speedUpgradeCost * 1.5f;
            savedStats.speedUpgradeCost = (int)tempIncrementedCost;

            CheckLevelIndicators(speedLevelIndicators, savedStats.speedLevel, savedStats.speedMaxLevel, speedButton, speedCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
        }
        else warningText.text = "You can't afford this item!";
       
    }
    public void BuyXPBoost()
    {
        if (savedStats.coins >= savedStats.xPUpgradeCost)
        {
            savedStats.xPLevel++;
            savedStats.coins -= savedStats.xPUpgradeCost;

            savedStats.extraXPBoost *= 1.2f;

            float tempIncrementedCost = savedStats.xPUpgradeCost * 1.4f;
            savedStats.xPUpgradeCost = (int)tempIncrementedCost;

            CheckLevelIndicators(xPLevelIndicators, savedStats.xPLevel, savedStats.xPMaxLevel, xPButton, xPCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
        }
        else warningText.text = "You can't afford this item!";
      
    }
    public void BuyDMGBoost()
    {
        if (savedStats.coins >= savedStats.dMGUpgradeCost)
        {
            savedStats.dMGLevel++;
            savedStats.coins -= savedStats.dMGUpgradeCost;
           
           
            
                savedStats.extraDMG *= 1.25f;
            
            

            float tempIncrementedCost = savedStats.dMGUpgradeCost * 1.75f;
            savedStats.dMGUpgradeCost = (int)tempIncrementedCost;

            CheckLevelIndicators(dMGLevelIndicators, savedStats.dMGLevel, savedStats.dMGMaxLevel, dMGButton, dMGCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
            UpdatePrice();
        }
        else warningText.text = "You can't afford this item!";
       
    }
}
