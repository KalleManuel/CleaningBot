using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public float experience = 0;
    public float experienceTreshhold = 3;
    public float experienceIncrement = 1.25f;
    public int experienceLevel = 1;
    public int upgradesAmounts;
    public float experienceBoost;

    private bool updateWindowOpened;

    public GameObject upgradeScreen;

    private PlayerHud playerHud;
    private AudioSource sfxPlayer;
    [SerializeField]
    private AudioClip[] pickUpSounds;
    
    [SerializeField]
    private WeaponInventory iconDisplay;

    [SerializeField]
    private UpgradeHandler upgradeHandler;

    private void Start()
    {
        playerHud = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHud>();
        upgradeScreen.SetActive(false);
        updateWindowOpened = false;
        upgradesAmounts = 3;
        sfxPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (experience >= experienceTreshhold)
        {
            if (!updateWindowOpened)
            {
                upgradeScreen.SetActive(true);
                upgradeScreen.GetComponent<UpgradeHandler>().UpgradeScrambler(upgradesAmounts);
                updateWindowOpened = true;
            }
            
        }
        
    }

    public void GainExperience(float experienceGained)
    {
        experience += (experienceGained * experienceBoost);
        int random = Random.Range(0, pickUpSounds.Length);
        if (experienceGained > 0)
        {
            sfxPlayer.clip = pickUpSounds[random];
            sfxPlayer.Play();
        }
       
        playerHud.UpdatePlayerExperience();
    } 

    public void CloseUpgradeScreen(Item upgrade)
    {
        upgradeHandler.AddToActivated();
        upgradeScreen.SetActive(false);
        experienceLevel++;
        experience = 0;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>().ResumeGame(false);
        updateWindowOpened = false;
        iconDisplay.UpdateIcon(upgrade);
        Debug.Log(upgrade);

        playerHud.UpdatePlayerLevel();
        playerHud.UpdatePlayerExperience();


        experienceTreshhold *= experienceIncrement;
    }
}
