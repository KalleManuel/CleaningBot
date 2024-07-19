using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsMeter : MonoBehaviour
{
    private Statistic stats;
    public TextMeshProUGUI kills;
   // public TextMeshProUGUI dirt;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>();
    }

    // Update is called once per frame
    void Update()
    {
        kills.text = "Kills: " + stats.enemiesKilled;
       // dirt.text = "dirt: " + (stats.debris + stats.plungersInScene + stats.bloodClots);
    }
}
