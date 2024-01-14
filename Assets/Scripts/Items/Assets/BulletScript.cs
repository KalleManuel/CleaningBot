using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float speed = 20;
    public float dealDamage = 0.5f;
    private Transform spawndPos;

    public GameObject hit;
    public bool strongBullets = false;

    public bool onWall = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
        spawndPos = gameObject.transform;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!onWall)
            { 
                EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.health -= dealDamage;
                enemyHealth.positionOfInflictor = new Vector3(transform.position.x, transform.position.y, 0);

                EnemyDrop damageDrop = collision.gameObject.GetComponent<EnemyDrop>();
                damageDrop.DamageDrop();

                VisualDamage visualDmg = collision.gameObject.GetComponent<VisualDamage>();
                visualDmg.visualDmg = true;
                
                if (collision.gameObject.TryGetComponent<Knockback>(out Knockback knockback))
                {
                    knockback.ApplyKnockback(spawndPos);
                }
                
                

                if (!strongBullets)
                {
                    Destroy(gameObject);
                }
                else strongBullets = false;
            } 

        } else if (collision.gameObject.tag == "NonWalkable")
        {
           
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            onWall = true;
            
          
        }

        if (collision.gameObject.tag == "Player" && onWall)
        {
            collision.gameObject.GetComponent<PlayerInventory>().AddCoin(5);
            Destroy(gameObject);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "SpawnStopper" && !onWall)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Player")
        {
            hit = collision.gameObject;
            Destroy(gameObject);
        }

       
    }
    
        
}


