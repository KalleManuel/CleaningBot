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

        //set Level Indicators for Revive 
        SetIndicators();
       
    }

    public void BuyRevive()
    {

        if (savedStats.coins >= reviveUpgradeCost)
        {
            //savedStats.Revives(reviveUpgradeCost);
            savedStats.amountRevives++;
            savedStats.coins -= reviveUpgradeCost;
           // DataPersistanceManager.instance.SaveGame();
            CheckLevelIndicators(reviveLevelIndicators, savedStats.amountRevives, savedStats.maxAmountRevive);
            GetComponent<StartScreen>().UpdateCoinDisplay();

            if (savedStats.amountRevives == savedStats.maxAmountRevive)
            {
                reviveButton.interactable = false;
            }

        }
        else warningText.text = "You can't afford this item!";
    }

    public void SetIndicators()
    {
        reviveCostText.text = "" + reviveUpgradeCost;
        CheckLevelIndicators(reviveLevelIndicators, savedStats.amountRevives, savedStats.maxAmountRevive);
    }
    public void CheckLevelIndicators(Image[] indicatorsToCheck, int level, int maxLevel)
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
    }
    public void ResetTextElement()
    {
        warningText.text = "";
    }
}
