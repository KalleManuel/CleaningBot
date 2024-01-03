using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInfoRevealer : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Item upgrade;
    public LitLevelLights lightPanel;
    public bool notUpgradeble, chosen, activated;

    private void Start()
    {
        if (upgrade.activated)
        {
            activated = true;
        }
    }
    public void SetStatus()
    {
        if (upgrade.activated)
        {
            activated = true;
        }

        if (upgrade.maxLevelReached)
        {
            notUpgradeble = true;
        }

    }

    public void ShowInfo()
    {
        if (upgrade.itemLevel < (upgrade.itemMaxLevel - 1))
        {
            title.text = upgrade.itemName;
            description.text = upgrade.levelDescription[upgrade.itemLevel];
            lightPanel.LevelLights(upgrade);
        }
        else if (upgrade.itemLevel == (upgrade.itemMaxLevel - 1))
        {
            title.text = upgrade.itemName;
            description.text = "This item has reached its maxlevel. It can not be upgraded";
            lightPanel.LevelLights(upgrade);
        }
    }

    public void HideInfo()
    {
        title.text = string.Empty;
        description.text = string.Empty;
    }
}
