using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_LaserClean : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float reloadTime = 1.5f;
        public float blastDmg;
        public float rotationSpeed = 100;
        public float blastTime = 3f;
        public bool constantRotation;
    }
    public ItemTier[] itemTier;

    [Header("Laser Clean")]
    public GameObject blast;
    public GameObject beam;

    public bool blasting;

    public float dmgDealt;

    public bool reload, blastOut, blastOn, blastIn;

    public float reloadTimer;
    public float blastTimer;

    private PlayerExperience playerXP;

    [SerializeField]
    private Animator anim;
    private float animLength = 0.5f;
    private SavedStats savedStats;


    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();

        beam.GetComponent<SpriteRenderer>().enabled = false;

        dmgDealt = itemTier[itemLevel].blastDmg;
        reloadTimer = itemTier[itemLevel].reloadTime;
        blastTimer = itemTier[itemLevel].blastTime;

        reload = false;
        blastOut = false;
        blastOn = false;
        blastIn = false;
        blasting = false;     
    }

    // Update is called once per frame
    void Update()
    {
        if (startItem)
        {
            startItem = false;
            UpdateLaserClean(true);
        }
        if (activated)
        {
            blast.transform.Rotate(0, 0, itemTier[itemLevel].rotationSpeed * Time.deltaTime);

            if (reload)
            {

                if (reloadTimer > 0)
                {
                    reloadTimer -= Time.deltaTime;
                }
                else
                {
                    reload = false;
                    blastOut = true;
                    blasting = true;
                }
            }
            if (blasting)
            {
                if (blastOut)
                {
                    beam.GetComponent<SpriteRenderer>().enabled = true;
                    anim.SetBool("laserOut", true);

                    if (animLength > 0)
                    {
                        animLength -= Time.deltaTime;
                    }
                    else
                    {
                        blastOut = false;
                        blastOn = true;
                        animLength = 0.5f;
                    }

                }

                else if (blastOn)
                {
                    anim.SetBool("laserOut", false);
                    anim.SetBool("laserOn", true);
                    blastOn = false;
                    blastIn = true;
                }

                if (blastIn)
                {
                    if (blastTimer > 0)
                    {
                        blastTimer -= Time.deltaTime;
                    }

                    else

                    {
                        anim.SetBool("laserIn", true);
                        anim.SetBool("laserOn", false);

                        if (animLength > 0)
                        {
                            animLength -= Time.deltaTime;
                        }
                        else
                        {
                            blastIn = false;
                            reloadTimer = itemTier[itemLevel].reloadTime;
                            blastTimer = itemTier[itemLevel].blastTime;

                            blasting = false;
                            animLength = 0.5f;
                            anim.SetBool("laserIn", false);
                            beam.GetComponent<SpriteRenderer>().enabled = false;
                            reload = true;
                        }
                    }
                }
            }
        }

       
    }

    public void UpdateLaserClean(bool _isStartItem)
    {
        if (!activated)
        {
            activated = true;
            reload = true;
            reloadTimer = 0.2f;

            if (savedStats.level2Boost > 0)
            {
                itemLevel++;
            }
        }
        else
        {
            if (itemLevel < itemMaxLevel)
            {
                itemLevel++;
                dmgDealt = itemTier[itemLevel].blastDmg;

                if (itemLevel == itemMaxLevel - 1)
                {
                    maxLevelReached = true;
                }
            }
        }   
        
        playerXP.CloseUpgradeScreen(this, _isStartItem);

    }
}

