using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 20;
    public float dealDamage = 0.5f;

    public GameObject hit;

    public float timer;
    public float lifespan = 2;

    private bool goingOut;
    public Transform sender;

    public BoomerangThrower boomerangThrower;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;
        timer = lifespan;
        goingOut = true;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else goingOut = false;

        if (!goingOut)
        {
            rb.velocity = Vector2.zero;
            float step = speed * Time.deltaTime;

            if (sender != null)
            {
                Vector2 target = new Vector2(sender.position.x, sender.position.y);
                transform.position = Vector2.MoveTowards(transform.position, target, step);

                if (Vector2.Distance(transform.position, target) < 0.1f)
                {
                    boomerangThrower.boomerangHome = true;
                    Destroy(gameObject);
                }
            }
            else Destroy(gameObject);
            
        }
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
                boomerangThrower.target = null;
            }

        }
        else if (collision.gameObject.tag == "NonWalkable")
        {
            goingOut = false;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "Human")
        {
           // hit = collision.gameObject;
            //Destroy(gameObject);
            goingOut = false;
        }
    }


}


