using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float dealDamage;
    private GameObject objectToHurt;

    private bool hurt;

    private void Update()
    {
        if (hurt)
        { 
            if (!objectToHurt.GetComponent<Health>().revived)
            {
                if (objectToHurt.GetComponent<Health>().shield)
                {
                    objectToHurt.GetComponent<Health>().health -= Time.deltaTime * (dealDamage / objectToHurt.GetComponent<Health>().shieldAmount);
                    objectToHurt.GetComponent<VisualDamage>().visualDmg = true;
                }

                else
                {
                    objectToHurt.GetComponent<Health>().health -= Time.deltaTime * dealDamage;
                    objectToHurt.GetComponent<VisualDamage>().visualDmg = true;
                }
            }   
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "BodySoul") 
        {
            hurt = true;
            objectToHurt = collision.gameObject;
        }

        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "BodySoul")
        {
            
            hurt = false;
            objectToHurt = null;
        }
    }
}
