using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInventory : MonoBehaviour
{

    public List<Image> icons;
    public List<GameObject> levelBackground;
    public List<TextMeshProUGUI> levels;

    private void Start()
    {
        for (int i = 0; i < levelBackground.Count; i++)
        {
            if (icons[i].sprite == null)
            {
                levelBackground[i].SetActive(false);
            }
            
        }
    }

    public void UpdateIcon(Item upgrade)
    { 
        for (int i = 0; i < icons.Count; i++)
        {
            if (icons[i].sprite != null)
            { 
                if (icons[i].sprite == upgrade.itemImage)
                {
                    levels[i].text = "" + (upgrade.itemLevel + 1);
                    break;
                }
            }
            else
            {
                Debug.Log("empty slot" + i);
                icons[i].sprite = upgrade.itemImage;
                icons[i].color = Color.white;
                levelBackground[i].SetActive(true);
                levels[i].text = "" + (upgrade.itemLevel + 1);
                break;
            }
        }
        
    }
}

 
