using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiminGun : MonoBehaviour
{
    public float bulletDmg;
    public float bulletSpeed;

    public List <Transform> enemies;

    public Transform target;
    public float turnSpeed= 100;

    private Transform spawnLocation;


    [SerializeField]
    private GameObject bulletPrefab;

    public GameObject spawnedBullet;
    
    public float shootTimer;
    public float reloadTime = 1.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = reloadTime;
        spawnLocation = GameObject.FindGameObjectWithTag("FriendlyFire").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
       if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
       else
        {
            shootTimer = reloadTime;

            if (enemies.Count >= 1)
            {
                target = enemies[Random.Range(0, enemies.Count)];
                Vector2 targetDirection = target.position - transform.position;
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

                transform.localRotation = Quaternion.Slerp(transform.localRotation, q, 200);

                Shoot();
            } else Shoot();

        }
    }
    public void Shoot()
    {
        if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>().gamePaused)
        {
            spawnedBullet = Instantiate(bulletPrefab, transform.position, transform.rotation, spawnLocation.transform);
            spawnedBullet.GetComponent<StrongBulletScript>().dealDamage = bulletDmg;
            spawnedBullet.GetComponent<StrongBulletScript>().speed = bulletSpeed;
            spawnedBullet.GetComponent<StrongBulletScript>().motherGun = GetComponent<AiminGun>();
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
