using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
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

    // Start is called before the first frame update
    void Start()
    { 
        playerXP = player.GetComponent<PlayerExperience>();
        gunAim = player.GetComponent<PlayerMovment>();

        itemLevel = 0;
        startCoolDown = true;

        coolDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
            spawnedBullet.GetComponent<BulletScript>().dealDamage = itemTier[itemLevel].bulletDmg;
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

    public void UpgradeGun()
    {
        if (!activated)
        {
            activated = true;
            playerXP.CloseUpgradeScreen(this);
        }
        else
        {
            itemLevel++;
            playerXP.CloseUpgradeScreen(this);

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }
        }
        

       

    }

}
