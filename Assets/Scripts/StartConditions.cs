using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartConditions : MonoBehaviour
{
    public GameObject freeChoice;
    public SavedStats stats;
    GameObject player;
    
    
    public GameObject[] allItems;

    [SerializeField]
    private WeaponInventory iconDisplay;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();

        //set upgrade boosters
        player.GetComponent<PlayerMovment>().acceleration = player.GetComponent<PlayerMovment>().acceleration * stats.extraSpeed;
        player.GetComponent<PlayerMovment>().maxSpeed = player.GetComponent<PlayerMovment>().maxSpeed * stats.extraMaxSpeed;
        player.GetComponent<Health>().health = player.GetComponent<Health>().health * stats.extraHealth;
        

        freeChoice.SetActive(false);

        SetAvailableItems();

        if (stats.amountExtraStartItems > 0) 
        {
            GetComponent<Pause>().PauseGame(false);
            freeChoice.SetActive(true);
        }
       
    }
    public void CloseFreeChoice()
    {
        freeChoice.SetActive(false);
    }

    public void SetAvailableItems()
    {
        for(int i = 0; i < stats.availebleItems.Count; i++)
        {
            for (int e = 0; e < allItems.Length; e++)
            {
                if (stats.availebleItems[i].gameObject.name == allItems[e].gameObject.name)
                {
                    allItems[e].GetComponent<Item>().available = true;

                    if (stats.firstWeapon.gameObject.name == allItems[e].gameObject.name)
                    {
                        allItems[e].GetComponent<Item>().startItem = true;

                        if (stats.level2Boost > 0)
                        {
                            allItems[e].GetComponent<Item>().itemLevel++;
                            
                        }
                    }

                }
            }

        }
    }


}
