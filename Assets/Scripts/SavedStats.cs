using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStats : MonoBehaviour, IDataPersistance
{
    public int coins;
    public int tempCoins;

    public int amountRevives = 0;
    public int maxAmountRevive = 2;
    public GameObject[] humansSaved;


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
        this.coins = data.coinsStored;
        this.amountRevives = data.amountRevive;
    }

    public void SaveData(ref GameData data)
    {
        data.coinsStored = this.coins;
        data.amountRevive = this.amountRevives;
    }

    public void ResetData()
    {
        this.coins = 0;
        this.amountRevives = 0;

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

}
