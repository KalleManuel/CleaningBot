using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongBulletScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 20;
    public float dealDamage = 0.5f;

    public GameObject hit;

    public float timer;
    public float lifespan = 2;

    public AiminGun motherGun;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;
        timer = lifespan;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.health -= dealDamage;
            enemyHealth.positionOfInflictor = new Vector3(transform.position.x, transform.position.y, 0);

            VisualDamage visualDmg = collision.gameObject.GetComponent<VisualDamage>();
            visualDmg.visualDmg = true;

            if (enemyHealth.health <= 0)
            {
                motherGun.target = null;
            }

            Destroy(gameObject);

        }
        else if (collision.gameObject.tag == "NonWalkable")
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "Human")
        {
            hit = collision.gameObject;
            Destroy(gameObject);
        }
    }


}


