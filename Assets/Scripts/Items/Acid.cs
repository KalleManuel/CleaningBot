using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    private Animator anim;
    public float duration;
    public float dmg;
    public float hurtInterval;
    public bool dealDmg;
    public float dmgTimer;

    public List <GameObject> enemiesOnAcid;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        dealDmg = false;
        dmgTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("acidEnd", true);
            dealDmg = false;
        }

        if (dealDmg)
        {
            if (dmgTimer < hurtInterval)
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

    public void ActivateHurt()
    {
        dealDmg = true;
    }

    public void HurtEnemies()
    {
        for (int i = 0; i < enemiesOnAcid.Count; i++)
        {
            enemiesOnAcid[i].GetComponent<EnemyHealth>().health -= dmg;
            enemiesOnAcid[i].GetComponent<EnemyHealth>().positionOfInflictor = new Vector3(transform.position.x, transform.position.y, 0);


            enemiesOnAcid[i].GetComponent<VisualDamage>().visualDmg = true;
        }
    }

    public void DestroyAcid()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            enemiesOnAcid.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesOnAcid.Remove(collision.gameObject);
        }
    }


}
