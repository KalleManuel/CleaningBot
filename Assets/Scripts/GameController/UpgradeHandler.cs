using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeHandler : MonoBehaviour
{
    public GameObject[] upgradeButtons;
    public GameObject slotholder;
    public GameObject upgradePool;

    public List<GameObject> availableItems;

    public List<GameObject> activatedUpgrades;
    private bool inventoryFull;

    public int buttonsLit;
    
    public GameObject[] currencyButtons;

    Pause pause;

    private SavedStats savedStats;

    private void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        pause = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>();

        for (int i = 0; i < currencyButtons.Length; i++)
        {
            currencyButtons[i].SetActive(false);
        }

        for (int e = 0; e < upgradeButtons.Length; e++)
        {
            upgradeButtons[e].SetActive(false);
        }

    }

    public void UpgradeScrambler(int amountChoices)
    {
        // Find out wich items that are currently availeble

        for (int i = 0; i < savedStats.availebleItemsGO.Count; i++)
        {
            string itemToCompare = savedStats.availebleItemsGO[i].GetComponent<Item>().itemName;
            

            for (int e = 0; e < upgradeButtons.Length; e++)
            {
                Item itemToLookFor = upgradeButtons[e].GetComponent<ItemInfoRevealer>().upgrade;
                string itemName = itemToLookFor.itemName;
                

                if (itemToCompare == itemName)
                {

                    availableItems.Add(upgradeButtons[e]);
                }  
            }
        }

        // set how meny alternativs player get
        
        buttonsLit = amountChoices;


        for (int i = 0; i < availableItems.Count; i++)
        {
            availableItems[i].SetActive(true);
            availableItems[i].transform.SetParent(upgradePool.transform);
            availableItems[i].GetComponent<ItemInfoRevealer>().SetStatus();
            availableItems[i].GetComponent<ItemInfoRevealer>().chosen = false;

        }

        if (!inventoryFull)
        {
            Scrambler(Random.Range(0, upgradeButtons.Length));
        } 
        else 
        {
            for (int i = 0; i < activatedUpgrades.Count; i++)
            {
                if (activatedUpgrades[i].GetComponent<ItemInfoRevealer>().notUpgradeble)
                {
                    activatedUpgrades.Remove(activatedUpgrades[i]);
                }
            }

            if (activatedUpgrades.Count > 3)
            {
                ScrambleAllActivated(Random.Range(0, activatedUpgrades.Count));
            }
            else
            {
                DisplayWhatsLeft();
            }

        }
    }

    public void Scrambler(int random)
    {
        ItemInfoRevealer itemToUpgrade = availableItems[random].GetComponent<ItemInfoRevealer>();

        if (!itemToUpgrade.chosen)
        {
            if (!itemToUpgrade.notUpgradeble)
            {
                availableItems[random].transform.parent = slotholder.transform;
                availableItems[random].SetActive(true);
                buttonsLit--;
                itemToUpgrade.chosen = true;

                if (buttonsLit != 0)
                {
                    Scrambler(Random.Range(0, availableItems.Count));
                }
                else
                {
                    for (int i = 0; i< availableItems.Count; i++)
                    {
                        if (!availableItems[i].GetComponent<ItemInfoRevealer>().chosen)
                        {
                            availableItems[i].SetActive(false);
                        }
                    }

                    pause.PauseGame(false);
                }

            }
            else Scrambler(Random.Range(0, availableItems.Count));

        }
        else Scrambler(Random.Range(0, availableItems.Count));

    }

    public void ScrambleAllActivated(int random)
    {
        ItemInfoRevealer itemToUpgrade = activatedUpgrades[random].GetComponent<ItemInfoRevealer>();

        if (!itemToUpgrade.chosen)
        {
            if (!itemToUpgrade.notUpgradeble)
            {
                activatedUpgrades[random].transform.parent = slotholder.transform;
                activatedUpgrades[random].SetActive(true);
                buttonsLit--;
                itemToUpgrade.chosen = true;

                if (buttonsLit != 0)
                {
                    ScrambleAllActivated(Random.Range(0, activatedUpgrades.Count));
                }
                else
                {
                    for (int i = 0; i < upgradeButtons.Length; i++)
                    {
                        if (!upgradeButtons[i].GetComponent<ItemInfoRevealer>().chosen)
                        {
                            upgradeButtons[i].SetActive(false);
                        }
                    }

                    pause.PauseGame(false);
                }

            }
            else ScrambleAllActivated(Random.Range(0, activatedUpgrades.Count));

        }
        else ScrambleAllActivated(Random.Range(0, activatedUpgrades.Count));

    }

    public void AddToActivated()
    {
        if (!inventoryFull)
        {
            for (int i = 0; i < availableItems.Count; i++)
            {
                if (availableItems[i].GetComponent<ItemInfoRevealer>().activated)
                {
                    if (!activatedUpgrades.Contains(availableItems[i]))
                        activatedUpgrades.Add(availableItems[i]);
                }
            }

            if (activatedUpgrades.Count == 12)
            {
              
                inventoryFull = true;
            }
        }
        
    }

    public void DisplayWhatsLeft()
    {
        for (int s = 0; s < currencyButtons.Length; s++)
        {
            currencyButtons[s].transform.parent = upgradePool.transform;
            currencyButtons[s].SetActive(false);
        }
        for (int i = 0; i < activatedUpgrades.Count; i++)
        {
            ItemInfoRevealer itemToUpgrade = activatedUpgrades[i].GetComponent<ItemInfoRevealer>();
            activatedUpgrades[i].transform.parent = slotholder.transform;
            activatedUpgrades[i].SetActive(true);
            buttonsLit--;
            itemToUpgrade.chosen = true;
        }
        if (buttonsLit != 0)
        {
            for (int i = 0; i < buttonsLit; i++)
            {
                 
                currencyButtons[i].transform.parent = slotholder.transform;
                currencyButtons[i].SetActive(true);
            }
        }
        else pause.PauseGame(false);
    }

}
        

    


