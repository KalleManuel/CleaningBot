using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public bool human, player;
    private Health health;
    private float tempHP;
    public string healthCode;
    private GameObject lifeBar;
    


    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        tempHP = health.health;
        lifeBar = GameObject.FindGameObjectWithTag(healthCode);
    }

    void Update()
    {
        if (health.alive)
        {
            if (tempHP != health.health)
            {
                float currentHealth = health.health / health.maxHealth;

                if (player)
                {
                    lifeBar.transform.localScale = new Vector3(currentHealth, 1f, 1f);
                }
                else if (human)
                {
                    lifeBar.transform.localScale = new Vector3(1f, currentHealth, 1f);
                }

                tempHP = health.health;
            }
        }
        else 
        {
            if (human)
            {
                lifeBar.transform.localScale = new Vector3(1f, 0f, 1f);
            }
        } 
        
        
    }
}
