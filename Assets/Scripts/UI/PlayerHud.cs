using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    [SerializeField]
    private string StartMessage;
    
    public TextMeshProUGUI playerLevel;
    
    public Health playerHealth;
    public PlayerExperience playerExperience;

    [SerializeField, Range(0, 1)]
    public float currentExperience;

        
    [SerializeField]
    private GameObject experienceBar;

    [SerializeField]
    private TextMeshProUGUI messageBoard;

    [SerializeField]
    private GameObject lifeBar;

    [SerializeField, Range(0, 1)]
    private float currentHealth;

    private int humansInGame;
    public int humansWithPlayer;

    [SerializeField]
    private HumanHandler humanHandler;

    [SerializeField]
    private TextMeshProUGUI humanText;

    private GameOver playerStatus;

    private bool playMessageSound;
    private AudioSource sfxPlayer;



    // Start is called before the first frame update
    void Start()
    {
        playMessageSound = true;
        sfxPlayer = GetComponent<AudioSource>();

        playerStatus = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        playerExperience = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
       
        playerLevel.text = "" + playerExperience.experienceLevel;
        currentHealth = 1;
        currentExperience = 0;
        UpdateLifeBar();
        UpdatePlayerExperience();

        StartCoroutine(SendMessageToHUD(StartMessage,5,false));

        humansInGame = humanHandler.humanPrefabs.Length;
        humansWithPlayer = 0;
        humanText.text ="Humans: "+ humansWithPlayer + "/" + humansInGame;

    }

    public void UpdateLifeBar()
    {
        if (!playerStatus.dead)
        {
            currentHealth = playerHealth.health / playerHealth.maxHealth;
            lifeBar.transform.localScale = new Vector3(currentHealth, 1f, 1f);
        }
       
    }

    public void UpdatePlayerLevel()
    {
        if (!playerStatus.dead)
        {
            playerLevel.text = "" + playerExperience.experienceLevel;
        }
         
    }

    public void UpdatePlayerExperience()
    {
        if (!playerStatus.dead)
        {
            currentExperience = playerExperience.experience / playerExperience.experienceTreshhold;
            float meterWidth = Mathf.Lerp(0, 366.61f, currentExperience);

            RectTransform rt = experienceBar.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(meterWidth, 27.6f);

        }       
    }

    public IEnumerator SendMessageToHUD(string message, float sec, bool stayOn)
    {
        if (playMessageSound)
        {
            sfxPlayer.Play();
            playMessageSound = false;
        }
        messageBoard.text = message;

        yield return new WaitForSeconds(sec);

        if (!stayOn)
        {
            messageBoard.text = null;
            playMessageSound = true;
            //yield break;
        }
        playMessageSound = true;



    }
    public void UpdateHumanCollected()
    {


        humanText.text = "Humans: " + humansWithPlayer + "/" + humansInGame;
    } 

    public void UpdateHumansInGame()
    {
        humansInGame--;
        humanText.text = "Humans: " + humansWithPlayer + "/" + humansInGame;

        if (humansInGame <= 0)
        {

            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>().GameOverScreen(false,false);
        }
    }

}
