using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whipping : MonoBehaviour
{
    [SerializeField]
    private Item_Whip whip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.health -= whip.itemTier[whip.itemLevel].dmg;
            enemyHealth.positionOfInflictor = new Vector3(transform.position.x, transform.position.y, 0);

            EnemyDrop damageDrop = collision.gameObject.GetComponent<EnemyDrop>();
            damageDrop.DamageDrop();

            VisualDamage visualDmg = collision.gameObject.GetComponent<VisualDamage>();
            visualDmg.visualDmg = true;

            if (collision.gameObject.TryGetComponent<Knockback>(out Knockback knockback))
            {
                knockback.ApplyKnockback(this.gameObject.transform);
            }

        }
    }
}
