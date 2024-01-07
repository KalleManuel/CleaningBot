using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coinsStored;
    public int amountRevive;
    public int amountExtraStartItems;

    public GameData()
    {
        this.coinsStored = 0;
        this.amountRevive = 0;
        this.amountExtraStartItems = 0;
    }
}
