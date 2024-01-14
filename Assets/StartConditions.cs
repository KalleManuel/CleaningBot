using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartConditions : MonoBehaviour
{
    public GameObject freeChoice;
    SavedStats stats;
    
    
    public GameObject[] allItems;

    [SerializeField]
    private WeaponInventory iconDisplay;

    void Start()
    {
        
        stats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
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

                    if (stats.startItem.gameObject.name == allItems[e].gameObject.name)
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
