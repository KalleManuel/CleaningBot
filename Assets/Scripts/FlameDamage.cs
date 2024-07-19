using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDamage : MonoBehaviour
{
    [SerializeField]
    private FlameAim flameAim;
    public float flameTimer = 0.5f;
    private float timer;
    public float dealDMG =0.3f;


    public List<GameObject> enemiesOnFire;

    private void Start()
    {
        timer = 0;
    }
    private void Update()
    {
        if (flameAim.flameOn)
        {
            if (enemiesOnFire.Count >= 1)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }

                else
                {
                    HurtEnemies();
                    timer = flameTimer;
                }
            } 
        }
    }

    public void HurtEnemies()
    {
        for (int i = 0; i < enemiesOnFire.Count; i++)
        {
            enemiesOnFire[i].GetComponent<EnemyHealth>().health -= dealDMG;
            enemiesOnFire[i].GetComponent<EnemyHealth>().positionOfInflictor = new Vector3(transform.position.x, transform.position.y, 0);


            enemiesOnFire[i].GetComponent<VisualDamage>().visualDmg = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            enemiesOnFire.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesOnFire.Remove(collision.gameObject);
        }
    }

}
