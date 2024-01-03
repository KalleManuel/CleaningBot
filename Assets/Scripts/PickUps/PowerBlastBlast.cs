using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlastBlast : MonoBehaviour
{
    public float dealDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.health -= dealDamage;

            EnemyDrop damageDrop = collision.gameObject.GetComponent<EnemyDrop>();
            damageDrop.DamageDrop();

            VisualDamage visualDmg = collision.gameObject.GetComponent<VisualDamage>();
            visualDmg.visualDmg = true;

            if (collision.gameObject.TryGetComponent<Knockback>(out Knockback knockback))
            {
                knockback.ApplyKnockback(transform);
            }
        }
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
