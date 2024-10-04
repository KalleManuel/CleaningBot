using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGame : MonoBehaviour
{
    private Camera mCamera;
    
    private Statistic stats;
    private GameOver gameOverScript;   
    private MusicHandler music;
    public GameObject boss;
    public bool bossFightOn, bossDefeated;
    
    [SerializeField]
    private EnemySpawnController spawnController;

    private bool gameOver;
    public GameObject pickUpPointIcon;
    private GameObject bossen;
    public GameObject exit, blocker, whiteKey;
    public int HumansInFight;

    void Start()
    {
        mCamera = Camera.main;
        pickUpPointIcon.SetActive(false);
        gameOver = false;
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>();
      

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
       
        
        stats = gameController.GetComponent<Statistic>();
        gameOverScript = gameController.GetComponent<GameOver>();
        music = gameController.GetComponent<MusicHandler>();
        HumansInFight = 0;
    }

    void Update()
    {
        if (!gameOver)
        {
            if (bossFightOn)
            {
                if (mCamera.orthographicSize < 7)
                {
                    mCamera.orthographicSize += Time.deltaTime;
                }
                else mCamera.orthographicSize = 7.1f;

                if (bossen == null)
                {
                    bossDefeated = true;
                    
                    bossFightOn = false;
               
                    LetMadnessBegin();
                  
                }
            }

            if (exit.activeSelf == false)
            {

                gameOverScript.GameOverScreen(false, true);
                gameOver = true;
               
            }
        }
    }
    public void LetMadnessBegin()
    {
        
        // Spawn meny enemies from outside room - force player toward exit
        spawnController.waveNumber = (spawnController.waves.Length - 2);
        spawnController.state = EnemySpawnController.SpawnState.Check;
    }

    public void ShowPickUpPoint()
    {
        StartCoroutine(PlayerHud.playerHud.SendMessageToHUD("Take the humans to the pick up point",5,false));
        pickUpPointIcon.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
           
            if (!bossFightOn && !bossDefeated)
            {
                music.PlaySong(1);
                bossen = Instantiate(boss, transform.position, transform.rotation);
                spawnController.state = EnemySpawnController.SpawnState.Off;
                bossFightOn = true;
            }
        }
        if (collision.gameObject.tag == "BodySoul")
        {
            HumansInFight++;
            stats.UpdateStats();

            if (HumansInFight == stats.humansSaved)
            {
                blocker.SetActive(true);
            }
        }
    }

}
