using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coinsStored;
    public int amountRevive;

    public GameData()
    {
        this.coinsStored = 0;
        this.amountRevive = 0;
    }
}
