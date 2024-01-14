using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_MissileLauncher : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float radius;
        public float maxRange;
        public float minRange;
        public float damage;
        public float speed;
        public float reloadtime;
        public float interval;
        public int amountProjectiles;

    }
    public ItemTier[] itemTier;

    [Header("Rocket Launcher deteils")]

    public float range;
    public bool timeToLaunch;
    public float randomDirection;

    public bool launch;

    public GameObject missilePrefab, missileLaunched;

    public Transform spawnLocation;


    public float launchTimer;
    public float launchWait;

    private PlayerExperience playerXP;
    private SavedStats savedStats;

    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();

        launch = false;
        launchTimer = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (launch)
            {
                if (launchTimer > 0)
                {
                    launchTimer -= Time.deltaTime;
                }
                else
                {
                    launch = false;
                    StartCoroutine(LaunchMissile(itemTier[itemLevel].interval, itemTier[itemLevel].amountProjectiles));
                }
            }        
        }      
    }

    public IEnumerator LaunchMissile(float seconds, int missilecount)
    {
        for (int i = 0; i < missilecount; i++)
        {
            range = Random.Range(itemTier[itemLevel].minRange, itemTier[itemLevel].maxRange);
            randomDirection = Random.Range(0, 360);
            Quaternion tempRotation = Quaternion.Euler(0, 0, randomDirection);
            transform.rotation = tempRotation;

            missileLaunched = Instantiate(missilePrefab, transform.position, transform.rotation, spawnLocation.transform);
            missileLaunched.GetComponent<Missile>().speed = itemTier[itemLevel].speed;
            missileLaunched.GetComponent<Missile>().damage = itemTier[itemLevel].damage;
            missileLaunched.GetComponent<Missile>().radius = itemTier[itemLevel].radius;
            missileLaunched.GetComponent<Missile>().range = range;

            yield return new WaitForSeconds(seconds);

        }

        launch = true;
        launchTimer = itemTier[itemLevel].reloadtime;
    }

    public void UpdateRocketLauncher()
    {
        if (!activated)
        {
            activated = true;
            launch = true;

            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            playerXP.CloseUpgradeScreen(this);

        }
        else
        {
            itemLevel++;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            playerXP.CloseUpgradeScreen(this);
        }

    }
}
