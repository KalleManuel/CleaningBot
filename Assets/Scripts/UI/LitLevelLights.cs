using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LitLevelLights : MonoBehaviour
{
    public GameObject[] levelLights;
    public Sprite greenLight, redLight;

    public void LevelLights(Item upgrade)
    {
        for (int i = 0; i < levelLights.Length; i++)
        {
            if (i < upgrade.itemMaxLevel)
            {
                if (!upgrade.activated)
                {

                    levelLights[i].SetActive(true);
                    levelLights[i].GetComponent<Image>().sprite = redLight;

                }
                else if (i < upgrade.itemLevel+1)
                {
                    levelLights[i].SetActive(true);
                    levelLights[i].GetComponent<Image>().sprite = greenLight;
                }
                else
                {
                    levelLights[i].SetActive(true);
                    levelLights[i].GetComponent<Image>().sprite = redLight;
                }


            }
            else levelLights[i].SetActive(false);

        }
    }
}
