using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGun : Item
{
    [Header("Gun details")]
    public int bulletCount;
    public float bulletDmg;
    public float bulletSpeed;

    public GameObject spawnedBullet;

    public int[] aims;
    public int aimIndex;

    [SerializeField]
    private GameObject player;

    private PlayerMovment gunAim;

    [SerializeField]
    private Vector2 direction;
    
    [SerializeField]
    private float interval, gunTimer, rotateAngle;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform bulletSpawner;

    [SerializeField]
    private PlayerExperience playerXP;

    // Start is called before the first frame update
    void Start()
    {
        gunAim = player.GetComponent<PlayerMovment>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();

        bulletSpeed = 20;
        bulletDmg = 0.5f;
        interval = 2;
      
        gunTimer = 0;
        rotateAngle = 0;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get bullet direction
        BulletDirection();
        
       
        //add bullet interval
        if (gunTimer < interval)
        {
            gunTimer += Time.deltaTime;
        }
        else
        {
            gunTimer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
      
            bulletCount = itemLevel;
            
            for (int i = 0; i < bulletCount; i++)
            {
                spawnedBullet =  Instantiate(bullet, bulletSpawner.position, transform.rotation);
                spawnedBullet.GetComponent<BulletScript>().dealDamage = bulletDmg;
                spawnedBullet.GetComponent<BulletScript>().speed = bulletSpeed;              
            if (aimIndex <7)
            {
                aimIndex++;
                
            } else aimIndex = 0;

            ChangeAim();
            }
            aimIndex = 0;
            ChangeAim();            
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

    public void ChangeAim()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle + aims[aimIndex]);
    }

    public void UpgradeGun()
    {
        itemLevel++;

        if (itemLevel == 1)
        {
            bulletSpeed = 20;
            bulletDmg = 0.5f;
            interval = 2;
        }

        else if (itemLevel == 2)
        {
            bulletSpeed = 22;
            bulletDmg = 1;
            interval = 1.8f;
        }

        else if (itemLevel == 3)
        {
            bulletSpeed = 24;
            bulletDmg = 1.2f;
            interval = 1.6f;
        }

        else if (itemLevel == 4)
        {
            bulletSpeed = 26;
            bulletDmg = 1.5f;
            interval = 1.6f;
        }
        else if (itemLevel == 5)
        {
            bulletSpeed = 28;
            bulletDmg = 1.8f;
            interval = 1.4f;
        }
        else if (itemLevel == 6)
        {
            bulletSpeed = 30;
            bulletDmg = 2f;
            interval = 1.2f;
        }

        else if (itemLevel == 7)
        {
            bulletSpeed = 32;
            bulletDmg = 2.5f;
            interval = 1f;
        }
        else if (itemLevel >= 8)
        {
            bulletSpeed = 35;
            bulletDmg = 3f;
            interval = 0.5f;
        }

        playerXP.CloseUpgradeScreen(this);

    }

}
