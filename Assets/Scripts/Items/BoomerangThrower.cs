using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrower : MonoBehaviour
{
    public float dmg;
    public float speed;
    

    public List <Transform> enemies;

    public Transform target;
    public float turnSpeed= 100;
  

    [SerializeField]
    private GameObject boomerang;

    public GameObject spawnedBullet;
    
    public float shootTimer;
    public float reloadTime = 1.5f;
    public bool boomerangHome;
    

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = reloadTime;
        boomerangHome = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boomerangHome)
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
                }
                else Shoot();

            }
        }
       
    }
    public void Shoot()
    {
        spawnedBullet = Instantiate(boomerang, transform.position, transform.rotation);
        boomerangHome = false;
        spawnedBullet.GetComponent<Boomerang>().dealDamage = dmg;
        spawnedBullet.GetComponent<Boomerang>().speed = speed;
        spawnedBullet.GetComponent<Boomerang>().boomerangThrower = GetComponent<BoomerangThrower>();
        spawnedBullet.GetComponent<Boomerang>().sender = gameObject.transform;
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
