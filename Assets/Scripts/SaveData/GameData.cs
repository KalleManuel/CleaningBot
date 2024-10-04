using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coinsStored;
    public int amountRevive;
    public int amountExtraStartItems;
    public int level2Boost;
    public int healthLevel;
    public int speedLevel;
    public int xPLevel;
    public int dMGLevel;

    public int reviveUpgradeCost;
    public int level2ItemUpgradeCost;
    public int plus1ItemUpgradeCost;
    public int healthUpgradeCost;
    public int speedUpgradeCost;
    public int xPUpgradeCost;
    public int dMGUpgradeCost;

    public bool enemiesKilled100;
    public bool enemiesKilled1000;
    public bool hankSavedLevel1;
    public bool allHumansSavedLevel1;
    public bool allKeysFound;
    public bool stayAliveFor10min;
    public bool stayAliveFor20min;

    public float extraSpeed;
    public float extraMaxSpeed;
    public float extraHealth;
    public float extraXPBoost;
    public float extraDMG; 

    public List<int> availebleItems;

    public GameData()
    {
        this.coinsStored = 0;
        this.amountRevive = 0;
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
        this.enemiesKilled1000 = false;
        this.hankSavedLevel1 = false;
        this.allHumansSavedLevel1 = false;
        this.allKeysFound = false;
        this.stayAliveFor20min = false;
        this.stayAliveFor10min = false;
        this.availebleItems = null; //is this wrong?

        this.reviveUpgradeCost = 0;
        this.level2ItemUpgradeCost = 0;
        this.plus1ItemUpgradeCost = 0;
        this.healthUpgradeCost = 0;
        this.speedUpgradeCost = 0;
        this.xPUpgradeCost = 0;
        this.dMGUpgradeCost = 0;


    }
}
