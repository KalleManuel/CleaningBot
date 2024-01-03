using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public List <InventoryItem> inventoryItems;
    public Image[] inventorySlots;
   
    public int trash = 0;
    public TextMeshProUGUI coinStack;

    public void addToInventory(InventoryItem item)
    {
        inventoryItems.Add(item);
        UpdateInventory();

    }

    private void UpdateInventory()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (!inventorySlots[i].enabled)
            {
                
                inventorySlots[i].enabled = true;
                inventorySlots[i].sprite = inventoryItems[i].itemImage;
            }
        }
    }

    public void AddCoin(float coinsGained)
    {
        trash+= (int)coinsGained;
        coinStack.text = "" + trash;
    }
}
