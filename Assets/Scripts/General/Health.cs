using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public string characterName; 
    public float health;
    public float maxHealth;

    public bool shield;
    public float shieldAmount = 1;

    public bool alive, revived;
    private float reviveTimer = 3;
    public Color reviveColor;


    void Start()
    {
        alive = true;
    }

    void Update()
    {
        if (revived)
        {
            SpriteRenderer playerColor = GetComponent<SpriteRenderer>();
           // Color tempColor = playerColor.color;
            playerColor.color = reviveColor;

            if (reviveTimer > 0)
            {
                reviveTimer -= Time.deltaTime;
            }

            else
            {
                playerColor.color = Color.white;
                revived = false;
                reviveTimer = 3;
            }
            
        }

        if (alive)
        {
            if (health > maxHealth)
            {
                health = maxHealth;
            }

            if (health < 0)
            {
                alive = false;
                health = 0;

                if (TryGetComponent<Death>(out Death death))
                {
                    death.Dead();
                    
                }
            }
        }
        
    }
}
