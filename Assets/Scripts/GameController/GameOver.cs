using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public float sec = 0.005f;
    public Button reviveButton, getPaidButton;
    public TextMeshProUGUI reviveButtonText;
    public TextMeshProUGUI deathMessage, title;
    public TextMeshProUGUI trashAmount;
    public TextMeshProUGUI coinAmount;
    public GameObject endExplosion;

    public Color winColor, loseColor;
    public CanvasRenderer background;

    public GameObject messageGO;
    public GameObject GameOverGO;
    public GameObject deathScreen, getPaidGO, backButtonGO, reviveGO;

    public bool dead;

    private SavedStats saveStats;
    private int amountRevives;

    public int goodGame = 250;
    public int greatGame = 200;
    public int perfectGame = 100;

    private GameObject player;
    public GameObject items;
    public GameObject shadow;
    private Health playerHealth;
    private Statistic stats;

    void Start()
    {
        saveStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>();

        deathScreen.SetActive(false);
        GameOverGO.SetActive(false);
        messageGO.SetActive(false);
        backButtonGO.SetActive(false);
        getPaidGO.SetActive(true);
        reviveGO.SetActive(true);
        getPaidButton.interactable = false;

        amountRevives = saveStats.amountRevives;
    }


    public void GameOverScreen(bool killed, bool win)
    {
        GetComponent<Pause>().PauseGame(true);
        
        stats.UpdateStats();
        dead = true;
        trashAmount.text = ""+ stats.trash;
        coinAmount.text = "0";  
        
        if (win)
        {
            title.text = "Level Cleared";
            background.SetColor(winColor);
            reviveGO.SetActive(false);
            getPaidGO.transform.localPosition = new Vector3(0, getPaidGO.transform.localPosition.y, getPaidGO.transform.localPosition.z);

            int leftBehinds = (stats.bloodClots + stats.plungersInScene + stats.debris);
            
            if (leftBehinds <= perfectGame)
            {
                deathMessage.text = "You managed to save " + stats.humansSaved + " humans. You killed " + stats.enemiesKilled + " enemies and kept the ship tidy and clean! There's only " + stats.bloodClots + " uncleaned bloodclots, " + stats.plungersInScene + " forgotten plungers and " + stats.debris + " pieces of random debris. Perfect Game! ";
            }
            else if (leftBehinds <= greatGame)
            {
                deathMessage.text = "You managed to save " + stats.humansSaved + " humans. You killed " + stats.enemiesKilled + " enemies and kept the ship in order! There's " + stats.bloodClots + " uncleaned bloodclots, " + stats.plungersInScene + " forgotten plungers and " + stats.debris + " pieces of random debris. Great game!";
            }
            else if (leftBehinds <= goodGame)
            {
                deathMessage.text = "You managed to save " + stats.humansSaved + " humans. You killed " + stats.enemiesKilled + " enemies and kept the ship in order! There's " + stats.bloodClots + " uncleaned bloodclots, " + stats.plungersInScene + " forgotten plungers and " + stats.debris + " pieces of random debris. Good game!";
            }
            else
            {
                deathMessage.text = "You managed to save " + stats.humansSaved + " humans. I'll give you that. And you killed " + stats.enemiesKilled + " enemies. But the ship! Oh, my, what a mess. Are you a cleaning bot or a dirty bot?! There's " + stats.bloodClots + " uncleaned bloodclots, " + stats.plungersInScene + " forgotten plungers and " + stats.debris + " pieces of random debris. You can do better.";
            }

        }
        else if (killed)
        {
            getPaidGO.transform.localPosition = new Vector3(100, getPaidGO.transform.localPosition.y, getPaidGO.transform.localPosition.z);
            
            title.text = "You're dead";
            background.SetColor(loseColor);
            reviveGO.SetActive(true);
            deathMessage.text = "And alongside you, humanity. You killed " + stats.enemiesKilled + " enemies but left " + stats.bloodClots + " uncleaned bloodclots behind,  " + stats.plungersInScene + " forgotten plungers and " + stats.debris + " pieces of random debris. What a bloody mess!";
        }

        else
        {
            getPaidGO.transform.localPosition = new Vector3(100, getPaidGO.transform.localPosition.y, getPaidGO.transform.localPosition.z);
           
            title.text = "Humanity's gone";
            background.SetColor(loseColor);
            reviveGO.SetActive(true);
            deathMessage.text = "There's no humans left to defend. You killed " + stats.enemiesKilled + " enemies but left " + stats.bloodClots + " uncleaned bloodclots behind, alongside " + stats.plungersInScene + " forgotten plungers and " + stats.debris + " pieces of random debris. What a bloody mess!";
        }

        deathScreen.SetActive(true);
        GameOverGO.SetActive(true);
        messageGO.SetActive(true);

        if (!win)
        {
            reviveButtonText.text = "Revive(" + amountRevives + ")";

            if (amountRevives > 0)
            {
                reviveButton.interactable = true;

            }
            else reviveButton.interactable = false;
        }
        

        getPaidButton.interactable = true;

    }
    public void Revive()
    {
        amountRevives--;
        player.SetActive(true);
        dead = false;
        playerHealth.health = playerHealth.maxHealth / 2;
        playerHealth.alive = true;
        playerHealth.revived = true;

        deathScreen.SetActive(false);
        GameOverGO.SetActive(false);
        messageGO.SetActive(false);

        GetComponent<Pause>().ResumeGame(true);

    }
    public void CountTrash()
    {
       
        Instantiate(endExplosion, player.transform.position, player.transform.rotation, transform);
        reviveGO.SetActive(false);
        Destroy(player);
        Destroy(items);
        Destroy(shadow);
        getPaidButton.interactable = false;
        StartCoroutine(ConvertTrash());
    }
    public IEnumerator ConvertTrash()
    {
        
        int dl = 0;
        int pricePerLiter = 5;
        int tempAmountCoin = 0;
        int tempAmountTrash = stats.trash;
        
        if (stats.trash > 0)
        {
            
            for (int i = 0; i < stats.trash; i++)
            {
                if (tempAmountTrash > 2500)
                {
                    sec = 0f;
                }
                else if (tempAmountTrash > 1000)
                {
                    sec = 0.0000005f;
                }
                else if (tempAmountTrash > 100)
                {
                    sec = 0.00005f;
                }
                else if (tempAmountTrash > 10)
                {
                    sec = 0.0001f;
                }
                else sec = 0.005f;

                
                tempAmountTrash--;
                trashAmount.text = "" + tempAmountTrash;
                dl++;

                if (dl >= 10)
                {
                    tempAmountCoin += pricePerLiter;
                    coinAmount.text = "" + tempAmountCoin;
                    dl = 0;
                }
                yield return new WaitForSecondsRealtime(sec);
            }
            
            if (tempAmountTrash <= 0)
            {
                saveStats.AddtempCoins(tempAmountCoin);
                getPaidGO.SetActive(false);
                backButtonGO.SetActive(true);

            }
        } 
        else
        {
            saveStats.AddtempCoins(tempAmountCoin);
            getPaidGO.SetActive(false);
            backButtonGO.SetActive(true);
        }
        
        

        
    }
    
}
