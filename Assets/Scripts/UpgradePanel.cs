using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    private SavedStats savedStats;
   
    [Header("REVIVE")]
    public int reviveUpgradeCost = 1000;
    public TextMeshProUGUI reviveCostText;
    public Image [] reviveLevelIndicators;
    public Button reviveButton;

    [Header("PLUS ONE ITEM")]
    public int plusOneItemUpgradeCost = 1000;
    public TextMeshProUGUI plusOneItemUpgradeCostText;
    public Image[] plusOneItemLevelIndicators;
    public Button plusOneItemButton;

    [Header("START AT LEVEL 2")]
    public int level2BoostCost = 750;
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
        reviveCostText.text = "" + reviveUpgradeCost;
        plusOneItemUpgradeCostText.text = "" + plusOneItemUpgradeCost;
        level2BoostCostText.text = "" + level2BoostCost;

        //set Level Indicators for Upgrades 
        SetIndicators();
       
    }

    public void SetIndicators()
    {
        reviveCostText.text = "" + reviveUpgradeCost;
        plusOneItemUpgradeCostText.text = "" + plusOneItemUpgradeCost;

        CheckLevelIndicators(reviveLevelIndicators, savedStats.amountRevives, savedStats.maxAmountRevive,reviveButton,reviveCostText);
        CheckLevelIndicators(plusOneItemLevelIndicators, savedStats.amountExtraStartItems, savedStats.maxAmountExtraStartItems, plusOneItemButton,plusOneItemUpgradeCostText);
        CheckLevelIndicators(level2BoostLevelIndicators, savedStats.level2Boost, savedStats.maxAmountLevel2Boost, level2BoostButton, level2BoostCostText);
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

        if (savedStats.coins >= reviveUpgradeCost)
        {
            //savedStats.Revives(reviveUpgradeCost);
            savedStats.amountRevives++;
            savedStats.coins -= reviveUpgradeCost;
            // DataPersistanceManager.instance.SaveGame();
            CheckLevelIndicators(reviveLevelIndicators, savedStats.amountRevives, savedStats.maxAmountRevive,reviveButton,reviveCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
        }
        else warningText.text = "You can't afford this item!";
    }

    public void BuyOneExtraItem()
    {

        if (savedStats.coins >= plusOneItemUpgradeCost)
        {
            //savedStats.Revives(reviveUpgradeCost);
            savedStats.amountExtraStartItems++;
            savedStats.coins -= plusOneItemUpgradeCost;
            // DataPersistanceManager.instance.SaveGame();
            CheckLevelIndicators(plusOneItemLevelIndicators, savedStats.amountExtraStartItems, savedStats.maxAmountExtraStartItems,plusOneItemButton,plusOneItemUpgradeCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
        }
        else warningText.text = "You can't afford this item!";
    }

    public void BuyLevel2Boost()
    {

        if (savedStats.coins >= level2BoostCost)
        {
            savedStats.level2Boost++;
            savedStats.coins -= level2BoostCost;
            CheckLevelIndicators(level2BoostLevelIndicators, savedStats.level2Boost, savedStats.maxAmountLevel2Boost, level2BoostButton, level2BoostCostText);
            GetComponent<StartScreen>().UpdateCoinDisplay();
        }
        else warningText.text = "You can't afford this item!";
    }
}
