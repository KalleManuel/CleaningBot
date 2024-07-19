using System.Collections;
using UnityEngine;

public class Item_Gun : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float reloadTime;
        public float interval;
        public int amountBullets;
        public float bulletDmg;
        public float bulletSpeed;
        public bool strongBullets = false;     
    }

    public ItemTier[] itemTier;

    public float coolDown;
    public bool startCoolDown;

    [SerializeField]
    private GameObject player;

    private PlayerMovment gunAim;

    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    private float rotateAngle;

    [SerializeField]
    private GameObject bullet;
    public GameObject spawnedBullet;

    [SerializeField]
    private Transform bulletSpawner;

    public Transform spawnLocation;

    [SerializeField]
    private PlayerExperience playerXP;
    private SavedStats savedStats;

    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        playerXP = player.GetComponent<PlayerExperience>();
        gunAim = player.GetComponent<PlayerMovment>();

        itemLevel = 0;
        startCoolDown = true;

        coolDown = 0;
    }

    
    void Update()
    {
        if (startItem)
        {
            startItem = false;
            UpgradeGun(true);
        }
        if (activated)
        {
            BulletDirection();

            if (startCoolDown)
            {
                //add bullet interval
                if (coolDown < itemTier[itemLevel].reloadTime)
                {
                    coolDown += Time.deltaTime;
                }
                else
                {
                    StartCoroutine(Shoot(itemTier[itemLevel].interval, itemTier[itemLevel].amountBullets));
                    startCoolDown = false;
                }            }
        }
    }

    public IEnumerator Shoot(float sec, int bulletCount)
    {
       for (int i = 0; i < bulletCount; i++)
            {
            
            spawnedBullet =  Instantiate(bullet, bulletSpawner.position, transform.rotation, spawnLocation.transform);
            spawnedBullet.GetComponent<BulletScript>().dealDamage = itemTier[itemLevel].bulletDmg *savedStats.extraDMG;
            spawnedBullet.GetComponent<BulletScript>().speed = itemTier[itemLevel].bulletSpeed;
            spawnedBullet.GetComponent<BulletScript>().strongBullets = itemTier[itemLevel].strongBullets;


            yield return new WaitForSeconds(sec);
        }
        coolDown = 0;
        startCoolDown = true;

    }

    public void BulletDirection()
    {
        direction = gunAim.direction;

        if (direction.x == 1 && direction.y == 0)
        {
            rotateAngle = 0;
        }

        else if (direction.x == 1 && direction.y == 1)
        {
            rotateAngle = 45;
        }
        else if (direction.x == 0 && direction.y == 1)
        {
            rotateAngle = 90;
        }
        else if (direction.x == -1 && direction.y == 1)
        {
            rotateAngle = 135;
        }
        else if (direction.x == -1 && direction.y == 0)
        {
            rotateAngle = 180;
        }
        else if (direction.x == -1 && direction.y == -1)
        {
            rotateAngle = 225;
        }
        else if (direction.x == 0 && direction.y == -1)
        {
            rotateAngle = 270;
        }
        else if (direction.x == 1 && direction.y == -1)
        {
            rotateAngle = 315;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
    }

    public void UpgradeGun(bool _startItem)
    {
        if (!activated)
        {
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            activated = true;
        }
        else
        {
            itemLevel++;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }
        }

        playerXP.CloseUpgradeScreen(this,_startItem);
    }

}
