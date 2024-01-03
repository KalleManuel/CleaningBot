using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAmmo : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float speed = 20;
    public float dealDamage = 0.5f;
    private Transform spawndPos;

    public GameObject hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
        spawndPos = gameObject.transform;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "BodySoul")
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.health -= dealDamage;

            VisualDamage visualDmg = collision.gameObject.GetComponent<VisualDamage>();
            visualDmg.visualDmg = true;
            
            Destroy(gameObject);

        } else if (collision.gameObject.tag == "NonWalkable")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "SpawnStopper")
        {
            Destroy(gameObject);
        }
    }
    
        
}


