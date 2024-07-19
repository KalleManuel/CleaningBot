using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    private SavedStats savedStats;

    public Item_LaserClean parent;

    private void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (parent.blasting)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.health -= parent.dmgDealt *savedStats.extraDMG;
                enemyHealth.positionOfInflictor = new Vector3(transform.position.x, transform.position.y, 0);

                VisualDamage visualDmg = collision.gameObject.GetComponent<VisualDamage>();
                visualDmg.visualDmg = true;
            }
        }

    }
}
