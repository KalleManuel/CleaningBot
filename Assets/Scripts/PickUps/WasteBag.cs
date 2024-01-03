using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteBag : MonoBehaviour
{
   
    
    public float value = 200f;

    public GameObject player;

    private GameOver playerStatus;
    private PlayerInventory inventory;


    // Start is called before the first frame update
    void Start()
    {
       
        playerStatus = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
      
        if (!playerStatus.dead)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<PlayerInventory>();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inventory.AddCoin(value);
            Destroy(gameObject);
        }
    }
}
