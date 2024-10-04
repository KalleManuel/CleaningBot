using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

   
    public string missingKeyMessage;
    public InventoryItem theCorrectKey;
    public string correctKey;
    private bool doorOpened;

    private Animator anim;
    public GameObject col;

    private void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();

            //  for (int i =0; i < playerInventory.inventoryItems.Count; i++)
            for (int i = 0; i < Player.playerInventory.inventoryItems.Count; i++)
            {
                //if (correctKey == playerInventory.inventoryItems[i].itemName)
                if (correctKey == Player.playerInventory.inventoryItems[i].itemName)
                {
                    doorOpened = true;
                    anim.SetBool("doorOpen", true);
                    break;
                }
            }

            if (!doorOpened)
            {
                StartCoroutine(PlayerHud.playerHud.SendMessageToHUD(missingKeyMessage, 3, false));
            }

        }
    }

    public void DoorIsOpened()
    {
        gameObject.SetActive(false);
        col.SetActive(false);
       
    }
}
