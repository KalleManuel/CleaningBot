using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHandler : MonoBehaviour
{
    
    public List<Transform> humanSpawnPoints;
    public GameObject[] humanPrefabs;

    private void Start()
    {
        for (int i = 0; i< humanPrefabs.Length; i++)
        {
            int sp = Random.Range(0, humanSpawnPoints.Count);

            GameObject hum = Instantiate(humanPrefabs[i], humanSpawnPoints[sp].position, humanSpawnPoints[sp].rotation, transform);
          
            humanSpawnPoints.RemoveAt(sp);
            
        }
    }
}
