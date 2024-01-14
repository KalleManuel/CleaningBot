using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private PlayerHud playerHUD;
    public string missingKeyMessage;
    public InventoryItem theCorrectKey;
    public string correctKey;
    private bool doorOpened;

    private Animator anim;
    public GameObject col;

    private void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHud>();
        anim = GetComponent<Animator>();
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
                    doorOpened = true;
                    anim.SetBool("doorOpen", true);
                    break;
                }
            }

            if (!doorOpened)
            {
                StartCoroutine(playerHUD.SendMessageToHUD(missingKeyMessage, 3, false));
            }

        }
    }

    public void DoorIsOpened()
    {
        gameObject.SetActive(false);
        col.SetActive(false);
       
    }
}
