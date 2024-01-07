using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinDisplay;
    private SavedStats savedStats;
    public bool coinsLoaded;
    public float loadTimer;
    public TextMeshProUGUI touchbutton;

    [SerializeField]
    private GameObject playPanel;

    [SerializeField]
    private GameObject upgradePanel;

    [SerializeField]
    private GameObject resetPanel;

    public AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        UpdateCoinDisplay();
        playPanel.SetActive(false);
        upgradePanel.SetActive(false);
        resetPanel.SetActive(false);

        loadTimer = 0.5f;
    }
    private void Update()
    {
        if (!coinsLoaded)
        {
            if (loadTimer < 0)
            {
                loadTimer -= Time.deltaTime;
            }
            else
            {
                UpdateCoinDisplay();
                coinsLoaded = true;

            }
        }
    }

    public void PlayScreenToggle()
    {
        if (playPanel.activeSelf == false)
        {
            playPanel.SetActive(true);
        }
        else playPanel.SetActive(false);

        UpdateCoinDisplay();
        player.Play();
    }

    public void UpdateCoinDisplay()
    {
        coinDisplay.text = "= " + savedStats.coins;
    }

    public void UpgradeScreenToggle()
    {
        if (upgradePanel.activeSelf == false)
        {
            upgradePanel.SetActive(true);
            GetComponent<UpgradePanel>().SetIndicators();
            GetComponent<UpgradePanel>().ResetTextElement();
        }
        else upgradePanel.SetActive(false);

        player.Play();
    }

    public void ResetScreenToggle()
    {
        if (resetPanel.activeSelf == false)
        {
            resetPanel.SetActive(true);
        }
        else resetPanel.SetActive(false);

        player.Play();
    }

    public void ResetGame() 
    {
        player.Play();
        savedStats.ResetData();
        UpdateCoinDisplay();
        ResetScreenToggle();
    }
    public void EnableTouch()
    {
        if (savedStats.touchControls)
        {
            savedStats.touchControls = false;
            touchbutton.color = Color.white;
        }
        else
        {
            savedStats.touchControls = true;
            touchbutton.color = Color.green;
        }
    }



}
