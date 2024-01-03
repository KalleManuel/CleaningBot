using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    public float radius;
    public float r;



    // Start is called before the first frame update
    void Start()
    {

        r = 0;
        transform.localScale = new Vector3(r, r, 0);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < radius)
        {
            r += Time.deltaTime;
            transform.localScale = new Vector3(r, r, 0);
        }
        else 
        {
            
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.health -= damage;
            enemyHealth.positionOfInflictor = new Vector3(transform.position.x, transform.position.y, 0);

            VisualDamage visualDmg = collision.gameObject.GetComponent<VisualDamage>();
            visualDmg.visualDmg = true;
        }
    }
}
