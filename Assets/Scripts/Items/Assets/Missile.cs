using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Rigidbody2D rb;

    public float radius;
    public float range;
    public float damage;
    public float speed;

    public GameObject explosionPrefab;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (range > 0)
        {
            range -= Time.deltaTime;
        }
        else
        {
            SpawnExplosion();
        }
    }
    public void SpawnExplosion()
    {
        explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.GetComponent<Explosion>().radius = radius;
        explosion.GetComponent<Explosion>().damage = damage;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NonWalkable")
        {
            SpawnExplosion();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "SpawnStopper")
        {
            SpawnExplosion();
        }
    }
}
