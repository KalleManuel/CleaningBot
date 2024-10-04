using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public int bloodClots, debris, enemiesKilled, humansSaved, plungersInScene, trash, keys;
    public bool levelCleared;
 

    // Start is called before the first frame update
    void Start()
    {
        bloodClots = 0;
        debris = 0;
        enemiesKilled = 0;
        humansSaved = 0;
        plungersInScene = 0;
        trash = 0;
        keys = 0;
        levelCleared = false;

    }
    public void UpdateStats()
    {
        bloodClots = GameObject.FindGameObjectsWithTag("Blood").Length;
        plungersInScene = GameObject.FindGameObjectsWithTag("Bullet").Length;
        humansSaved = PlayerHud.playerHud.humansWithPlayer;
        debris = GameObject.FindGameObjectsWithTag("Debris").Length;
        trash = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().trash;
    }
}