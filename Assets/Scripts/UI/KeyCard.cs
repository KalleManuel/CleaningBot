using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : InventoryItem
{
    public string message;
    private bool pickedUp;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           // PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>();
            Player.playerInventory.addToInventory(GetComponent<InventoryItem>());

            if (!pickedUp) 
            {
                StartCoroutine(PlayerHud.playerHud.SendMessageToHUD(message, 3, false));
                GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>().keys++;
                pickedUp = true;
            }
            gameObject.SetActive(false);   
        }
    }
}
