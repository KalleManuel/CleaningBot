using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStats : MonoBehaviour, IDataPersistance
{
    [Header("Currency")]
    public int coins;
    public int tempCoins;

    [Header("Upgrades")]
    public int amountRevives = 0;
    public int maxAmountRevive = 2;

    public int amountExtraStartItems;
    public int maxAmountExtraStartItems = 1;

    public int level2Boost;
    public int maxAmountLevel2Boost = 1;

    [Header("Objectives Level 1")]
    public bool enemiesKilled100;
    public bool enemiesKilled1000;
    public bool hankSavedLevel1;
    public bool allHumansSavedLevel1;
    public bool allKeysFound;
    public bool stayAliveFor20min;
    
    [Header("Stats")]
    public int enemiesKilledLatestRun;
    public int enemiesKilledInTotal;
    

    public float timeAliveLatestRun;


    [Header("Controls")]

    public bool touchControls = false;

    //test

    public GameObject[] allItems;
    public List<GameObject> availebleItems;
    public GameObject startItem;

    private static GameObject instance;

    // Start is called before the first frame update
    void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = gameObject;
        }
        else Destroy(gameObject);

    }
    public void LoadData(GameData data)
    {
        //currency load
        this.coins = data.coinsStored;

        //updates load
        this.amountRevives = data.amountRevive;
        this.amountExtraStartItems = data.amountExtraStartItems;
        this.level2Boost = data.level2Boost;

        //objectives load
        this.enemiesKilled100 = data.enemiesKilled100;
        this.enemiesKilled1000 = data.enemiesKilled1000;
        this.hankSavedLevel1 = data.hankSavedLevel1;
        this.allHumansSavedLevel1 = data.allHumansSavedLevel1;
        this.allKeysFound = data.allKeysFound;
        this.stayAliveFor20min = data.stayAliveFor20min;

        this.availebleItems = data.availebleItems;
    }

    public void SaveData(ref GameData data)
    {
        data.coinsStored = this.coins;
        data.amountRevive = this.amountRevives;
        data.amountExtraStartItems = this.amountExtraStartItems;
        data.level2Boost = this.level2Boost;

        data.enemiesKilled100 = this.enemiesKilled100;
        data.enemiesKilled1000 = this.enemiesKilled1000;
        data.hankSavedLevel1 = this.hankSavedLevel1;
        data.allHumansSavedLevel1 = this.allHumansSavedLevel1;
        data.allKeysFound = this.allKeysFound;
        data.stayAliveFor20min = this.stayAliveFor20min;
        data.availebleItems = this.availebleItems;
    }

    public void ResetData()
    {
        this.coins = 0;
        this.amountRevives = 0;
        this.amountExtraStartItems = 0;
        this.level2Boost = 0;

        this.enemiesKilled100 = false;
        this.enemiesKilled1000 = false;
        this.hankSavedLevel1 = false;
        this.allHumansSavedLevel1 = false;
        this.allKeysFound = false;
        this.stayAliveFor20min = false;

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

    public void CheckOcjectives()
    {
   
        
        if (!enemiesKilled100)
        {
            enemiesKilledLatestRun = GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().enemiesKilled;
            if (enemiesKilledLatestRun >= 100)
            {
                enemiesKilled100 = true;
                
                if (enemiesKilledLatestRun < 1000)
                {
                    enemiesKilledInTotal += enemiesKilledLatestRun;
                } 
            }
        }

        if (!enemiesKilled1000)
        {
            enemiesKilledLatestRun = GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().enemiesKilled;
            if (enemiesKilledLatestRun >= 100)
            {
                enemiesKilled100 = true;
                enemiesKilledInTotal += enemiesKilledLatestRun;
            }
        }
        if (!allKeysFound)
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().keys >= 5)
            {
                allKeysFound = true;
            }
        }
        if (!stayAliveFor20min)
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Timer>().minutes >= 20)
            {
                stayAliveFor20min = true;
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
                        hankSavedLevel1 = true;
                    }
                }
            }
            if (!allHumansSavedLevel1)
            {
                if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().humansSaved >= 6)
                {
                    allHumansSavedLevel1 = true;
                }

            }
        }
       
        
    }
    
  
}
