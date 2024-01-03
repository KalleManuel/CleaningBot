using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private PlayerHud playerHud;
    
    [SerializeField]
    private int healthFill;

    private void Start()
    {
        playerHud = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHud>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health player = collision.gameObject.GetComponent<Health>();
            player.health += healthFill;
            
            if (player.health > player.maxHealth)
            {
                player.health = player.maxHealth;
            }
  
            playerHud.UpdateLifeBar();

            Destroy(gameObject);
        }
    }
}
