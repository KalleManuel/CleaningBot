using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_VacumBoost : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float range;

    }
    public ItemTier[] itemTier;

    
    private PlayerExperience playerXP;

    public bool SuperVac;
    private float superVacTimer = 5;
    private bool playSound;
    private AudioSource sfxPlayer;


    private SavedStats savedStats;

    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        SuperVac = false;
        playSound = true;
        itemTier[itemLevel].range = 0.5f;
        GetComponent<CircleCollider2D>().radius = itemTier[itemLevel].range;
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        sfxPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (startItem)
        {
            startItem = false;
            UpgradeVacum(true);
        }
        if (SuperVac)
        {
            if (playSound)
            {
                sfxPlayer.Play();
                playSound = false;
            }

            if (superVacTimer > 0)
            {
                superVacTimer -= Time.deltaTime;
            }
            else
            {
                SuperVac = false;
                superVacTimer = 5;
                GetComponent<CircleCollider2D>().radius = itemTier[itemLevel].range;
                playSound = true;
            }
        }

        
    }

    public void UpgradeVacum(bool _isStartItem)
    {
        if (!activated)
        {
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            
            activated = true;
            GetComponent<CircleCollider2D>().radius = itemTier[itemLevel].range;
            
        } 
        else
        {
            itemLevel++;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            GetComponent<CircleCollider2D>().radius = itemTier[itemLevel].range;
           
        }
           playerXP.CloseUpgradeScreen(this, _isStartItem);  
    }
}
