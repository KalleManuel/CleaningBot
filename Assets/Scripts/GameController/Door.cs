using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject theDoor;
    private PlayerHud playerHUD;
    public string missingKeyMessage;
    public InventoryItem theCorrectKey;
    public string correctKey;
    private bool doorOpened;
    

    private void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHud>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();

            for (int i =0; i < playerInventory.inventoryItems.Count; i++)
            {
                if (correctKey == playerInventory.inventoryItems[i].itemName)
                {
                    theDoor.SetActive(false);
                    doorOpened = true;
                    break;
                }
            }

            if (!doorOpened)
            {
                StartCoroutine(playerHUD.SendMessageToHUD(missingKeyMessage, 3, false));
            }

        }
    }
}
