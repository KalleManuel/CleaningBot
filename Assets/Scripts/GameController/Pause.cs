using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
  
    public TextMeshProUGUI touchButton;

    SavedStats savedStats;


    public bool gamePaused;

    private void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        pauseMenu.SetActive(false);
        ResumeGame(true);
    }
    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            TooglePauseMenu();

        }
    }
    public void PauseGame(bool pauseAudio)
    {
        gamePaused = true;
        Time.timeScale = 0;
        StopAllCoroutines();
        if (pauseAudio)
        {
            AudioListener.pause = true;
        }
    }

   public void ResumeGame(bool resumeAudio)
    {
        gamePaused = false;
        Time.timeScale = 1;

        if (resumeAudio)
        {
            AudioListener.pause = false;
        }

    }
    public void TooglePauseMenu()
    {
        if (pauseMenu.activeSelf == false)
        {
            pauseMenu.SetActive(true);
            
            if (savedStats.touchControls)
            {
                touchButton.color = Color.green;
            }
            else touchButton.color = Color.white;

            PauseGame(true);
        }
        else
        {
            pauseMenu.SetActive(false);
            ResumeGame(true);
        }
    }

    public void EnableTouch()
    {
        if (savedStats.touchControls)
        {
            savedStats.touchControls = false;
            touchButton.color = Color.white;
        }
        else
        {
            savedStats.touchControls = true;
            touchButton.color = Color.green;
        }
    }
}
