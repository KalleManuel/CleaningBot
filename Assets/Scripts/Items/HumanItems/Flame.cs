using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float dmg;
    public float hurtIntervall;

    public List<GameObject> enemiesInRange;
    private Pause pause;
  
    public bool dealDmg;
    public float dmgTimer;

    private void Start()
    {
        pause = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>();
        dealDmg = true;
    }
    void Update()
    {
        if (!pause.gamePaused)
        {
            if (dealDmg)
            {
                if (dmgTimer < hurtIntervall)
                {
                    dmgTimer += Time.deltaTime;

                }
                else
                {
                    HurtEnemies();
                    dmgTimer = 0;
                }
            }
        }
    }

    public void HurtEnemies()
    {
        for (int i = 0; i < enemiesInRange.Count; i++)
        {

            enemiesInRange[i].GetComponent<EnemyHealth>().health -= dmg;
            enemiesInRange[i].GetComponent<EnemyHealth>().positionOfInflictor = this.gameObject.transform.position;
            enemiesInRange[i].GetComponent<VisualDamage>().visualDmg = true;

            if (enemiesInRange[i].gameObject.TryGetComponent<Knockback>(out Knockback knockback))
            {
                knockback.ApplyKnockback(this.transform);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
}
