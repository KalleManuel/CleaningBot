using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public enum SpawnState { Check, Locate, Spawn, Off }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject[] enemies;
        public int amountEnemies;
        public float maxRate;
        public float minRate;

        public bool spawnBoss;
        public int AmountBosses;
        public GameObject[] boss;
        

        public float lenghtOfWave;
    }
    public Wave[] waves;

    public SpawnState state = SpawnState.Check;


    public List<GameObject> enemiesInWorld;

    public GameObject[] spawnPoints;
    private int activeSP;
    public Transform enemyParent;

    public int waveNumber = 0;

    private Timer time;


    // Start is called before the first frame update
    void Start()
    {
        time = GameObject.FindGameObjectWithTag("GameController").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.Check)
        {
            for (int i = 0; i < enemiesInWorld.Count; i++)
            {
                if (enemiesInWorld[i] == null)
                {
                    enemiesInWorld.RemoveAt(i);
                }
            }
            if (enemiesInWorld.Count < waves[waveNumber].amountEnemies)
            {
                state = SpawnState.Locate;
                FindSpawnPoint();
            }

        }

        if (time.minutes > waves[waveNumber].lenghtOfWave)
        {
            if (waveNumber +1 > waves.Length)
            {
                waveNumber = 0;

            } else waveNumber++;
        }

        if (state == SpawnState.Off)
        {
            StopAllCoroutines();
        }
    }

    public void FindSpawnPoint()
    {
  
        activeSP = Random.Range(0, spawnPoints.Length);

        if (spawnPoints[activeSP].GetComponent<EnemySpawner>().spawn)
        {
            state = SpawnState.Spawn;
            StartCoroutine(SpawningWave(waves[waveNumber]));
        }
        else state = SpawnState.Check;
    }

    IEnumerator SpawningWave(Wave _wave)
    {
        if (_wave.spawnBoss)
        {
            if (_wave.AmountBosses > 0)
            {
                enemiesInWorld.Add(Instantiate(_wave.boss[Random.Range(0, _wave.boss.Length)], spawnPoints[activeSP].transform.position, spawnPoints[activeSP].transform.rotation, enemyParent.transform));
                _wave.AmountBosses--;
                yield return new WaitForSeconds(Random.Range(_wave.minRate, _wave.maxRate));

                state = SpawnState.Check;
            }
            else
            {
                _wave.spawnBoss = false;
                state = SpawnState.Check;
            }
        }
        else
        {
            enemiesInWorld.Add(Instantiate(_wave.enemies[Random.Range(0, _wave.enemies.Length)], spawnPoints[activeSP].transform.position, spawnPoints[activeSP].transform.rotation,enemyParent.transform));

            yield return new WaitForSeconds(Random.Range(_wave.minRate, _wave.maxRate));

            state = SpawnState.Check;
        }
        

    }
}
