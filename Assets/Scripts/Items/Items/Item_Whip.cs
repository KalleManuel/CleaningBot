using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Whip : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float size;
        public float dmg;
        public bool whipReverse;
        public float coolDown;  

    }
    public ItemTier[] itemTier;

    public FacePlayerDirection faceDir;
    private PlayerExperience playerXP;
    public Animator anim;
    public float animLength= 0.9f;
    

    public bool coolingDown, whipping;
    public float timer;

    private SavedStats savedStats;

    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        timer = itemTier[itemLevel].coolDown;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startItem)
        {
            startItem = false;
            UpgradeWhip(true);
        }

        if (activated)
        {
            if (coolingDown)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    coolingDown = false;
                    timer = itemTier[itemLevel].coolDown;
                }
            }
            else if (!whipping)
            {
                faceDir.AimWhip();
                whipping = true;
                StartCoroutine(Whipping());
                
            }              
        }
    }

    IEnumerator Whipping()
    {
        anim.SetTrigger("whipTriggerRight");
       


        if (itemTier[itemLevel].whipReverse)
        {
            yield return new WaitForSeconds(animLength);
            anim.SetTrigger("whipTriggerLeft");
        }

        yield return new WaitForSeconds(animLength);
        coolingDown = true;
        whipping = false;
  
    }

    public void UpgradeWhip(bool _isStartItem)
    {
        if (!activated)
        {
            activated = true;
           
            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }

            if (_isStartItem)
            {
                playerXP.CloseUpgradeScreen(this, true);
            }
            else playerXP.CloseUpgradeScreen(this, false);

        }
        else
        {
            itemLevel++;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            playerXP.CloseUpgradeScreen(this,false);
        }               
    }
}
