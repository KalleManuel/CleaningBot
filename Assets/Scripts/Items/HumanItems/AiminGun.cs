using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiminGun : MonoBehaviour
{
    
    public float bulletDmg;
    public float bulletSpeed;
    public int mag = 1;


    public List <Transform> enemies;

    public Transform target;
    public float turnSpeed= 100;

    private Transform spawnLocation;
    public bool shooting;


    [SerializeField]
    private GameObject bulletPrefab;

    public GameObject spawnedBullet;
    
    public float shootTimer;

    [Header("Gun Settings")]
    public int gunMagSize = 1;
    public float gunReloadTime = 1.5f; 

    public float reloadTime = 1.5f;

    [Header("Machine Gun Settings")]
    public bool isMachineGun;
    public int machineGunMagSize = 6;
    public float machineGunReloadeTime = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        if (isMachineGun)
        {
            reloadTime = machineGunReloadeTime;
            mag = machineGunMagSize;
        }
        else
        {
            reloadTime = gunReloadTime;
            mag = gunMagSize;
        }

        shootTimer = reloadTime;
        spawnLocation = GameObject.FindGameObjectWithTag("FriendlyFire").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
       if (!shooting)
        {
            if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }
            else
            {
                if (enemies.Count >= 1)
                {
                    target = enemies[Random.Range(0, enemies.Count)];
                    Vector2 targetDirection = target.position - transform.position;
                    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                    Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

                    transform.localRotation = Quaternion.Slerp(transform.localRotation, q, 200);

                    shooting = true;
                    StartCoroutine(Shoot());
                }
                else
                {
                    shooting = true;
                    StartCoroutine(Shoot());
                }
            }
        }
        
    }
    IEnumerator Shoot()
    {
        if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>().gamePaused)
        {
            for (int i = 0; i < mag; i++)
            {
                spawnedBullet = Instantiate(bulletPrefab, transform.position, transform.rotation, spawnLocation.transform);
                spawnedBullet.GetComponent<StrongBulletScript>().dealDamage = bulletDmg;
                spawnedBullet.GetComponent<StrongBulletScript>().speed = bulletSpeed;
                spawnedBullet.GetComponent<StrongBulletScript>().motherGun = GetComponent<AiminGun>();

                yield return new WaitForSeconds(0.15f);
            }

            shootTimer = reloadTime;
            shooting = false;
            

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Add(collision.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject.transform);
        }
    }
}
