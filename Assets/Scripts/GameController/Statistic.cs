using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public int bloodClots, debris, enemiesKilled, humansSaved, plungersInScene, trash;
 

    // Start is called before the first frame update
    void Start()
    {
        bloodClots = 0;
        debris = 0;
        enemiesKilled = 0;
        humansSaved = 0;
        plungersInScene = 0;
        trash = 0;

    }
    public void UpdateStats()
    {
        bloodClots = GameObject.FindGameObjectsWithTag("Blood").Length;
        plungersInScene = GameObject.FindGameObjectsWithTag("Bullet").Length;
        humansSaved = GetComponent<PlayerHud>().humansWithPlayer;
        debris = GameObject.FindGameObjectsWithTag("Debris").Length;
        trash = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().trash;
    }
}