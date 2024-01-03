using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : InventoryItem
{
    public string message;
    private PlayerHud playerHUD;
    private bool pickedUp;

    private void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHud>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>();
            inventory.addToInventory(GetComponent<InventoryItem>());

            if (!pickedUp) 
            {
                StartCoroutine(playerHUD.SendMessageToHUD(message, 3, false));
                pickedUp = true;
            }
            gameObject.SetActive(false);   
        }
    }
}
