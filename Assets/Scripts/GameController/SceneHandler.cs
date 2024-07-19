using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void RestartGame()
    {
        if (SceneManager.GetActiveScene().name != "StartMenu")
        {
            SavedStats stash = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
            stash.CollectCoins();
        }
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneNumber);
        Time.timeScale = 1;
    }

    public void MainMenu(bool save)
    {
        if (save)
        {  
            SavedStats stash = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
            stash.CollectCoins();
           
            DataPersistanceManager.instance.SaveGame();   
        }

        GetComponent<Pause>().ResumeGame(true);
        SceneManager.LoadScene(0);

    }

    public void PlayLevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLevelTwo()
    {
        SceneManager.LoadScene(2);
    }
    public void PlayLevelThree()
    {
        SceneManager.LoadScene(3);
    }
    public void QuitGame()
    {
        if (SceneManager.GetActiveScene().name != "StartMenu")
        {
            SavedStats stash = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
            stash.CollectCoins();
        }
       
        Application.Quit(); 
    }

    
}
