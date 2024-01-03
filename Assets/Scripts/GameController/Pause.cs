using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;


    public bool gamePaused;

    private void Start()
    {
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
            PauseGame(true);
        }
        else
        {
            pauseMenu.SetActive(false);
            ResumeGame(true);
        }
    }
}
