using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStats : MonoBehaviour, IDataPersistance
{
    [Header("Currency")]
    public int coins;
    public int tempCoins;

    [Header("Upgrade Revive")]
    public int amountRevives = 0;
    public int maxAmountRevive = 2;
    public int reviveUpgradeCost;
    public int reviveUpgradeStartCost;

    [Header("Upgrade StartWithExtraItem")]
    public int amountExtraStartItems;
    public int maxAmountExtraStartItems = 1;
    public int plus1ItemUpgradeCost;
    public int plus1ItemUpgradeStartCost;

    [Header("Upgrade Upgrade StartItemsAtLevel2")]
    public int level2Boost;
    public int maxAmountLevel2Boost = 1;
    public int level2ItemUpgradeCost;
    public int level2UpgradeStartCost;

    [Header("Upgrade Health")]
    public int healthLevel;
    public int healthMaxLevel = 4;
    public int healthUpgradeCost;
    public int healthUpgradeStartCost;
    public float extraHealth;

    [Header("Upgrade Speed")]
    public int speedLevel;
    public int speedMaxLevel = 4;
    public int speedUpgradeCost;
    public int speedUpgradeStartCost;
    public float extraSpeed;
    public float extraMaxSpeed;

    [Header("Upgrade ExperienceGain")]
    public int xPLevel;
    public int xPMaxLevel = 4;
    public int xPUpgradeCost;
    public int xPUpgradeStartCost;
    public float extraXPBoost;

    [Header("Upgrade DamageDelt")]
    public int dMGLevel;
    public int dMGMaxLevel =4;
    public int dMGUpgradeCost;
    public int DmgUpgradeStartCost;
    public float extraDMG;

    [Header("Objectives Level 1")]
    public bool enemiesKilled100;
    public bool enemiesKilled500;
    public bool hankSavedLevel1;
    public bool allHumansSavedLevel1;
    public bool allKeysFound;
    public bool stayAliveFor10min;
    public bool stayAliveFor20min;
    
    [Header("Stats")]
    public int enemiesKilledLatestRun;
    public int enemiesKilledInTotal;
    

    public float timeAliveLatestRun;


    [Header("Controls")]

    public bool touchControls = false;

    [Header("Weapons and Items")]

    public GameObject[] allItems;
    
    public GameObject[] startItems;
    public GameObject firstWeapon;

    public List<GameObject> availebleItemsGO;

    public List<int> availebleItems;


    private static GameObject instance;


    void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = gameObject; // is this correct?
        }
        else Destroy(gameObject);

    }
    private void Start()
    {
        if (availebleItemsGO == null)
        {
            AddAvailibleItems();
        }

        if (reviveUpgradeCost == 0)
        {
            this.reviveUpgradeCost = reviveUpgradeStartCost;
            this.level2ItemUpgradeCost = level2UpgradeStartCost;
            this.plus1ItemUpgradeCost = plus1ItemUpgradeStartCost;
            this.healthUpgradeCost = healthUpgradeStartCost;
            this.speedUpgradeCost = speedUpgradeStartCost;
            this.xPUpgradeCost = xPUpgradeStartCost;
            this.dMGUpgradeCost = DmgUpgradeStartCost;
        }
    }

    public void LoadData(GameData data)
    {
        //currency load
        this.coins = data.coinsStored;

        //updates load
        this.amountRevives = data.amountRevive;
        this.amountExtraStartItems = data.amountExtraStartItems;
        this.level2Boost = data.level2Boost;
        this.healthLevel = data.healthLevel;
        this.speedLevel = data.speedLevel;
        this.xPLevel = data.xPLevel;
        this.dMGLevel = data.dMGLevel;

        this.reviveUpgradeCost = data.reviveUpgradeCost;
        this.level2ItemUpgradeCost = data.level2ItemUpgradeCost;
        this.plus1ItemUpgradeCost = data.plus1ItemUpgradeCost;
        this.healthUpgradeCost = data.healthUpgradeCost;
        this.speedUpgradeCost = data.speedUpgradeCost;
        this.xPUpgradeCost = data.xPUpgradeCost;
        this.dMGUpgradeCost = data.dMGUpgradeCost;

        this.extraSpeed = data.extraSpeed;
        this.extraMaxSpeed = data.extraMaxSpeed;
        this.extraHealth = data.extraHealth;
        this.extraXPBoost = data.extraXPBoost;
        this.extraDMG = data.extraDMG;


        //objectives load
        this.enemiesKilled100 = data.enemiesKilled100;
        this.enemiesKilled500 = data.enemiesKilled1000;
        this.hankSavedLevel1 = data.hankSavedLevel1;
        this.allHumansSavedLevel1 = data.allHumansSavedLevel1;
        this.allKeysFound = data.allKeysFound;
        this.stayAliveFor10min = data.stayAliveFor10min;
        this.stayAliveFor20min = data.stayAliveFor20min;

        this.availebleItems = data.availebleItems;

        if (availebleItemsGO == null)
        {
            LoadAvailibleItems();
        } 
    }

    public void SaveData(ref GameData data)
    {
        data.coinsStored = this.coins;
        data.amountRevive = this.amountRevives;
        data.amountExtraStartItems = this.amountExtraStartItems;
        data.level2Boost = this.level2Boost;

        data.healthLevel = this.healthLevel;
        data.speedLevel = this.speedLevel;
        data.xPLevel = this.xPLevel;
        data.dMGLevel = this.dMGLevel;

        data.reviveUpgradeCost = this.reviveUpgradeCost;
        data.level2ItemUpgradeCost = this.level2ItemUpgradeCost;
        data.plus1ItemUpgradeCost = this.plus1ItemUpgradeCost;
        data.healthUpgradeCost = this.healthUpgradeCost;
        data.speedUpgradeCost = this.speedUpgradeCost;
        data.xPUpgradeCost = this.xPUpgradeCost;
        data.dMGUpgradeCost = this.dMGUpgradeCost;

        data.extraSpeed = this.extraSpeed;
        data.extraMaxSpeed = this.extraMaxSpeed;
        data.extraHealth = this.extraHealth;
        data.extraXPBoost = this.extraXPBoost;
        data.extraDMG = this.extraDMG;


        data.enemiesKilled100 = this.enemiesKilled100;
        data.enemiesKilled1000 = this.enemiesKilled500;
        data.hankSavedLevel1 = this.hankSavedLevel1;
        data.allHumansSavedLevel1 = this.allHumansSavedLevel1;
        data.allKeysFound = this.allKeysFound;
        data.stayAliveFor10min = this.stayAliveFor10min;
        data.stayAliveFor20min = this.stayAliveFor20min;
        data.availebleItems = this.availebleItems;
    }

    public void ResetData()
    {
        this.coins = 0;
        this.amountRevives = 0;
        this.amountExtraStartItems = 0;
        this.level2Boost = 0;

        this.healthLevel = 0;
        this.speedLevel = 0;
        this.xPLevel = 0;
        this.dMGLevel = 0;

        this.extraSpeed = 1;
        this.extraMaxSpeed = 1;
        this.extraHealth = 1;
        this.extraXPBoost = 1;
        this.extraDMG = 1;

        this.enemiesKilled100 = false;
        this.enemiesKilled500 = false;
        this.hankSavedLevel1 = false;
        this.allHumansSavedLevel1 = false;
        this.allKeysFound = false;
        this.stayAliveFor10min = false;
        this.stayAliveFor20min = false;

        this.availebleItems.Clear();
        AddAvailibleItems();

        this.reviveUpgradeCost = reviveUpgradeStartCost;
        this.level2ItemUpgradeCost = level2UpgradeStartCost;
        this.plus1ItemUpgradeCost = plus1ItemUpgradeStartCost;
        this.healthUpgradeCost = healthUpgradeStartCost;
        this.speedUpgradeCost = speedUpgradeStartCost;
        this.xPUpgradeCost = xPUpgradeStartCost;
        this.dMGUpgradeCost = DmgUpgradeStartCost;

        DataPersistanceManager.instance.SaveGame();

    }

    public void AddtempCoins(int coinsSoFar)
    {
        tempCoins = coinsSoFar;
    }

    public void CollectCoins()
    {
        coins += tempCoins;
        tempCoins = 0;
    }
    public void AddAvailibleItems()
    {
        if (availebleItemsGO != null)
        {
            availebleItemsGO.Clear();
        }

        for (int i = 0; i < startItems.Length; i++)
        {
            availebleItemsGO.Add(startItems[i]);
            availebleItems.Add(startItems[i].GetComponent<Item>().weaponID);
        }
    }

    public void LoadAvailibleItems()
    {
        if (availebleItemsGO != null)
        {
            availebleItemsGO.Clear();
        }

        for (int i = 0; i < availebleItems.Count; i++)
        {
            for (int e = 0; e < allItems.Length; i++)
            {
                if (allItems[i].GetComponent<Item>().weaponID == availebleItems[i])
                {
                    availebleItemsGO.Add(allItems[i]);
                }

            }

        }
    }

    public void CheckOcjectives()
    {    
        if (!enemiesKilled100)
        {
            enemiesKilledLatestRun = GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().enemiesKilled;
            if (enemiesKilledLatestRun >= 100)
            {
                enemiesKilled100 = true;
                availebleItemsGO.Add(allItems[9]);
                availebleItems.Add(allItems[9].GetComponent<Item>().weaponID); //Adds GPS to the list of items
                
                if (enemiesKilledLatestRun < 500)
                {
                    enemiesKilledInTotal += enemiesKilledLatestRun;
                } 
            }
        }

        if (!enemiesKilled500)
        {
            enemiesKilledLatestRun = GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().enemiesKilled;
            if (enemiesKilledLatestRun >= 500)
            {
                enemiesKilled500 = true;
                availebleItemsGO.Add(allItems[4]); // adds missile Launcher
                availebleItems.Add(allItems[4].GetComponent<Item>().weaponID);
                enemiesKilledInTotal += enemiesKilledLatestRun;
            }
        }

        if (!allKeysFound)
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().keys >= 4)
            {
                allKeysFound = true;
            }
        }

        if (!stayAliveFor10min)
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Timer>().minutes >= 10)
            {
                stayAliveFor10min = true;
                
            }
        }

        if (!stayAliveFor20min)
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Timer>().minutes >= 20)
            {
                stayAliveFor20min = true;
                availebleItemsGO.Add(allItems[2]); // add Acid Launcher
                availebleItems.Add(allItems[2].GetComponent<Item>().weaponID);
            }
        }

        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().levelCleared)
        {
            if (!hankSavedLevel1)
            {
                PlayerCompanions humans = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCompanions>();

                for (int i = 0; i < humans.humanCompanions.Count; i++)
                {
                    if (humans.humanCompanions[i].gameObject.name == "Hank")
                    {
                        availebleItemsGO.Add(allItems[3]); // Laser Clean
                        availebleItems.Add(allItems[3].GetComponent<Item>().weaponID);
                        hankSavedLevel1 = true;
                    }
                }
            }
            if (!allHumansSavedLevel1)
            {
                if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().humansSaved >= 6)
                {
                    allHumansSavedLevel1 = true;
                    // Unlocks level 2
                }

            }
        }
       
        
    } 
}
