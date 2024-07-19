using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_AcidLauncher : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float coolDown = 1.5f;
        public float acidDmg;
        public float hurtInterval;
        public float duration = 3f;
    }
    public ItemTier[] itemTier;

    public GameObject acid;


    public float dmgDealt;

    public bool reload;

    public float reloadTimer;
    public float blastTimer;

    public Transform spawnLocation;

    private PlayerExperience playerXP;
    private SavedStats savedStats;
   
    
    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
    

        reloadTimer = 0.2f;
        reload = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startItem)
        {
            startItem = false;
            UpdateAcid(true);
        }
        if (activated)
        {
            if (reload)
            {

                if (reloadTimer > 0)
                {
                    reloadTimer -= Time.deltaTime;
                }
                else
                {
                    GameObject acidObject = Instantiate(acid, transform.position, transform.rotation, spawnLocation.transform);
                    Acid acidScript = acidObject.GetComponent<Acid>();
                    acidScript.duration = itemTier[itemLevel].duration;
                    acidScript.dmg = itemTier[itemLevel].acidDmg * savedStats.extraDMG;
                    acidScript.hurtInterval = itemTier[itemLevel].hurtInterval;


                    reloadTimer = itemTier[itemLevel].coolDown;
                }
            }
        }
    }

    public void UpdateAcid(bool _isStartItem)
    {
        if (!activated)
        {
            activated = true;
            reload = true;
           
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }
        }

        else
        {  
             itemLevel++;
           
            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }
        } 
        
        playerXP.CloseUpgradeScreen(this,_isStartItem);
    }
}

