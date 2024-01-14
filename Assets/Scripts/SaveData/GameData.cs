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

    public bool enemiesKilled100;
    public bool enemiesKilled1000;
    public bool hankSavedLevel1;
    public bool allHumansSavedLevel1;
    public bool allKeysFound;
    public bool stayAliveFor20min;

    public List<GameObject> availebleItems;

    public GameData()
    {
        this.coinsStored = 0;
        this.amountRevive = 0;
        this.amountExtraStartItems = 0;
        this.level2Boost = 0;

        this.enemiesKilled100 = false;
        this.enemiesKilled1000 = false;
        this.hankSavedLevel1 = false;
        this.allHumansSavedLevel1 = false;
        this.allKeysFound = false;
        this.stayAliveFor20min = false;
        this.availebleItems = null;
    }
}
