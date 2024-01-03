using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS: Item
{

    public GameObject miniMap;
    public GameObject camBox;
    public GameObject shipMap;
    public GameObject noSignalText;
    public bool mapAcces;
    public GameObject mapCam;
    

    private PlayerExperience playerXP;

    // Start is called before the first frame update
    void Start()
    {
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();

        miniMap.SetActive(false);
        camBox.SetActive(false);
        noSignalText.SetActive(true);
        shipMap.SetActive(false);
        mapAcces = false;
        mapCam.SetActive(false);
       
    }
    private void Update()
    {
        if (mapAcces)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMap();
            }
        }
    }

    public void ToggleMap()
    {
        if (shipMap.activeSelf)
        {
            mapCam.SetActive(false);
            shipMap.SetActive(false);
        }
        else
        {
            mapCam.SetActive(true);
            shipMap.SetActive(true);
        }
    }

    public void UpdateGPS()
    {
        if (!activated)
        {
            activated = true;
            noSignalText.SetActive(false);
            miniMap.SetActive(true);
        }
        else
        {
            itemLevel++;
        }
       

        if (itemLevel == 1)
        {
            camBox.SetActive(true);
        }

        else if (itemLevel == 2)
        {
            mapAcces = true;
        }

        if (itemLevel == itemMaxLevel - 1)
        {
            maxLevelReached = true;
        }

        playerXP.CloseUpgradeScreen(this);
    }
}
